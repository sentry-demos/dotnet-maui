using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows.Input;
using Sentry.Maui;

namespace EmpowerPlant;


/// <summary>
/// This looks for async commands from the MVVM Community toolkit to help see their execution spans them running
/// NOTE: that we only going searching for a subset of known controls at the moment, but this shows how easy it can be to add
/// </summary>
public class CtMvvmMauiElementEventBinder : IMauiElementEventBinder
{
    /// <summary>
    /// Binds to the element
    /// </summary>
    /// <param name="element"></param>
    /// <param name="addBreadcrumb"></param>
    public void Bind(VisualElement element, Action<BreadcrumbEvent> addBreadcrumb) => Iterate(element, true);

    /// <summary>
    /// Unbinds from the element
    /// </summary>
    /// <param name="element"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void UnBind(VisualElement element) => Iterate(element, false);


    private static void Iterate(VisualElement element, bool bind)
    {
        switch (element)
        {
            case Button button:
                TryBindTo(button.Command, bind);
                break;

            case RefreshView refresh:
                TryBindTo(refresh.Command, bind);
                break;

            default:
                TryGestureBinding(element, bind);
                break;
        }
    }

    private static void TryGestureBinding(VisualElement element, bool bind)
    {
        if (element is IGestureRecognizers gestureRecognizers)
        {
            foreach (var gestureRecognizer in gestureRecognizers.GestureRecognizers)
            {
                TryBindTo(gestureRecognizer, bind);
            }
        }
    }

    private static void TryBindTo(IGestureRecognizer recognizer, bool bind)
    {
        switch (recognizer)
        {
            case TapGestureRecognizer tap:
                TryBindTo(tap.Command, bind);
                break;

            case SwipeGestureRecognizer swipe:
                TryBindTo(swipe.Command, bind);
                break;
        }
    }

    private static void TryBindTo(ICommand? command, bool bind)
    {
        if (command is IAsyncRelayCommand relayCommand)
        {
            relayCommand.PropertyChanged -= RelayCommandOnPropertyChanged;
            
            if (bind)
            {
                relayCommand.PropertyChanged += RelayCommandOnPropertyChanged;
            }
        }
    }


    static ConcurrentDictionary<IAsyncRelayCommand, ITransactionTracer> _contexts = new();

    private static void RelayCommandOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAsyncRelayCommand.IsRunning))
        {
            var relay = (IAsyncRelayCommand)sender!;

            if (relay.IsRunning)
            {
                var transaction = SentrySdk.StartTransaction("ctmvvm", "async.command");
                _contexts.TryAdd(relay, transaction);
            }
            else if (_contexts.TryGetValue(relay, out var value))
            {
                value.Finish();
                _contexts.TryRemove(relay, out _);
            }
        }
    }
}
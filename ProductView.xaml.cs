using System.Windows.Input;
using Empower.Models;

namespace DotNetMaui;

public partial class ProductView : ContentView
{
    public ProductView()
    {
        InitializeComponent();
    }

    public static BindableProperty ProductProperty = BindableProperty.Create(
        nameof(Product), 
        typeof(Product), 
        typeof(ProductView)
    );

    public Product Product
    {
        get => (Product)GetValue(ProductProperty);
        set => SetValue(ProductProperty, value);
    }
    
    public static BindableProperty ImageProperty = BindableProperty.Create(
        nameof(Image), 
        typeof(ImageSource), 
        typeof(ProductView)
    );
    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    
    public static BindableProperty PriceProperty = BindableProperty.Create(
        nameof(Price), 
        typeof(int), 
        typeof(ProductView)
    );
    
    public int Price
    {
        get => (int)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public static BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), 
        typeof(string), 
        typeof(ProductView)
    );
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static BindableProperty ActionTextProperty = BindableProperty.Create(
        nameof(ActionText), 
        typeof(string), 
        typeof(ProductView)
    );
    public string ActionText
    {
        get => (string)GetValue(ActionTextProperty);
        set => SetValue(ActionTextProperty, value);
    }
    
    public static BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), 
        typeof(ICommand), 
        typeof(ProductView)
    );
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public static BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter), 
        typeof(object), 
        typeof(ProductView)
    );
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }


    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        switch (propertyName)
        {
            case nameof(Product):
                if (this.Product == null)
                    return;
                
                this.btnAction.CommandParameter = this.Product;
                this.Price = this.Product.Price;
                this.Description = this.Product.Description;
                this.Image = ImageSource.FromUri(new Uri(this.Product.ImgCropped));
                break;
            
            case nameof(Image):
                this.imgProduct.Source = this.Image;
                break;
            
            case nameof(Price):
                this.lblPrice.Text = $"${this.Price}";
                break;
            
            case nameof(Description):
                this.lblDescription.Text = this.Description;
                break;
            
            case nameof(Command):
                this.btnAction.Command = this.Command;
                break;
            
            case nameof(CommandParameter):
                this.btnAction.CommandParameter = this.CommandParameter;
                break;
            
            case nameof(ActionText):
                this.btnAction.Text = this.ActionText;
                break;
        }
    }
}
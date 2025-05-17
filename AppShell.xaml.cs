namespace EmpowerPlant;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("ListPage", typeof(ListPage));
		Routing.RegisterRoute("CartPage", typeof(CartPage));
		Routing.RegisterRoute("OrderPage", typeof(OrderPage));
	}
}

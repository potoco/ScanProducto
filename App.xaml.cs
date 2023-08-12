using Microsoft.Maui.Controls;
namespace ScanProducto;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new NavigationPage(new AppShell());
        MainPage = new AppShell();

    }
}

namespace ScanProducto;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	async void OnCounterClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new VisorPrecio());

    }
}


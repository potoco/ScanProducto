namespace ScanProducto;

public partial class MainPage : ContentPage
{
    IDispatcherTimer timerImageShow;

    public MainPage()
	{
		InitializeComponent();

        timerImageShow = Dispatcher.CreateTimer();
        timerImageShow.Interval = TimeSpan.FromMilliseconds(850);
        timerImageShow.Tick += (sender, e) => DisplayImage(sender, e);

        this.Loaded += MainPage_Loaded;
	}

    private async void MainPage_Loaded(object sender, EventArgs e)
    {


    }

    private void DisplayImage(object sender, EventArgs e)
    {
    }

    async void OnCounterClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new VisorPrecio());
    }
}


using ZXing.Net.Maui;
using CommunityToolkit.Maui.Core.Primitives;

namespace ScanProducto;

public partial class VisorPrecio : ContentPage
{
    string codigoUltimo = string.Empty;
    IDispatcherTimer timer4ImageShow;
    bool alternar = true;


    public VisorPrecio()
	{
		InitializeComponent();
        barcoderReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };

        timer4ImageShow = Application.Current.Dispatcher.CreateTimer();
        timer4ImageShow.Interval = TimeSpan.FromMilliseconds(200);
        timer4ImageShow.Tick += (sender, e) => DisplayImage(sender, e);

        this.Loaded += async (s, e) =>
        {
            string v = "-";

            await Task.Delay(500);
            
            txtResultado.Text = v;
            txtFormato.Text = v;

            lnHor.X1 = 45;
            lnHor.X2 = grdMain.Width - 45;
            lnHor.Y1 = lnHor.Y2 = (grdMain.Height - 90) / 2;

            lnVer.Y1 = 45;
            lnVer.Y2 = grdMain.Height - 45;
            lnVer.X1 = lnVer.X2 = (grdMain.Width) / 2;

            await Task.Delay(1000);
            timer4ImageShow.Start();


        };
    }

    private void DisplayImage(object sender, EventArgs e)
    {
        alternar = !alternar;
        if (alternar) {
            lnVer.FadeTo(0, 190);
            lnHor.FadeTo(0, 190);
        }
        else {
            lnVer.FadeTo(1, 190);
            lnHor.FadeTo(1, 190);
        }
    }

    private async Task showProducto(bool show=true)
    {
        borderMargin.IsVisible = !show;
        lnVer.IsVisible = !show;
        lnHor.IsVisible = !show;
        if (show)
        {
            timer4ImageShow.Stop();
            await baseProducto.FadeTo(1, 900);
        }
        else
        {
            timer4ImageShow.Start();
            await baseProducto.FadeTo(0, 400);
        }
    }

    void OnTapped(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (barcoderReader.IsDetecting)
            {
                barcoderReader.AutoFocus();
            }

        });
    }

    void BarcodeDetected(object sender, BarcodeDetectionEventArgs args)
    {
        barcoderReader.IsDetecting = false;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            audioControl.Stop();
            var num = args.Results[0];
            if (!codigoUltimo.Equals(num.Value)) {
                timer4ImageShow.Stop();
                codigoUltimo = num.Value;
                txtResultado.Text = $"{codigoUltimo}";
                txtFormato.Text = $"{num.Format}";
                if (audioControl.CurrentState != MediaElementState.Playing)
                    audioControl.Play();
                await showProducto();
                await Task.Delay(5000);
                codigoUltimo = string.Empty;
            }
        });
    }

    async private void Button_Clicked(object sender, EventArgs e)
    {
        barcoderReader.IsDetecting = true;
        txtResultado.Text = "";
        txtFormato.Text = "";
        await showProducto(false);
        
    }
}
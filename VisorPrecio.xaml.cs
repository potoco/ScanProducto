using ZXing.Net.Maui;
using CommunityToolkit.Maui.Core.Primitives;

namespace ScanProducto;

public partial class VisorPrecio : ContentPage
{
    string codigoUltimo = string.Empty;
	public VisorPrecio()
	{
		InitializeComponent();
        barcoderReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };

        this.Loaded += async (s, e) =>
        {
            await Task.Delay(500);
            string v = "-";
            txtResultado.Text = v;
            txtFormato.Text = v;
            apagarProducto();
        };
    }

    private void apagarProducto()
    {
        //baseNegro.Opacity = 0;
        //baseNegroFront.Opacity = 0;
        baseProducto.Opacity = 0;
    }
    private async Task showProducto(bool show=true)
    {
        if (show)
        {
            //await baseNegro.FadeTo(0.6, 900);
            //await baseNegroFront.FadeTo(1, 900);
            await baseProducto.FadeTo(1, 900);
        }
        else
        {
            //await baseNegro.FadeTo(0, 400);
            //await baseNegroFront.FadeTo(0, 400);
            await baseProducto.FadeTo(0, 400);
        }
    }

    async void OnTapped(object sender, EventArgs e)
    {
        txtResultado.Text = "AutoFocus";
        txtFormato.Text = "AutoFocus";
        barcoderReader.AutoFocus();
        await Task.Delay(1500);
        txtResultado.Text = "-";
        txtFormato.Text = "-";

    }

    void BarcodeDetected(object sender, BarcodeDetectionEventArgs args)
    {
        barcoderReader.IsDetecting = false;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            audioControl.Stop();
            var num = args.Results[0];
            if (!codigoUltimo.Equals(num.Value)) {
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
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Test.Integration.WpfAppTest;

internal static class WindowExtensions
{
    internal static Task StartSTATask(Action action)
    {
        var taskCompletionSource = new TaskCompletionSource<object>();
        var thread = new Thread(() =>
        {
            try
            {
                action();
                taskCompletionSource.SetResult(new object());
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        return taskCompletionSource.Task;
    }

    internal static MemoryStream ToStream(this Window window)
    {
        window.WindowStyle = WindowStyle.None;
        window.ShowInTaskbar = false;
        window.AllowsTransparency = true;
        window.Opacity = 0d;
        window.Show();
        //https://learn.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/how-to-create-a-bitmap-from-a-visual
        var bmp = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, 96, 96, PixelFormats.Pbgra32);
        bmp.Render((Visual)window.Content);
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bmp));
        var stream = new MemoryStream();
        encoder.Save(stream);
        return stream;
    }

    internal static string ToXaml(this Window window)
    {
        var sb = new StringBuilder();
        //Use XmlWriterSettings to enable indentation
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "    ", // 4 spaces for indentation
            NewLineOnAttributes = true,
            OmitXmlDeclaration = true
        };
        using var writer = XmlWriter.Create(sb, settings);
        XamlWriter.Save(window, writer);
        return sb.ToString();
    }
}

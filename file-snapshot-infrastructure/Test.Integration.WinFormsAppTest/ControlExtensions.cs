using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Test.Integration.WinFormsAppTest;

internal static class ControlExtensions
{
    public static MemoryStream ToStream(this Form form)
    {
        form.AutoScaleMode = AutoScaleMode.None;
        form.ShowInTaskbar = false;
        form.TopLevel = false;
        form.Show();
        using (var snapshot = new Bitmap(form.Width, form.Height, PixelFormat.Format32bppArgb))
        {
            form.DrawToBitmap(snapshot, new(0, 0, form.Width, form.Height));
            var stream = new MemoryStream();
            snapshot.Save(stream, ImageFormat.Png);
            return stream;
        }
    }
}

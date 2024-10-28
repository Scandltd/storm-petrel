using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;

namespace Test.Integration.Shared
{
    public static class Calculator
    {
        private const string LogoPath = "CalculatorLogo.png";
        /// <summary>
        /// Use "Shared" extra segment to adapt .NET Framework 4.7.2 relative path special handling
        /// </summary>
        public const string LogoPathShared = "Test.Integration.Shared/CalculatorLogo.png";
        public static byte[] GetLogo()
        {
            try
            {
                return File.ReadAllBytes(LogoPath);
            }
            catch (FileNotFoundException)
            {
                return File.ReadAllBytes(LogoPathShared);
            }
        }

        public static AddResult Add(int a, int b)
        {
            var result = a + b;
            return new AddResult
            {
                Value = result,
                ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
            };
        }

        public static Stream GetLogoAsStream()
        {
            try
            {
                return File.OpenRead(LogoPath);
            }
            catch (FileNotFoundException)
            {
                return File.OpenRead(LogoPathShared);
            }
        }

#if NET8_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public static void GetLogoAndRepeatToStream(Stream stream, int widthRepeat = 2, int heightRepeat = 2)
        {
            var inputPath = File.Exists(LogoPath) ? LogoPath : LogoPathShared;
            using (var inputImage = new Bitmap(inputPath))
            {
                // Create a new bitmap with double the width of the input image
                int newWidth = inputImage.Width * widthRepeat;
                int newHeight = inputImage.Height * heightRepeat;
                using (var newImage = new Bitmap(newWidth, newHeight))
                {
                    using (var g = Graphics.FromImage(newImage))
                    {
                        for (int x = 0; x < widthRepeat; x++)
                        {
                            for (int y = 0; y < heightRepeat; y++)
                            {
                                g.DrawImage(inputImage, x * inputImage.Width, y * inputImage.Height);
                            }
                        }
                    }

                    // Save the new image
                    newImage.Save(stream, ImageFormat.Png);
                }
            }
        }
    }
}

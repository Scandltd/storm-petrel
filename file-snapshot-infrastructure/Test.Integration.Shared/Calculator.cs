using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using SkiaSharp;

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

        public static void GetLogoAndRepeatToStream(Stream stream, int widthRepeat = 2, int heightRepeat = 2)
        {
            var inputPath = File.Exists(LogoPath) ? LogoPath : LogoPathShared;
            using (var fileStream = File.OpenRead(inputPath))
            {
                using (var inputImage = SKBitmap.Decode(fileStream))
                {
                    // Create a new bitmap with double the width of the input image
                    int newWidth = inputImage.Width * widthRepeat;
                    int newHeight = inputImage.Height * heightRepeat;
                    using (var newImage = new SKBitmap(newWidth, newHeight))
                    {
                        using (var canvas = new SKCanvas(newImage))
                        {
                            canvas.Clear(SKColors.Transparent);

                            for (int x = 0; x < widthRepeat; x++)
                            {
                                for (int y = 0; y < heightRepeat; y++)
                                {
                                    canvas.DrawBitmap(inputImage, x * inputImage.Width, y * inputImage.Height);
                                }
                            }
                        }

                        // Save the new image
                        using (var data = newImage.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            data.SaveTo(stream);
                        }
                    }
                }
            }
        }
    }
}

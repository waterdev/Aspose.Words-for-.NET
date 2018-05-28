using System.IO;
using SkiaSharp;

namespace NUnit.Tests.Android.TestData.TestClasses
{
    public class ImageTestClass
    {
        public SKBitmap Image { get; set; }
        public Stream ImageStream { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImageUri { get; set; }

        public ImageTestClass(SKBitmap image, Stream imageStream, byte[] imageBytes, string imageUri)
        {
            this.Image = image;
            this.ImageStream = imageStream;
            this.ImageBytes = imageBytes;
            this.ImageUri = imageUri;
        }
    }
}

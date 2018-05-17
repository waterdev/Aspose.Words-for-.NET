using System.IO;

namespace NUnit.Tests.Android.TestData.TestClasses
{
    public class ImageTestClass
    {
        public SkiaSharp.SKBitmap Image { get; set; }
        public Stream ImageStream { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImageUri { get; set; }

        public ImageTestClass(SkiaSharp.SKBitmap image, Stream imageStream, byte[] imageBytes, string imageUri)
        {
            this.Image = image;
            this.ImageStream = imageStream;
            this.ImageBytes = imageBytes;
            this.ImageUri = imageUri;
        }
    }
}

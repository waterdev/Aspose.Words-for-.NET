using System.IO;
using NUnit.Tests.Android.TestData.TestClasses;
using SkiaSharp;

namespace NUnit.Tests.Android.TestData.TestBuilders
{
    public class ImageTestBuilder : ApiExampleBase
    {
        private SKBitmap mImage;
        private Stream mImageStream;
        private byte[] mImageBytes;
        private string mImageUri;

        public ImageTestBuilder()
        {
            this.mImage = SKBitmap.Decode(ImageDir + "Watermark.png");
            this.mImageStream = Stream.Null;
            this.mImageBytes = new byte[0];
            this.mImageUri = string.Empty;
        }

        public ImageTestBuilder WithImage(SKBitmap image)
        {
            this.mImage = image;
            return this;
        }

        public ImageTestBuilder WithImageStream(Stream imageStream)
        {
            this.mImageStream = imageStream;
            return this;
        }

        public ImageTestBuilder WithImageBytes(byte[] imageBytes)
        {
            this.mImageBytes = imageBytes;
            return this;
        }

        public ImageTestBuilder WithImageUri(string imageUri)
        {
            this.mImageUri = imageUri;
            return this;
        }

        public ImageTestClass Build()
        {
            return new ImageTestClass(mImage, mImageStream, mImageBytes, mImageUri);
        }
    }
}

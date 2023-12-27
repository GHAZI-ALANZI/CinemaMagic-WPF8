using System.IO;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels
{
    public class ImageVsSQL
    {
        // Convert Bitmap to Byte array
        public static byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder(); //  PNG encoder
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }

            return data;
        }


        /// Method to convert ByteArray to BitmapImage
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();// Freeze the image for use on the UI
                return bitmapImage;
            }
        }
    }
}

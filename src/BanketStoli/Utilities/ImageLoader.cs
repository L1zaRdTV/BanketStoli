using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace BanketStoli.Utilities
{
    public static class ImageLoader
    {
        public static BitmapImage Load(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return null;

            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = ResolveUri(imagePath);
                image.EndInit();
                image.Freeze();
                return image;
            }
            catch
            {
                return null;
            }
        }

        private static Uri ResolveUri(string imagePath)
        {
            if (Uri.TryCreate(imagePath, UriKind.Absolute, out var absoluteUri)) return absoluteUri;

            var baseDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            if (File.Exists(baseDirectoryPath)) return new Uri(baseDirectoryPath, UriKind.Absolute);

            return new Uri(Path.GetFullPath(imagePath), UriKind.Absolute);
        }
    }
}

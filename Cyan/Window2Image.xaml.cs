using Saina.Common.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF
{
    /// <summary>
    /// Interaction logic for Window2Image.xaml
    /// </summary>
    public partial class Window2Image : Window
    {
        public Window2Image()
        {
            InitializeComponent();

            //باز کردن عکس در مسیر مشخص شده
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"{Environment.CurrentDirectory}\\test.jpg");
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            ImageViewer1.Source = bitmap;
            bitmap.EndInit();

        }
        byte[] imageInDb;

        private  byte[] ToBytes(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        private  BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();

                return image;
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            RadOpenFileDialog openFileDialog = new RadOpenFileDialog
            {
                Owner = this,
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*",
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.DialogResult == true)
            {
                string selectedFileName = openFileDialog.FileName;
                FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
                imageInDb = ToBytes(bitmap);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer2.Source = ToImage(imageInDb);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            String filePath = $"{Environment.CurrentDirectory}\\test.jpg";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageViewer1.Source));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                encoder.Save(stream);
        }
    }
}

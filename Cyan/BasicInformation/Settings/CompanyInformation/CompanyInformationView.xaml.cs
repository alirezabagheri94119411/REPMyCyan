using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Settings.CompanyInformation
{
    /// <summary>
    /// Interaction logic for CompanyInformationView.xaml
    /// </summary>
    public partial class CompanyInformationView : UserControl
    {
        private CompanyInformationViewModel companyInformationViewModel;
        public CompanyInformationView()
        {

            InitializeComponent();
            Loaded += (s, e) =>
            {
            companyInformationViewModel =   DataContext as CompanyInformationViewModel;

                if (companyInformationViewModel.CompanyInformationModel.Logo != null)
                    {

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri($"{Environment.CurrentDirectory}\\Image\\CompanyInformationPictur.jpg");
                        bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        ImageLogo.Source = bitmap;
                        bitmap.EndInit();
                    }
                
            };
     
    }
        private void UploadImage_Click(object sender, RoutedEventArgs e)
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

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageLogo.Source = bitmap;


                // imageInDb = ToBytes(bitmap);
            }

        }

        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageLogo.Source != null )
            {
                String filePath = $"{Environment.CurrentDirectory}\\Image\\CompanyInformationPictur.jpg";
                if (File.Exists(filePath))
                    File.Delete(filePath);
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageLogo.Source));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    encoder.Save(stream);
                companyInformationViewModel.CompanyInformationModel.Logo= filePath;
            }
            companyInformationViewModel.onSave();
            var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

            var alert = new RadDesktopAlert
            {
                FlowDirection = FlowDirection.RightToLeft,
                Header = "اطلاعات",
                Content = ".اطلاعات با موفقیت ثبت شد",
                ShowDuration = 5000,
            };
            manager.ShowAlert(alert);
        }
    }
}

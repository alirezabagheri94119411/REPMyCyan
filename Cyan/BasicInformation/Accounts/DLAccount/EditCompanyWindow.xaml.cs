using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
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

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// Interaction logic for EditCompanyWindow.xaml
    /// </summary>
    public partial class EditCompanyWindow : RadWindow
    {
        private DLListViewModel _ViewModel;
        string _Invoker;
        public EditCompanyWindow(string invoker = "gridView")
        {
            _Invoker = invoker;

            InitializeComponent();
            Loaded += (s, e) =>
            {
                _ViewModel = DataContext as DLListViewModel;
                if (_ViewModel.DLss.CurrentItem is DL dL)
                {
                    _ViewModel.DL = dL;
                    if (dL.Logo!=null)
                    {

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri($"{Environment.CurrentDirectory}\\Image\\{dL.Title}.jpg");
                    bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    ImageLogo.Source = bitmap;
                    bitmap.EndInit();
                    }
                }
            };
        }
        private void okRadButton_Click(object sender, RoutedEventArgs e)
        {

            var errorMessage = "";
            if (_ViewModel.DLss.CurrentItem is DL dL)
            {
                if (companyNameTextBox.Text == "")
                {
                    errorMessage += $"نام خانوادگی شخص وارد نشده است {Environment.NewLine}";
                }
                if (ImageLogo.Source!=null && companyNameTextBox.Text != "")
                {
                    String filePath = $"{Environment.CurrentDirectory}\\Image\\{companyNameTextBox.Text}.jpg";
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageLogo.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                    dL.Logo = filePath;
                }
                if (errorMessage.Length > 0)
                {

                    DialogParameters parameters = new DialogParameters();
                    parameters.OkButtonContent = "بستن";
                    parameters.Header = "!اخطار";
                    parameters.Content = errorMessage;
                    RadWindow.Alert(parameters);
                }
                else
                {
                    dL.Title = companyNameTextBox.Text;
                    _ViewModel.Active = false;
                    Close();

                }
            }

        }

        private void cancelRadButton_Click(object sender, RoutedEventArgs e)
        {
            if (_ViewModel.DLss.CurrentItem is DL dL)
            {
                if (_Invoker == "radioButton")
                    dL.DLType = DLTypeEnum.Others;
                Close();
            }

        }
        private void companyNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
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
    }
}

using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Settings.ShoppingSetting;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Telerik.Windows.Controls.Filtering.Editors;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using Saina.ApplicationCore.DTOs;
using System.IO;
using Microsoft.Win32;

namespace Saina.WPF.Commerce.CommProduct
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl
    {
        private ProductListViewModel _viewModel;
        private bool? _dialogResult;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        private bool isModified;

        //  private SainaDbContext _uow;
        public ProductListView()
        {
            //  _uow = new SainaDbContext();
            _shoppingSystemSettingsService = SmObjectFactory.Container.GetInstance<IShoppingSystemSettingsService>();

            InitializeComponent();
            Loaded += (s, e) =>
            {
                _viewModel = DataContext as ProductListViewModel;
                _viewModel.Loaded();
                //  BeginEdit();
                //if (ProductDataForm?.ItemsSource != null)
                //{
                //    var products = ProductDataForm.ItemsSource.Cast<Product>();
                //    foreach (var product in Products)
                //    {
                //        product.PropertyChanged += Product_PropertyChanged;

                //    }
                //}
                _viewModel.Error += OnError;
               
                //   DataContext = _viewModel;
            };
            Unloaded += (s, ea) =>
            {

                _viewModel.Error -= OnError;
            

            };
           
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
           
             
                // imageInDb = ToBytes(bitmap);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"{Environment.CurrentDirectory}\\test.jpg");
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            ImageViewer1.Source = bitmap;
            bitmap.EndInit();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
         var i=   (_viewModel.ProductCode)+1;
            String filePath = $"{Environment.CurrentDirectory}\\Image\\{i}.jpg";
            if (File.Exists(filePath))
                File.Delete(filePath);
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageViewer1.Source));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                encoder.Save(stream);
          //  var x = "dfdfsdf";

            ((Product)_viewModel.Products.CurrentItem).ImageProducts.Add(new ImageProduct {Picture=filePath });
           
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            isModified = true;
            //RaiseCanExecuteChanged();
        }

        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void OnFailed(Exception ex)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "اخطار";
            parameters.Content = ex.Message;
            RadWindow.Alert(parameters);
        }

        #region Report
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string extension = "xls";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (.{0})|.{0}|All files (.)|.", extension, "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                var col0Visibility = productDataGrid.Columns[0].IsVisible;
                productDataGrid.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    productDataGrid.Export(stream,
                     opt);
                }
                productDataGrid.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
            }
        }
        private void Report()
        {
        }
        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = productDataGrid.SelectedItems.Cast<Product>().Select(Product =>
             new
             {
                 ProductCode = Product.ProductCode,
                 ProductTitle = Product.ProductTitle,
               
             });

                report.RegBusinessObject("ProductReport", data);
                report.Load($"{Environment.CurrentDirectory}\\report.mrt");
                report.Show();
            }
        }

        private void designButton_Click(object sender, RoutedEventArgs e)
        {
            DesignReport();
        }
        private void DesignReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = productDataGrid.SelectedItems.Cast<Product>().Select(Product =>
               new
               {
                   ProductCode = Product.ProductCode,
                   ProductTitle = Product.ProductTitle,
               
               });
                var path = $"{Environment.CurrentDirectory}\\Report\\documentNumberingReport.mrt";
                report.RegBusinessObject("ProductReport", data);
                if (!File.Exists(path))
                    File.Copy($"{Environment.CurrentDirectory}\\Report\\Report.mrt", path, false);
                report.Load(path);
                report.Design();
            }
        }
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            StiReport report = new StiReport();
            using (var uow = new SainaDbContext())
            {
                var data = productDataGrid.SelectedItems.Cast<Product>().Select(Product =>
                  new
                  {
                      ProductCode = Product.ProductCode,
                      ProductTitle = Product.ProductTitle,
                    
                  });
                report.RegBusinessObject("ProductReport", data);
                report.Load($"{Environment.CurrentDirectory}\\glReport.mrt");
                report.Print();
            }
        }




        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            //using (FileStream stream = new FileStream("d:\\exportSCV.xls", FileMode.OpenOrCreate))
            //{
            //    //radGridView.Columns[5].IsVisible = false;
            //    var opt = new GridViewExportOptions()
            //    {
            //        Format = ExportFormat.ExcelML,
            //        ShowColumnHeaders = true,
            //        ShowColumnFooters = false,
            //        ShowGroupFooters = false,
            //        Encoding = Encoding.UTF8,

            //    };
            //    radGridView.Export(stream,opt);
            // }


            //string extension = "xls";
            //SaveFileDialog dialog = new SaveFileDialog()
            //{
            //    DefaultExt = extension,
            //    Filter = String.Format("{1} files (.{0})|.{0}|All files (.)|.", extension, "Excel"),
            //    FilterIndex = 1
            //};
            //if (dialog.ShowDialog() == true)
            //{
            //    using (Stream stream = dialog.OpenFile())
            //    {
            //        gridViewExport.Export(stream,
            //         new GridViewExportOptions()
            //         {
            //             Format = ExportFormat.Html,
            //             ShowColumnHeaders = true,
            //             ShowColumnFooters = true,
            //             ShowGroupFooters = false,
            //         });
            //    }
            //}
        }
        #endregion
        #region GridView


        private void ProductRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        private void ProductRadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void ProductDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        private void ProductDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            //  if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            //  _viewModel.Save();
        }
        #endregion
        #region Add
        private void ProductDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            var newItem = e.NewItem as Product;
            _viewModel.AddBrandCommand.Execute(null);

            newItem.ProductCode = _viewModel.Product.ProductCode;
            newItem.ProductTitle = _viewModel.Product.ProductTitle;
          

            _viewModel.AddProduct((Product)e.NewItem);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            NavStateFalse();
            (sender as FrameworkElement).Focus();
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, this.ProductDataForm);
            // if (_viewModel.AddProductCommand.CanExecute(null))
            // _viewModel.AddBrandCommand.Execute(null);
            //   ProductDataForm.AddNewItem();
        }
        #endregion
        #region Delete
        private void ProductDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا برای حذف  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            e.Cancel = _dialogResult == false;
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        private void ProductDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            var r = _viewModel.DeleteProduct((Product)e.DeletedItem);
            if (r > 0)
            {
                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = ".حذف با موفقیت انجام شد",
                    ShowDuration = 5000,
                };
                manager.ShowAlert(alert);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseCanExecuteChanged();
            NavStateTrue();
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, ProductDataForm);

        }
        #endregion
        #region Validation
        private void ProductDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        Product Product => ProductDataForm.CurrentItem as Product;
        private bool ValidateProduct()
        {
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            int.TryParse(ShoppingSystemSettingModel.ProductCodeLenght, out int productCodeLenght);
            int.TryParse(ShoppingSystemSettingModel.Barcode, out int barCodeLenght);
            int.TryParse(ShoppingSystemSettingModel.IranCodeProduct, out int iranCodeLenght);

            var ProductLenght = productCodeLenght;
            var ProductCode = Product.ProductCode;
            var ProductCodelenght = (ProductCode.ToString()).Length;

            var BarCodeLenght = barCodeLenght;
            var BarCode = Product.ProductBarcode;
            var BarCodeProductlenght = (BarCode.ToString()).Length;

            var IranCodeProductLenght = iranCodeLenght;
            var IranCode = Product.IranCode;
            var IranCodelenght = (IranCode.ToString()).Length;

            var result = true;
            var errorMessage = "";
            ProductDataForm.ValidationSummary.Errors.Clear();
            using (var _uow = new SainaDbContext())
            {
                if (_uow.Products.Where(y => y.ProductId != Product.ProductId).Any(x => x.ProductTitle == Product.ProductTitle))
                {
                    errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

                }
                if (_uow.Products.Where(y => y.ProductId != Product.ProductId).Any(x => x.ProductCode == Product.ProductCode))
                {
                    errorMessage += $"کد کالا نباید تکراری باشد {Environment.NewLine}";

                }
                if (_uow.Products.Where(y => y.ProductId != Product.ProductId).Any(x => x.IranCode == Product.IranCode))
                {
                    errorMessage += $"ایران کد نباید تکراری باشد {Environment.NewLine}";

                }
                if (_uow.Products.Where(y => y.ProductId != Product.ProductId).Any(x => x.ProductBarcode == Product.ProductBarcode))
                {
                    errorMessage += $"بار کد نباید تکراری باشد {Environment.NewLine}";

                }
               
            }
            if (Product.ProductCode == 0)
            {
                errorMessage += $"کد کالا نباید 0 باشد {Environment.NewLine}";

            }
            if (Product.IranCode == 0)
            {
                errorMessage += $"ایران کد نباید 0 باشد {Environment.NewLine}";

            }
            if (Product.ProductBarcode == 0)
            {
                errorMessage += $"بارکد نباید 0 باشد {Environment.NewLine}";

            }
            if (Product.ProductTitle == null || Product.ProductTitle == "")
            {
                errorMessage += $"عنوان کالا نباید خالی باشد {Environment.NewLine}";

            }
            if (Product.ProductRackId==0)
            {
                errorMessage += $"قفسه کالا نباید خالی باشد {Environment.NewLine}";

            }
            if (ProductCodelenght != ProductLenght)
            {
                errorMessage += $"طول کد کالا نامتبر است  {Environment.NewLine}";

            }
            if (BarCodeProductlenght != BarCodeLenght)
            {
                errorMessage += $"طول بارکد نامتبر است  {Environment.NewLine}";

            }
            if (IranCodeProductLenght != IranCodelenght)
            {
                errorMessage += $"طول ایران کد نامتبر است  {Environment.NewLine}";

            }
            if (errorMessage.Length > 0)
            {
                result = false;
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "!اخطار";
                parameters.Content = errorMessage;
                RadWindow.Alert(parameters);

            }
            return result;
            return true;
        }
        public void NavStateFalse()
        {
            addButton.IsEnabled = false;
            saveButton.IsEnabled = true;
            deleteButton.IsEnabled = false;
            cancelButton.IsEnabled = true;
            editButton.IsEnabled = false;
            firstButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            lastButton.IsEnabled = false;
            previousButton.IsEnabled = false;
        }
        public void NavState1False()
        {
            //addButton1.IsEnabled = false;
            //saveButton1.IsEnabled = true;
            //deleteButton1.IsEnabled = false;
            //cancelButton1.IsEnabled = true;
            //editButton1.IsEnabled = false;
            //firstButton1.IsEnabled = false;
            //nextButton1.IsEnabled = false;
            //lastButton1.IsEnabled = false;
            //previousButton1.IsEnabled = false;
        }
        public void NavStateTrue()
        {
            firstButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            lastButton.IsEnabled = true;
            previousButton.IsEnabled = true;
        }
        public void NavState1True()
        {
            //firstButton1.IsEnabled = true;
            //nextButton1.IsEnabled = true;
            //lastButton1.IsEnabled = true;
            //previousButton1.IsEnabled = true;
        }
        #endregion
        #region Navigation
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToPrevious();
        }
        #endregion
        #region SaveAndCancel
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateProduct())
            {
                RaiseCanExecuteChanged();
                NavStateTrue();
                CommitAndBeginEdit();

                var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

                var alert = new RadDesktopAlert
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    Header = "اطلاعات",
                    Content = "اطلاعات با موفقیت ثبت شد",
                    ShowDuration = 2000,
                };
                manager.ShowAlert(alert);
            }// _viewModel.SaveCommand
        }
        private void CommitAndBeginEdit()
        {
            var commitEditCommand = RadDataFormCommands.CommitEdit as RoutedUICommand;
            if (_viewModel != null)
            {

                commitEditCommand.Execute(null, ProductDataForm);
                _viewModel.Save();
                BeginEdit();
            }

        }
        private bool OnCanceling()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اخطار";
            parameters.Content = "آیا  مطمئن هستید؟";
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            return _dialogResult == true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            OnCanceling();
            if (_dialogResult == true)
            {
                if (ProductDataForm != null)
                {
                    RaiseCanExecuteChanged();
                    NavStateTrue();
                    ProductDataForm.CancelEdit();
                    ProductDataForm.CommitEdit();
                }
            }

        }
        private void RaiseCanExecuteChanged()
        {
            addButton.IsEnabled = true;
            saveButton.IsEnabled = false;
            deleteButton.IsEnabled = true;
            cancelButton.IsEnabled = false;
            editButton.IsEnabled = true;

        }
        private void BeginEdit()
        {

            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
            beginEditCommand.Execute(null, ProductDataForm);

        }
        #endregion
        #region Textbox
        private void productCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }

        private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {

        }

        private void RadDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

        }

        private void ProductDataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            ProductDataForm.AddNewItem();
        }


        #endregion

        private void GLRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        private void GLRadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavStateFalse();
            (sender as FrameworkElement).Focus();

            BeginEdit();
        }

        private void RadPathButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductCodeButton_Click(object sender, RoutedEventArgs e)
        {
            ProductCodeWindow productCodeWindow = new ProductCodeWindow();
            productCodeWindow.DataContext = DataContext;
            productCodeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            productCodeWindow.Width = 400;
            productCodeWindow.Height = 300;
            productCodeWindow.CanClose = true;
            productCodeWindow.Owner = Window.GetWindow(this);
            productCodeWindow.Show(); 
        }

        private void SimilarProductDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {

        }

        private void SimilarProductDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {

        }

        private void SimilarProductDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {

        }

        private void SimilarProductDataForm_DeletingItem(object sender, CancelEventArgs e)
        {

        }

        public void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            //var headerCell = (sender as CheckBox).ParentOfType<GridViewHeaderCell>();

            //if (headerCell != null)
            //{
            //    foreach (var item in this.sender.Items)
            //    {
            //        (item as Club).IsChecked = false;
            //    }
            //}
            foreach (var Stock in _viewModel.Stocks)
            {
                Stock.IsSelected = false;
            }
            

        }

        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //var headerCell = (sender as CheckBox).ParentOfType<GridViewHeaderCell>();
            //if (headerCell != null)
            //{ 
            //    foreach (var item in this.clubsGrid.Items)
            //    {
            //        (item as Club).IsChecked = true;
            //    }
            //}
            foreach (var Stock in _viewModel.Stocks)
            {
                Stock.IsSelected = true;
            }

        }

        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            NavState1False();
            (sender as FrameworkElement).Focus();
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
        //    addNewCommand.Execute(null, this.SimilarProductDataForm);
        }

        private void EditButton1_Click(object sender, RoutedEventArgs e)
        {
            NavState1False();
            (sender as FrameworkElement).Focus();

            BeginEditSimilar();
        }
        private void BeginEditSimilar()
        {

            var beginEditCommand = RadDataFormCommands.BeginEdit as RoutedUICommand;
        //    beginEditCommand.Execute(null, SimilarProductDataForm);

        }
        private void SaveButton1_Click(object sender, RoutedEventArgs e)
        {
            //if (ValidateProduct())
            //{
            //    RaiseCanExecuteChanged();
            //    NavState1True();
            //    CommitAndBeginEdit();

            //    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

            //    var alert = new RadDesktopAlert
            //    {
            //        FlowDirection = FlowDirection.RightToLeft,
            //        Header = "اطلاعات",
            //        Content = "اطلاعات با موفقیت ثبت شد",
            //        ShowDuration = 2000,
            //    };
            //    manager.ShowAlert(alert);
            //}// _viewModel.SaveCommand
        }

        private void cancelButton1_Click(object sender, RoutedEventArgs e)
        {
            //OnCanceling();
            //if (_dialogResult == true)
            //{
            //    if (ProductDataForm != null)
            //    {
            //        RaiseCanExecuteChanged();
            //        NavStateTrue();
            //        ProductDataForm.CancelEdit();
            //        ProductDataForm.BeginEdit();
            //    }
            //}

        }

        private void deleteButton1_Click(object sender, RoutedEventArgs e)
        {
            RaiseCanExecuteChanged();
            NavStateTrue();
            var deleteCommand = RadDataFormCommands.Delete as RoutedUICommand;
            deleteCommand.Execute(null, ProductDataForm);
        }
        private void FirstButton1_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToFirst();
        }

        private void LastButton1_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToLast();
        }

        private void NextButton1_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToNext();
        }

        private void PreviousButton1_Click(object sender, RoutedEventArgs e)
        {
            ProductDataForm.MoveCurrentToPrevious();
        }

        private void radGridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            
        if (e.Cell.Column.Name== "ProductName")
            {
                var ee = ((RadComboBox)e.EditingElement).SelectedValue;
                if (ee==null)
                {
                    e.ErrorMessage = "نام محصول را انتخاب نمایید";
                    e.IsValid = false;
                }

            }
            //else if (((GridViewCellValidatingEventArgs)e).Cell.Column.Name == "Ratio")
            //{
            //    var ee = (e.EditingElement).SelectedValue;
            //    if (ee == null)
            //    {
            //        e.ErrorMessage = "نام محصول را انتخاب نمایید";
            //        e.IsValid = false;
            //    }

            //}
            //Order




        }

        private void RadTabControl_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (detailRadTabItem.IsSelected && productDataGrid.SelectedItem != null)
            {
                // NavStateFalse();
                ProductDataForm.CommitEdit();
                //BeginEdit();

            }
        }
        private void ProductRadGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            detailRadTabItem.IsSelected = true;
        }
    }
}
      

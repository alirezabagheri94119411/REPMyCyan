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
using Saina.ApplicationCore.DTOs;
using Microsoft.Win32;
using System.IO;

namespace Saina.WPF.Commerce.ProductTypes
{
    /// <summary>
    /// Interaction logic for ProductTypeListView.xaml
    /// </summary>
    public partial class ProductTypeListView : UserControl
    {
        private ProductTypeListViewModel _viewModel;
        private bool? _dialogResult;
        private IShoppingSystemSettingsService _shoppingSystemSettingsService;
        private bool isModified;

        //  private SainaDbContext _uow;
        public ProductTypeListView()
        {
            //  _uow = new SainaDbContext();
            _shoppingSystemSettingsService = SmObjectFactory.Container.GetInstance<IShoppingSystemSettingsService>();

            InitializeComponent();
            Loaded += (s, e) =>
            {
                _viewModel = DataContext as ProductTypeListViewModel;
                _viewModel.Loaded();
                //  BeginEdit();
                //if (ProductTypeDataForm?.ItemsSource != null)
                //{
                //    var productTypes = ProductTypeDataForm.ItemsSource.Cast<ProductType>();
                //    foreach (var productType in ProductTypes)
                //    {
                //        productType.PropertyChanged += ProductType_PropertyChanged;

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
        void btnExport_Click(object sender, RoutedEventArgs e)
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
                var col0Visibility = productTypeDataGrid.Columns[0].IsVisible;
                productTypeDataGrid.Columns[0].IsVisible = false;//ستون هایی که نمی خواهیم در اکسل دیده شوند
                var opt = new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnHeaders = true,
                    ShowColumnFooters = true,
                    ShowGroupFooters = false,
                };
                using (Stream stream = dialog.OpenFile())
                {
                    productTypeDataGrid.Export(stream,
                     opt);
                }
                productTypeDataGrid.Columns[0].IsVisible = col0Visibility;//این ستون در بالا مخفی شده بود، حالا به حالت اول باز گردانده می شود
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
                var data = productTypeDataGrid.SelectedItems.Cast<ProductType>().Select(ProductType =>
             new
             {
                 ProductTypeCode = ProductType.ProductTypeCode,
                 ProductTypeTitle = ProductType.ProductTypeTitle,
                 RequiredCode = ProductType.RequiredCode,
                 RequiredDocument = ProductType.RequiredDocument
             });

                report.RegBusinessObject("ProductTypeReport", data);
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
                var data = productTypeDataGrid.SelectedItems.Cast<ProductType>().Select(ProductType =>
               new
               {
                   ProductTypeCode = ProductType.ProductTypeCode,
                   ProductTypeTitle = ProductType.ProductTypeTitle,
                   RequiredCode = ProductType.RequiredCode,
                   RequiredDocument = ProductType.RequiredDocument
               });

                report.RegBusinessObject("ProductTypeReport", data);
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
                var data = productTypeDataGrid.SelectedItems.Cast<ProductType>().Select(ProductType =>
                  new
                  {
                      ProductTypeCode = ProductType.ProductTypeCode,
                      ProductTypeTitle = ProductType.ProductTypeTitle,
                      RequiredCode = ProductType.RequiredCode,
                      RequiredDocument = ProductType.RequiredDocument
                  });
                report.RegBusinessObject("ProductTypeReport", data);
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


        private void ProductTypeRadGridView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
            }
        }

        private void ProductTypeRadGridView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsContainedIn);
            e.AvailableOperators.Remove(Telerik.Windows.Data.FilterOperator.IsNotContainedIn);
        }
        private void ProductTypeDataForm_CurrentItemChanged(object sender, EventArgs e)
        {

        }

        private void ProductTypeDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            //  if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            //  _viewModel.Save();
        }
        #endregion
        #region Add
        private void ProductTypeDataForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            var newItem = e.NewItem as ProductType;
            _viewModel.AddBrandCommand.Execute(null);

            newItem.ProductTypeCode = _viewModel.ProductType.ProductTypeCode;
            newItem.ProductTypeTitle = _viewModel.ProductType.ProductTypeTitle;
            newItem.RequiredCode = _viewModel.ProductType.RequiredCode;
            newItem.RequiredDocument = _viewModel.ProductType.RequiredDocument;

            _viewModel.AddProductType((ProductType)e.NewItem);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            NavStateFalse();
            (sender as FrameworkElement).Focus();
            var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            addNewCommand.Execute(null, this.ProductTypeDataForm);
            // if (_viewModel.AddProductTypeCommand.CanExecute(null))
            // _viewModel.AddBrandCommand.Execute(null);
            //   ProductTypeDataForm.AddNewItem();
        }
        #endregion
        #region Delete
        private void ProductTypeDataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
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
        private void ProductTypeDataForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            var r = _viewModel.DeleteProductType((ProductType)e.DeletedItem);
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
            deleteCommand.Execute(null, ProductTypeDataForm);

        }
        #endregion
        #region Validation
        private void ProductTypeDataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        ProductType ProductType => ProductTypeDataForm.CurrentItem as ProductType;
        private bool ValidateProductType()
        {
            var ShoppingSystemSettingModel = AutoMapper.Mapper.Map<ShoppingSystemSettingModel, EditableShoppingSystemSettingViewModel>(_shoppingSystemSettingsService.GetShoppingSystemSettingModel());
            var ProductTypeLenght = int.Parse(ShoppingSystemSettingModel.ProductTypeLenght ?? "0");
            var code = ProductType.ProductTypeCode;
            var Lenght = (code.ToString()).Length;
            var result = true;
            var errorMessage = "";
            ProductTypeDataForm.ValidationSummary.Errors.Clear();
            using (var _uow = new SainaDbContext())
            {
                if (_uow.ProductTypes.Where(y => y.ProductTypeId != ProductType.ProductTypeId).Any(x => x.ProductTypeTitle == ProductType.ProductTypeTitle))
                {
                    errorMessage += $"عنوان نباید تکراری باشد {Environment.NewLine}";

                }
                if (_uow.ProductTypes.Where(y => y.ProductTypeId != ProductType.ProductTypeId).Any(x => x.ProductTypeCode == ProductType.ProductTypeCode))

                {
                    errorMessage += $"کد حساب نباید تکراری باشد {Environment.NewLine}";

                }
            }
            if (ProductType.ProductTypeCode == 0)
            {
                errorMessage += $"کد حساب نباید 0 باشد {Environment.NewLine}";

            }
            if (ProductType.ProductTypeTitle == null || ProductType.ProductTypeTitle == "")
            {
                errorMessage += $"عنوان حساب نباید خالی باشد {Environment.NewLine}";

            }
            if (Lenght != ProductTypeLenght)
            {
                errorMessage += $"طول کد حساب نامتبر است  {Environment.NewLine}";

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
        public void NavStateTrue()
        {
            firstButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            lastButton.IsEnabled = true;
            previousButton.IsEnabled = true;
        }
        #endregion
        #region Navigation
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            ProductTypeDataForm.MoveCurrentToFirst();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            ProductTypeDataForm.MoveCurrentToLast();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ProductTypeDataForm.MoveCurrentToNext();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            ProductTypeDataForm.MoveCurrentToPrevious();
        }
        #endregion
        #region SaveAndCancel
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateProductType())
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

                _viewModel.Save();
                commitEditCommand.Execute(null, ProductTypeDataForm);
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
                if (ProductTypeDataForm != null)
                {
                    RaiseCanExecuteChanged();
                    NavStateTrue();
                    ProductTypeDataForm.CancelEdit();
                    ProductTypeDataForm.BeginEdit();
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
            beginEditCommand.Execute(null, ProductTypeDataForm);

        }
        #endregion
        #region Textbox
        private void productTypeCodeTextbox_GotFocus(object sender, RoutedEventArgs e)
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

        private void ProductTypeDataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            ProductTypeDataForm.AddNewItem();
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
    }
}

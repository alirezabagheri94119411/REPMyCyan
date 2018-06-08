using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System;
using System.Collections.Generic;
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

namespace Saina.WPF.BasicInformation.Settings.AccountingSetting
{
    /// <summary>
    /// Interaction logic for SystemAccountingSettingView.xaml
    /// </summary>
    public partial class SystemAccountingSettingView : UserControl
    {
        private SystemAccountingSettingViewModel _viewModel;
        private ISystemAccountingSettingsService _systemAccountingSettingsService;
        private bool? _dialogResult;

        public SystemAccountingSettingView()
        {
            _systemAccountingSettingsService = SmObjectFactory.Container.GetInstance<ISystemAccountingSettingsService>();
            InitializeComponent();
            Loaded += (s, ea) =>
            {
                _viewModel = DataContext as SystemAccountingSettingViewModel;
                _viewModel.Error += OnError;
                gLLength.Focus();
            };
            Unloaded += (s, ea) =>
            {

                _viewModel.Error -= OnError;
            };

        }

        private void OnError(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اطلاعات";
            parameters.Content = error;
            RadWindow.Alert(parameters);
            //var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

            //var alert = new RadDesktopAlert
            //{
            //    FlowDirection = FlowDirection.RightToLeft,
            //    Header = "اطلاعات",
            //    Content = error,
            //    ShowDuration = 5000,
            //};
        }
        private void gLLength_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
          var SystemAccountingSettingModel = _viewModel.SystemAccountingSettingModel;
            var editableSystemAccountingSettingModel = AutoMapper.Mapper.Map<EditableSystemAccountingSettingModel, SystemAccountingSettingModel>(SystemAccountingSettingModel);
            _systemAccountingSettingsService.SaveSystemAccountingSettingModel(editableSystemAccountingSettingModel);
        }
    }
}

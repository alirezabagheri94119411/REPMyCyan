using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;

namespace Saina.WPF
{
    public class CustomCommandProvider : DataFormCommandProvider
    {
        private bool? _dialogResult;

        public CustomCommandProvider() : base(null)
        {
        }
        public CustomCommandProvider(RadDataForm dataForm)
                : base(dataForm)
        {
            this.DataForm = dataForm;
        }
        protected override void CommitEdit()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اطلاعات";
            parameters.Content = "آیا از ثبت/ویرایش اطلاعات مطمئن هستید؟";
            _dialogResult= _dialogResult == true;
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            if (_dialogResult == true)
            {
                if (this.DataForm != null && this.DataForm.ValidateItem())
                {
                    this.DataForm.CommitEdit();
                }
            }
            //MessageBoxResult result = MessageBox.Show("آیا از ویرایش اطلاعات مطمئن هستید؟", "اطلاعات ویرایش", MessageBoxButton.OKCancel);
            //if (result == MessageBoxResult.OK)
            //{
            //    if (this.DataForm != null && this.DataForm.ValidateItem())
            //    {
            //        this.DataForm.CommitEdit();
            //    }
            //}
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
        protected override void CancelEdit()
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بله، مطمئنم";
            parameters.CancelButtonContent = "خیر";
            parameters.Header = "اطلاعات";
            parameters.Content = "آیا از تغییرات آیتم جاری انصراف می دهید؟";
            _dialogResult = _dialogResult == true;
            parameters.Closed = OnClosed;
            RadWindow.Confirm(parameters);
            if (_dialogResult == true)
            {
                if (this.DataForm != null && this.DataForm.ValidateItem())
                {
                    this.DataForm.CancelEdit();

                }
            }
            //MessageBoxResult result = MessageBox.Show("آیا از تغییرات آیتم جاری انصراف می دهید؟", "اطلاعات انصراف", MessageBoxButton.OKCancel);
            //if (result == MessageBoxResult.OK)
            //{
            //    if (this.DataForm != null)
            //    {
            //        this.DataForm.CancelEdit();
            //    }
            //}
        }
        //. . .
    }
}

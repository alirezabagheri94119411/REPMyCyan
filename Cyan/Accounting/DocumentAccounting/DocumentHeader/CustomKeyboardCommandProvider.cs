using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data;
using Telerik.Windows.Controls.Data.DataForm;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    public class AccDocumentHeaderDataFormKeyboardCommandProvider : DataFormCommandProvider
    {
        private bool? _dialogResult;
        private AccDocumentHeaderListViewModel _accDocumentHeaderListViewModel;
        private Action NavAction;

      
        public AccDocumentHeaderDataFormKeyboardCommandProvider(AccDocumentHeaderListViewModel accDocumentHeaderListViewModel)
            : base(null)
        {
            _accDocumentHeaderListViewModel = accDocumentHeaderListViewModel;
        }
        public AccDocumentHeaderDataFormKeyboardCommandProvider(RadDataForm dataForm)
            : base(dataForm)
        {
            this.DataForm = dataForm;
        }
        public override List<DelegateCommandWrapper> ProvideCommandsForKey(KeyEventArgs args)
        {
            List<DelegateCommandWrapper> actionsToExecute = base.ProvideCommandsForKey(args);
            //if (args.Key == Key.Right)
            //{
            //    actionsToExecute.Clear();
            //    actionsToExecute.Add(new DataFormDelegateCommandWrapper(RadDataFormCommands.MoveCurrentToNext, this.DataForm));
            //    actionsToExecute.Add(new DataFormDelegateCommandWrapper(RadDataFormCommands.BeginEdit, this.DataForm));
            //}
            //if (args.Key == Key.Left)
            //{
            //    actionsToExecute.Clear();
            //    actionsToExecute.Add(new DataFormDelegateCommandWrapper(RadDataFormCommands.MoveCurrentToPrevious, this.DataForm));
            //    actionsToExecute.Add(new DataFormDelegateCommandWrapper(RadDataFormCommands.BeginEdit, this.DataForm));
            //}
            //if (actionsToExecute.Count > 0)
            //{
            //    actionsToExecute.Add(new DataFormDelegateCommandWrapper(new Action(() => { this.DataForm.AcquireFocus(); }), 100, this.DataForm));
            //    args.Handled = true;
            //}

            if (args.Key == Key.Enter)
            {
                actionsToExecute.Clear();
            }
            return actionsToExecute;
        }
        public event Action Reject;
        protected override void MoveCurrentToNext()
        {
            Confirm(base.MoveCurrentToNext);
        }

        private void Confirm(Action action)
        {
            if (_accDocumentHeaderListViewModel.ContextHasChanges)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بله";
                parameters.CancelButtonContent = "خیر";
                parameters.Header = "اخطار";
                parameters.Content = "آیا مطمئن هستید که می خواهید بدون ذخیره تغیرات، به آیتم بعدی بروید؟";
                parameters.Closed = OnClosed;
                RadWindow.Confirm(parameters);
                if (_dialogResult == true)
                {
                    Reject?.Invoke();
                    action();
                }
            }
        }

        protected override void MoveCurrentToFirst()
        {
            Confirm(base.MoveCurrentToFirst);

        }
        protected override void MoveCurrentToLast()
        {
            Confirm(base.MoveCurrentToLast);

        }
        protected override void MoveCurrentToPrevious()
        {
            Confirm(base.MoveCurrentToPrevious);

        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            _dialogResult = e.DialogResult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
   public class EditableTreeAccount:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی درخت حساب ها
        /// </summary>
        private int _treeAccountId;

        public int TreeAccountId
        {
            get { return _treeAccountId; }
            set { SetProperty(ref _treeAccountId, value); }
        }
        /// <summary>
        /// کد گروه
        /// </summary>
        private long _groupCode;

        public long GroupCode
        {
            get { return _groupCode; }
            set { SetProperty(ref _groupCode, value); }
        }
        /// <summary>
        /// نام گروه
        /// </summary>
        private string _groupName;

        public string GroupName
        {
            get { return _groupName; }
            set { SetProperty(ref _groupName, value); }
        }
        /// <summary>
        /// نوع حساب
        /// </summary>
        private string _treeAccountType;

        public string TreeAccountType
        {
            get { return _treeAccountType; }
            set { SetProperty(ref _treeAccountType, value); }
        }
    }
}

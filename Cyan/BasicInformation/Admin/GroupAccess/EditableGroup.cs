using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Admin.GroupAccess
{
   public class EditableGroup:ValidatableBindableBase
    {
        /// <summary>
        /// آی دی گروه
        /// </summary>
        private int _groupId;
        public int GroupId
        {
            get { return _groupId; }
            set { SetProperty(ref _groupId, value); }
        }
        /// <summary>
        /// نام گروه
        /// </summary>
        private string _name;
       
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}

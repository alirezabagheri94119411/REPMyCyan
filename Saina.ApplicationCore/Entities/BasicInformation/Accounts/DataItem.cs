using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.BasicInformation.Accounts
{
  // [ContentProperty("Children")]
    public class DataItem : BindableBase
    {
        private bool isExpanded;
        private string imageUrl;

       
        
        private string _imageUrl;
        [NotMapped]

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private bool _isExpanded;

        [NotMapped]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

       
    }
}

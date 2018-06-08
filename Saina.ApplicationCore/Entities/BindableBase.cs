using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saina.ApplicationCore.Entities
{
    public class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            if (!Handled)
            {
                member = val;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        private bool _isBusy;
        [NotMapped]
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        private bool _isBusyGrid;
        [NotMapped]
        public bool IsBusyGrid
        {
            get { return _isBusyGrid; }
            set { SetProperty(ref _isBusyGrid, value); }
        }
        public virtual void UnLoaded()
        { }
        [NotMapped]
        public bool Handled { get; set; }
    }
}

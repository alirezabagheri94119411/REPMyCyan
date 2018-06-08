using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Saina.ApplicationCore
{
    public class PropertyObservable:INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T member, T val,[CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
                member = val;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}

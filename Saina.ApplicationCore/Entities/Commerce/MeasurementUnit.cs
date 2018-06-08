using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Commerce
{
    /// <summary>
    /// جدول واحد سنجش
    /// </summary>
   public class MeasurementUnit: BaseEntity
    {
        public MeasurementUnit()
        {
            SubUnits = new ObservableCollection<Product>();
            MainUnits = new ObservableCollection<Product>();
        }
        private int _measurementUnitId;
        /// <summary>
        /// آی دی واحد سنجش
        /// </summary>
        public int MeasurementUnitId
        {
            get { return _measurementUnitId; }
            set { SetProperty(ref _measurementUnitId, value);  }
        }
       
        private string _measurementUnitTitle;
        /// <summary>
        /// عنوان 1 واحد سنجش
        /// </summary>
        public string MeasurementUnitTitle
        {
            get { return _measurementUnitTitle; }
            set { SetProperty(ref _measurementUnitTitle, value);  }
        }
        private string _measurementUnitTitle2;
        /// <summary>
        /// عنوان2 واحد سنجش
        /// </summary>
        public string MeasurementUnitTitle2
        {
            get { return _measurementUnitTitle2; }
            set { SetProperty(ref _measurementUnitTitle2, value);  }
        }


        /// <summary>
        /// ارتباط با کالا
        /// </summary>
        [InverseProperty("SubUnit")]
        public virtual ICollection<Product> SubUnits { get; set; }
        [InverseProperty("MainUnit")]

        public virtual ICollection<Product> MainUnits { get; set; }


    }
}

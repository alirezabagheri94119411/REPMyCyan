using Saina.ApplicationCore.Entities.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;

namespace Saina.WPF.Commerce.MeasurementUnits
{
    class MeasurementUnitListViewModel : BindableBase
    {

        #region Constructors
        public MeasurementUnitListViewModel()
        {
          //  AddBrandCommand = new RelayCommand(OnAddProduct);
            MeasurementUnit = new MeasurementUnit();
            _uow = new SainaDbContext();

        }


        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _measurementUnits;
        public event Action<string> Error;

        #endregion
        #region Commands
       // public ICommand AddBrandCommand { get; private set; }


        #endregion
        #region Properties
        private MeasurementUnit _measurementUnit;
        public MeasurementUnit MeasurementUnit
        {
            get { return _measurementUnit; }
            set { SetProperty(ref _measurementUnit, value); }
        }

        public IEditableCollectionViewAddNewItem MeasurementUnits
        {
            get { return _measurementUnits; }
            set
            {
                SetProperty(ref _measurementUnits, value);
            }
        }


        public int ShoppingSystemSettingMeasurementUnitLenght { get; private set; }


        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            MeasurementUnits = new QueryableCollectionView(_uow.MeasurementUnits.ToList());
        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddMeasurementUnit(MeasurementUnit measurementUnit)
        {
            _uow.MeasurementUnits.Add(measurementUnit);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteMeasurementUnit(MeasurementUnit measurementUnit)
        {
            _uow.MeasurementUnits.Remove(measurementUnit);
            return _uow.SaveChanges();
        }

        #endregion

    }
}

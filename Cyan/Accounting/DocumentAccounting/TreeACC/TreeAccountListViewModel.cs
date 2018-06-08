using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Data.Entity;
using System.ComponentModel;
using Telerik.Windows.Data;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    public class TreeAccountListViewModel : BindableBase
    {
        #region Constructors
        public TreeAccountListViewModel(SainaDbContext uow)
        {
            _uow = uow;    //Load();
        }
        #endregion
        #region Fields
        #endregion

        #region Properties
        private ObservableCollection<GL> items;
        private SainaDbContext _uow;

        public ObservableCollection<GL> Items
        {
            get
            {
                return items;
            }
            set
            {
                SetProperty(ref items, value);
            }
        }
        public string Tree { get; set; }
        #endregion
        #region Methode
        public void Load()
        {

            Items = new ObservableCollection<GL>(_uow.GLs.Include(x => x.TLs.Select(y => y.SLs)).AsNoTracking().ToList());

        }
        public void AddGL(GL gl)
        {
            var c = $"INSERT INTO Info.GLs(AccountGLEnum, GLCode, Title, Title2, Status)VALUES({(int)gl.AccountGLEnum}, {gl.GLCode}, '{gl.Title}', '{gl.Title2}', '{gl.Status}')";
         var x=   _uow.Database.ExecuteSqlCommand(c);

            //var x =   _uow.GLs.Add(gl);
        }
        public void AddSL(SL sl)
        {
            var c = ($"INSERT INTO Info.SLs(TLId, SLCode, Title, Title2, Status, Property, AccountsNatureEnum, DLType1, DLType2) VALUES ({ sl.TLId}, { sl.SLCode}, '{ sl.Title}', '{ sl.Title2}', '{ sl.Status}', {(int) sl.Property}, {(int)sl.AccountsNatureEnum}, {(int)sl.DLType1}, {(int)sl.DLType2})");
            var x = _uow.Database.ExecuteSqlCommand(c);

            //  _uow.SLs.Add(sl);
        }
        public void AddTL(TL tl)
        {
            _uow.Database.ExecuteSqlCommand($"INSERT INTO Info.TLs(GLId, TLCode, Title, Title2, Status)VALUES({ tl.GLId}, { tl.TLCode}, '{ tl.Title}', '{ tl.Title2}', '{ tl.Status}')");

            // _uow.TLs.Add(tl);
        }
        public void Save()
        {
            var x = _uow.SaveChanges();
        }
        public override void UnLoaded()
        {
            /// Items = new ObservableCollection<GL>();
        }

        internal int GetGLId(long gLCode)=> _uow.GLs.Where(x => x.GLCode == gLCode).Select(x => x.GLId).First();

        internal int GetTLId(long tLCode) => _uow.TLs.Where(x => x.TLCode == tLCode).Select(x => x.TLId).First();

        internal int GetSLId(long sLCode) => _uow.SLs.Where(x => x.SLCode == sLCode).Select(x => x.SLId).First();
      


        #endregion
    }
}

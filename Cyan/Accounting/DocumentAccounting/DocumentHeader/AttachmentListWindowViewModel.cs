using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    class AttachmentListWindowViewModel:BindableBase
    {

        #region Constructors
        public AttachmentListWindowViewModel()
        {
            AccDocumentHeader = new AccDocumentHeader();
        }
            #endregion
            #region Fields
            private SainaDbContext _uow;
            private IEditableCollectionViewAddNewItem _attachments;
            #endregion
            #region Commands

            #endregion
            #region Properties
            public IEditableCollectionViewAddNewItem Attachments
            {
                get { return _attachments; }
                set
                {
                    SetProperty(ref _attachments, value);
                }
            }
        private AccDocumentHeader _accDocumentHeader;
        public AccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { SetProperty(ref _accDocumentHeader, value); }
        }

        #endregion
        #region Methods
        public void Loaded()
            {
                _uow = new SainaDbContext();
            var x = _uow.Attachments.ToList();
           
                Attachments = new QueryableCollectionView(x);

            }
            public override void UnLoaded()
            {
                _uow.Dispose();
            }
            public void AddAttachment(Attachment attachment)
            {
                _uow.Attachments.Add(attachment);
            }
            public void Save()
            {
                _uow.SaveChanges();
            }
            public int DeleteAttachment(Attachment attachment)
            {
                _uow.Attachments.Remove(attachment);
                return _uow.SaveChanges();
            }

            #endregion

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.Entities.Accounting.DocumentAccounting
{
    [Table("Attachments", Schema = "Acc")]

    public class Attachment:BaseEntity
    {

        private int _attachmentId;
        /// <summary>
        /// آی دی مکاتبات
        /// </summary>
        public int AttachmentId
        {
            get { return _attachmentId; }
            set { SetProperty(ref _attachmentId, value); }
        }

        private string _attachmentTitle;
        /// <summary>
        /// عنوان پیام
        /// </summary>
        public string AttachmentTitle
        {
            get { return _attachmentTitle; }
            set { SetProperty(ref _attachmentTitle, value); }
        }
        private string _attachmentText;
        /// <summary>
        /// متن پیام
        /// </summary>
        public string AttachmentText
        {
            get { return _attachmentText; }
            set { SetProperty(ref _attachmentText, value); }
        }

        private string _userName;
        /// <summary>
        /// نام کاربر
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private DateTime _systemDate=DateTime.Now;
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime SystemDate
        {
            get { return _systemDate; }
            set { SetProperty(ref _systemDate, value); }
        }
        [NotMapped]
        public string SystemDateString
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();

                string result = string.Format($"{pc.GetYear(_systemDate)}/{pc.GetMonth(_systemDate)}/{pc.GetDayOfMonth(_systemDate)}");
                return result;
            }
        }
        private AccDocumentHeader _accDocumentHeader;
        public virtual AccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { SetProperty(ref _accDocumentHeader, value); }
        }
        private int? _accDocumentHeaderId;
        public int? AccDocumentHeaderId
        {
            get { return _accDocumentHeaderId; }
            set { SetProperty(ref _accDocumentHeaderId, value); }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Z.EntityFramework.Plus;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    /// <summary>
    /// Interaction logic for DocumentHistoryListWindow.xaml
    /// </summary>
    public partial class DocumentHistoryListWindow : RadWindow
    {
        private int accDocumentHeaderId;

        public DocumentHistoryListWindow()
        {
            InitializeComponent();
            Loaded += DocumentHistoryListWindow_Loaded;
        }

        public DocumentHistoryListWindow(int accDocumentHeaderId):this()
        {
            this.accDocumentHeaderId = accDocumentHeaderId;
        }

        private void DocumentHistoryListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using (var _uow=new Saina.Data.Context.SainaDbContext())
            {
                
                var auditEntries = _uow.Database.SqlQuery<AuditEntryX>($@"SELECT e.AuditEntryID,
       e.EntitySetName,
       e.EntityTypeName,
       e.State,
       CASE e.State WHEN 0 THEN N'افزودن' WHEN 1 THEN N'حذف'  WHEN 2 THEN N'تغییر' ELSE e.StateName END AS StateName,
       e.CreatedBy,
       e.CreatedDate FROM dbo.AuditEntries e
	INNER JOIN dbo.AuditEntryProperties p ON p.AuditEntryID = e.AuditEntryID
	WHERE e.EntityTypeName='AccDocumentItem' AND p.PropertyName='AccDocumentItemId' 
	AND p.NewValue IN (SELECT AccDocumentItemId FROM Acc.AccDocumentItems WHERE AccDocumentHeaderId={accDocumentHeaderId})").ToList(); //_uow.AuditEntries.Where(X => X.EntityTypeName == "AccDocumentHeader").ToList();
                documentsHistoriesRadGridView.ItemsSource = auditEntries;
            }
        }

        private void productBrandDataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {

        }
    }
    public class AuditEntryX
    {
        public int AuditEntryID { get; set; }
        public string CreatedBy { get; set; }
      //  public DateTime CreatedDate { get; set; }
        private DateTime _CreatedDate;

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set {
                _CreatedDate = value;
                if (CreatedDate!=null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    var getMonth = string.Format($"{pc.GetMonth(_CreatedDate)}");
                    var getDay = string.Format($"{pc.GetDayOfMonth(_CreatedDate)}");
                    if (Convert.ToInt16(getMonth) < 10)
                    {
                        getMonth = "0" + getMonth;
                    }
                    if (Convert.ToInt16(getDay) < 10)
                    {
                        getDay = "0" + getDay;
                    }
                     CreatedDateString = string.Format($"{pc.GetYear(_CreatedDate)}/{getMonth}/{getDay}");
                    // string result = string.Format($"{pc.GetYear(_documentDate)}/{pc.GetMonth(_documentDate)}/{pc.GetDayOfMonth(_documentDate)}");
                   
                }
           

            }
        }

        public string CreatedDateString { get; set; }
        public object Entity { get; set; }
        public string EntitySetName { get; set; }
        public string EntityTypeName { get; set; }
        public Audit Parent { get; set; }
        public List<AuditEntryProperty> Properties { get; set; }
        public AuditEntryState State { get; set; }
        public string StateName { get; set; }

    }
}

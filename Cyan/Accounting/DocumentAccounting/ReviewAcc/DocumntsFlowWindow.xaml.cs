using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
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
using Telerik.Windows.Data;
using System.Data.Entity;
using Telerik.Windows.Controls;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;

namespace Saina.WPF.Accounting.DocumentAccounting.ReviewAcc
{
    /// <summary>
    /// Interaction logic for DocumntsFlowWindow.xaml
    /// </summary>
    public partial class DocumntsFlowWindow : RadWindow
    {
        public DocumntsFlowWindow(AccDocumentItemDTO sender, string state)
        {
            InitializeComponent();
            //var _uow = SmObjectFactory.Container.GetInstance<SainaDbContext>();
            //gridViewGrid.DataContext = new VirtualQueryableCollectionView(ctx.DocItems.OrderBy(x => x.Id)) { LoadSize = 10 };
            Loaded += (s, e) =>
            {
                    accDocumentItemsGridView.IsBusy = true;
                var _uow = SmObjectFactory.Container.GetInstance<SainaDbContext>();

                    switch (state)
                    {
                        case "OnGLGrouped":
                        case "OnGLDetailed":
                        var cc = _uow.AccDocumentItems
                                            .Where(x => x.SL.TL.GL.GLId == sender.Id)
                                            .OrderBy(x => x.AccDocumentHeaderId);
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.TL.GL.GLId == sender.Id)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        case "OnTLGrouped":
                        case "OnTLDetailed":
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.TLId == sender.Id)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        case "OnSLGrouped":
                        case "OnSLDetailed":
                            accDocumentItemsGridView.DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                .OrderBy(x => x.AccDocumentHeaderId)
                                            .Where(x => x.SLId == sender.Id)
                                            )
                            { LoadSize = 10 };
                            break;
                        case "OnDL1Grouped":
                        case "OnDL1Detailed":
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.SLCode == sender.Code)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        case "OnDL2Grouped":
                        case "OnDL2Detailed":
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.SLCode == sender.Code)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        case "OnCurrencyGrouped":
                        case "OnCurrencyDetailed":
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.SLCode == sender.Code)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        case "OnTrackingGrouped":
                        case "OnTrackingDetailed":
                            DataContext = new VirtualQueryableCollectionView(_uow.AccDocumentItems
                                            .Where(x => x.SL.SLCode == sender.Code)
                                            .OrderBy(x => x.AccDocumentHeaderId))
                            { LoadSize = 10 };
                            break;
                        default:
                            break;
                           
                    }
                accDocumentItemsGridView.IsBusy = false;
            };
            }
        public event Action<int> TestRequested;

        private void accDocumentItemsGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(accDocumentItemsGridView.SelectedItem is AccDocumentItem accDocumentItem)
            {
                TestRequested?.Invoke(accDocumentItem.AccDocumentHeaderId);
            }
        }
    }
    }

using Saina.WPF.Properties;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.Filtering.Editors;

namespace Saina.WPF
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {

            LocalizationManager.Manager = new LocalizationManager()
            {
                ResourceManager = GridViewResources.ResourceManager
            };
            //dark color variation
            //VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Dark);
            InitializeComponent();
            //StyleManager.ApplicationTheme = new Office2016Theme();
          //  StyleManager.SetTheme(radTreeListView, new GreenTheme ());
            DataContext = SmObjectFactory.Container.GetInstance<MainWindowViewModel>();
            Loaded += (s, ea) =>
            {
                //Style baseStyle = new Style(typeof(RadTreeListView));
                ////Style editingStyle = new Style(typeof(RadTreeListView));
                ////editingStyle.BasedOn = baseStyle;
                //Style editingStyle = new Style(typeof(RadTreeListView), baseStyle);
                _viewModel = DataContext as MainWindowViewModel;
                _viewModel.Failed += OnFailed;
                //StyleManager.SetTheme(radTreeListView, new VistaTheme());
            };
            Unloaded += (s, ea) =>
            {
                _viewModel.Failed -= OnFailed;


            };
        }
        private void OnFailed(string error)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.OkButtonContent = "بستن";
            parameters.Header = "!اخطار";
            parameters.Content = error;
            RadWindow.Alert(parameters);
        }
        private void radTreeListView_FieldFilterEditorCreated(object sender, Telerik.Windows.Controls.GridView.EditorCreatedEventArgs e)
        {
            //get the StringFilterEditor in your RadGridView
            var stringFilterEditor = e.Editor as StringFilterEditor;
            if (stringFilterEditor != null)
            {
                stringFilterEditor.MatchCaseVisibility = Visibility.Hidden;
                e.Editor.Loaded += (s1, e1) =>
                {
                    var textBox = e.Editor.ChildrenOfType<TextBox>().Single();
                    textBox.TextChanged += (s2, e2) =>
                    {
                        textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    };
                };
            }

        }

        private void radTreeListView_FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.Contains;
        }

        private void radTreeListView_Filtered(object sender, Telerik.Windows.Controls.GridView.GridViewFilteredEventArgs e)
        {
            radTreeListView.ExpandAllHierarchyItems();
        }
    }
    public class CustomDockingPanesFactory : DockingPanesFactory
    {
        protected override void AddPane(Telerik.Windows.Controls.RadDocking radDocking, Telerik.Windows.Controls.RadPane pane)
        {
            if (pane?.Tag != null)
            {
                if (pane.Name == "login")
                {
                    pane.CanUserClose = false;
                    pane.Visibility = Visibility.Collapsed;
                    //pane.IsDragDisabled = false;
                }
                //pane.CanFloat = false;
                pane.FlowDirection = FlowDirection.RightToLeft;
                var tag = pane.Tag.ToString();
                var paneGroup = radDocking.SplitItems.ToList().FirstOrDefault(i => i.Control.Name.Contains(tag)) as RadPaneGroup;

                if (paneGroup != null)
                {
                    paneGroup.Items.Add(pane);
                }
                else
                {
                    base.AddPane(radDocking, pane);
                }
            }
        }
    }
  
}

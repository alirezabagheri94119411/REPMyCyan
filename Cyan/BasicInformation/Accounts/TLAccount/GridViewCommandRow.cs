using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
    public class GridViewCommandRow
    {
        public ControlTemplate ViewCommandTemplate { get; set; }

        public ControlTemplate EditCommandTemplate { get; set; }

        private ContentControl CommandRow { get; set; }

        private RadGridView radGridView;

        public RadGridView RadGridView
        {
            get
            {
                return radGridView;
            }
            private set
            {
                radGridView = value;
            }
        }

        public GridViewCommandRow(RadGridView grid)
        {
            this.RadGridView = grid;
            this.CommandRow = new ContentControl();
            CommandRow.SetValue(Grid.RowProperty, 1);

            if (this.ViewCommandTemplate == null)
            {
                this.ViewCommandTemplate = App.Current.Resources["CommandRowTemplate"] as ControlTemplate;
            }

            if (this.EditCommandTemplate == null)
            {
                this.EditCommandTemplate = App.Current.Resources["CommandRowEditTemplate"] as ControlTemplate;
            }
        }

        public static readonly DependencyProperty IsEnabledProperty =
                                    DependencyProperty.RegisterAttached("IsEnabled",
                                                typeof(bool),
                                                typeof(GridViewCommandRow),
                                                new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(IsEnabledProperty, enabled);
        }

        public static object GetIsEnabled(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(IsEnabledProperty);
        }

        private static void OnIsEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RadGridView radGridView = sender as RadGridView;
            if (radGridView != null)
            {
                if ((bool)e.NewValue)
                {
                    // Create new GridViewCommandRow and attach RadGridView events.
                    GridViewCommandRow commandRow = new GridViewCommandRow(radGridView);
                    commandRow.Attach();
                }
            }
        }

        private void Attach()
        {
            this.RadGridView.DataLoaded += new EventHandler<EventArgs>(RadGridView_DataLoaded);
            this.RadGridView.BeginningEdit += new EventHandler<GridViewBeginningEditRoutedEventArgs>(RadGridView_BeginningEdit);
            this.RadGridView.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(RadGridView_RowEditEnded);
        }

        void RadGridView_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            this.CommandRow.Template = this.ViewCommandTemplate;
        }

        void RadGridView_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            this.CommandRow.Template = this.EditCommandTemplate;

        }

        void RadGridView_DataLoaded(object sender, EventArgs e)
        {
            if (this.RadGridView.Items.Count > 0)
            {
                this.RadGridView.SelectedItem = this.RadGridView.Items[0];
            }

            Grid hierarchyGrid = this.RadGridView.ChildrenOfType<Grid>().Where(c => c.Name == "HierrarchyBackground").FirstOrDefault();

            if (hierarchyGrid != null && this.CommandRow.Parent == null)
            {
                hierarchyGrid.Children.Add(CommandRow);

                this.CommandRow.Template = this.ViewCommandTemplate;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Saina.WPF.CustomControls
{
    public class RadGridViewSimpleComboBoxColumn : GridViewComboBoxColumn
    {
        private object _dataItem;
        private RadComboBox _combobox;

        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            var box = cell.Content as TextBlock;
            if (box == null)
            {
                box = new TextBlock { TextAlignment = TextAlignment };
                box.SetBinding(TextBlock.TextProperty, DataMemberBinding);
            }
            return box;
        }

        public override FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            _dataItem = dataItem;

            _combobox = new RadComboBox { ItemsSource = ItemsSource, IsEditable = IsComboBoxEditable };
            _combobox.LostFocus += ComboBoxLostFocus;

            var prop = _dataItem.GetType().GetProperty(DataMemberBinding.Path.Path);
            if (prop != null)
            {
                if (_combobox.IsEditable)
                {
                    _combobox.Text = prop.GetValue(_dataItem) as string;
                }
                else
                {
                    _combobox.SelectedItem = prop.GetValue(_dataItem);
                }
            }

            return _combobox;
        }

        private void ComboBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var prop = _dataItem.GetType().GetProperty(DataMemberBinding.Path.Path);
            if (prop == null) return;
            if (_combobox.IsEditable)
            {
                prop.SetValue(_dataItem, _combobox.Text);
            }
            else
            {
                prop.SetValue(_dataItem, _combobox.SelectedItem);
            }
        }
    }
}

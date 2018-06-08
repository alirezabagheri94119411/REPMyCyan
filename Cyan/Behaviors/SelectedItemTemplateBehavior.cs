using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace Saina.WPF.Behaviors
{
    public static class SelectedItemTemplateBehavior
    {
        public static readonly DependencyProperty SelectedItemDataTemplateProperty =
            DependencyProperty.RegisterAttached("SelectedItemDataTemplate", typeof(DataTemplate), typeof(SelectedItemTemplateBehavior), new PropertyMetadata(default(DataTemplate), PropertyChangedCallback));

        public static void SetSelectedItemDataTemplate(this UIElement element, DataTemplate value)
        {
            element.SetValue(SelectedItemDataTemplateProperty, value);
        }

        public static DataTemplate GetSelectedItemDataTemplate(this RadComboBox element)
        {
            return (DataTemplate)element.GetValue(SelectedItemDataTemplateProperty);
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = d as RadComboBox;
            if (e.Property == SelectedItemDataTemplateProperty && uiElement != null)
            {
                uiElement.Loaded -= UiElementLoaded;
                UpdateSelectionTemplate(uiElement);
                uiElement.Loaded += UiElementLoaded;

            }
        }

        static void UiElementLoaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectionTemplate((RadComboBox)sender);
        }

        private static void UpdateSelectionTemplate(RadComboBox uiElement)
        {
            var contentPresenter = GetChildOfType<ContentPresenter>(uiElement);
            if (contentPresenter == null)
                return;
            var template = uiElement.GetSelectedItemDataTemplate();
            contentPresenter.ContentTemplate = template;
        }


        public static T GetChildOfType<T>(DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}

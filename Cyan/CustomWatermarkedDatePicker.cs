using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Saina.WPF
{
    public class CustomWatermarkedDatePicker : Microsoft.Windows.Controls.DatePicker
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DatePickerTextBox box = base.GetTemplateChild("PART_TextBox") as DatePickerTextBox;
            box.ApplyTemplate();

            ContentControl watermark = box.Template.FindName("PART_Watermark", box) as ContentControl;
            watermark.Content = Tag.ToString();
        }
    }
}

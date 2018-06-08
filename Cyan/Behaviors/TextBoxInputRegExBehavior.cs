using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Saina.WPF.Behaviors
{
    class TextBoxInputRegExBehavior : Behavior<TextBox>
    {

        #region DependencyProperties
        public static readonly DependencyProperty RegularExpressionProperty =
            DependencyProperty.Register("RegularExpression", typeof(string), typeof(TextBoxInputRegExBehavior), new FrameworkPropertyMetadata(".*"));

        public string RegularExpression
        {
            get { return (string)GetValue(RegularExpressionProperty); }
            set { SetValue(RegularExpressionProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(TextBoxInputRegExBehavior),
                                            new FrameworkPropertyMetadata(int.MinValue));

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }


        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinLengthProperty =
            DependencyProperty.Register("MinLength", typeof(int), typeof(TextBoxInputRegExBehavior), new PropertyMetadata(0));



        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(string), typeof(TextBoxInputRegExBehavior), null);

        public string EmptyValue
        {
            get { return (string)GetValue(EmptyValueProperty); }
            set { SetValue(EmptyValueProperty, value); }
        }
        #endregion
        /// <summary>
        ///     Attach our behaviour. Add event handlers
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            //AssociatedObject.GotFocus += AssociatedObject_GotFocus;
            DataObject.AddPastingHandler(AssociatedObject, PastingHandler);
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            //AssociatedObject.ClearValue(Border.BorderBrushProperty);
            //AssociatedObject.ToolTip = "";
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateAll();
        }

        private void ValidateAll()
        {
            string wholeRegex = RegularExpression?.Replace("0,", "");
            var notValid = !ValidateText(AssociatedObject.Text, wholeRegex);
            if (notValid)
            {
           //     AssociatedObject.BorderBrush = Brushes.Red;
                AssociatedObject.ToolTip = AssociatedObject.Tag;
            }
            else
            {
                AssociatedObject.ClearValue(Border.BorderBrushProperty);
                AssociatedObject.ToolTip = "";
            }
        }

        /// <summary>
        ///     Deattach our behaviour. remove event handlers
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= PreviewTextInputHandler;
            AssociatedObject.PreviewKeyDown -= PreviewKeyDownHandler;
            AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
            //AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            DataObject.RemovePastingHandler(AssociatedObject, PastingHandler);
        }

        #region Event handlers [PRIVATE] --------------------------------------

        void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
        {
            string text;
            if (this.AssociatedObject.Text.Length < this.AssociatedObject.CaretIndex)
                text = this.AssociatedObject.Text;
            else
            {
                //  Remaining text after removing selected text.

                text = TreatSelectedText(out string remainingTextAfterRemoveSelection)
                    ? remainingTextAfterRemoveSelection.Insert(AssociatedObject.SelectionStart, e.Text)
                    : AssociatedObject.Text.Insert(this.AssociatedObject.CaretIndex, e.Text);
            }
            e.Handled = !ValidateText(text);
        }

        /// <summary>
        ///     PreviewKeyDown event handler
        /// </summary>
        void PreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.EmptyValue))
                return;

            

            string text = null;

            // Handle the Backspace key
            if (e.Key == Key.Back)
            {
                if (EmptyValue.Length == AssociatedObject.Text.Length)
                    e.Handled = true;
                if (!this.TreatSelectedText(out text))
                {
                    if (AssociatedObject.SelectionStart > 0)
                        text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart - 1, 1);
                }
            }
            // Handle the Delete key
            else if (e.Key == Key.Delete)
            {
                // If text was selected, delete it
                if (!this.TreatSelectedText(out text) && this.AssociatedObject.Text.Length > AssociatedObject.SelectionStart)
                {
                    // Otherwise delete next symbol
                    text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, 1);
                }
            }
            else
                ValidateAll();
            if (text == string.Empty)
            {
                this.AssociatedObject.Text = this.EmptyValue;
                if (e.Key == Key.Back)
                    AssociatedObject.SelectionStart++;
                e.Handled = true;
            }
            
        }

        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!ValidateText(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }
        #endregion Event handlers [PRIVATE] -----------------------------------

        #region Auxiliary methods [PRIVATE] -----------------------------------

        /// <summary>
        ///     Validate certain text by our regular expression and text length conditions
        /// </summary>
        /// <param name="text"> Text for validation </param>
        /// <returns> True - valid, False - invalid </returns>
        private bool ValidateText(string text, string wholeRegex = "")
        {
            if (string.IsNullOrWhiteSpace( wholeRegex))
                return RegularExpression != null && (new Regex(this.RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text) && (MaxLength == int.MinValue || MaxLength == 0 ||  text.Length <= MaxLength);
            return  ( new Regex(wholeRegex, RegexOptions.IgnoreCase)).IsMatch(text) && (MaxLength == int.MinValue || MaxLength==0 || text.Length <= MaxLength);
        }

        /// <summary>
        ///     Handle text selection
        /// </summary>
        /// <returns>true if the character was successfully removed; otherwise, false. </returns>
        private bool TreatSelectedText(out string text)
        {
            text = null;
            if (AssociatedObject.SelectionLength <= 0)
                return false;

            var length = this.AssociatedObject.Text.Length;
            if (AssociatedObject.SelectionStart >= length)
                return true;

            if (AssociatedObject.SelectionStart + AssociatedObject.SelectionLength >= length)
                AssociatedObject.SelectionLength = length - AssociatedObject.SelectionStart;

            text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
            return true;
        }
        #endregion Auxiliary methods [PRIVATE] --------------------------------
    }
}

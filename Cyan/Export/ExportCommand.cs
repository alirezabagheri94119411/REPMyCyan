using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.GridView;
using System.Windows;
using System.Windows.Input;


using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System.IO;

namespace Saina.WPF.Export
{
    public class ExportCommand : ICommand
    {
        private readonly ExportingModel model;

        public ExportCommand(ExportingModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.model.Export(parameter);
        }
    }

    public class ExportingModel : ViewModelBase
    {
        public ExportingModel()
        {
            this.ExportCommand = new ExportCommand(this);
        }

        private ExportCommand _exportCommand = null;

        public ExportCommand ExportCommand
        {
            get
            {
                return this._exportCommand;
            }
            set
            {
                if (this._exportCommand != value)
                {
                    this._exportCommand = value;
                    OnPropertyChanged("ExportCommand");
                }
            }
        }

        public void Export(object parameter)
        {
            RadGridView grid = parameter as RadGridView;
            if (grid != null)
            {
                grid.ElementExporting -= this.ElementExporting;
                grid.ElementExporting += this.ElementExporting;

                grid.ElementExported -= this.ElementExported;
                grid.ElementExported += this.ElementExported;

                string extension = "xls";
                ExportFormat format = ExportFormat.Html;

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = extension;
                dialog.Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel");
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == true)
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        GridViewExportOptions options = new GridViewExportOptions();
                        options.Format = format;
                        options.ShowColumnHeaders = true;
                        options.Encoding = System.Text.Encoding.UTF8;
                        grid.Export(stream, options);
                    }
                }
            }
        }

        private void ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            if (e.Element == ExportElement.HeaderRow)
            {
                var htmlVisualExportParameters = e.VisualParameters as GridViewHtmlVisualExportParameters;
                if (htmlVisualExportParameters != null)
                {
                    htmlVisualExportParameters.FontWeight = FontWeights.Bold;
                }
            }
        }

        private void ElementExported(object sender, GridViewElementExportedEventArgs e)
        {
            if (e.Element == ExportElement.Row)
            {
                GL obj = e.Context as GL;
                if (obj != null)
                {
                    e.Writer.Write(@"<tr><td style=""background-color:#CCC;"" colspan=""{0}"">", ((RadGridView)sender).Columns.Count);

                    e.Writer.Write("<b>GLCode:</b> {0} <br />", obj.GLCode);
                    e.Writer.Write("<b>Title:</b> {0} <br />", obj.Title);
              

                    e.Writer.Write("</td></tr>");
                }
            }
        }
    }
}

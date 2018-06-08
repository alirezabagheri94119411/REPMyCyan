using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    public class SLFilteringBehavior : ComboBoxFilteringBehavior
    {
        private int charLenght;
        public override List<int> FindMatchingIndexes(string text)
        {
          return ComboBox.Items.OfType<SL>().Where(i => i.SLCode.ToString().Contains(text) || i.Title.Contains(text)).Select(i => ComboBox.Items.IndexOf(i)).ToList();
        }
        //public override int FindFullMatchIndex(ReadOnlyCollection<int> matchIndexes)
        //{
        //    var fullMatch = this.ComboBox.Items.OfType<SL>().FirstOrDefault(i => i.SLCode == charLenght);
        //    if (fullMatch == null)
        //    {
        //        return -1;
        //    }
        //    var fullMatchIndex = this.ComboBox.Items.IndexOf(fullMatch);
        //    if (matchIndexes.Contains(fullMatchIndex))
        //    {
        //        return fullMatchIndex;
        //    }
        //    return -1;
        //}
    }
    public class DLFilteringBehavior : ComboBoxFilteringBehavior
    {
        private int charLenght;
        public override List<int> FindMatchingIndexes(string text)
        {
            return ComboBox.Items.OfType<DL>().Where(i => i.DLCode.ToString().Contains(text) || i.Title.Contains(text)).Select(i => ComboBox.Items.IndexOf(i)).ToList();
        }
      
    }
}

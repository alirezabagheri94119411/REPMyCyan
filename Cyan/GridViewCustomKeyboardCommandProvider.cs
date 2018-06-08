using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Saina.WPF
{
    public class GridViewCustomKeyboardCommandProvider : DefaultKeyboardCommandProvider
    {
        private GridViewDataControl parentGrid;

        public GridViewCustomKeyboardCommandProvider(GridViewDataControl grid)
         : base(grid)
        {
            parentGrid = grid;
        }

        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();
            // get the last column index
            var lastcolumnIndex = parentGrid.Columns.Count - 1;
            // check if the current column is the last one
            var isLastColumn = parentGrid.CurrentCell?.Column == parentGrid.Columns[lastcolumnIndex];

            // get the last row index
            var lastRowIndex = parentGrid.Items.Count - 1;
            // check if the current row is the last one
            var isLastRow = parentGrid.Items.IndexOf(parentGrid.SelectedItem) == lastRowIndex;

            if (key == Key.Enter)
            {
                commandsToExecute.Clear();
                if (parentGrid.Items.IsAddingNew)
                {
                    commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                }
                commandsToExecute.Add(RadGridViewCommands.MoveNext);
                commandsToExecute.Add(RadGridViewCommands.SelectCurrentUnit);
                if (parentGrid.Items.IsAddingNew)
                    commandsToExecute.Add(RadGridViewCommands.BeginEdit);
                if (isLastColumn && isLastRow)
                {
                    commandsToExecute.Clear();
                    if (parentGrid.Items.IsAddingNew)
                    {
                        commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                    }
                    commandsToExecute.Add(RadGridViewCommands.BeginInsert);
                    parentGrid.CurrentColumn = parentGrid.Columns[0];
                }
            }

            //else 
            ////if (key == Key.Right)
            ////{
            ////    commandsToExecute.Clear();
            ////    if (parentGrid.Items.IsEditingItem)
            ////    {
            ////        commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            ////    }
            ////    else
            ////    {
            ////        commandsToExecute.Add(RadGridViewCommands.MoveNext);
            ////        commandsToExecute.Add(RadGridViewCommands.SelectCurrentUnit);

            ////        //if (isLastColumn)
            ////        //{
            ////        //    commandsToExecute.Add(RadGridViewCommands.MoveDown);
            ////        //    //parentGrid.CurrentColumn = parentGrid.Columns[0];
            ////        //}

            ////        commandsToExecute.Add(RadGridViewCommands.BeginEdit);

            ////        ////////////////////////////////////////////////////////////


            ////        // check if we're at the last cell
            ////        if (isLastColumn && isLastRow)
            ////        {
            ////            // remove all commands to execute
            ////            commandsToExecute.Clear();

            ////            // commit any changes if we're on the last column of the newest row
            ////            if (parentGrid.Items.IsAddingNew)
            ////            {
            ////                commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            ////            }

            ////            // insert the new row and set it's focus to the first cell
            ////            commandsToExecute.Add(RadGridViewCommands.BeginInsert);
            ////            parentGrid.CurrentColumn = parentGrid.Columns[0];
            ////        }
            ////    }
            ////}
            ////if (key == Key.Left)
            ////{
            ////    commandsToExecute.Clear();
            ////    if (parentGrid.Items.IsEditingItem)
            ////    {
            ////        commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            ////    }
            ////    else
            ////    {
            ////        commandsToExecute.Add(RadGridViewCommands.MoveLeft);
            ////        commandsToExecute.Add(RadGridViewCommands.SelectCurrentUnit);

            ////        //if (isLastColumn)
            ////        //{
            ////        //    commandsToExecute.Add(RadGridViewCommands.MoveDown);
            ////        //    //parentGrid.CurrentColumn = parentGrid.Columns[0];
            ////        //}

            ////        commandsToExecute.Add(RadGridViewCommands.BeginEdit);

            ////        ////////////////////////////////////////////////////////////


            ////        // check if we're at the last cell
            ////        if (isLastColumn && isLastRow)
            ////        {
            ////            // remove all commands to execute
            ////            commandsToExecute.Clear();

            ////            // commit any changes if we're on the last column of the newest row
            ////            if (parentGrid.Items.IsAddingNew)
            ////            {
            ////                commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            ////            }

            ////            // insert the new row and set it's focus to the first cell
            ////            commandsToExecute.Add(RadGridViewCommands.BeginInsert);
            ////            parentGrid.CurrentColumn = parentGrid.Columns[0];
            ////        }
            ////    }
            ////}

            // check if the tab key was hit
            else if (key == Key.Tab)
            {
                // check if we're at the last cell
                if (isLastColumn && isLastRow)
                {
                    // remove all commands to execute
                    commandsToExecute.Clear();

                    // commit any changes if we're on the last column of the newest row
                    if (parentGrid.Items.IsAddingNew)
                    {
                        commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                    }

                    // insert the new row and set it's focus to the first cell
                    commandsToExecute.Add(RadGridViewCommands.BeginInsert);
                    parentGrid.CurrentColumn = parentGrid.Columns[0];
                }
            }
            //else if (key == Key.Down)
            //{

            //    if (!isLastColumn || isLastRow)
            //    {
            //        commandsToExecute.Clear();

            //        // commit any changes if we're on the last column of the newest row
            //        if (parentGrid.Items.IsAddingNew)
            //        {
            //            commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //        }

            //        // insert the new row and set it's focus to the first cell
            //        commandsToExecute.Add(RadGridViewCommands.BeginInsert);
            //        parentGrid.CurrentColumn = parentGrid.Columns[0];
            //    }
            //    else
            //    {
            //        commandsToExecute.Clear();
            //        if (parentGrid.Items.IsAddingNew)
            //        {
            //            commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //        }
            //        commandsToExecute.Add(RadGridViewCommands.MoveDown);
            //        //parentGrid.CurrentColumn = parentGrid.Columns[0];
            //    }

            //}
            //else if (key == Key.Up)
            //{
            //    if (!isLastColumn || isLastRow)
            //    {
            //        commandsToExecute.Clear();

            //        // commit any changes if we're on the last column of the newest row
            //        if (parentGrid.Items.IsAddingNew)
            //        {
            //            commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //        }

            //        // insert the new row and set it's focus to the first cell
            //        //commandsToExecute.Add(RadGridViewCommands.BeginInsert);
            //        //parentGrid.CurrentColumn = parentGrid.Columns[0];
            //    }
            //    else
            //    {
            //        commandsToExecute.Clear();
            //        if (parentGrid.Items.IsAddingNew)
            //        {
            //            commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //        }
            //        commandsToExecute.Add(RadGridViewCommands.MoveUp);
            //        //parentGrid.CurrentColumn = parentGrid.Columns[0];
            //    }
            //    //commandsToExecute.Clear();
            //    //if (parentGrid.Items.IsAddingNew)
            //    //{
            //    //    commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //    //}
            //    //commandsToExecute.Add(RadGridViewCommands.MoveUp);
            //    //commandsToExecute.Add(RadGridViewCommands.CommitEdit);
            //    //parentGrid.CurrentColumn = parentGrid.Columns[0];

            //}

            return commandsToExecute;
        }
    }
}

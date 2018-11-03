using System.Windows.Controls;

namespace MealTracking.Controls
{
    internal class DataGrid : System.Windows.Controls.DataGrid
    {
        public DataGrid()
        {
            CanUserSortColumns = true;
            CanUserAddRows = false;
            IsReadOnly = true;
            AutoGenerateColumns = false;
            SelectionMode = DataGridSelectionMode.Single;
            SelectionUnit = DataGridSelectionUnit.FullRow;
        }
    }
}
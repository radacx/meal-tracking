using System.Threading.Tasks;

namespace MealTracking.Structures.Dialogs
{
    internal interface IDialog<out TDataContext>
    {
        TDataContext Data { get; }

        Task<DialogResult> Show();
    }
}
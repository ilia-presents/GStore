using GStore.Models.ViewModels;
using GStore.Models;
using System.Linq.Expressions;

namespace GStore.ModelsHelper
{
    public class SizeSetHelper
    {

        public static void ToggleActivityStatus(SizeSet sizeSet)
        {
            sizeSet.IsActive = !sizeSet.IsActive;
        }

        public static SizeStutusChangeVM SetStatusChange(SizeSetVM sizeSetVM)
        {
            SizeStutusChangeVM statusChangeVM = new SizeStutusChangeVM();

            statusChangeVM.sizeSetVM = sizeSetVM;

            if (sizeSetVM.IsActive)
            {
                statusChangeVM.TitleText = "Деактивиране на Размер";
                statusChangeVM.WarningText = "Сигурен ли сте, че искате да деактивирате този размер ?";
                statusChangeVM.ButtonText = "Деактивирай";
            }
            else
            {
                statusChangeVM.TitleText = "Активиране на Размер";
                statusChangeVM.WarningText = "";
                statusChangeVM.ButtonText = "Активирай";
            }

            return statusChangeVM;
        }

        public static SizeSet MapVmToEntity(SizeSetVM sizeSetVM)
        {
            SizeSet sizeSet = new SizeSet()
            {
                Id = sizeSetVM.Id,
                Name = sizeSetVM.Name,
                IsActive = sizeSetVM.IsActive
            };

            return sizeSet;
        }

        public static void MapEntityForEdit(SizeSet sizeSet, SizeSetVM sizeSetVM)
        {
            sizeSet.Name = sizeSetVM.Name;
        }

        public static SizeSetVM MapEntityToVm(SizeSet sizeSet)
        {
            SizeSetVM sizeSetVM = new SizeSetVM()
            {
                Id = sizeSet.Id,
                Name = sizeSet.Name,
                IsActive = sizeSet.IsActive
            };

            return sizeSetVM;
        }

        public static Expression<Func<SizeSet, SizeSetVM>> SizeSetToVmSelector
        {
            get
            {
                return sizeSet => new SizeSetVM()
                {
                    Id = sizeSet.Id,
                    Name = sizeSet.Name,
                    IsActive = sizeSet.IsActive
                };
            }
        }
    }
}

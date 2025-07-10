using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GStore.Models.ViewModels;
using System.Linq.Expressions;

namespace GStore.Models
{
    public class ColorSet
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(82)")]
        public string ColorCode { get; set; } = "";

        [StringLength(42)]
        public string Name { get; set; } = "";

        public bool IsActive { get; set; } = true;

        public virtual List<Shirt> Products { get; set; }

        public static void ToggleActivityStatus(ColorSet colorSet)
        {
            colorSet.IsActive = !colorSet.IsActive;
        }

        public static ColorStutusChangeVM SetStatusChange(ColorSetVM colorSetVM)
        {
            ColorStutusChangeVM statusChangeVM = new ColorStutusChangeVM();

            statusChangeVM.colorSetVM = colorSetVM;

            if (colorSetVM.IsActive)
            {
                statusChangeVM.TitleText = "Деактивиране на Цвят";
                statusChangeVM.WarningText = "Сигурен ли сте, че искате да деактивирате този цвят ?";
                statusChangeVM.ButtonText = "Деактивирай";
            }
            else
            {
                statusChangeVM.TitleText = "Активиране на Цвят";
                statusChangeVM.WarningText = "";
                statusChangeVM.ButtonText = "Активирай";
            }


            return statusChangeVM;
        }

        public static ColorSet MapVmToEntity(ColorSetVM colorSetVM)
        {
            ColorSet colorSet = new ColorSet()
            {
                Id = colorSetVM.Id,
                Name = colorSetVM.Name,
                ColorCode = colorSetVM.ColorCode,
                IsActive = colorSetVM.IsActive
            };

            return colorSet;
        }

        public static void MapEntityForEdit(ColorSet colorSet, ColorSetVM colorSetVM)
        {
            colorSet.Name = colorSetVM.Name;
            colorSet.ColorCode = colorSetVM.ColorCode;
        }

        public static ColorSetVM MapEntityToVm(ColorSet colorSet)
        {
            ColorSetVM colorSetVM = new ColorSetVM()
            {
                Id = colorSet.Id,
                Name = colorSet.Name,
                ColorCode = colorSet.ColorCode,
                IsActive = colorSet.IsActive
            };

            return colorSetVM;
        }

        public static Expression<Func<ColorSet, ColorSetVM>> ColorSetToVmSelector
        {
            get
            {
                return colorSet => new ColorSetVM()
                {
                    Id = colorSet.Id,
                    Name = colorSet.Name,
                    ColorCode = colorSet.ColorCode,
                    IsActive = colorSet.IsActive
                };
            }
        }
    }
}

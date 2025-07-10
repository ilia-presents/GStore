using GStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GStore.Models
{
    public class Level1Set
    {

        // Those are my product categories. Level1 means top level category
        public int Id { get; set; }
        [StringLength(42)]
        public string Name { get; set; } = "";
        public bool IsActive { get; set; }
        public virtual IEnumerable<Level2Set> Level2Sets { get; set; }

        public static void ToggleActivityStatus(Level1Set l1Set)
        {
            l1Set.IsActive = !l1Set.IsActive;
        }

        public static L1SetStutusChangeVM SetStatusChange(L1SetVM l1SetVM)
        {
            L1SetStutusChangeVM statusChangeVM = new L1SetStutusChangeVM();

            statusChangeVM.l1SetVM = l1SetVM;

            if (l1SetVM.IsActive)
            {
                statusChangeVM.TitleText = "Деактивиране на Категория Ниво 1";
                statusChangeVM.WarningText = "Сигурен ли сте, че искате да деактивирате тази Категория Ниво 1 ?";
                statusChangeVM.ButtonText = "Деактивирай";
            }
            else
            {
                statusChangeVM.TitleText = "Активиране на Категория Ниво 1";
                statusChangeVM.WarningText = "";
                statusChangeVM.ButtonText = "Активирай";
            }


            return statusChangeVM;
        }

        public static Level1Set MapVmToEntity(L1SetVM l1SetVM)
        {
            Level1Set l1Set = new Level1Set()
            {
                Id = l1SetVM.Id,
                Name = l1SetVM.Name,
                IsActive = l1SetVM.IsActive
            };

            return l1Set;
        }

        public static void MapEntityForEdit(Level1Set l1Set, L1SetVM l1SetVM)
        {
            l1Set.Name = l1SetVM.Name;
        }

        public static L1SetVM MapEntityToVm(Level1Set l1Set)
        {
            L1SetVM l1SetVM = new L1SetVM()
            {
                Id = l1Set.Id,
                Name = l1Set.Name,
                IsActive = l1Set.IsActive
            };

            return l1SetVM;
        }

        public static Expression<Func<Level1Set, L1SetVM>> LevelSetToVmSelector
        {
            get
            {
                return l1Set => new L1SetVM()
                {
                    Id = l1Set.Id,
                    Name = l1Set.Name,
                    IsActive = l1Set.IsActive
                };
            }
        }
    }
}

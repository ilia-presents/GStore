using GStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GStore.Models
{
    public class Level2Set
    {
        public int Id { get; set; }

        [StringLength(42)]
        public string Name { get; set; } = "";
        public int? Level1SetId { get; set; }
        public bool IsActive { get; set; }
        public virtual Level1Set Level1Set { get; set; }

        public virtual IEnumerable<Shirt> Shirts { get; set; }

        public static void ToggleActivityStatus(Level2Set l2Set)
        {
            l2Set.IsActive = !l2Set.IsActive;
        }
        public static L2SetStutusChangeVM SetStatusChange(Level2Set l2Set)
        {

            L2SetStutusChangeVM statusChangeVM = new L2SetStutusChangeVM()
            {
                Id = l2Set.Id,
                Name = l2Set.Name,
                IsActive = l2Set.IsActive,
                L1Name = l2Set.Level1Set.Name
            };

            statusChangeVM.TextSetter = new StatusChangeTextSetter();

            if (l2Set.IsActive)

                statusChangeVM.TextSetter.SetOnActive("Категория 2 Ниво", "категория ?");

            else

                statusChangeVM.TextSetter.SetOnDeactivated("Категория 2 Ниво");

            return statusChangeVM;
        }

        public static void MapVMToEntityForEditPost(Level2Set l2Set, L2SetVM l2SetVM)
        {
            l2Set.Name = l2SetVM.Name;

            int tempInt = 0;

            bool resultFromParse = int.TryParse(l2SetVM.SelectedRadioValue, out tempInt);

            if (resultFromParse == true)

                l2Set.Level1SetId = tempInt;

            else

                l2Set.Level1SetId = null;
        }

        public static L2SetVM MapEntityToVm(Level2Set l2Set)
        {
            L2SetVM l2SetVM = new L2SetVM()
            {
                Id = l2Set.Id,
                Name = l2Set.Name,
                IsActive = l2Set.IsActive,
                L1Name= l2Set.Level1Set.Name
            };

            if (l2Set.Level1SetId == null)
                l2SetVM.SelectedRadioValue = null;

            else
                l2SetVM.SelectedRadioValue = l2Set.Level1SetId.ToString();


            return l2SetVM;
        }

        public static Level2Set MapVmToEntity(L2SetVM l2SetVM)
        {       
            Level2Set l2Set = new Level2Set()
            {
                Id = l2SetVM.Id,
                Name = l2SetVM.Name,
                IsActive = l2SetVM.IsActive
            };

            int tempInt = 0;

            bool resultFromParse = int.TryParse(l2SetVM.SelectedRadioValue, out tempInt);

            if (resultFromParse == true) 

                l2Set.Level1SetId = tempInt;

            else 

                l2Set.Level1SetId = null;

            return l2Set;
        }
    }
}

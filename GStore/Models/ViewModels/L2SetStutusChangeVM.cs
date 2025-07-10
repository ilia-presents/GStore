namespace GStore.Models.ViewModels
{
    public class L2SetStutusChangeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string L1Name { get; set; } = "";
        public bool IsActive { get; set; }

        public StatusChangeTextSetter TextSetter { get; set; }
    }
}

namespace GStore.Models
{
    // This class is used only because DbSets can not have repeating entities
    public class spShirtShortWithCategoryNameById : spShirtPreviewModel
    {
        public string CategoryName { get; set; }
    }
}

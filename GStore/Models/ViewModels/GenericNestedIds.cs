namespace GStore.Models.ViewModels
{
    public class GenericNestedIds
    {
        public int MainId { get; set; }

        public IEnumerable<int> NestdId { get; set; }

        public virtual IEnumerable<SizeSet> SizeSets { get; set; }
    }
}

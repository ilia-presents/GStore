namespace GStore.Models.OtherModels
{
    public class ImageAssistMain
    {
        public string ImageDbName { get; set; }

        public bool IsNewImage { get; set; } = false;

        public List<ImageAssist> ImageAssistList { get; set; }
    }
}

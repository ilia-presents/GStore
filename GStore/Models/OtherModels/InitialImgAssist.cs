using System.Drawing;

namespace GStore.Models.OtherModels
{
    public class InitialImgAssist
    {
        public bool IsImageOk { get; set; }

        public string ErrorMessage { get; set; } = "";

        public string? ImageExtension { get; set; }

        public Bitmap? InitialBmp { get; set; } = null;

    }
}

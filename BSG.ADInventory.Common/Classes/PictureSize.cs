namespace BSG.ADInventory.Common.Classes
{
    public class PictureSize
    {
        public PictureSize()
        {
            this.Watermark = false;
        }
        public int Width { get; set; }
        public int Height { get; set; }

        public string Prefix { get; set; }

        public bool Watermark { get; set; }


    }
}

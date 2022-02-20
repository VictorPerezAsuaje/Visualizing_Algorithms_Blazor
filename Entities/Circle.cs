namespace PoC_VisualizingAlgorithms.Entities
{
    public class Circle
    {
        public int Value { get; set; }
        public int Order { get; set; } = 0;
        public int Hue { get; set; }
        public string CssClass { get; set; } = "";

        public void GenerateHueByOrder(int numberOfItems)
            => this.Hue = this.Order * 180 / numberOfItems;

        public void GenerateHueByValue()
            => this.Hue = Convert.ToInt32(this.Value * 1.8);
    }
}

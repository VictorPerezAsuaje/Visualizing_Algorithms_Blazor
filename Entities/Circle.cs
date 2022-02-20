namespace PoC_VisualizingAlgorithms.Entities
{
    public class Circle
    {
        public int Value { get; set; }
        public int Order { get; set; } = 0;
        public int hue { get; set; }

        public void GenerateHue(int numberOfItems)
            => this.hue = Convert.ToInt32(Value * 1.8);
    }
}

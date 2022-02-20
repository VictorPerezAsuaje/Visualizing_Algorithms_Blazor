using PoC_VisualizingAlgorithms.Entities;

namespace PoC_VisualizingAlgorithms.Pages
{
    public partial class SelectionSort
    {
        private static int numberOfItems { get; set; } = 5;
        private Circle[] circles = new Circle[numberOfItems];
        private Config algorithmConfig = new Config();

        class Config
        {
            public AlgorithmState State { get; set;} = AlgorithmState.Ready;
            public int taskDelay { get; set; } = 1;
            public Stats stats { get; set; } = new Stats();
        }

        class Stats
        {
            public int iterationNumber { get; set; } = 0;
            public int totalIterations { get; private set; } = 0;
            public int swapsMade { get; set; } = 0;
            public int swapRatio { get; private set; } = 0;
            public int LessThan { get; set; } = 0;
            public int EqualTo { get; set; } = 0;
            public int GreaterThan { get; set; } = 0;

            public void CalculateSwapRatio()
                => this.swapRatio = (this.swapsMade * 100) / this.iterationNumber;


            public void CalculateTotalIterations(int numberOfItems)
            {
                while(numberOfItems > 0)
                {
                    this.totalIterations += numberOfItems;
                    numberOfItems--;
                }
            }
        }

        private void GenerateRandomValues()
        {
            if (numberOfItems < 2 || numberOfItems > 100) numberOfItems = 5;
            circles = new Circle[numberOfItems];

            for (int i = 0; i < numberOfItems; i++)
            {
                Random random = new Random();
                circles[i] = new Circle()
                {
                    Value = random.Next(1, 100),
                    Order = i + 1,
                };

                circles[i].GenerateHue(numberOfItems);
            }

            algorithmConfig.State = AlgorithmState.Ready;
            algorithmConfig.stats = new Stats();
            algorithmConfig.stats.CalculateTotalIterations(numberOfItems - 1);
            StateHasChanged();
        }

        private void SwapStateValues(Circle swaper, Circle swaped)
        {
            swaper.Value = swaped.Value;
            swaper.hue = swaped.hue;
        }

        private async Task ConventionalSelectionSort()
        {
            Circle intermediarySquare = new Circle();
            algorithmConfig.State = AlgorithmState.Ongoing;
            for (int i = 1; i < circles.Count(); i++)
            {
                for (int j = i; j < circles.Count(); j++)
                {
                    await Task.Delay(algorithmConfig.taskDelay);

                    if (circles[i - 1].Value == circles[j].Value)
                    {
                        algorithmConfig.stats.iterationNumber++;
                        algorithmConfig.stats.EqualTo++;
                        StateHasChanged();
                        continue;
                    }

                    if (circles[i - 1].Value > circles[j].Value)
                    {
                        algorithmConfig.stats.GreaterThan++;
                        algorithmConfig.stats.iterationNumber++;
                        algorithmConfig.stats.CalculateSwapRatio();
                        StateHasChanged();
                        continue;
                    }

                    SwapStateValues(intermediarySquare, circles[i - 1]);
                    SwapStateValues(circles[i - 1], circles[j]);
                    SwapStateValues(circles[j], intermediarySquare);
                    algorithmConfig.stats.swapsMade++;
                    algorithmConfig.stats.LessThan++;
                    algorithmConfig.stats.iterationNumber++;
                    algorithmConfig.stats.CalculateSwapRatio();
                    StateHasChanged();
                }
            }

            algorithmConfig.State = AlgorithmState.Finished;
            StateHasChanged();
        }
    }
}

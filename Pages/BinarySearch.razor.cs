using PoC_VisualizingAlgorithms.Entities;

namespace PoC_VisualizingAlgorithms.Pages
{
    public partial class BinarySearch
    {
        private Circle[] circles = new Circle[100];
        private Config algorithmConfig = new Config();
        private int currentGuess = 50;
        private int minGuess = 1;
        private int maxGuess = 100;
        private bool isSuccess = false;

        enum Answer
        {
            Lower,
            Higher,
            Correct
        }

        class Config
        {
            public AlgorithmState State { get; set; } = AlgorithmState.Ready;
            public int taskDelay { get; set; } = 1;
            public Stats stats { get; set; } = new Stats();
        }

        class Stats
        {
            public int guessCount { get; set; } = 0;
            public int guessProbability { get; private set; } = 0;
            public void CalculateGuessProbability() 
                => guessProbability = guessCount * 100 / 7;
        }

        private void GenerateCircles()
        {
            circles = new Circle[100];

            for (int i = 0; i < circles.Count(); i++)
            {
                circles[i] = new Circle()
                {
                    Value = i+1,
                    Order = i+1,
                };

                circles[i].GenerateHueByValue();
            }

            algorithmConfig.State = AlgorithmState.Ready;
            algorithmConfig.stats = new Stats();
            StateHasChanged();
        }

        private void CheckAnswer(Answer answer)
        {
            switch (answer)
            {
                case Answer.Correct:
                    isSuccess = true;
                    break;
                case Answer.Higher:
                    minGuess = currentGuess;
                    currentGuess = (minGuess + maxGuess) / 2;
                    algorithmConfig.stats.guessCount++;
                    break;
                case Answer.Lower:
                    maxGuess = currentGuess;
                    currentGuess = (minGuess + maxGuess) / 2;
                    algorithmConfig.stats.guessCount++;
                    break;
            }

            foreach (Circle circle in circles)
            {
                Task.Delay(algorithmConfig.taskDelay);
                if (circle.Value < minGuess || circle.Value > maxGuess)
                {
                    circle.CssClass = "discarted";
                    StateHasChanged();
                    continue;
                }
            }

            algorithmConfig.stats.CalculateGuessProbability();
        }
    }
}

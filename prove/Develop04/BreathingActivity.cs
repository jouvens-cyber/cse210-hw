public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by breathing in and out slowly."
        )
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.Write("Breathe in...");
            ShowCountdown(4);

            Console.WriteLine();
            Console.Write("Breathe out...");
            ShowCountdown(6);

            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
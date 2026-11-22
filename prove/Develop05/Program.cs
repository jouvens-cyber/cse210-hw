// Exceeding requirements:
// I added a level system to the program.
// The player gains a level for every 500 points.
// The program also tells the player when they level up.

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}
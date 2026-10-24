using System;

// Exceeding requirements:
// I made the program only choose words that are still visible when
// hiding random words. This prevents it from choosing words that
// have already been hidden, so new words disappear each round.

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);

        string text =
            "Trust in the Lord with all thine heart and lean not unto thine own understanding. "
            + "In all thy ways acknowledge him, and he shall direct thy paths.";

        Scripture scripture = new Scripture(reference, text);

        string input = "";

        while (input.ToLower() != "quit" && !scripture.IsCompletelyHidden())
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            Console.Write("Press Enter to continue or type 'quit' to finish: ");
            input = Console.ReadLine() ?? "";

            if (input.ToLower() != "quit")
            {
                scripture.HideRandomWords(3);
            }
        }

        if (scripture.IsCompletelyHidden())
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("All words are hidden!");
        }
    }
}

// Exceeding requirements:
// I added a mood feature that asks the user how they are feeling.
// The mood is displayed with each entry and is also saved and loaded from the file.


using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                string prompt = promptGenerator.GetRandomPrompt();

                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine() ?? "";
                Console.Write("How are you feeling today? ");
                string mood = Console.ReadLine() ?? "";

                DateTime currentTime = DateTime.Now;
                string date = currentTime.ToShortDateString();

                Entry newEntry = new Entry();
                newEntry._date = date;
                newEntry._promptText = prompt;
                newEntry._entryText = response;
                newEntry._mood = mood;

                journal.AddEntry(newEntry);
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine() ?? "";

                journal.LoadFromFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine() ?? "";

                journal.SaveToFile(filename);
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose a number from 1 to 5.");
            }

            Console.WriteLine();
        }
    }
}
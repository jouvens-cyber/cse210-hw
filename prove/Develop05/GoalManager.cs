using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        string choice = "";

        while (choice != "6")
        {
            Console.WriteLine();
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                CreateGoal();
            }
            else if (choice == "2")
            {
                ListGoalDetails();
            }
            else if (choice == "3")
            {
                SaveGoals();
            }
            else if (choice == "4")
            {
                LoadGoals();
            }
            else if (choice == "5")
            {
                RecordEvent();
            }
            else if (choice == "6")
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose a number from 1 to 6.");
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level: {GetLevel()}");
    }

    public int GetLevel()
    {
        return (_score / 500) + 1;
    }

    public void CreateGoal()
    {
        Console.WriteLine();
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine() ?? "";

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? "";

        Console.Write("How many points is this goal worth? ");
        int points = ReadNumber();

        if (type == "1")
        {
            SimpleGoal goal = new SimpleGoal(name, description, points);
            _goals.Add(goal);
        }
        else if (type == "2")
        {
            EternalGoal goal = new EternalGoal(name, description, points);
            _goals.Add(goal);
        }
        else if (type == "3")
        {
            Console.Write("How many times does this goal need to be completed? ");
            int target = ReadNumber();

            Console.Write("What is the bonus for completing it? ");
            int bonus = ReadNumber();

            ChecklistGoal goal = new ChecklistGoal(
                name,
                description,
                points,
                target,
                bonus
            );

            _goals.Add(goal);
        }
        else
        {
            Console.WriteLine("That goal type was not valid.");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine();
        Console.WriteLine("The goals are:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals to record.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("The goals are:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }

        Console.Write("Which goal did you accomplish? ");
        int goalNumber = ReadNumber() - 1;

        if (goalNumber < 0 || goalNumber >= _goals.Count)
        {
            Console.WriteLine("That goal does not exist.");
            return;
        }

        int oldLevel = GetLevel();

        int pointsEarned = _goals[goalNumber].RecordEvent();
        _score += pointsEarned;

        if (pointsEarned == 0)
        {
            Console.WriteLine("That goal is already complete.");
        }
        else
        {
            Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");
            Console.WriteLine($"You now have {_score} points.");
        }

        int newLevel = GetLevel();

        if (newLevel > oldLevel)
        {
            Console.WriteLine($"You leveled up! You are now Level {newLevel}!");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "";

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "";

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file was not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        _goals.Clear();
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            int colonPosition = line.IndexOf(':');

            if (colonPosition == -1)
            {
                continue;
            }

            string goalType = line.Substring(0, colonPosition);
            string goalData = line.Substring(colonPosition + 1);

            string[] parts = goalData.Split('|');

            if (goalType == "SimpleGoal")
            {
                string name = parts[0];
                string description = parts[1];
                int points = int.Parse(parts[2]);
                bool isComplete = bool.Parse(parts[3]);

                SimpleGoal goal = new SimpleGoal(
                    name,
                    description,
                    points,
                    isComplete
                );

                _goals.Add(goal);
            }
            else if (goalType == "EternalGoal")
            {
                string name = parts[0];
                string description = parts[1];
                int points = int.Parse(parts[2]);

                EternalGoal goal = new EternalGoal(
                    name,
                    description,
                    points
                );

                _goals.Add(goal);
            }
            else if (goalType == "ChecklistGoal")
            {
                string name = parts[0];
                string description = parts[1];
                int points = int.Parse(parts[2]);
                int bonus = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int amountCompleted = int.Parse(parts[5]);

                ChecklistGoal goal = new ChecklistGoal(
                    name,
                    description,
                    points,
                    target,
                    bonus,
                    amountCompleted
                );

                _goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded.");
    }

    private int ReadNumber()
    {
        int number;

        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Please enter a number: ");
        }

        return number;
    }
}
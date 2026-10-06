using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> _goals = new List<Goal>();
    static int _score = 0;

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            DisplayScore();

            Console.WriteLine();
            Console.WriteLine("Menu Options");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    ListGoals();
                    break;

                case "3":
                    SaveGoals();
                    break;

                case "4":
                    LoadGoals();
                    break;

                case "5":
                    RecordEvent();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Pause();
                    break;
            }
        }
    }

    static void DisplayScore()
    {
        Console.WriteLine($"You have {_score} points.");

        int level = GetLevel();

        Console.WriteLine($"Level: {level}");

        if (level >= 5)
        {
            Console.WriteLine("Amazing! You are becoming an Eternal Quest Master!");
        }
    }

    static int GetLevel()
    {
        return (_score / 500) + 1;
    }

    static void CreateGoal()
    {
        Console.Clear();

        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");

        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

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
            int target = int.Parse(Console.ReadLine());

            Console.Write("What is the bonus for completing the goal? ");
            int bonus = int.Parse(Console.ReadLine());

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
            Console.WriteLine("Invalid goal type.");
            Pause();
            return;
        }

        Console.WriteLine("Goal created successfully!");
        Pause();
    }

    static void ListGoals()
    {
        Console.Clear();

        Console.WriteLine("The goals are:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet.");
        }
        else
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        Pause();
    }

    static void RecordEvent()
    {
        Console.Clear();

        Console.WriteLine("The goals are:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals to record.");
            Pause();
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        Console.Write("Which goal did you accomplish? ");

        int choice = int.Parse(Console.ReadLine());

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            Pause();
            return;
        }

        Goal selectedGoal = _goals[choice - 1];

        int pointsEarned = selectedGoal.RecordEvent();

        _score += pointsEarned;

        Console.WriteLine();
        Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");

        if (selectedGoal.IsComplete())
        {
            Console.WriteLine("You completed this goal!");
        }

        Console.WriteLine($"Your new score is {_score}.");

        Pause();
    }

    static void SaveGoals()
    {
        Console.Write("Enter the filename to save: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
        Pause();
    }

    static void LoadGoals()
    {
        Console.Write("Enter the filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            Pause();
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            Pause();
            return;
        }

        _goals.Clear();

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            string goalType = parts[0];
            string name = parts[1];
            string description = parts[2];
            int points = int.Parse(parts[3]);

            if (goalType == "SimpleGoal")
            {
                bool isComplete = bool.Parse(parts[4]);

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
                int timesCompleted = int.Parse(parts[4]);

                EternalGoal goal = new EternalGoal(
                    name,
                    description,
                    points,
                    timesCompleted
                );

                _goals.Add(goal);
            }
            else if (goalType == "ChecklistGoal")
            {
                int target = int.Parse(parts[4]);
                int bonus = int.Parse(parts[5]);
                int amountCompleted = int.Parse(parts[6]);

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

        Console.WriteLine("Goals loaded successfully!");
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}

/*
 * CREATIVITY / EXCEEDING REQUIREMENTS:
 *
 * I added a level-up system to make the Eternal Quest more fun.
 * Every 500 points the user earns, their level increases.
 * The program also displays a special message when the user reaches
 * Level 5, encouraging them to continue their quest.
 *
 * This gives the user another reward besides simply increasing their score.
 */

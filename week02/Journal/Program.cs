using System;

class Program
{
    static void Main(string[] args)
    {
        // Creativity / Exceeding Requirements:
        // I added extra prompts and made the program display friendly
        // messages when the journal is empty or when a file is saved/loaded.

        Journal journal = new Journal();

        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is one thing I learned today?",
            "What is something I am grateful for today?"
        };

        Random random = new Random();

        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            if (choice == "1")
            {
                string prompt = prompts[random.Next(prompts.Count)];

                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();

                string date = DateTime.Now.ToShortDateString();

                Entry newEntry = new Entry(date, prompt, response);

                journal.AddEntry(newEntry);

                Console.WriteLine("Entry added successfully.");
                Console.WriteLine();
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("Enter the filename: ");
                string filename = Console.ReadLine();

                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter the filename: ");
                string filename = Console.ReadLine();

                try
                {
                    journal.LoadFromFile(filename);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("The file could not be found.");
                    Console.WriteLine();
                }
            }
            else if (choice == "5")
            {
                running = false;
                Console.WriteLine("Thank you for using the Journal program!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1-5.");
                Console.WriteLine();
            }
        }
    }
}
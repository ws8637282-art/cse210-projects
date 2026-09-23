using System;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // EXTRA FEATURE:
            // This program exceeds the core requirements by hiding
            // only words that have not already been hidden.
            // This prevents the program from randomly selecting
            // the same hidden word again.

            Reference reference = new Reference("Proverbs", 3, 5, 6);

            string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding";

            Scripture scripture = new Scripture(reference, text);

            while (!scripture.AllWordsHidden())
            {
                Console.Clear();

                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();
                Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");

                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWords(3);
            }

            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
        }
    }
}

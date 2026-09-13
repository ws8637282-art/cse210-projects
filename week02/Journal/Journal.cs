using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
            Console.WriteLine();
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.GetFileLine());
            }
        }

        Console.WriteLine("Journal saved successfully.");
        Console.WriteLine();
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');

            if (parts.Length >= 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];

                Entry entry = new Entry(date, prompt, response);
                _entries.Add(entry);
            }
        }

        Console.WriteLine("Journal loaded successfully.");
        Console.WriteLine();
    }
}
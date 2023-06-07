using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Absence : AbsenceSystem
{
    public Absence(string Path) : base(Path) { }

    public override void WriteText()
    {
        int? absences = null;

        Console.Write("Input absence count: ");
        string abscense_count = Console.ReadLine();
        Console.Write("\n\nIf student has 0 abscenses keep empty input");

        if (!string.IsNullOrEmpty(abscense_count))
        {
            absences = Convert.ToInt32(abscense_count);
        }

        int max_increment = 0;
        int auto_increment = 0;
        string last_line = null;
        string lineWithMaxIncrement = null;

        if (File.Exists(Path))
        {
            string[] lines = File.ReadAllLines(Path);

            using (StreamReader r = new StreamReader(Path))
            {
                while (!r.EndOfStream)
                {
                    last_line = r.ReadLine();
                }
            }

            if (string.IsNullOrEmpty(last_line))
            {
                auto_increment = 1;
            }
            else
            {
                try
                {
                    foreach (string line in lines)
                    {
                        auto_increment = Convert.ToInt32(line.Split(". ")[0]);

                        if (auto_increment > max_increment)
                        {
                            max_increment = auto_increment;
                            lineWithMaxIncrement = line;
                        }
                    }
                    if (lineWithMaxIncrement != null)
                    {
                        auto_increment = max_increment + 1;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Auto increment set to 1 by default.");
                    auto_increment = 1;
                }
            }
            using (StreamWriter w = File.AppendText(Path))
            {
                w.WriteLineAsync($"{auto_increment++}. {absences ?? 0}");
            }
        }
        else { Console.WriteLine("\nError! This file doesn't exist"); }
    }

    public override void Sorting()
    {
        Dictionary<int, int> savedLines = new Dictionary<int, int>();

        Console.Write("\n[1] Sort abences from lower to higher\n");
        Console.Write("[2] Sort abences from higher to lower\n");
        ConsoleKeyInfo settings = Console.ReadKey();

        switch (settings.KeyChar)
        {
            case '1':
                using (StreamReader reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        string lineTxt = reader.ReadLine();
                        string increment;

                        increment = lineTxt.Split(".")[0];
                        string numbers = lineTxt.Split(". ")[1];
                        savedLines.Add(int.Parse(increment), int.Parse(numbers));
                    }
                    using (StreamWriter writer = new StreamWriter(Path))
                    {
                        foreach (KeyValuePair<int, int> line in savedLines.OrderBy(key => key.Value))
                        {
                            writer.WriteLine(line.Key + ". " + line.Value);
                        }
                    }
                }
                Console.Clear();
                break;

            case '2':
                using (StreamReader reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        string lineTxt = reader.ReadLine();
                        string increment;

                        increment = lineTxt.Split(".")[0];
                        string numbers = lineTxt.Split(". ")[1];
                        savedLines.Add(int.Parse(increment), int.Parse(numbers));
                    }
                    using (StreamWriter writer = new StreamWriter(Path))
                    {
                        foreach (KeyValuePair<int, int> line in savedLines.OrderByDescending(x => x.Value))
                        {
                            writer.WriteLine(line.Key + ". " + line.Value);
                        }
                    }
                    Console.Clear();
                    break;
                }
        }
    }

    public override void ReadFile()
    {
        base.ReadFile();
        Console.WriteLine("\n\nPress any key to continue.");
    }

    public override void DeleteText()
    {
        Console.WriteLine("[1] Delete all text\n[2] Delete specific\n[3] Back");
        ConsoleKeyInfo key = Console.ReadKey();

        switch (key.KeyChar)
        {
            case '1':
                base.DeleteText();
                break;
            case '2':
                base.DeleteSpecific();
                break;
            case '3':
                Program.FileChoice();
                break;
            default:
                Console.WriteLine("Wrong input, try again!\n");
                DeleteText();
                break;
        }
    }

    public override void Filter()
    {
        base.Filter();
    }

    public static int Summary()
    {
        Dictionary<int, int> savedLines = new Dictionary<int, int>();

        using (StreamReader reader = new StreamReader("Absence.txt"))
        {
            while (!reader.EndOfStream)
            {
                string lineTxt = reader.ReadLine();
                string increment;

                increment = lineTxt.Split(".")[0];
                string numbers = lineTxt.Split(". ")[1];
                
                savedLines.Add(int.Parse(increment), int.Parse(numbers));
            }
        }
        return savedLines.Sum(x => x.Value);
    }
}

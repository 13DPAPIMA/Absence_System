using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

class Name : AbsenceSystem
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Name(string Path) : base(Path) { }

    public void Input()
    {
        Regex regex = new Regex(@"^[a-zA-Z\-]+$");
    
        Console.Write("If you have double name use " + "-\n");
        
        do
        {
            Console.Write("\nYou can only input letters and hyphens!\n");
            Console.WriteLine("\nInput students firstname");
             
            FirstName = Console.ReadLine().Trim();
        } while (!regex.IsMatch(FirstName));

        do
        {
            Console.Write("\nYou can only input letters and hyphens!\n");
            Console.WriteLine("\nInput students lastname");
            LastName = Console.ReadLine().Trim();
        } while (!regex.IsMatch(LastName));

    }

    public override void WriteText()
    {
        int max_increment = 0;
        int auto_increment = 0;
        string last_line = null;
        string lineWithMaxIncrement = null;

        Input();

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
                w.WriteLineAsync($"{auto_increment++}. {FirstName} {LastName}");
            }
        }
        else { Console.WriteLine("\nError! This file doesn't exist"); }
    }

    public override void ReadFile()
    {
        base.ReadFile();
    }

    public override void DeleteText()
    {
        base.DeleteText();
    }

    public override void Filter()
    {
        base.Filter();
    }

    public override void Sorting()
    {
        Console.Clear();
        Console.WriteLine("Choose option: \n");
        Console.WriteLine("[1] Sort from A to Z");
        Console.WriteLine("[2] Sort from Z to A");
        ConsoleKeyInfo option = Console.ReadKey();
        Console.Clear();

        switch (option.KeyChar)
        {
            case '1':
                List<string> lines = new List<string>(File.ReadAllLines(Path));
                Dictionary<int, string> data = new Dictionary<int, string>();

                foreach (string line in lines)
                {
                    int incrementEndIndex = line.IndexOf(".");
                    int increment = int.Parse(line.Substring(0, incrementEndIndex));

                    string nameSurname = line.Substring(incrementEndIndex + 2);
                    data.Add(increment, nameSurname);
                }

                List<string> sortedLines = new List<string>();

                foreach (KeyValuePair<int, string> entry in data.OrderBy(x => x.Value))
                {
                    sortedLines.Add($"{entry.Key}. {entry.Value}");
                }

                File.WriteAllLines(Path, sortedLines);
                Console.WriteLine("Sorted!");
                break;

            case '2':
                lines = new List<string>(File.ReadAllLines(Path));
                data = new Dictionary<int, string>();

                foreach (string line in lines)
                {
                    int incrementEndIndex = line.IndexOf(".");
                    int increment = int.Parse(line.Substring(0, incrementEndIndex));

                    string nameSurname = line.Substring(incrementEndIndex + 2);
                    data.Add(increment, nameSurname);
                }

                sortedLines = new List<string>();

                foreach (KeyValuePair<int, string> entry in data.OrderByDescending(x => x.Value))
                {
                    sortedLines.Add($"{entry.Key}. {entry.Value}");
                }

                File.WriteAllLines(Path, sortedLines);
                Console.WriteLine("Sorted!");
                break;

        }
    }


}

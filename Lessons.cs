using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class Lessons : AbsenceSystem
{
    public Lessons(string Path) : base(Path) { }
    public string Lesson { get; set; }

    public void LessonInput()
    {
        Console.Clear();

        Console.WriteLine("Choose lesson that student skipped\n");

        Console.WriteLine("\n[1] Math");
        Console.WriteLine("[2] English");
        Console.WriteLine("[3] Latvian");
        Console.WriteLine("[4] Sport\n");
        Console.WriteLine("\n\n If you want to exit press 'Enter'");

        ConsoleKeyInfo settings = Console.ReadKey();

        switch (settings.KeyChar)
        {
            case '1':
                Lesson = "Math";
                break;
            case '2':
                Lesson = "English";
                break;
            case '3':
                Lesson = "Latvian";
                break;
            case '4':
                Lesson = "Sport";
                break;
        }
    }

    public override void WriteText()
    {
        Console.Clear();

        int max_increment = 0;
        int auto_increment = 0;
        string last_line = null;
        string lineWithMaxIncrement = null;

        LessonInput();


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
                w.WriteLineAsync($"{auto_increment++}. {Lesson}");
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
                break;
        }

    }
}

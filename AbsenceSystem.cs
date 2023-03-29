using System;
using System.IO;
using System.Collections.Generic;

public class AbsenceSystem
{
    #region Var
    internal string Path { get; set; }
    #endregion

    public AbsenceSystem(string Path) { this.Path = Path; }

    #region Methods

    /// <summary>
    /// Read all text from file
    /// </summary>
    public virtual void ReadFile()
    {
        Console.Clear();


        if (File.Exists(Path))
        {
            string newString = File.ReadAllText(Path);

            if (string.IsNullOrEmpty(newString))
            {
                Console.Write("\nFile is empty.\n\n");
            }
            else
            {
                Console.WriteLine(newString);
            }
        }
        else { Console.WriteLine("\nFile does not exist"); }
    }

    /// <summary>
    /// Append new data to .txt file. Inputs Firstname and Lastname of students
    /// </summary>
    public virtual void WriteText()
    {
        if (File.Exists(Path))
        {
            Console.WriteLine("\nWrite text in file: ");
            string newString = Console.ReadLine();
            using (StreamWriter w = File.AppendText(Path))
            {
                w.WriteLineAsync($"{newString}");
            }
        }
        else { Console.WriteLine("\nFile does not exist"); }
    }

    /// <summary>
    ///  Delets all the data from file
    /// </summary>
    public virtual void DeleteText()
    {
        Console.Write("\nPress key " + "'Enter'" + " to confirm the action.");
        Console.Write("\n\nIf you want to abort action press any other key");

        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            File.WriteAllText(Path, string.Empty);
            Console.WriteLine("\n\nAction completed!\n");

        }
        if (Console.ReadKey().Key != ConsoleKey.Enter)
        {
            Console.WriteLine("\n\nAction cancelled!\n");
        }
    }

    /// <summary>
    ///  Filter and Searcher with user interface 
    /// </summary>
    public virtual void Filter()
    {
        int interval = 0;
        
        List<string> linesToKeep = new List<string>();

        #region Input
        Console.WriteLine("[1] Search specific");
        Console.WriteLine("[2] Filter specific");
        Console.WriteLine("[3] Delete specific\n");

        ConsoleKeyInfo type = Console.ReadKey();
        Console.Clear();

        if (type.KeyChar == '3')
        {
            Console.WriteLine("[1] All absences equal to 0 (perfect)");
            Console.WriteLine("[2] All absences higher than 0 (bad)");
            Console.WriteLine("[3] All absences higher than 10 (super bad)");
            Console.WriteLine("[4] All absences higher than 20 (critical)");
        }
        ConsoleKeyInfo settings = Console.ReadKey();
        Console.Clear();
        #endregion
        
        #region Settings
        switch (settings.KeyChar)
        {
            case '1':
                interval = 0;
                break; 
            case '2':
                interval = 0;
                break;
            case '3':
                interval = 10;
                break;
            case '4':
                interval = 20;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong option!");
                break;
        }
        
        #endregion

        #region Type
        switch (type.KeyChar)
        {
            case '1':
                using (StreamReader reader1 = new StreamReader(Path))
                {
                    Console.Write("Enter searching key: ");
                    string searchParam = Console.ReadLine();
                    while (!reader1.EndOfStream)
                    {
                        string line = reader1.ReadLine();
                        string[] parts = line.Split(". ");
    
                        if (parts.Length > 0)
                        {
                            if (parts[1].Contains(searchParam))
                            {
                                linesToKeep.Add(line);
                            }
                        }
                    }
                }
                foreach (string line in linesToKeep)
                {
                    Console.WriteLine(line);
                }
                break;
            case '2':
                using (StreamReader reader1 = new StreamReader(Path))
                {
                    Console.Write("Enter searching key: ");
                    string searchParam = Console.ReadLine();
                    while (!reader1.EndOfStream)
                    {
                        string line = reader1.ReadLine();
                        string[] parts = line.Split(". ");
    
                        if (parts.Length > 0)
                        {
                            if (parts[1].Contains(searchParam))
                            {
                                linesToKeep.Add(line);
                            }
                        }
                    }
                }
                using (StreamWriter writer = new StreamWriter(Path))
                {
                    foreach (string line in linesToKeep)
                    {
                        writer.WriteLine(line);
                    }
                }
                break;
            case '3':
                using (StreamReader reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(". ");

                        if (parts.Length > -1 && int.TryParse(parts[1], out int value))
                        {
                            // all equal to 0
                            
                            if (settings.KeyChar == '1') 
                            {
                                if (value > interval)
                                {
                                    linesToKeep.Add(line);
                                }
                            }
                            else
                            {
                                if (value <= interval)
                                {
                                    linesToKeep.Add(line);
                                }
                            }
                            
                        }
                    }
                }

                using (StreamWriter writer = new StreamWriter(Path))
                {
                    foreach (string line in linesToKeep)
                    {
                        writer.WriteLine(line);
                    }
                }
                break;
            default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong option!");
                    break;
        }
        #endregion
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AbsenceSystem
{
    internal string Path { get; set; }
    public AbsenceSystem(string Path) { this.Path = Path; }

    /// <summary> Append new data to .txt file. Inputs Firstname and Lastname of students </summary>
    public virtual void WriteText() { }

    /// <summary> Sort data in different ways </summary>
    public virtual void Sorting() { }

    /// <summary> If file is Empty, return false </summary> 
    public bool FileStatus(string Path)
    {
        string[] lines = File.ReadAllLines(Path);
        string last_line = null;
        using (StreamReader r = new StreamReader(Path))
        {
            while (!r.EndOfStream)
            {
                last_line = r.ReadLine();
            }
        }
        if (string.IsNullOrEmpty(last_line))
        {
            return false;
        }
        else { return true; }
    }

    public void Display()
    {
        string absencePath = "Absence.txt";
        string lessonsPath = "Lessons.txt";
        string namePath = "Name.txt";
        
        if (!FileStatus(absencePath) || !FileStatus(absencePath) || !FileStatus(namePath))
        {
            Console.WriteLine("One of the file is empty , please input data and try again!");
            Program.FileChoice();
        }
        
        // Создаем StreamReader для каждого файла
        StreamReader absenceReader = new StreamReader(absencePath);
        StreamReader lessonsReader = new StreamReader(lessonsPath);
        StreamReader nameReader = new StreamReader(namePath);


        // Считываем данные из каждого файла и сохраняем их в словарь
        Dictionary<int, string[]> absenceData = ReadData(absenceReader);
        Dictionary<int, string[]> lessonsData = ReadData(lessonsReader);
        Dictionary<int, string[]> nameData = ReadData(nameReader);

        // Определяем максимальный инкремент
        int maxIncrement = Math.Max(absenceData.Keys.Max(), Math.Max(lessonsData.Keys.Max(), nameData.Keys.Max()));


        Console.WriteLine("\t\t\t\t\t\tTable with data\n\n");

        // Выводим таблицу
        Console.WriteLine("№\t\t\tName\t\t\t\tLesson\t\t\t\tAbsence");

        string[] rows = new string[maxIncrement];
        for (int increment = 1; increment <= maxIncrement; increment++)
        {
            string[] rowData = new string[4];

            if (nameData.TryGetValue(increment, out string[] nameValues))
            {
                rowData[0] = nameValues[1]; // имя
            }
            else
            {
                rowData[0] = "No Data";
            }

            if (lessonsData.TryGetValue(increment, out string[] lessonValues))
            {
                rowData[1] = lessonValues[1]; // урок
            }
            else
            {
                rowData[1] = "-";
            }

            if (absenceData.TryGetValue(increment, out string[] absenceValues))
            {
                rowData[2] = absenceValues[1]; // пропуск
            }
            else
            {
                rowData[2] = "-";
            }

            rows[increment - 1] = $"{increment}\t{rowData[0]}\t{rowData[1]}\t\t{rowData[2]}";
        }

        rows = AlignValues(rows);

        foreach (var row in rows)
        {
            Console.WriteLine(row);
        }

        // Закрываем все StreamReader
        absenceReader.Close();
        lessonsReader.Close();
        nameReader.Close();
    }


    private Dictionary<int, string[]> ReadData(StreamReader reader)
    {
        Dictionary<int, string[]> data = new Dictionary<int, string[]>();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] values = line.Split('.');
            int increment = int.Parse(values[0]);
            data[increment] = values;
        }

        return data;
    }

    private string[] AlignValues(string[] values)
    {
        int numColumns = values[0].Split('\t').Length;
        int[] maxColumnWidths = new int[numColumns];

        // Определяем максимальную ширину для каждого столбца
        for (int i = 0; i < numColumns; i++)
        {
            maxColumnWidths[i] = values.Max(x => x.Split('\t')[i].Length);
        }

        // Выравниваем каждый столбец в каждой строке
        for (int i = 0; i < values.Length; i++)
        {
            string[] columns = values[i].Split('\t');

            for (int j = 0; j < numColumns; j++)
            {
                columns[j] = columns[j].PadRight(maxColumnWidths[j]);
            }

            values[i] = string.Join("\t\t", columns);
        }

        return values;
    }


    /// <summary> Read all text from file </summary>
    public virtual void ReadFile()
    {
        Console.Clear();

        if (File.Exists(Path))
        {
            string newString = File.ReadAllText(Path);

            if (string.IsNullOrEmpty(newString))
            {
                Console.Write("File is empty.\n");
            }
            else
            {
                Console.WriteLine(newString);
                int lineCount = 0;

                using (StreamReader reader = new StreamReader(Path))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
                Console.WriteLine($"Amount of records {lineCount}");


            }


        }
        else { Console.WriteLine("\nFile does not exist"); }
    }

    /// <summary> Delets all the data from file </summary>
    public virtual void DeleteText()
    {
        Console.Write("Press key " + "'Enter'" + " to confirm the action.");
        Console.Write("\n\nIf you want to abort action double-press any other key");

        ConsoleKeyInfo option = Console.ReadKey();

        if (option.Key == ConsoleKey.Enter)
        {
            File.WriteAllText(Path, string.Empty);
            Console.WriteLine("\n\nAction completed!\n");
        }
        else
        {
            Console.WriteLine("\n\nAction cancelled!\n");
        }
    }

    /// <summary> Filter and Searcher with user interface </summary>
    public virtual void Filter()
    {
        List<string> linesToKeep = new List<string>();

        Console.WriteLine("[1] Search specific");
        Console.WriteLine("[2] Filter specific");
        ConsoleKeyInfo type = Console.ReadKey();
        Console.Clear();

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
                    Console.Write("Enter filter key: ");
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
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong option!");
                break;
        }
    }

    /// <summary> Delete data in file by key </summary>
    public virtual void DeleteSpecific()
    {
        List<string> linesToKeep = new List<string>();
        int interval = 0;

        Console.WriteLine("=============== Delete specific ================");
        Console.Clear();

        Console.WriteLine("[1] All absences equal to 0 (perfect)");
        Console.WriteLine("[2] All absences higher than 0 (bad)");
        Console.WriteLine("[3] All absences higher than 10 (super bad)");
        Console.WriteLine("[4] All absences higher than 20 (critical)");

        ConsoleKeyInfo settings = Console.ReadKey();
        Console.Clear();

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

        using (StreamReader reader = new StreamReader(Path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split(". ");

                if (parts.Length > -1 && int.TryParse(parts[1], out int value))
                {
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
    }
}

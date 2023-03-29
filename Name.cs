using System;
using System.IO;
using System.Text.RegularExpressions;

class Name : AbsenceSystem
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Name(string Path) : base(Path) { }

    public void Input()
    {
        Regex regex = new Regex(@"^[a-zA-Z\-]+$");
        // паттерн который разрешен (разрешено только буквы и "-") 
        // паттерн который должен совпадать с входной строкой.
        // я так и до конца не понял как работает этот Regex , но на stackoverflow пишут , что он useless :D

        Console.Write("\nIf you have double name use " + "-\n");
        
        Console.WriteLine("\nInput students firstname: ");
        FirstName = Console.ReadLine();

        if (FirstName.Contains(" "))
        {
            FirstName = FirstName.Replace(" ", ""); // удаляем все пробелы из строки
        }

        if (!regex.IsMatch(FirstName)) // если не совпадает с паттерном то...
        {
            Console.WriteLine("\nYou can only type in letters and hyphens!");
            Environment.Exit(1); // если введено чтото кроме букв и "-" то exit
        }

        Console.WriteLine("\nInput students lastname");
        LastName = Console.ReadLine();

        if (LastName.Contains(" "))
        {
            LastName = LastName.Replace(" ", "");  // удаляем все пробелы из строки
        }

        if (!regex.IsMatch(LastName))
        {
            Console.WriteLine("\nYou can only type in letters and hyphens!");
            Environment.Exit(1);
        }
    }


    public override void WriteText()
    {
        int auto_increment;
        string last_line = null;

        Input();

        if (File.Exists(Path))
        {
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
                auto_increment = int.Parse(last_line.Split('.')[0]) + 1;
            }

            using (StreamWriter w = File.AppendText(Path))
            {
                w.WriteLineAsync($"{auto_increment++}. {FirstName} {LastName}");
            }
        }
        else { Console.WriteLine("Error"); }
    }

    public override void ReadFile()
    {
        base.ReadFile();
    }

    public override void DeleteText()
    {
        base.DeleteText();
    }
}



// int space_index1 = LastName.IndexOf(" ");  // находим индекс первого пробела

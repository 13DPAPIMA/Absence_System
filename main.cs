using System;

public class Program
{
    /// <summary> Logo </summary>
    public static void CapyBara()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        string Capy = @"
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣄⢘⣒⣀⣀⣀⣀⠀⠀⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣽⣿⣛⠛⢛⣿⣿⡿⠟⠂⠀
        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣀⡀⠀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣷⣿⡆⠀
        ⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠁⠀
        ⠀⠀⠀⢀⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀
        ⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀
        ⠀⠀⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⠜⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⠀⢿⣿⣿⣿⣿⠿⠿⣿⣿⡿⢿⣿⣿⠈⣿⣿⣿⡏⣠⡴⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⣠⣿⣿⣿⡿⢁⣴⣶⣄⠀⠀⠉⠉⠉⠀⢻⣿⡿⢰⣿⡇⠀⠀⠀⠀⠀⠀⠀
        ⠀⠀⢿⣿⠟⠋⠀⠈⠛⣿⣿⠀⠀⠀⠀⠀⠀⠸⣿⡇⢸⣿⠀⠀⠀⠀⠀⠀
        ⠀⠀⢸⣿⠀⠀⠀⠀⠀⠘⠿⠆⠀⠀⠀⠀⠀⠀⣿⡇⠀⠿⠇⠀⠀⠀⠀⠀⠀⠀
                     zena
                    artjom
        
            School Absence System
                     2023
        ";
        Console.Write(Capy);
    }

    /// <summary> Console file choice </summary>
    public static void FileChoice()
    {
        Absence absence = new Absence(Path: "Absence.txt");
        Name name = new Name(Path: "Name.txt");
        Lessons lessons = new Lessons(Path: "Lessons.txt");

        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\n================ File select ==================\n");

        Console.WriteLine("[1] Name.txt");
        Console.WriteLine("[2] Lessons.txt");
        Console.WriteLine("[3] Absence.txt");
        Console.WriteLine("\nPlease select a file to work with: \n");

        ConsoleKeyInfo option = Console.ReadKey();
        Console.Clear();

        switch (option.KeyChar)
        {
            case '1':
                Interface(name);
                break;
            case '2':
                Interface(lessons);
                break;
            case '3':
                Interface(absence);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong option");
                break;
        }
    }

    /// <summary> Console Interface for users </summary>
    public static void Interface(AbsenceSystem absenceSystem)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n================ Absence System ================\n");
        Console.WriteLine("\nPlease select an option: \n");
        Console.WriteLine("[0] Back");
        Console.WriteLine("[1] Display file data");
        Console.WriteLine("[2] Write data");
        Console.WriteLine("[3] Delete data");
        Console.WriteLine("[4] Filter and searching");
        Console.WriteLine("[5] Sorting");
        Console.WriteLine("[6] Summary (Output only for file absence)");
        Console.WriteLine("[7] Display table with the all the data about students");
        Console.WriteLine("[8] Exit");

        ConsoleKeyInfo option = Console.ReadKey();
        Console.Clear();

        switch (option.KeyChar)
        {
            case '0':
                FileChoice();
                break;
            case '1':
                absenceSystem.ReadFile();
                Console.ReadKey();
                break;
            case '2':
                absenceSystem.WriteText();
                Console.Clear();
                break;
            case '3':
                absenceSystem.DeleteText();
                break;
            case '4':
                absenceSystem.Filter();
                break;
            case '5':
                absenceSystem.Sorting();
                break;
            case '6':
                Console.WriteLine("Sum of all absences => " + Absence.Summary());
                break;
            case '7':
                absenceSystem.Display();
                Console.ReadKey();
                break;
            case '8':
                Console.Clear();
                Console.WriteLine("\n=================== Goodbye! ===================\n");
                Environment.Exit(1);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWrong option");
                break;
        }
    }

    public static void Main(string[] args)
    {
        CapyBara();
        do
        {
            FileChoice();
        } while (true);
    }
}

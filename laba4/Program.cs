using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using static LabTasks;

public class LabTasks
{
    // t 1: удаление элементов из списка
    public static List<int> RemoveElements()
    {
        Console.WriteLine("Введите список через пробел:");
        string[] inputStrings = Console.ReadLine().Split(' ');
        List<int> input = new List<int>();

        foreach (string s in inputStrings) input.Add(int.Parse(s));

        Console.WriteLine("Какой элемент вы хотите удалить:");
        int elementToRemove = int.Parse(Console.ReadLine());

        for (int i = input.Count - 1; i >= 0; i--)
            if (input[i] == elementToRemove) input.RemoveAt(i);
        return input;
    }

    // t 1: вывод обратного списка
    public static void PrintListReverse(List<int> list)
    {
        Console.WriteLine("Обратный список:");
        for (int i = list.Count - 1; i >= 0; i--) Console.Write(list[i] + " ");
        Console.WriteLine();
    }

    // t 2: закупки
    public class School
    {
        public string Name { get; set; }
        public List<string> PurchasedFrom { get; set; }
    }

    public static void AnalyzePurchases()
    {
        List<School> schools = new List<School>();

        Console.WriteLine("Сколько школ:");
        int schoolCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < schoolCount; i++)
        {
            Console.WriteLine($"Введите название школы {i + 1}:");
            string schoolName = Console.ReadLine();

            Console.WriteLine($"Введите компании, в которых купила школа {schoolName} (через запятую):");
            string[] purchasedFromRaw = Console.ReadLine().Split(',');
            List<string> purchasedFrom = new List<string>();

            foreach (string company in purchasedFromRaw) purchasedFrom.Add(company.Trim());

            schools.Add(new School { Name = schoolName, PurchasedFrom = purchasedFrom });
        }

        Console.WriteLine("\nАнализ:");
        // 1. в каких компаниях производилась закупка
        foreach (var school in schools) Console.WriteLine($"{school.Name} закупал в компаниях: {string.Join(", ", school.PurchasedFrom)}");

        // 2. в каких компаниях производилась закупка хотя бы одной школой
        HashSet<string> allPurchasedFrom = new HashSet<string>();
        foreach (var school in schools)
            foreach (var company in school.PurchasedFrom) allPurchasedFrom.Add(company);

        Console.WriteLine("Компании, где производилась закупка: " + string.Join(", ", allPurchasedFrom));

        // 3. в каких компаниях никто не покупал
        Console.WriteLine("Введите список всех компаний (через запятую):");
        string[] allCompaniesRaw = Console.ReadLine().Split(',');
        HashSet<string> allCompanies = new HashSet<string>();
        foreach (string company in allCompaniesRaw) allCompanies.Add(company.Trim());
        HashSet<string> purchasedCompanies = new HashSet<string>(allPurchasedFrom);
        HashSet<string> noPurchaseCompanies = new HashSet<string>(allCompanies);
        noPurchaseCompanies.ExceptWith(purchasedCompanies);
        Console.WriteLine("Компании без закупок: " + string.Join(", ", noPurchaseCompanies));
    }

    // t 3: звонкие согласные 
    public static void PrintSonorConsonants()
    {
        string filePath = "./words.txt";

        if (!File.Exists(filePath)) 
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        string text = File.ReadAllText(filePath);

        string[] sonorConsonants = { "б", "в", "г", "д", "ж", "з", "л", "м", "н", "р" };
        var words = text.ToLower().Split(new char[] { ' ', '.', ',', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Создаем пустое множество для хранения уникальных звонких согласных
        var uniqueSonorConsonants = new SortedSet<char>();

        // Итерируем по каждому слову
        foreach (var word in words)
            // Итерируем по каждой букве в слове
            foreach (var c in word)
                // Проверяем, является ли буква звонкой согласной
                if (Array.IndexOf(sonorConsonants, c.ToString()) != -1)
                    // Добавляем в множество, если она там еще не есть
                    uniqueSonorConsonants.Add(c);

        Console.WriteLine("Звонкие согласные, встречающиеся хотя бы в одном слове, в алфавитном порядке:");
        Console.WriteLine(string.Join(", ", uniqueSonorConsonants));
    }

    // t 4: генерация логинов
    public static void GenerateUniqueLoginsAndSaveToXml()
    {
        // Console.InputEncoding = System.Text.Encoding.UTF8;
        // Console.OutputEncoding = System.Text.Encoding.UTF8;
        int participantCount;
        while (true)
        {
            Console.WriteLine("Введите количество участников:");
            if (int.TryParse(Console.ReadLine(), out participantCount) && participantCount > 0) break;
            else Console.WriteLine("Ошибка: введите корректное число участников.");
        }

        var participants = new List<string>();
        for (int i = 0; i < participantCount; i++)
        {
            string fullName;
            while (true)
            {
                Console.WriteLine($"Введите ФИ участника {i + 1} (Фамилия Имя):");
                fullName = Console.ReadLine().Trim();
                if (fullName.Split().Length == 2) break;
                else Console.WriteLine("Ошибка: введите корректное ФИ (Фамилия Имя).");
            }
            participants.Add(fullName);
        }

        var loginCount = new Dictionary<string, int>();
        var students = new List<Student>();

        foreach (var participant in participants)
        {
            string lastName = participant.Split()[0]; 
            string login = loginCount.ContainsKey(lastName)
                ? lastName + (++loginCount[lastName]) 
                : lastName;

            loginCount[lastName] = loginCount.ContainsKey(lastName) ? loginCount[lastName] + 1 : 1;
            students.Add(new Student { Name = participant, Login = login });
        }
        //foreach (var student in students) Console.WriteLine($"{student.Name}: {student.Login}");
        SaveToXmlFile(students, "nicknames_manual_input.xml");

        Console.WriteLine("Сгенерированные логины сохранены в файл: nicknames_manual_input.xml");

        Console.WriteLine("\nСгенерированные логины:");
        foreach (var student in students) Console.WriteLine($"{student.Name}: {student.Login}");
    }


    // t 5: XML сериализация
    [Serializable]
    public class StudentsData
    {
        public List<Student> Students { get; set; }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Login { get; set; }
    }

    public static List<string> ReadNamesFromFile(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException("Файл не найден: " + filePath);

        return File.ReadAllLines(filePath).ToList();
    }

    public static List<Student> GenerateUniqueLogins(List<string> names)
    {
        var loginCount = new Dictionary<string, int>();
        var students = new List<Student>();

        foreach (var fullName in names)
        {
            string lastName = fullName.Split()[0];
            string login = loginCount.ContainsKey(lastName)
                ? lastName + (++loginCount[lastName])
                : lastName;

            loginCount[lastName] = loginCount.ContainsKey(lastName) ? loginCount[lastName] + 1 : 1;
            students.Add(new Student { Name = fullName, Login = login });
        }

        return students;
    }
    public static void CreateXmlFile()
    {
        string inputFilePath = "./names.txt";
        string outputFilePath = "./nicknames.xml";

        try
        {
            var names = ReadNamesFromFile(inputFilePath);

            var students = GenerateUniqueLogins(names);

            // XML файл
            SaveToXmlFile(students, outputFilePath);

            Console.WriteLine("Результаты успешно сохранены в файл: " + outputFilePath);
            Console.WriteLine("\nСгенерированные логины:");
            foreach (var student in students) Console.WriteLine($"{student.Name}: {student.Login}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
    // сохранение в XML файл
    public static void SaveToXmlFile(List<Student> students, string filePath)
    {
        var studentsData = new StudentsData { Students = students };
        var serializer = new XmlSerializer(typeof(StudentsData));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, studentsData);
        }
    }
    public static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("\tМеню:");
            Console.WriteLine("1. Удаление элементов из списка");
            Console.WriteLine("2. Анализ закупок");
            Console.WriteLine("3. Звонкие согласные");
            Console.WriteLine("4. Генерация логинов");
            Console.WriteLine("5. XML сериализация");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var list = RemoveElements();
                    PrintListReverse(list);
                    break;
                case "2":
                    AnalyzePurchases();
                    break;
                case "3":
                    PrintSonorConsonants();
                    break;
                case "4":
                    GenerateUniqueLoginsAndSaveToXml();
                    break;
                case "5":
                    CreateXmlFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Такой команды нет.");
                    break;
            }
        }
    }
}

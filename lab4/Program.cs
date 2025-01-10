using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: Проверка удаления дубликатов
            Console.WriteLine("Задание 1: Удаление дубликатов из списка");
            List<int> numbers = new List<int> { 1, 2, 2, 3, 4, 4, 5 };
            var uniqueNumbers = WorkClass.RemoveDuplicates(numbers);
            Console.WriteLine($"Уникальные числа: {string.Join(", ", uniqueNumbers)}\n");

            // Задание 2: Проверка смены соседей в LinkedList
            Console.WriteLine("Задание 2: Смена соседей в LinkedList");
            LinkedList<int> linkedList = new LinkedList<int>(new[] { 1, 2, 2, 3, 4 });
            WorkClass.SwapNeighborsIfNotEqual(linkedList);
            Console.WriteLine($"Результат LinkedList: {string.Join(", ", linkedList)}\n");

            // Задание 3: Проверка анализа языков
            Console.WriteLine("Задание 3: Анализ языков работников");
            var employeeLanguages = new Dictionary<string, HashSet<string>>
            {
                { "Иванов", new HashSet<string> { "Русский", "Английский" } },
                { "Петров", new HashSet<string> { "Английский", "Немецкий" } },
                { "Сидоров", new HashSet<string> { "Французский" } }
            };
            WorkClass.AnalyzeLanguages(employeeLanguages);
            Console.WriteLine();

            // Задание 4: Проверка получения начальных букв слов
            Console.WriteLine("Задание 4: Начальные буквы слов в тексте");
            string text = "Привет мир! Это тестовый текст.";
            var startingLetters = WorkClass.GetStartingLetters(text);
            Console.WriteLine($"Начальные буквы: {string.Join(", ", startingLetters)}\n");

            // Задание 5: Проверка нахождения лучших учеников
            Console.WriteLine("Задание 5: Нахождение лучших учеников школы №50");
            var students = new List<WorkClass.Student>
            {
                new WorkClass.Student { LastName = "Иванов", FirstName = "Сергей", SchoolNumber = 50, Score = 87 },
                new WorkClass.Student { LastName = "Сергеев", FirstName = "Иван", SchoolNumber = 50, Score = 92 },
                new WorkClass.Student { LastName = "Кузнецов", FirstName = "Алексей", SchoolNumber = 50, Score = 92 },
                new WorkClass.Student { LastName = "Петров", FirstName = "Дмитрий", SchoolNumber = 51, Score = 85 }
            };

            WorkClass.FindTopStudents(students);

            // Пример сериализации и десериализации студентов
            //string filePath = "students.xml";

            //Console.WriteLine("\nСериализация студентов в XML...");
            //WorkClass.SerializeStudentsToXml(filePath, students);

            //Console.WriteLine("Десериализация студентов из XML...");
            //var deserializedStudents = WorkClass.DeserializeStudentsFromXml(filePath);

            //Console.WriteLine("Десериализованные студенты:");
            //foreach (var student in deserializedStudents)
            //{
            //    Console.WriteLine($"{student.LastName} {student.FirstName} - Школа: {student.SchoolNumber}, Балл: {student.Score}");
            //}

            Console.ReadKey();
        }
    }
}

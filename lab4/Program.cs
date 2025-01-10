using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab4.WorkClass;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: Проверка удаления дубликатов
            Console.WriteLine("Задание 1: Удаление дубликатов из списка");
            List<int> numbers = new List<int> { };
            string tmp;
            Console.Write("Вам надо заполнить списочек: ");
            do
            {
                Console.Write("Введите элемент списка или команду !exit: ");
                tmp = Console.ReadLine();
                if (tmp.ToLower() == "!exit") break;
                if (int.TryParse(tmp, out int t)) numbers.Add(Convert.ToInt32(tmp));
            } while(true);
            var uniqueNumbers = WorkClass.RemoveDuplicates(numbers);
            Console.WriteLine($"Уникальные числа: {string.Join(", ", uniqueNumbers)}\n");

            // Задание 2: Проверка смены соседей в LinkedList
            Console.WriteLine("Задание 2: Смена соседей в LinkedList");
            Console.Write("Вам надо заполнить списочек (числа через пробел): ");
            LinkedList<int> linkedList = new LinkedList<int>(ParseListInput(Console.ReadLine()));
            WorkClass.SwapNeighborsIfNotEqual(linkedList);
            Console.WriteLine($"Результат LinkedList: {string.Join(", ", linkedList)}\n");

            // Задание 3: Проверка анализа языков
            Console.WriteLine("Задание 3: Анализ языков работников");
            var employeeLanguages = new Dictionary<string, HashSet<string>>();
            while (true)
            {
                Console.Write("Введите имя работника (или '!exit' для завершения): ");
                string employeeName = Console.ReadLine();
                if (employeeName.ToLower() == "!exit")
                {
                    break;
                }

                Console.Write("Введите языки, которые знает работник (через запятую, без пробела): ");
                string languagesInput = Console.ReadLine();
                var languages = new HashSet<string>(languagesInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                // Убираем лишние пробелы
                for (int i = 0; i < languages.Count; i++)
                {
                    languages.Add(languages.ElementAt(i).Trim());
                }

                employeeLanguages[employeeName.Trim()] = languages;
            }

            WorkClass.AnalyzeLanguages(employeeLanguages);
            Console.WriteLine();

            // Задание 4: Проверка получения начальных букв слов
            Console.WriteLine("Задание 4: Начальные буквы слов в тексте");
            Console.WriteLine("Напишите свой текст, представив, что он был в файле: ");
            string text = Console.ReadLine();
            var startingLetters = WorkClass.GetStartingLetters(text);
            Console.WriteLine($"Начальные буквы: {string.Join(", ", startingLetters)}\n");

            // Задание 5: Проверка нахождения лучших учеников
            Console.WriteLine("Задание 5: Нахождение лучших учеников школы №50");
            var students = new List<Student>();

            // Ввод данных о студентах
            while (true)
            {
                Console.Write("Введите фамилию нового студента (или '!exit' для завершения): ");
                string lastName = Console.ReadLine();
                if (lastName.ToLower() == "!exit")
                {
                    break;
                }

                Console.Write("Введите имя студента: ");
                string firstName = Console.ReadLine();

                Console.Write("Введите номер школы: ");
                int schoolNumber = Convert.ToInt32(Console.ReadLine());

                Console.Write("Введите оценку студента: ");
                int score = Convert.ToInt32(Console.ReadLine());

                students.Add(new Student { LastName = lastName, FirstName = firstName, SchoolNumber = schoolNumber, Score = score });
            }

            FindTopStudents(students);

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

        private static List<int> ParseListInput(string input)
        {
            List<int> list = new List<int>();
            foreach (string item in input.Split(' '))
            {
                if (int.TryParse(item, out int number))
                {
                    list.Add(number);
                }
                else
                {
                    Console.WriteLine($"'{item}' не является числом и будет проигнорировано.");
                }
            }
            return list;
        }

    }
}

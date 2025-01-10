using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab4
{
    internal class WorkClass
    {
        // Задание 1: Оставить в списке только первые вхождения одинаковых элементов.
        public static List<T> RemoveDuplicates<T>(List<T> list)
        {
            return list.Distinct().ToList();
        }

        // Задание 2: Если у элемента со значением E "соседи" не равны, поменять их местами.
        public static void SwapNeighborsIfNotEqual(LinkedList<int> list)
        {
            var current = list.First;
            while (current != null && current.Next != null)
            {
                if (current.Value != current.Next.Value)
                {
                    var next = current.Next;
                    int temp = current.Value;
                    current.Value = next.Value;
                    next.Value = temp;
                }
                current = current.Next;
            }
        }

        // Задание 3: Определить, какие языки знает каждый работник, хотя бы один и никто.
        public static void AnalyzeLanguages(Dictionary<string, HashSet<string>> employeeLanguages)
        {
            var allLanguages = new HashSet<string>();
            foreach (var languages in employeeLanguages.Values)
            {
                allLanguages.UnionWith(languages);
            }

            foreach (var language in allLanguages)
            {
                var knowsLanguage = employeeLanguages.Where(e => e.Value.Contains(language)).Select(e => e.Key).ToList();
                Console.WriteLine($"Язык: {language}, Знают: {string.Join(", ", knowsLanguage)}");
            }

            var noOneKnows = allLanguages.Where(lang => employeeLanguages.All(e => !e.Value.Contains(lang)));
            Console.WriteLine($"Языки, которые никто не знает: {string.Join(", ", noOneKnows)}");
        }

        // Задание 4: С каких букв начинаются слова в тексте.
        public static HashSet<char> GetStartingLetters(string text)
        {
            var words = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<char>(words.Select(word => word[0]));
        }

        // Задание 5: Определение лучших учеников школы № 50.
        [Serializable]
        public class Student
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public int SchoolNumber { get; set; }
            public int Score { get; set; }

            // Конструктор по умолчанию необходим для сериализации
            public Student() { }
        }

        public static void FindTopStudents(List<Student> students)
        {
            var school50Students = students.Where(s => s.SchoolNumber == 50).ToList();

            if (!school50Students.Any()) return;

            var maxScore = school50Students.Max(s => s.Score);
            var topStudents = school50Students.Where(s => s.Score == maxScore).ToList();

            if (topStudents.Count > 2)
            {
                Console.WriteLine(topStudents.Count);
            }
            else if (topStudents.Count == 1)
            {
                Console.WriteLine($"{topStudents[0].LastName} {topStudents[0].FirstName}");
            }
            else
            {
                foreach (var student in topStudents)
                {
                    Console.WriteLine($"{student.LastName} {student.FirstName}");
                }
            }
        }

        // Метод для заполнения списка студентов и сериализации в XML
        public static void SerializeStudentsToXml(string filePath, List<Student> students)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, students);
            }
        }

        // Метод для десериализации студентов из XML
        public static List<Student> DeserializeStudentsFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                return (List<Student>)serializer.Deserialize(reader);
            }
        }
    }
}

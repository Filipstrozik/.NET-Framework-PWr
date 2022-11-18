using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

#pragma warning disable
// głównie nieużywane funkcje, które w programie głównym sa zakomentowane

namespace WykladLINQ
{

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }

        public Person(string name, int age, bool isActive)
        {
            Name = name;
            Age = age;
            IsActive = isActive;
        }

    }


    class Program
    {

        static void FirstLINQ()
        {
            string[] words = { "ala", "monkey", "cat", "dolly", "wind", "water" };
            IEnumerable<string> selection =
                from sequence in words
                where sequence.Contains("a")
                select sequence;
            foreach (string word in selection)
            {
                Console.WriteLine(word);
            }
        }
        static void FirstLINQFluent()
        {
            string[] words = { "ala", "monkey", "cat", "dolly", "wind", "water" };
            IEnumerable<string> selection = words
                .Select(s => s)
                .Where(s => s.Contains("a"))
                .Select(s => s);
            selection.ToList().ForEach(Console.WriteLine);
        }




        static void FirstLINQFluentSimpler()
        {
            string[] words = { "ala", "monkey", "cat", "dolly", "wind", "water" };
            IEnumerable<string> selection = words
                .Where(s => s.Contains("a"));
            selection.ToList().ForEach(Console.WriteLine);
        }
        static void List1(string rootDirectory, string searchPattern)
        {
            IEnumerable<string> filenames = Directory.GetFiles(rootDirectory, searchPattern);
            IEnumerable<FileInfo> fileInfos =
                from filename in filenames
                select new FileInfo(filename);
            foreach (var fileInfo in fileInfos)
            {
                Console.WriteLine($"{fileInfo.Name} ({fileInfo.LastWriteTime})");
            }
        }

#pragma warning disable
        static void List2(string rootDirectory, string searchPattern)
        {
            IEnumerable<string> filenames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileResults =
                from fileName in filenames
                select (
                    Name: fileName,
                    LastWriteTime: File.GetLastWriteTime(fileName)
                );
            foreach (var fileResult in fileResults)
            {
                Console.WriteLine($"{fileResult.Name} ({fileResult.LastWriteTime})");
            }
        }

        static void List3(string rootDirectory, string searchPattern)
        {
            var fileNames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileInfos =
              from fileName in fileNames
              where !fileName.Contains("vshost")
              let file = new FileInfo(fileName)
              orderby file.Length descending, fileName
              select file;

            foreach (var fileInfo in fileInfos)
            {
                Console.WriteLine($@"{ fileInfo.Name } ({fileInfo.LastWriteTime}), len={fileInfo.Length}");
            }
        }

        static void List4(string rootDirectory, string searchPattern)
        {
            var fileNames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileInfos = fileNames
                   .Where(fileName => !fileName.Contains("vshost"))
                   .OrderBy(fileName => (new FileInfo(fileName)).Length)
                   .ThenBy(fileName => fileName)
                   .Select(fileName => (new FileInfo(fileName)));
            foreach (var fileInfo in fileInfos)
            {
                Console.WriteLine($@"{ fileInfo.Name } ({fileInfo.LastWriteTime}), 	len={fileInfo.Length}");
            }
        }


        static bool IsLetterA(string word)
        {
            if (word.Contains("a"))
            {
                Console.Write("#");
                return true;
            }
            else
            {
                Console.Write("!");
                return false;
            }
        }

        static void LazyLINQ()
        {
            string[] words = { "ala", "monkey", "cat", "dolly", "wind", "water" };
            IEnumerable<string> selection =
                from ciag in words
                where IsLetterA(ciag)
                select ciag;
            Console.WriteLine("wynik:");
            foreach (string slowo in selection)
            {
                Console.Write(slowo);
            }
        }

        static List<Person> Persons { get; set; } = new List<Person>(){
            new Person("Nowak", 22, true),
            new Person("Schmidt", 40, false),
            new Person("Holmes", 30, true),
            new Person("Pepik", 12, true),
            new Person("Adamus", 55, false)
        };

        static public void ShowResult<T>(IEnumerable<T> list)
        {
            Console.WriteLine("Result:");
            foreach (T elem in list)
            {
                Console.WriteLine(elem);
            }
        }


        static void KlauzulaWhere()
        {
            IEnumerable<string> selection =
                from person in Persons
                where person.Age > 20
                select person.Name;
            ShowResult(selection);
        }

        static void KlauzulaOrderBy()
        {
            IEnumerable<string> selection =
                from person in Persons
                where person.IsActive
                orderby person.Age
                select person.Name;
            ShowResult(selection);
            selection =
                from person in Persons
                orderby person.IsActive, person.Age descending
                select person.Name;
            ShowResult(selection);
        }
#pragma warning disable
        static void InvocationLINQ()
        {
            string[] words = { "ala", "monkey", "cat", "dolly", "wind", "water" };
            int counter = 0;
            Func<string, string> ident =
                word => { counter++; return word; };
            IEnumerable<string> selection =
                from seq in words
                where seq.Contains("a")
                select ident(seq);
            Console.WriteLine("result:");
            Console.WriteLine($"before: {counter}");
            Console.WriteLine($"selection.Count()= { selection.Count()}");
            Console.WriteLine($"after 1 time: {counter}");
            Console.WriteLine($"selection.Count()= { selection.Count()}");
            Console.WriteLine($"after 2 time: {counter}");
            List<string> selectionCache = selection.ToList();
            Console.WriteLine($"after ToList(): {counter}");
            Console.WriteLine($"selectionCache.Count()= { selectionCache.Count()}");
            Console.WriteLine($"after 1 time: {counter}");
            Console.WriteLine($"selectionCache.Count()= { selectionCache.Count()}");
            Console.WriteLine($"after 2 time: {counter}");
        }

        static void ClauseWithoutLet(string rootDirectory, string searchPattern)
        {
            IEnumerable<string> filenames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileResults =
                from fileName in filenames
                orderby new FileInfo(fileName).Length, fileName
                select new FileInfo(fileName);
            foreach (var fileResult in fileResults)
            {
                Console.WriteLine($"{fileResult.Name} ({fileResult.Length})");
            }
        }

        static void ClauseWithLet(string rootDirectory, string searchPattern)
        {
            IEnumerable<string> filenames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileResults =
                from fileName in filenames
                let file = new FileInfo(fileName)
                orderby file.Length, fileName
                select file;
            foreach (var fileResult in fileResults)
            {
                Console.WriteLine($"{fileResult.Name} ({fileResult.Length})");
            }
        }

        static void FluentApiWithLet(string rootDirectory, string searchPattern)
        {
            IEnumerable<string> filenames = Directory.GetFiles(rootDirectory, searchPattern);
            var fileResults = filenames.Select(fileName => new { fileName, file = new FileInfo(fileName) })
                .OrderBy(elem => (elem.file.Length, elem.fileName))
                .Select(elem => elem.file);
            foreach (var fileResult in fileResults)
            {
                Console.WriteLine($"{fileResult.Name} ({fileResult.Length})");
            }
        }

        static void ClauseWithGroup()
        {
            IEnumerable<IGrouping<bool, string>> selection =
                from person in Persons
                group person.Name by person.IsActive;

            foreach (IGrouping<bool, string> group in selection)
            {
                Console.WriteLine($"GroupKey={group.Key}, elements:");
                foreach (string name in group)
                {
                    Console.Write($"{name}, ");
                }
                Console.WriteLine();
            }
        }
        static void ClauseWithInto()
        {
            var selection =
                from person in Persons
                group person.Name by person.IsActive
                into groups
                select
                (
                    IsActive: groups.Key,
                    Persons: groups
                );

            foreach (var group in selection)
            {
                Console.WriteLine($"Is active = {(group.IsActive?"YES":"NO")}, persons:");
                foreach (string name in group.Persons)
                {
                    Console.Write($"{name}, ");
                }
                Console.WriteLine();
            }
        }
        static void ClauseTwoFromFlat()
        {
            var selection =
                from person in Persons
                from letter in person.Name
                orderby letter
                select letter;

            foreach (var letter in selection)
            {
                Console.Write($"{letter},");
            }
            Console.WriteLine();
        }

        static void ClauseTwoFromCartesian()
        {
            var numbers = new[] { 1, 2, 3 };
            var letters = new[] { 'A', 'B', 'C', 'D' };

            var selection =
                from number in numbers
                from letter in letters
                select (letter,number);

            foreach (var (letter,number) in selection)
            {
                Console.Write($"{letter}{number},");
            }
            Console.WriteLine();
        }

        static void ClauseWithDistinct()
        {
            var selection =
                (from person in Persons
                from letter in person.Name
                orderby letter
                select letter)
                .Distinct();

            foreach (var letter in selection)
            {
                Console.Write($"{letter},");
            }
            Console.WriteLine();
        }
        static void Main()
        {
 //           Console.WriteLine("Start");
            FirstLINQFluentSimpler();
            //List1(".", "*");
            //List2(".", "*");
            //List3(".", "*");
            //List4(".", "*");
            //LazyLINQ();
            //InvocationLINQ();
            //KlauzulaWhere();
            //KlauzulaOrderBy();
            //ClauseWithoutLet(".", "*");
            //ClauseWithLet(".", "*");
            //FluentApiWithLet(".", "*");
            //ClauseWithGroup();
            //ClauseWithInto();
            //ClauseTwoFromFlat();
            //ClauseTwoFromCartesian();
            //ClauseWithDistinct();
        }
    }
}

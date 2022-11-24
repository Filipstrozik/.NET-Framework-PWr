using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab7
{


    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }


        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public class StudentMtm
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public StudentMtm(int id, int index, string name, Gender gender, bool active,
            int departmentId)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
        }


        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2},";
            return result;
        }
    }
    class Topic
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }
    }

    class StudentToTopic
    {
        public int StudentId { get; set; }
        public int TopicId { get; set; }

        public StudentToTopic(int studId, int topicId)
        {
            StudentId = studId;
            TopicId = topicId;
        }

        public override string ToString()
        {
            return $"{StudentId,2}), {TopicId,11}";
        }
    }



    class Program
    {
        public static void Zad1(int n)
        {
            Console.WriteLine("1a-----------------------");
            var resStud = Generator.GenerateStudentsWithTopicsEasy()
                            .OrderBy(s => s.Name)
                            .ThenBy(s => s.Index)
                            .GroupBy(s => s.Name)
                            .Select(gr => new { Name = gr.Key, Students = gr.Take(n)});
      
            foreach (var group in resStud)
            {
                Console.WriteLine(group.Name);
                group.Students.ToList().ForEach(s => Console.WriteLine(s));
            }

            Console.WriteLine("1b-----------------------");

            int index = 0;
            var sth = Generator.GenerateStudentsWithTopicsEasy()
                            .OrderBy(s => s.Name)
                            .ThenBy(s => s.Index)
                            .GroupBy(r => index++ / n).ToList();

            foreach (var group in sth)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(s => Console.WriteLine(s));
            }

            Console.WriteLine("qes-----------------------");

            var resStud2 = from s in Generator.GenerateStudentsWithTopicsEasy()
                           orderby s.Name, s.Index
                           group s by s.Name into grp
                           select new { Name = grp.Key, Students = grp.Take(n)};


            foreach (var group in resStud2)
            {
                Console.WriteLine(group.Name);
                group.Students.ToList().ForEach(s => Console.WriteLine(s));
            }
            Console.WriteLine("qes-2----------------------");
            index = 0;
            var qes2 = from s in Generator.GenerateStudentsWithTopicsEasy()
                       orderby s.Name, s.Index
                       group s by (index++ / n);


            foreach (var group in qes2)
            {
                Console.WriteLine(group.Key);
                group.ToList().ForEach(s => Console.WriteLine(s));
            }
        }


        public static void Zad2()
        {
            //a
            Console.WriteLine("a1------------");
            var resStud = Generator.GenerateStudentsWithTopicsEasy().SelectMany(elem => elem.Topics).GroupBy(t => t)
                .Select(group => new { Topic = group.Key, count = group.Count() }).OrderByDescending(g => g.count);
            resStud.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("a2------------");
            var qesRes = from s in Generator.GenerateStudentsWithTopicsEasy()
                         from t in s.Topics
                         group t by t into grp
                         select new { Topic = grp.Key , count = grp.Count()};

            qesRes.ToList().ForEach(Console.WriteLine);


            //b
            Console.WriteLine("b1------------");
            var res = Generator.GenerateStudentsWithTopicsEasy()
                .GroupBy(s => s.Gender)
                .Select(group => new { Gender = group.Key, Topics = group.SelectMany(elem => elem.Topics).
                                                                        GroupBy(t => t).
                                                                        Select(group => new { Topic = group.Key, count = group.Count() }).
                                                                        OrderByDescending(g => g.count) });

            foreach (var group in res)
            {
                Console.WriteLine(group.Gender);
                group.Topics.ToList().ForEach(s => Console.WriteLine(s));
            }

            Console.WriteLine("b2------------");

            var qesResB = from s in Generator.GenerateStudentsWithTopicsEasy()
                          group s by s.Gender into genders
                          select new
                          {
                              Gender = genders.Key,
                              Topics = from s in genders
                                       from t in s.Topics
                                       group t by t into grp
                                       select new { Topic = grp.Key, count = grp.Count() }
                          };
            foreach (var group in qesResB)
            {
                Console.WriteLine(group.Gender);
                group.Topics.ToList().ForEach(s => Console.WriteLine(s));
            }

        }

        public static void Zad3()
        {
            Console.WriteLine("lista tematow 1 ------------");
            //TODO how to make this as a qes query not hardcoded?
            var tematy = new List<Topic>() {
            new Topic(1,"C#"),
            new Topic(2,"C++"),
            new Topic(3,"Java"),
            new Topic(4,"PHP"),
            new Topic(5,"algorithms"),
            new Topic(6,"fuzzy logic"),
            new Topic(7,"Basic"),
            new Topic(8,"JavaScript"),
            new Topic(9,"neural networks"),
            new Topic(10,"web programming")
            };

            tematy.ForEach(Console.WriteLine);

            Console.WriteLine("lista tematow 2 (b)------------");
            List<string> topics = new()
            {
                "C#", "C++", "Java", "PHP", "algorithms", "fuzzy logic", "Basic", "JavaScript", "neural networks", "web programming"
            };

            List<Topic> tematy2 = new();
            int id = 1;
            tematy2 = (from t in topics
                      select new Topic(id++, t)).ToList();

            tematy2.ForEach(Console.WriteLine);

            Console.WriteLine("a1------------");
            List<Student> newStudList = Generator.GenerateStudentsWithTopicsEasy().Select(s => new Student(
            
                s.Id,
                s.Index,
                s.Name,
                s.Gender,
                s.Active,
                s.DepartmentId,
                s.Topics.Join(tematy, temat => temat, topic => topic.Name, (nazw, id) => id.Id).ToList()
            )).ToList();

            newStudList.ForEach(Console.WriteLine);
            Console.WriteLine("a2------------");
            
            var qesNewStudList = from s in Generator.GenerateStudentsWithTopicsEasy()
                                 select new Student(
                s.Id,
                s.Index,
                s.Name,
                s.Gender,
                s.Active,
                s.DepartmentId,
                (from o in s.Topics
                  join n in tematy
                  on o equals n.Name
                  select n.Id).ToList());
            qesNewStudList.ToList().ForEach(Console.WriteLine);

            Console.WriteLine("c1------------");

            //create new students
            List<StudentMtm> newStudMtmList = Generator.GenerateStudentsWithTopicsEasy().Select(s => new StudentMtm(
                s.Id,
                s.Index,
                s.Name,
                s.Gender,
                s.Active,
                s.DepartmentId
                )).ToList();

            newStudMtmList.ForEach(Console.WriteLine);

            //create topics
            int counter = 1;
            List<Topic> newTopicList = Generator.GenerateStudentsWithTopicsEasy().SelectMany(elem => elem.Topics).GroupBy(t => t)
                .Select(t => new Topic(
                    counter++, t.Key
                    )).ToList();

            newTopicList.ForEach(Console.WriteLine);

            //pray god for this below
            var sth = Generator.GenerateStudentsWithTopicsEasy()
                .SelectMany(s => s.Topics, (student, topic) => new
                {
                    student.Id,
                    Topic = newTopicList.First(t => t.Name.Equals(topic)).Id
                }).Select(r => new StudentToTopic(r.Id, r.Topic));
            

            foreach (var item in sth)
            {
                Console.WriteLine(item);

            }


            Console.WriteLine("c1---qes----------------");

            //Using Query Syntax
            var qes = (from std in Generator.GenerateStudentsWithTopicsEasy()
                       from topic in std.Topics
                       select new StudentToTopic
                       (
                           std.Id,
                           (from t in newTopicList
                           where t.Name == topic
                           select t.Id).Single()
                       )).ToList();

            qes.ForEach(Console.WriteLine);
        }


        public static void Zad4()
        {

            List<int> list = new() { 1, 2, 3, 4, 5 };

            /*            var meths = list.GetType().GetMethods();
                        foreach (var meth in meths)
                        {
                            Console.WriteLine($" Name : {meth.Name}");
                            Console.WriteLine($" Params amount : {meth.GetParameters().Length}"); // number of parameter
                        }*/

            Console.WriteLine("zad4---------------");

            MethodInfo methodInfo1 = list.GetType().GetMethod("RemoveRange", new Type[] { typeof(int), typeof(int)});
            methodInfo1.Invoke(list, new object[] { 1, 2 });
            list.ForEach(e => Console.Write($"{e} "));
            Console.WriteLine();
            

            MethodInfo methodInfo = list.GetType().GetMethod("get_Count");
            int res = (int)methodInfo.Invoke(list, null);
            Console.WriteLine($"Contains =: {res}");



            string test = "object reference not set to an instance of an object";
            MethodInfo m2 = test.GetType().GetMethod("Contains", new Type[] { typeof(string) });
            bool conatins = (bool)m2.Invoke(test, new object[] { "reference" });
            Console.WriteLine(conatins);
        }

        public static void ShowAllCollections()
        {
            //Generator.GenerateIntsEasy().ToList().ForEach(Console.WriteLine);
            Generator.GenerateDepartmentsEasy().ForEach(Console.WriteLine);
            Generator.GenerateStudentsWithTopicsEasy().ForEach(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            ShowAllCollections();
            Console.WriteLine("-----------------------");
            Zad1(2);
            Zad2();
            Zad3();
            Zad4();

        }
    }
}

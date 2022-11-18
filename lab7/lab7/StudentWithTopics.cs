using System.Collections.Generic;

namespace lab7
{
    public class StudentWithTopics
    {
        private int v1;
        private int v2;
        private string v3;
        private Gender female;
        private bool v4;
        private int v5;
        private List<string> list;

        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public StudentWithTopics(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

/*        public StudentWithTopics(int v1, int v2, string v3, Gender female, bool v4, int v5, List<string> list)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.female = female;
            this.v4 = v4;
            this.v5 = v5;
            this.list = list;
        }*/

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }
}

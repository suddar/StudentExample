namespace StudentExample.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreateTime { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }
        public int Count { get; set; } = 0;
    }
}

namespace FullStack.API.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public long Price { get; set; }
        public string Department { get; set; }
    }
}

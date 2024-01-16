namespace RentACar.Web.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public ICollection<User> Users { get; set; }
    }

    public class CarCreateArgs
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }

    
}

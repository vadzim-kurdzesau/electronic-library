namespace ElectronicLibrary.Models
{
    public class Reader
    {
        public int Id { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string Zip { get; set; }
    }
}

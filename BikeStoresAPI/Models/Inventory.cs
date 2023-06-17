namespace BikeStoresAPI.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Store { get; set; }
        public int Quantity { get; set; }
    }
}

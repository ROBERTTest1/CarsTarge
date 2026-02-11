namespace Cars.Core.Domain
{
    public class Car
    {
        public int Id { get; set; }
        
        public string Brand { get; set; } = string.Empty;
        
        public string Model { get; set; } = string.Empty;
        
        public int Year { get; set; }
        
        public decimal Price { get; set; }
        
        public string Color { get; set; } = string.Empty;
        
        public string? Notes { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime ModifiedAt { get; set; }
    }
}

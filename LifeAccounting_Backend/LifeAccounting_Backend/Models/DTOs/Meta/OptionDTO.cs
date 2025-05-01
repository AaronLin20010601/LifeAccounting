namespace LifeAccounting_Backend.Models.DTOs.Meta
{
    public class OptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Type { get; set; }
        public decimal? Balance { get; set; }
    }
}

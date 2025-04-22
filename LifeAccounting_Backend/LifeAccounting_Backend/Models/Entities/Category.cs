namespace LifeAccounting_Backend.Models.Entities
{
    // 收支類型
    public class Category
    {
        public int Id { get; set; } // 主鍵
        public int UserId { get; set; } // 使用者編號
        public string Name { get; set; } = null!; // 類型名稱
        public string Type { get; set; } = null!; // 收支類型

        public User User { get; set; } = null!;
        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}

namespace LifeAccounting_Backend.Models.DTOs.User
{
    // 帳戶餘額是否與收支紀錄同步
    public class SyncBalanceDTO
    {
        public bool IsSync { get; set; } // 是否同步
    }
}

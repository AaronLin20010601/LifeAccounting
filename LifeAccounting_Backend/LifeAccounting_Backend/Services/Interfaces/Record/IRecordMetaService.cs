using LifeAccounting_Backend.Models.DTOs.Record;

namespace LifeAccounting_Backend.Services.Interfaces.Record
{
    public interface IRecordMetaService
    {
        Task<(List<OptionDTO> Accounts, List<OptionDTO> Categories)> GetMetaDataAsync(int userId);
    }
}

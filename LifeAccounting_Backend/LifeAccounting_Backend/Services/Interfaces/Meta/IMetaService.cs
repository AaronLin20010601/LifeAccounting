using LifeAccounting_Backend.Models.DTOs.Meta;

namespace LifeAccounting_Backend.Services.Interfaces.Meta
{
    public interface IMetaService
    {
        Task<(List<OptionDTO> Accounts, List<OptionDTO> Categories)> GetMetaDataAsync(int userId, string? toCurrency);
    }
}

using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllTrade();

        Task Add(Trade trade);

        Task Update(Trade trade);

        Task<Trade> GetTradeId(int id);

        Task Delete(int id);
    }
}

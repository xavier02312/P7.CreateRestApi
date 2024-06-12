using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IBidListRepository
    {
        Task<IEnumerable<BidList>> GetAllBidList();

        Task Add(BidList bidList);

        Task Update(BidList bidList);

        Task<BidList> GetBidListById(int id);

        Task Delete(int id);
    }
}

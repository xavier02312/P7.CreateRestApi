using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private LocalDbContext _context;
        public BidListRepository(LocalDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<BidList>> GetAllBidList()
        {
            var bidLists = await _context.BidLists.ToListAsync();

            if (bidLists == null)
            {
                throw new Exception("BidList does not exist.");
            }

            return bidLists;
        }
        public async Task Add(BidList bidList)
        {
            if (BidListExists(bidList.BidListId))
            {
                throw new Exception("BidList already exists.");
            }

            _context.BidLists.Add(bidList);

            await _context.SaveChangesAsync();
        }
        public async Task Update(BidList bidList)
        {
            if (!BidListExists(bidList.BidListId))
            {
                throw new Exception("BidList does not exist.");
            }

            _context.Update(bidList).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task<BidList> GetBidListById(int id)
        {
            var bidList = await _context.BidLists.FindAsync(id);

            if (bidList == null)
            {
                throw new Exception("BidList does not exist.");
            }

            return bidList;
        }
        public async Task Delete(int id)
        {
            var bid = await _context.BidLists.FindAsync(id);

            if (bid != null)
            {
                _context.BidLists.Remove(bid);

                await _context.SaveChangesAsync();
            }
        }
        public bool BidListExists(int id)
        {
            return _context.BidLists.Any(e => e.BidListId == id);
        }
    }
}

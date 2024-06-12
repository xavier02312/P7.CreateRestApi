using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetAllTrade()
        {
            var trad = await _context.Trades.ToListAsync();

            if (trad == null)
            {
                throw new Exception("Trade does not exist.");
            }

            return trad;
        }

        public async Task Add(Trade trade)
        {
            if (TradeExists(trade.TradeId))
            {
                throw new Exception("Trade already exists.");
            }

            _context.Trades.Add(trade);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Trade trade)
        {
            if (!TradeExists(trade.TradeId))
            {
                throw new Exception("Trade does not exist.");
            }

            _context.Trades.Update(trade);

            await _context.SaveChangesAsync();
        }

        public async Task<Trade> GetTradeId(int id)
        {

            var trade = await _context.Trades.FindAsync(id);

            if (trade == null)
            {
                throw new Exception("Trade does not exist.");
            }

            return trade;
        }

        public async Task Delete(int id)
        {
            var trade = await _context.Trades.FindAsync(id);

            if (trade != null)
            {
                _context.Trades.Remove(trade);

                await _context.SaveChangesAsync();
            }
        }

        public bool TradeExists(int id)
        {
            return _context.Trades.Any(t => t.TradeId == id);
        }
    }
}

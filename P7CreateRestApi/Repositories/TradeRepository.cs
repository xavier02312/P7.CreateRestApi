using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _dbContext;
        public TradeRepository(LocalDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Create(Trade trade)
        {
            _dbContext.Trades.Add(trade);
            _dbContext.SaveChanges();
        }

        public Trade? Delete(int id)
        {
            var trade = _dbContext.Trades.FirstOrDefault(t => t.TradeId == id);
            if (trade is not null)
            {
                _dbContext.Trades.Remove(trade);
                _dbContext.SaveChanges();
            }
            return trade;
        }

        public Trade? Get(int id) => _dbContext.Trades.FirstOrDefault(t => t.TradeId == id);

        public List<Trade> List() => _dbContext.Trades.ToList();

        public Trade? Update(Trade trade)
        {
            var tradeAModifier = _dbContext.Trades.FirstOrDefault(t => t.TradeId == trade.TradeId);
            if (tradeAModifier is not null)
            {
                tradeAModifier.Account = trade.Account;
                tradeAModifier.AccountType = trade.AccountType;
                tradeAModifier.BuyQuantity = trade.BuyQuantity;
                tradeAModifier.SellQuantity = trade.SellQuantity;
                tradeAModifier.BuyPrice = trade.BuyPrice;
                tradeAModifier.SellPrice = trade.SellPrice;
                tradeAModifier.TradeDate = trade.TradeDate;
                tradeAModifier.TradeSecurity = trade.TradeSecurity;
                tradeAModifier.TradeStatus = trade.TradeStatus;
                tradeAModifier.Trader = trade.Trader;
                tradeAModifier.Benchmark = trade.Benchmark;
                tradeAModifier.Book = trade.Book;
                tradeAModifier.CreationName = trade.CreationName;
                tradeAModifier.RevisionName = trade.RevisionName;
                tradeAModifier.RevisionDate = trade.RevisionDate;
                tradeAModifier.DealName = trade.DealName;
                tradeAModifier.DealType = trade.DealType;
                tradeAModifier.SourceListId = trade.SourceListId;
                tradeAModifier.Side = trade.Side;
                _dbContext.SaveChanges();
            }
            return tradeAModifier;
        }
    }
}

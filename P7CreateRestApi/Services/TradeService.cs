using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Models.OutputModel;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public TradeOutputModel? Create(TradeInputModel inputModel)
        {
            var trade = new Trade
            {
                Account = inputModel.Account,
                AccountType = inputModel.AccountType,
                BuyQuantity = inputModel.BuyQuantity,
                SellQuantity = inputModel.SellQuantity,
                BuyPrice = inputModel.BuyPrice,
                SellPrice = inputModel.SellPrice,
                TradeDate = inputModel.TradeDate,
                TradeSecurity = inputModel.TradeSecurity,
                TradeStatus = inputModel.TradeStatus,
                Trader = inputModel.Trader,
                Benchmark = inputModel.Benchmark,
                Book = inputModel.Book,
                CreationName = inputModel.CreationName,
                CreationDate = inputModel.CreationDate,
                RevisionName = inputModel.RevisionName,
                RevisionDate = inputModel.RevisionDate,
                DealName = inputModel.DealName,
                DealType = inputModel.DealType,
                SourceListId = inputModel.SourceListId,
                Side = inputModel.Side
            };
            _tradeRepository.Create(trade);
            return ToOutputModel(trade);
        }

        public TradeOutputModel? Delete(int id)
        {
            var trade = _tradeRepository.Delete(id);
            if (trade is not null)
            {
                return ToOutputModel(trade);
            }
            return null;
        }

        public TradeOutputModel? Get(int id)
        {
            var trade = _tradeRepository.Get(id);
            if (trade is not null)
            {
                return ToOutputModel(trade);
            }
            return null;
        }

        public List<TradeOutputModel> List()
        {
            var list = new List<TradeOutputModel>();
            var trades = _tradeRepository.List();
            foreach (var trade in trades)
            {
                list.Add(ToOutputModel(trade));
            }
            return list;
        }

        public TradeOutputModel? Update(int id, TradeInputModel inputModel)
        {
            var trade = _tradeRepository.Update(new Trade
            {
                TradeId = id,
                Account = inputModel.Account,
                AccountType = inputModel.AccountType,
                BuyQuantity = inputModel.BuyQuantity,
                SellQuantity = inputModel.SellQuantity,
                BuyPrice = inputModel.BuyPrice,
                SellPrice = inputModel.SellPrice,
                TradeDate = inputModel.TradeDate,
                TradeSecurity = inputModel.TradeSecurity,
                TradeStatus = inputModel.TradeStatus,
                Trader = inputModel.Trader,
                Benchmark = inputModel.Benchmark,
                Book = inputModel.Book,
                CreationName = inputModel.CreationName,
                CreationDate = inputModel.CreationDate,
                RevisionName = inputModel.RevisionName,
                RevisionDate = inputModel.RevisionDate,
                DealName = inputModel.DealName,
                DealType = inputModel.DealType,
                SourceListId = inputModel.SourceListId,
                Side = inputModel.Side
            });
            if(trade is not null)
            {
                return ToOutputModel(trade);
            }
            return null;
        }

        private TradeOutputModel ToOutputModel(Trade trade) => new TradeOutputModel
        {
            TradeId = trade.TradeId,
            Account = trade.Account,
            AccountType = trade.AccountType,
            BuyQuantity = trade.BuyQuantity,
            SellQuantity = trade.SellQuantity,
            BuyPrice = trade.BuyPrice,
            SellPrice = trade.SellPrice,
            TradeDate = trade.TradeDate,
            TradeSecurity = trade.TradeSecurity,
            TradeStatus = trade.TradeStatus,
            Trader = trade.Trader,
            Benchmark = trade.Benchmark,
            Book = trade.Book,
            CreationName = trade.CreationName,
            CreationDate = trade.CreationDate,
            RevisionName = trade.RevisionName,
            RevisionDate = trade.RevisionDate,
            DealName = trade.DealName,
            DealType = trade.DealType,
            SourceListId = trade.SourceListId,
            Side = trade.Side
        };
    }
}

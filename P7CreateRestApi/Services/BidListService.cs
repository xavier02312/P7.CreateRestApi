using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Models.OutputModel;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _bidListRepository;
        public BidListService(IBidListRepository bidListRepository) 
        { 
            _bidListRepository = bidListRepository;
        }

        public BidListOutputModel? Create(BidListInputModel inputModel)
        {
            var bidList = new BidList
            {
                Account = inputModel.Account,
                BidType = inputModel.BidType,
                BidQuantity = inputModel.BidQuantity,
                AskQuantity = inputModel.AskQuantity,
                Bid = inputModel.Bid,
                Ask = inputModel.Ask,
                Benchmark = inputModel.Benchmark,
                BidListDate = inputModel.BidListDate,
                Commentary = inputModel.Commentary,
                BidSecurity = inputModel.BidSecurity,
                BidStatus = inputModel.BidStatus,
                Trader = inputModel.Trader,
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
            _bidListRepository.Create(bidList);
            return ToOutputModel(bidList);
        }

        public BidListOutputModel? Delete(int id)
        {
            var bidList = _bidListRepository.Delete(id);
            if (bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        public BidListOutputModel? Get(int id)
        {
            var bidList = _bidListRepository.Get(id);
            if (bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        public List<BidListOutputModel> List()
        {
            var list = new List<BidListOutputModel>();
            var bidLists = _bidListRepository.List();
            foreach (var bidList in bidLists)
            {
                list.Add(ToOutputModel(bidList));
            }
            return list;
        }

        public BidListOutputModel? Update(int id, BidListInputModel inputModel)
        {
            var bidList = _bidListRepository.Update(new BidList
            {
                BidListId = id,
                Account = inputModel.Account,
                BidType = inputModel.BidType,
                BidQuantity = inputModel.BidQuantity,
                AskQuantity = inputModel.AskQuantity,
                Bid = inputModel.Bid,
                Ask = inputModel.Ask,
                Benchmark = inputModel.Benchmark,
                BidListDate = inputModel.BidListDate,
                Commentary = inputModel.Commentary,
                BidSecurity = inputModel.BidSecurity,
                BidStatus = inputModel.BidStatus,
                Trader = inputModel.Trader,
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
            if(bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        private BidListOutputModel ToOutputModel(BidList bidList) => new BidListOutputModel
        {
            BidListId = bidList.BidListId,
            Account = bidList.Account,
            BidType = bidList.BidType,
            BidQuantity = bidList.BidQuantity,
            AskQuantity = bidList.AskQuantity,
            Bid = bidList.Bid,
            Ask = bidList.Ask,
            Benchmark = bidList.Benchmark,
            BidListDate = bidList.BidListDate,
            Commentary = bidList.Commentary,
            BidSecurity = bidList.BidSecurity,
            BidStatus = bidList.BidStatus,
            Trader = bidList.Trader,
            Book = bidList.Book,
            CreationName = bidList.CreationName,
            CreationDate = bidList.CreationDate,
            RevisionName = bidList.RevisionName,
            RevisionDate = bidList.RevisionDate,
            DealName = bidList.DealName,
            DealType = bidList.DealType,
            SourceListId = bidList.SourceListId,
            Side = bidList.Side
        };
    }
}

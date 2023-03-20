using ReceiptPrize.Repository;

namespace ReceiptPrize.Service
{
    public class FetchPrizeNumService : IFetchPrizeNumService
    {
        private IPrizeNumRepository _repository;

        public FetchPrizeNumService(IPrizeNumRepository repository)
        {
            this._repository = repository;
        }

        public List<string> GetPrizeNumber()
        {
            return _repository.GetPrizeNum();
        }
    }
}

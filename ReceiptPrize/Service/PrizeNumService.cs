using ReceiptPrize.Repository;

namespace ReceiptPrize.Service
{
    public class PrizeNumService
    {
        private IPrizeNumRepository _repository;

        public PrizeNumService(IPrizeNumRepository repository)
        {
            this._repository = repository;
        }

        public List<string> GetPrizeNumber()
        {
            return _repository.GetPrizeNum();
        }
    }
}

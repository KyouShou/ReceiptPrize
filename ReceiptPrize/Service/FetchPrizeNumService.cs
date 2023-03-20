using ReceiptPrize.Exceptions;
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
            try
            {
                var result = _repository.GetPrizeNum();

                if (result.Count >= 1)
                {
                    return result;
                }
                else
                {
                    throw new FetchPrizeFailException();
                }
            }
            catch
            {
                throw new FetchPrizeFailException();
            }
        }
    }
}

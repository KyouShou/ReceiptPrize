namespace ReceiptPrize.Exceptions
{
    public class NoPrizeListInCacheException : Exception
    {
        public NoPrizeListInCacheException() : base()
        {
        
        }

        public NoPrizeListInCacheException(string message) : base(message)
        {
        
        }
    }
}

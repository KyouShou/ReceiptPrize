namespace ReceiptPrize.Exceptions
{
    public class NumberFormatErrorException:Exception
    {
        public NumberFormatErrorException() : base()
        {
        
        }

        public NumberFormatErrorException(string message = "輸入數字格式錯誤") : base(message)
        {
        
        }
    }
}

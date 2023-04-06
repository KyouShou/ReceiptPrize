using Microsoft.Extensions.Caching.Memory;
using Moq;
using ReceiptPrize.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptPrize.Tests
{
    public class CheckPrizeServiceBuilder
    {
        private CheckPrizeService _checkPrizeService;
        private IFetchPrizeNumService _fetchPrizeNumService;
        private MemoryCache _memoryCache;

        public CheckPrizeServiceBuilder()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public CheckPrizeServiceBuilder SetFakePrizeNumToFetchPrizeNumService(List<string> fakePrizeNum)
        {
            var stubFetchPrizeNumService = new Mock<IFetchPrizeNumService>();

            stubFetchPrizeNumService.Setup(m => m.GetPrizeNumber()).Returns(fakePrizeNum);

            this._fetchPrizeNumService = stubFetchPrizeNumService.Object;

            return this;
        }

        public CheckPrizeServiceBuilder SetExceptionToFetchPrizeNumService(Exception e)
        {
            var stubFetchPrizeNumService = new Mock<IFetchPrizeNumService>();

            stubFetchPrizeNumService.Setup(m => m.GetPrizeNumber()).Throws(e);

            this._fetchPrizeNumService = stubFetchPrizeNumService.Object;

            return this;
        }

        public CheckPrizeServiceBuilder SetFakePrizeNumToMemoryCache(List<string> fakePrizeNum)
        {
            _memoryCache.Set("prizeListInCache", fakePrizeNum);

            return this;
        }

        public CheckPrizeService Build()
        {
            _checkPrizeService = new CheckPrizeService(_fetchPrizeNumService, _memoryCache);
            return _checkPrizeService;
        }
    }


}

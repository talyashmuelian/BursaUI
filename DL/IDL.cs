using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DL
{
    public interface IDL
    {
        public Task UpdateCurrencyPairValues(int pairId, decimal newValue);
        public Task<List<TradingPair>> GetListOfTradingPair();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        public Task SimulateTrades();
        public Task<List<TradingPair>> GetListOfTradingPair();
    }
}

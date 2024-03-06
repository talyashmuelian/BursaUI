using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BE;
using DL;


namespace BL
{
    public class BLImp : IBL
    {

        private IDL dl = DLImp.theInstance();
        private static BLImp instance=null;
        private BLImp()
        {

        }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
        }

        public async Task SimulateTrades()
        {
            Random random = new Random();

            int pairId = random.Next(1, 4); // Assuming 3 pairs exist
            decimal newValue = (decimal)random.NextDouble() * 100; // Random value between 0 and 100
            await dl.UpdateCurrencyPairValues(pairId, newValue);
        }
        public async Task<List<TradingPair>> GetListOfTradingPair()
        {
            try
            {
                return await dl.GetListOfTradingPair();
            }
            catch (Exception ex)
            {
                throw new NoDataException(ex.Message);
            }
        }
    }
}

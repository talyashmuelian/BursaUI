using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BE;

namespace DL
{
    public class DLImp :IDL
    {
        private readonly string connectionString= "Server=(localdb)\\MSSQLLocalDB;Database=CurrencyExchangeDB;Integrated Security=true;";

        private static DLImp instance=null;
        private DLImp()
        {
        }
        static public DLImp theInstance()
        {
            if (instance == null)
                instance = new DLImp();
            return instance;
        }

        public async Task UpdateCurrencyPairValues(int pairId, decimal newValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateCommand = "UPDATE CurrencyPair SET MaxValue = @NewValue WHERE PairId = @PairId AND MaxValue < @NewValue;"
                + "UPDATE CurrencyPair SET MinValue = @NewValue WHERE PairId = @PairId AND MinValue > @NewValue;"
                + "UPDATE CurrencyPair SET Value = @NewValue WHERE PairId = @PairId;";
                SqlCommand command = new SqlCommand(updateCommand, connection);
                command.Parameters.AddWithValue("@PairId", pairId);
                command.Parameters.AddWithValue("@NewValue", newValue);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //retrieving data

        public async Task<List<TradingPair>> GetListOfTradingPair()
        {
            List<TradingPair> tradingPairs = new List<TradingPair>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
    CP.PairId,
    CP.BaseCurrencyId AS BaseCurrencyId,
    B.CurrencyName AS BaseCurrencyName,
    B.Country AS BaseCurrencyCountry,
    B.CurrencyAbbreviation AS BaseCurrencyAbbreviation,
    CP.TargetCurrencyId AS TargetCurrencyId,
    T.CurrencyName AS TargetCurrencyName,
    T.Country AS TargetCurrencyCountry,
    T.CurrencyAbbreviation AS TargetCurrencyAbbreviation,
    CP.Value,
    CP.MinValue,
    CP.MaxValue
FROM
    CurrencyPair CP
JOIN
    Currency B ON CP.BaseCurrencyId = B.CurrencyId
JOIN
    Currency T ON CP.TargetCurrencyId = T.CurrencyId;
                ";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TradingPair pair = new TradingPair
                        {
                            PairId = reader.GetInt32(0),
                            BaseCurrencyId = reader.GetInt32(1),
                            BaseCurrencyName = reader.GetString(2),
                            BaseCurrencyCountry = reader.GetString(3),
                            BaseCurrencyAbbreviation = reader.GetString(4),
                            TargetCurrencyId = reader.GetInt32(5),
                            TargetCurrencyName = reader.GetString(6),
                            TargetCurrencyCountry = reader.GetString(7),
                            TargetCurrencyAbbreviation = reader.GetString(8),
                            Value = reader.GetDecimal(9),
                            MinValue = reader.GetDecimal(10),
                            MaxValue = reader.GetDecimal(11)
                        };

                        tradingPairs.Add(pair);
                    }


                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new NoDataException(ex.Message);
                }
            }

            return tradingPairs;
        }
    }

}

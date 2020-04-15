using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Demo
{
    public class Ogloszenie
    {
        public long Id;
        public string Number;
        public string DataPublikacji;
        public string Miejscowosc;
        public string Plik;

        public void InsertAutomat()
        {
            var connectionString = new ConnectionStrings();

            //Console.WriteLine($"{Id}; { Number}; { DataPublikacji}; { Miejscowosc}; { Plik}");
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = connectionString.Bzp();
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = $@"
                 INSERT INTO Ads (Number, DataPublikacji, Miejscowosc, Plik)
                 VALUES ('{Number}', '{DataPublikacji}', '{Miejscowosc}', '{Plik}')";

            insertData.ExecuteNonQuery();
            connection2.Close();
        }
    }
}
//
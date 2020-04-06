using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Demo
{
    class Ogloszenie
    {
        public int Id;
        public long Number;
        public long DataPublikacji;
        public string Miejscowosc;
        public string Plik;

        public void InsertAutomat()
        {
            //Console.WriteLine($"{Id}; { Number}; { DataPublikacji}; { Miejscowosc}; { Plik}");
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = $@"
                 INSERT INTO Ads (Id, Number, DataPublikacji, Miejscowosc, Plik)
                 VALUES ({Id}, '{Number}', '{DataPublikacji}', '{Miejscowosc}', '{Plik}')";

            insertData.ExecuteNonQuery();
            connection2.Close();
        }

    }
}
//
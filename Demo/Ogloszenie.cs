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
            if (Id != 0)
            {
                throw new Exception();
            }

            var connectionString = new ConnectionStrings();

            //Console.WriteLine($"{Id}; { Number}; { DataPublikacji}; { Miejscowosc}; { Plik}");
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = connectionString.Bzp();
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = $@"
                INSERT INTO Ads (Number, DataPublikacji, Miejscowosc, Plik)
                OUTPUT Inserted.Id                 
                VALUES ('{Number}', '{DataPublikacji}', '{Miejscowosc}', '{Plik}')
                 ";

            var reader = insertData.ExecuteReader();
            reader.Read();
            Id = reader.GetInt64(0);
            
            connection2.Close();
        }
    }
}
//
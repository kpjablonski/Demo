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
        
        public void InsertAutomat()
        {
            if (Id != 0)
            {
                throw new Exception();
            }

            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = ConnectionStrings.Bzp();
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = $@"
                INSERT INTO Ads (Number, DataPublikacji)
                OUTPUT Inserted.Id                 
                VALUES ('{Number}', '{DataPublikacji}')
                 ";

            var reader = insertData.ExecuteReader();
            reader.Read();
            Id = reader.GetInt64(0);
            
            connection2.Close();
        }
    }
}
//
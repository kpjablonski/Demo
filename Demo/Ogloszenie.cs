using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Demo
{
    class Ogloszenie
    {
        public int Id = 4;
        public long Number = 124124124;
        public long DataPublikacji = 5456432020;
        public string Miejscowosc = "Warszawa";
        public string Plik = "Pobierz";
        private object ogl;

        public void InsertAutomat()
        {
            Console.WriteLine($"{Id}; { Number}; { DataPublikacji}; { Miejscowosc}; { Plik}");
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = "Server=localhost\\SQLExpress;Database=Bzp;Trusted_Connection=True;";
            connection2.Open();

            var insertData = new SqlCommand();
            insertData.Connection = connection2;
            insertData.CommandText = $@"
                INSERT INTO Ads (Id, Number, DataPublikacji, Miejscowosc, Plik)
                VALUES ({ogl.Id}, '{ogl.Number}', {ogl.DataPublikacji}, {ogl.Miejscowosc}, {ogl.Plik})";

            insertData.ExecuteNonQuery();
            connection2.Close();
        }

    }
}
//
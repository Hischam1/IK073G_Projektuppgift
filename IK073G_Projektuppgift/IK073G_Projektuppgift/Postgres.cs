using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

namespace IK073G_Projektuppgift
{

    class Postgres
    {
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private NpgsqlDataReader dr;
        private DataTable tabell;
        public Person aktuellPerson;

        //Kontaktar databasen.
        public Postgres()
        {
            conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432;Database=interaktiva_g20;User Id=interaktiva_g20;Password=banken;Database= interaktiva_g20;SslMode=Require;trustServerCertificate=true;Pooling=false");

            try
            {
                conn.Open();
            }
            catch (NpgsqlException ex)
            {
                string meddelande = ex.Message;
                if (meddelande.Contains("53300"))
                {
                    meddelande = "Kan inte kontakta databasen, vänligen försök igen lite senare.";
                }
                //MessageBox.Show(meddelande);
            }
            tabell = new DataTable();

        }

        //Test av fråga.
        public DataTable sqlFråga(string sql)
        {

            try
            {

                cmd = new NpgsqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                tabell.Load(dr);
                return tabell;

            }
            catch (NpgsqlException ex)
            {
                string meddelande = ex.Message;
                if (meddelande.Contains("53300"))
                {
                    meddelande = "Kan inte kontakta databasen, vänligen försök igen lite senare.";
                }
                //MessageBox.Show(meddelande);


                DataColumn c1 = new DataColumn("Error");
                DataColumn c2 = new DataColumn("ErrorMeddelande");


                c1.DataType = System.Type.GetType("System.Boolean");
                c2.DataType = System.Type.GetType("System.String");

                tabell.Columns.Add(c1);
                tabell.Columns.Add(c2);

                DataRow rad = tabell.NewRow();
                rad[c1] = true;
                rad[c2] = ex.Message;
                tabell.Rows.Add(rad);


                return tabell;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Person> HämtaNyanställda()
        {
            string sql = "select * from person WHERE nyanställd = true ORDER BY förnamn";

            tabell.Clear();
            tabell = sqlFråga(sql);
            List<Person> NyanställdLista = new List<Person>();
            Person p;

            foreach (DataRow rad in tabell.Rows)
            {
                p = new Person();

                p.anställningsID = (int)rad[0];
                p.förnamn = rad[1].ToString();
                p.efternamn = rad[2].ToString();
                p.telefonnummer = rad[3].ToString();
                p.nyanställd = (bool)rad[4];
                p.anställd = (bool)rad[5];
                p.admin = (bool)rad[6];

                NyanställdLista.Add(p);


            }
            return NyanställdLista;
        }
        public List<Person> HämtaAnställda()
        {
            string sql = "select * from person WHERE anställd = true ORDER BY förnamn";

            tabell.Clear();
            tabell = sqlFråga(sql);
            List<Person> AnställdLista = new List<Person>();
            Person p;

            foreach (DataRow rad in tabell.Rows)
            {
                p = new Person();

                p.anställningsID = (int)rad[0];
                p.förnamn = rad[1].ToString();
                p.efternamn = rad[2].ToString();
                p.telefonnummer = rad[3].ToString();
                p.nyanställd = (bool)rad[4];
                p.anställd = (bool)rad[5];
                p.admin = (bool)rad[6];

                AnställdLista.Add(p);


            }
            return AnställdLista;
        }
        public List<Person> HämtaAdmin()
        {
            string sql = "select * from person WHERE admin = true ORDER BY förnamn";

            tabell.Clear();
            tabell = sqlFråga(sql);
            List<Person> AdminLista = new List<Person>();
            Person p;

            foreach (DataRow rad in tabell.Rows)
            {
                p = new Person();

                p.anställningsID = (int)rad[0];
                p.förnamn = rad[1].ToString();
                p.efternamn = rad[2].ToString();
                p.telefonnummer = rad[3].ToString();
                p.nyanställd = (bool)rad[4];
                p.anställd = (bool)rad[5];
                p.admin = (bool)rad[6];

                AdminLista.Add(p);


            }
            return AdminLista;
        }

    }
}

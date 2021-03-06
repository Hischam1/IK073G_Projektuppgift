﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace IK073G_Projektuppgift
{

    class Postgres
    {
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private NpgsqlDataReader dr;
        private DataTable tabell;
        

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
        public void StängConnection()
        {
            conn.Close();
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
                p.klaratProv = (bool)rad[7];

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
        public List<Prov> HämtaProvResultat()
        {
            string sql = "select förnamn, efternamn, prov_typ, prov_datum, prov_status, prov_antal_rätt, prov_totaltid_minuter from prov, person WHERE prov.provdeltagare = person.anställnings_id ORDER BY person.förnamn";

            tabell.Clear();
            tabell = sqlFråga(sql);
            List<Prov> ProvResultatLista = new List<Prov>();
            Prov p;

            foreach (DataRow rad in tabell.Rows)
            {
                p = new Prov();

                p.förnamn = rad[0].ToString();
                p.efternamn = rad[1].ToString();
                p.provTyp = rad[2].ToString();
                p.datum = (DateTime)rad[3];
                p.provStatus = rad[4].ToString();
                p.antalRätt = (int)rad[5];
                p.totalTid = (int)rad[6];

                ProvResultatLista.Add(p);


            }
            return ProvResultatLista;
        }
        public void LäggTillProv(int provID, int provDeltagareID, string provTyp, DateTime datum, string provStatus, int antalRätt, int provTotalTidMinuter, int kat1, int kat2, int kat3, string xmlstring)
        {
            string meddelande;
            try
            {
                string sql = "insert into prov (prov_id, provdeltagare, prov_typ, prov_datum, prov_status, prov_antal_rätt, prov_totaltid_minuter, prov_kat1_rätt, prov_kat2_rätt, prov_kat3_rätt, xmlprov)"
                   + " values (@prov_id, @provdeltagare, @prov_typ, @prov_datum, @prov_status, @prov_antal_rätt, @prov_totaltid_minuter, @prov_kat1_rätt, @prov_kat2_rätt, @prov_kat3_rätt, @xmlprov)";

                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@prov_id", provID);
                cmd.Parameters.AddWithValue("@provdeltagare", provDeltagareID);
                cmd.Parameters.AddWithValue("@prov_typ", provTyp);
                cmd.Parameters.AddWithValue("@prov_datum", datum);
                cmd.Parameters.AddWithValue("@prov_status", provStatus);
                cmd.Parameters.AddWithValue("@prov_antal_rätt", antalRätt);
                cmd.Parameters.AddWithValue("@prov_totaltid_minuter", provTotalTidMinuter);
                cmd.Parameters.AddWithValue("@prov_kat1_rätt", kat1);
                cmd.Parameters.AddWithValue("@prov_kat2_rätt", kat2);
                cmd.Parameters.AddWithValue("@prov_kat3_rätt", kat3);
                cmd.Parameters.AddWithValue("@xmlprov", xmlstring);


                dr = cmd.ExecuteReader();
                dr.Close();
            }
            catch (NpgsqlException ex)
            {
                meddelande = ex.Message;
                if (meddelande.Contains("23505"))
                {
                    meddelande = "fel";
                }
                
            }

            conn.Close();

        }
        public void LäggTillXMLString(string XmlString)
        {
            string meddelande;
            try
            {
                string sql = "insert into XMLtest (xmlstring)"
                   + " values (@xmlstring)";

                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@xmlstring", XmlString);

                dr = cmd.ExecuteReader();
                dr.Close();
            }
            catch (NpgsqlException ex)
            {
                meddelande = ex.Message;
                if (meddelande.Contains("23505"))
                {
                    meddelande = "fel";
                }

            }

            conn.Close();

        }
        public void HämtaXmlFrånDatabas(XmlDocument doc, int idnummer, int provId)
        {
            string meddelande;
            try
            {
                string sql = "select xmlprov from prov where prov.provdeltagare = '" + idnummer + "' AND prov.prov_id = '" + provId + "'";


                cmd = new NpgsqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("xmlstring", "hejjj");

                dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    if (dr["xmlprov"].ToString() != "")
                    {
                        doc.LoadXml(dr["xmlprov"].ToString());
                    }
                    //else
                    //{
                    //    string path = HttpContext.Current.Server.MapPath("aktuelltprov.xml");
                    //}
                }

                dr.Close();
            }
            catch (NpgsqlException ex)
            {
                meddelande = ex.Message;
                if (meddelande.Contains("23505"))
                {
                    meddelande = "fel";
                }

            }

            conn.Close();

        }
        public void HämtaXmlFrånDatabas2(XmlDocument doc, int idnummer)
        {
            string meddelande;
            try
            {
                string sql = "select xmlprov from prov where prov.provdeltagare = '" + idnummer + "' order by prov_datum desc";


                cmd = new NpgsqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("xmlstring", "hejjj");

                dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    if (dr["xmlprov"].ToString() != "")
                    {
                        doc.LoadXml(dr["xmlprov"].ToString());
                    }
                    //else
                    //{
                    //    string path = HttpContext.Current.Server.MapPath("aktuelltprov.xml");
                    //}
                }

                dr.Close();
            }
            catch (NpgsqlException ex)
            {
                meddelande = ex.Message;
                if (meddelande.Contains("23505"))
                {
                    meddelande = "fel";
                }

            }

            conn.Close();

        }
        public Prov HämtaResultat(int personId)
        {

                string sql = "select prov_typ, prov_datum, prov_status, prov_antal_rätt, prov_kat1_rätt, prov_kat2_rätt, prov_kat3_rätt from prov where prov.provdeltagare = '" + personId + "'";

            tabell.Clear();
            tabell = sqlFråga(sql);
        
            Prov p = new Prov();

            foreach (DataRow rad in tabell.Rows)
            {

                p.provTyp = rad[0].ToString();
                p.datum = (DateTime)rad[1];
                p.provStatus = rad[2].ToString();
                p.antalRätt = (int)rad[3];
                p.kat1Rätt = (int)rad[4];
                p.kat2Rätt = (int)rad[5];
                p.kat3Rätt = (int)rad[6];

            }

            return p;

        }
        public void UppdateraPerson(bool klaratProv, bool anställd, bool nyanställd, int anställningsID)
        {

            try
            {
                string sql = "update person SET klaratprov = '" + klaratProv + "', anställd ='" + anställd + "', nyanställd ='" + nyanställd + "' where anställnings_id = '" + anställningsID + "'";

                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@klaratprov", klaratProv);
                cmd.Parameters.AddWithValue("@anställd", anställd);
                cmd.Parameters.AddWithValue("@nyanställd", nyanställd);

                dr = cmd.ExecuteReader();
                dr.Close();


            }
            catch 
            {
                
            }

            conn.Close();
        }

    }
}

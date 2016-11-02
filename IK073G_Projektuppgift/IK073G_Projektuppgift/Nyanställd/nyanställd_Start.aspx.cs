﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace IK073G_Projektuppgift
{
    public partial class nyanställd : System.Web.UI.Page
    {
        List<QA> AllaFrågor = new List<QA>();
        List<QA> AllaQALista = new List<QA>();
        List<QA> PåbörjadFråga = new List<QA>();
        List<QA> BesvaradeFrågor = new List<QA>();
        List<Person> AllaNyAnställda = new List<Person>();
        QA aktuellFråga = new QA();
        Person aktuellPerson = new Person();

        Postgres p = new Postgres();

        int resultat;
        int[] allaResultat = new int[4];
        int fråganummer = 1;
        string användarenSvar1;
        string användarenSvar2;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                AllaNyAnställda = p.HämtaNyanställda();
                nyanställningsLista.DataSource = AllaNyAnställda;
                nyanställningsLista.DataBind();
                Session["AllaNyanställda"] = AllaNyAnställda;
            }

            p.StängConnection();

            if (Session["AllaFrågor"] != null)
            {
                AllaFrågor = (List<QA>)Session["AllaFrågor"];
            }
            if (Session["AktuellPerson"] != null)
            {
                aktuellPerson = (Person)Session["AktuellPerson"];
            }
            if (Session["resultat"] != null)
            {
                resultat = (int)Session["resultat"];
            }
            if (Session["AllaResultat"] != null)
            {
                allaResultat = (int[])Session["AllaResultat"];
            }
        }
        public void VisaAllt(List<QA> QALista)
        {
            Random r = new Random();
            var blandadLista = QALista.OrderBy(c => r.Next()).ToList();

            foreach (QA qa in blandadLista)
            {

                HtmlGenericControl kategoriFråga = new HtmlGenericControl("p class=frågaKategori");
                kategoriFråga.InnerText = qa.kategori;
                fråga.Controls.Add(kategoriFråga);

                HtmlGenericControl bildFråga = new HtmlGenericControl("img src= '" + qa.bild + "' class=bildFråga width=20%");
                fråga.Controls.Add(bildFråga);

                HtmlGenericControl rubrikFråga = new HtmlGenericControl("p class=frågaRubrik id=hej runat=server");
                rubrikFråga.InnerText = qa.fråga;
                fråga.Controls.Add(rubrikFråga);

                HtmlGenericControl divText = new HtmlGenericControl("div class=text");
                divText.InnerText = qa.text;
                fråga.Controls.Add(divText);
            }
        }
        public List<QA> XmlTillLista()
        {
            AllaQALista.Clear();

            string path = Server.MapPath("../Q&A.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList allaFrågorOchSvar = doc.SelectNodes("/bank/Frågor/FrågorLicensiering/Frågenummer");


            foreach (XmlNode node in allaFrågorOchSvar)
            {
                QA qa = new QA();
                qa.kategori = node["Kategori"].InnerXml;
                qa.typ = node["Typ"].InnerXml;
                qa.text = node["Text"].InnerXml;
                qa.bild = node["Bild"].InnerXml;
                qa.fråga = node["Fråga"].InnerXml;
                qa.svar1 = node["Svar1"].InnerXml;
                qa.svar2 = node["Svar2"].InnerXml;
                qa.svar3 = node["Svar3"].InnerXml;
                qa.svar4 = node["Svar4"].InnerXml;
                qa.rättSvar1 = node["RättSvar1"].InnerXml;
                qa.rättSvar2 = node["RättSvar2"].InnerXml;


                AllaQALista.Add(qa);
            }

            return AllaQALista;

        }
        public List<QA> XmlTillLista2(XmlDocument xmldoc)
        {
            AllaQALista.Clear();

            //string path = Server.MapPath("../Q&A.xml");
            XmlDocument doc = xmldoc;
            //doc.Load(path);

            XmlNodeList allaFrågorOchSvar = doc.SelectNodes("/Frågor/Frågenummer");


            foreach (XmlNode node in allaFrågorOchSvar)
            {
                QA qa = new QA();
                qa.kategori = node["Kategori"].InnerXml;
                qa.typ = node["Typ"].InnerXml;
                qa.bild = node["Bild"].InnerXml;
                qa.fråga = node["Fråga"].InnerXml;
                qa.svar1 = node["Svar1"].InnerXml;
                qa.svar2 = node["Svar2"].InnerXml;
                qa.svar3 = node["Svar3"].InnerXml;
                qa.svar4 = node["Svar4"].InnerXml;
                qa.rättSvar1 = node["RättSvar1"].InnerXml;
                qa.rättSvar2 = node["RättSvar2"].InnerXml; 
                //qa.rättSvar2 = node["användarenSvar2"].InnerXml;
                //qa.rättSvar2 = node["användarenSvar2"].InnerXml;


                AllaQALista.Add(qa);
            }

            return AllaQALista;

        }
        public void TaUtEnFråga()
        {

            aktuellFråga = null;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, AllaFrågor.Count);

            CheckBox1.Text = AllaFrågor[randomIndex].svar1;
            CheckBox2.Text = AllaFrågor[randomIndex].svar2;
            CheckBox3.Text = AllaFrågor[randomIndex].svar3;
            CheckBox4.Text = AllaFrågor[randomIndex].svar4;

            aktuellFråga = AllaFrågor[randomIndex];
            Session["AktuellFråga"] = aktuellFråga;

            PåbörjadFråga.Add(AllaFrågor[randomIndex]);
            Session["PåbörjadFråga"] = PåbörjadFråga;
            //namnet.InnerHtml = randomIndex.ToString() + " " + AllaFrågor.Count.ToString() + " " + AllaFrågor[randomIndex].rättSvar1;
            AllaFrågor.RemoveAt(randomIndex);

        }

        protected void startaNyttTest_Click(object sender, EventArgs e)
        {
            AllaFrågor = XmlTillLista();
            Session["AllaFrågor"] = AllaFrågor;

            if (Session["AllaNyanställda"] != null)
            {
                AllaNyAnställda = (List<Person>)Session["AllaNyanställda"];
            }

            for (int i = 0; i < AllaNyAnställda.Count; i++)
            {
                if (AllaNyAnställda[i].förnamn + " " + AllaNyAnställda[i].efternamn == nyanställningsLista.SelectedValue.ToString())
                {
                    aktuellPerson = AllaNyAnställda[i];
                }
            }

            Session["AktuellPerson"] = aktuellPerson;

            provText.InnerHtml = aktuellPerson.förnamn;

            TaUtEnFråga();
            VisaAllt(PåbörjadFråga);



            if (Session["AktuellFråga"] != null)
            {
                aktuellFråga = (QA)Session["AktuellFråga"];
            }

            nästaSida1.Visible = true;
            CheckBox1.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = true;
            CheckBox4.Visible = true;
            frågenummer.Visible = true;
            //provText.Visible = false;
            startaNyttTest.Visible = false;
            nyanställningsLista.Visible = false;
            namnet.Visible = false;
            //provText.InnerHtml = "";
            frågenummer.InnerHtml = "Fråga: " + fråganummer + " av 25";

        }

        protected void nästaSida1_Click(object sender, EventArgs e)
        {
            CheckBox[] checkArray = new CheckBox[] { CheckBox1, CheckBox2, CheckBox3, CheckBox4 };

            if (Session["fråganummer"] != null)
            {
                fråganummer = (int)Session["fråganummer"];
            }

            if (Session["AktuellFråga"] != null)
            {
                aktuellFråga = (QA)Session["AktuellFråga"];
            }


            if (aktuellFråga.typ == "EttRätt")
            {
                int kollaCheckbox = 0;

                for (int i = 0; i < checkArray.Length; i++)
                {
                    if (checkArray[i].Checked == true)
                    {
                        kollaCheckbox += 1;
                    }
                }

                if (kollaCheckbox > 1 || kollaCheckbox < 1)
                {
                    if (Session["PåbörjadFråga"] != null)
                    {
                        PåbörjadFråga = (List<QA>)Session["PåbörjadFråga"];
                    }
                    provText.InnerHtml = "Du har angett fel antal svarsalternativ.";

                    VisaAllt(PåbörjadFråga);

                }
                else
                {
                    foreach (CheckBox checkbox in checkArray)
                    {
                        if (checkbox.Checked == true)
                        {
                            if (checkbox.Text == aktuellFråga.rättSvar1)
                            {
                                if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                                {
                                    allaResultat[0] += 1; // TotalResultat
                                    allaResultat[1] += 1; // Kategori 1
                                }
                                else if (aktuellFråga.kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
                                {
                                    allaResultat[0] += 1; // TotalResultat
                                    allaResultat[2] += 1; // Kategori 2
                                }
                                else if (aktuellFråga.kategori == "Etik och regelverk")
                                {
                                    allaResultat[0] += 1; // TotalResultat
                                    allaResultat[3] += 1; // Kategori 3
                                }
                                användarenSvar1 = checkbox.Text;
                                Session["användarenSvar1"] = användarenSvar1;
                            }
                            provText.InnerHtml = allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                            AvslutAvFråga();
                        }
                    }
                }

                }

            else if (aktuellFråga.typ == "TvåRätt")
            {
                int kollaCheckbox = 0;

                for (int i = 0; i < checkArray.Length; i++)
                {
                    if (checkArray[i].Checked == true)
                    {
                        kollaCheckbox += 1;
                    }
                }

                if (kollaCheckbox > 2 || kollaCheckbox < 2)
                {
                    if (Session["PåbörjadFråga"] != null)
                    {
                        PåbörjadFråga = (List<QA>)Session["PåbörjadFråga"];
                    }
                    provText.InnerHtml = "Du har angett fel antal svarsalternativ.";

                    VisaAllt(PåbörjadFråga);

                }
                else
                {
                    int tvåRätt = 0;

                    foreach (CheckBox checkbox in checkArray)
                    {
                        if (checkbox.Checked == true)
                        {
                            if (checkbox.Text == aktuellFråga.rättSvar1 || checkbox.Text == aktuellFråga.rättSvar2)
                            {
                                tvåRätt += 1;

                            }
                        }
                    }
                    if (tvåRätt == 2)
                    {
                        if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                        {
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[1] += 1; // Kategori 1
                        }
                        else if (aktuellFråga.kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
                        {
                            resultat += 1;
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[2] += 1; // Kategori 2
                        }
                        else if (aktuellFråga.kategori == "Etik och regelverk")
                        {
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[3] += 1; // Kategori 3
                        }

                        användarenSvar1 = aktuellFråga.rättSvar1;
                        Session["användarenSvar1"] = användarenSvar1;

                        användarenSvar2 = aktuellFråga.rättSvar2;
                        Session["användarenSvar2"] = användarenSvar2;
                    }
                    provText.InnerHtml = allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                    AvslutAvFråga();
                }

            }
    }

        protected void avslutaProv_Click(object sender, EventArgs e)
        {
            XmlDocument docc = new XmlDocument();
            docc = DatabasTillXml();

            VisaAllt(XmlTillLista2(docc));

            //Postgres p = new Postgres();
            //Random rnd = new Random();
            //int provId = rnd.Next(1, 1000);

            //string godkänd;

            //double procent = (double)allaResultat[0] / 25 * 100;
            //double procentKat1 = (double)allaResultat[1] / 8 * 100;
            //double procentKat2 = (double)allaResultat[2] / 8 * 100;
            //double procentKat3 = (double)allaResultat[3] / 9 * 100;

            //kategori1.Visible = true;
            //kategori2.Visible = true;
            //kategori3.Visible = true;
            //status.Visible = true;



            //if (procent > 69 && procentKat1 > 59 && procentKat2 > 59 && procentKat3 > 59)
            //{
            //    godkänd = "GODKÄND";
            //    frågenummer.InnerText = "Ditt totalresultat är " + allaResultat[0].ToString() + " av 25 poäng vilket är " + procent + "%";
            //    status.InnerHtml = godkänd;
            //    kategori1.InnerHtml = "Kategori 1: " + allaResultat[1].ToString() + " av 8 poäng vilket är " + procentKat1 + "%";
            //    kategori2.InnerHtml = "Kategori 2: " + allaResultat[2].ToString() + " av 8 poäng vilket är " + procentKat2 + "%";
            //    kategori3.InnerHtml = "Kategori 3: " + allaResultat[3].ToString() + " av 9 poäng vilket är " + procentKat3 + "%";
            //}
            //else
            //{
            //    godkänd = "ICKE GODKÄND";
            //    frågenummer.InnerText = "Ditt totalresultat är " + allaResultat[0].ToString() + " av 25 poäng vilket är " + procent + "%";
            //    status.InnerHtml = godkänd;
            //    kategori1.InnerHtml = "Kategori 1: " + allaResultat[1].ToString() + " av 8 poäng vilket är " + procentKat1 + "%";
            //    kategori2.InnerHtml = "Kategori 2: " + allaResultat[2].ToString() + " av 8 poäng vilket är " + procentKat2 + "%";
            //    kategori3.InnerHtml = "Kategori 3: " + allaResultat[3].ToString() + " av 9 poäng vilket är " + procentKat3 + "%";
            //}


            //p.LäggTillProv(provId, aktuellPerson.anställningsID, "LICENS", DateTime.Today, godkänd, allaResultat[0], 22, allaResultat[1], allaResultat[2], allaResultat[3]);
            //p.StängConnection();


        }

        public void AvslutAvFråga()
        {
            if (Session["användarenSvar1"] != null)
            {
                användarenSvar1 = (string)Session["användarenSvar1"];
            }
            if (Session["användarenSvar2"] != null)
            {
                användarenSvar2 = (string)Session["användarenSvar2"];
            }


            string path = Server.MapPath("../aktuelltprov.xml");

            try
            {
                XDocument prov = XDocument.Load(path);

                XElement xmlElement = new XElement("Frågenummer",
                    new XElement("Kategori", aktuellFråga.kategori),
                    new XElement("Typ", aktuellFråga.typ),
                    new XElement("Bild", aktuellFråga.bild),
                    new XElement("Fråga", aktuellFråga.fråga),
                    new XElement("Svar1", aktuellFråga.svar1),
                    new XElement("Svar2", aktuellFråga.svar2),
                    new XElement("Svar3", aktuellFråga.svar3),
                    new XElement("Svar4", aktuellFråga.svar4),
                    new XElement("RättSvar1", aktuellFråga.rättSvar1),
                    new XElement("RättSvar2", aktuellFråga.rättSvar2),
                    new XElement("användarenSvar1", användarenSvar1),
                    new XElement("användarenSvar2", användarenSvar2)
                    );
                prov.Element("Frågor").Add(xmlElement);
                prov.Save(path);
            }
            catch
            {

            }

            if (AllaFrågor.Count != 0)
            {
                if (AllaFrågor.Count == 1)
                {
                    nästaSida1.Text = "Avsluta prov";
                }
                Session["AllaResultat"] = allaResultat;
                fråganummer += 1;
                Session["fråganummer"] = fråganummer;
                TaUtEnFråga();
                VisaAllt(PåbörjadFråga);
                frågenummer.InnerHtml = "Fråga: " + fråganummer + " av 25";
            }
            else
            {
                CheckBox1.Visible = false; CheckBox2.Visible = false; CheckBox3.Visible = false; CheckBox4.Visible = false;
                frågenummer.InnerText = "Nu är du klar med provet. Tryck på rätta för att få reda på ditt resultat";
                nästaSida1.Visible = false;
                avslutaProv.Visible = true;
                avslutaProv.Text = "Rätta";

                Postgres p = new Postgres();
                p.LäggTillXMLString(XmlTillDataBas());
                p.StängConnection();                

            }
        }

        public string XmlTillDataBas()
        {
            string path = Server.MapPath("../aktuelltprov.xml");

            XDocument doc = XDocument.Load(path, LoadOptions.None);

            string xmlstring = doc.ToString();

            return xmlstring;
         }

        public XmlDocument DatabasTillXml()
        {
            XmlDocument doc = new XmlDocument();
            Postgres p = new Postgres();

            p.HämtaXmlFrånDatabas(doc);


            return doc;
        }
    }
}

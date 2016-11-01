using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;

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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                AllaNyAnställda = p.HämtaNyanställda();
                nyanställningsLista.DataSource = AllaNyAnställda;
                nyanställningsLista.DataBind();
                Session["AllaNyanställda"] = AllaNyAnställda;
            }

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
                qa.rättSvar3 = node["RättSvar3"].InnerXml;


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
            int kollaCheckbox = new int();

            if (Session["fråganummer"] != null)
            {
                fråganummer = (int)Session["fråganummer"];
            }

            if (Session["AktuellFråga"] != null)
            {
                aktuellFråga = (QA)Session["AktuellFråga"];
            }

            foreach (CheckBox checkbox in checkArray)
            {
                if (checkbox.Checked == true)
                {
                    kollaCheckbox += 1;
                }
            }

            if (kollaCheckbox > 1 /*|| kollaCheckbox == 0*/)
            {
                provText.InnerHtml = "Markera rätt antal checkboxar";

                //if (Session["PåbörjadFråga"] != null)
                //{
                //    PåbörjadFråga = (List<QA>)Session["PåbörjadFråga"];
                //}
                //provText.InnerHtml = "Inget svarsalternativ är markerat \n Markera ett svarsalternativ";
                //VisaAllt(PåbörjadFråga);
            }
            else
            {

            if (aktuellFråga.typ == "EttRätt")
            {
                if (CheckBox1.Checked == true)
                {
                    if (CheckBox1.Text == aktuellFråga.rättSvar1)
                    {
                        if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                        {
                            resultat += 1;
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
                            resultat += 1;
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[3] += 1; // Kategori 3
                        }
                    }
                    provText.InnerHtml = resultat.ToString() + " = " + allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                }
                else if (CheckBox2.Checked == true)
                {
                    if (CheckBox2.Text == aktuellFråga.rättSvar1)
                    {
                        if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                        {
                            resultat += 1;
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
                            resultat += 1;
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[3] += 1; // Kategori 3
                        }
                    }
                    provText.InnerHtml = resultat.ToString() + " = " + allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                }
                else if (CheckBox3.Checked == true)
                {
                    if (CheckBox3.Text == aktuellFråga.rättSvar1)
                    {
                        if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                        {
                            resultat += 1;
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
                            resultat += 1;
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[3] += 1; // Kategori 3
                        }
                    }
                    provText.InnerHtml = resultat.ToString() + " = " + allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                }
                else if (CheckBox4.Checked == true)
                {
                    if (CheckBox4.Text == aktuellFråga.rättSvar1)
                    {
                        if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
                        {
                            resultat += 1;
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
                            resultat += 1;
                            allaResultat[0] += 1; // TotalResultat
                            allaResultat[3] += 1; // Kategori 3
                        }
                    }
                    provText.InnerHtml = resultat.ToString() + " = " + allaResultat[0].ToString() + " = " + allaResultat[1].ToString() + " = " + allaResultat[2].ToString() + " = " + allaResultat[3].ToString();
                }
            }

            }

            //else if (aktuellFråga.typ == "TvåRätt")
            //    {

            //    if (CheckBox1.Checked == true)
            //    {
            //        if (CheckBox1.Text == aktuellFråga.rättSvar1 || CheckBox1.Text == aktuellFråga.rättSvar2)
            //        {
            //            if (CheckBox2.Checked == true)
            //            {
            //                if (CheckBox2.Text == aktuellFråga.rättSvar1 || CheckBox2.Text == aktuellFråga.rättSvar2)
            //                {
            //                    if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[1] += 1; // Kategori 1
            //                    }
            //                    else if (aktuellFråga.kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[2] += 1; // Kategori 2
            //                    }
            //                    else if (aktuellFråga.kategori == "Etik och regelverk")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[3] += 1; // Kategori 3
            //                    }
            //                }
            //            }
            //            if (CheckBox3.Checked == true)
            //            {
            //                if (CheckBox3.Text == aktuellFråga.rättSvar1 || CheckBox3.Text == aktuellFråga.rättSvar2)
            //                {
            //                    if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[1] += 1; // Kategori 1
            //                    }
            //                    else if (aktuellFråga.kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[2] += 1; // Kategori 2
            //                    }
            //                    else if (aktuellFråga.kategori == "Etik och regelverk")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[3] += 1; // Kategori 3
            //                    }
            //                }
            //            }
            //            if (CheckBox4.Checked == true)
            //            {
            //                if (CheckBox4.Text == aktuellFråga.rättSvar1 || CheckBox4.Text == aktuellFråga.rättSvar2)
            //                {
            //                    if (aktuellFråga.kategori == "Produkter och hantering av kundens affärer")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[1] += 1; // Kategori 1
            //                    }
            //                    else if (aktuellFråga.kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[2] += 1; // Kategori 2
            //                    }
            //                    else if (aktuellFråga.kategori == "Etik och regelverk")
            //                    {
            //                        resultat += 1;
            //                        allaResultat[0] += 1; // TotalResultat
            //                        allaResultat[3] += 1; // Kategori 3
            //                    }
            //                }
            //            }

            //        }
            //    }

            //    provText.InnerHtml = "Japp";

            //    }



            if (CheckBox1.Checked == false && CheckBox2.Checked == false && CheckBox3.Checked == false && CheckBox4.Checked == false)
            {
                if (Session["PåbörjadFråga"] != null)
                {
                    PåbörjadFråga = (List<QA>)Session["PåbörjadFråga"];
                }
                provText.InnerHtml = "Inget svarsalternativ är markerat \n Markera ett svarsalternativ";
                VisaAllt(PåbörjadFråga);
            }
            else
            {
                if (AllaFrågor.Count != 0)
                    {
                        if (AllaFrågor.Count == 1)
                        {
                            nästaSida1.Text = "Avsluta prov";
                        }
                        Session["resultat"] = resultat;
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
                    }
        }
    }

        protected void avslutaProv_Click(object sender, EventArgs e)
        {
            Postgres p = new Postgres();
            Random rnd = new Random();
            int provId = rnd.Next(1, 1000);

            string godkänd;

            double procent = (double)allaResultat[0] / 25 * 100;

            if (procent > 69)
            {
                godkänd = "GODKÄND";
                p.LäggTillProv(provId, aktuellPerson.anställningsID, "LICENS", DateTime.Today, godkänd, resultat, 22, allaResultat[1], allaResultat[2], allaResultat[3]);
            }
            else
            {
                godkänd = "ICKE GODKÄND";

            }

            frågenummer.InnerText = "Ditt totalresultat är " + resultat + " av 25 poäng vilket betyder " + procent + "%";
            kategorier.InnerHtml = "Kategori 1: " + allaResultat[1].ToString() + "Kategori 2: " + allaResultat[2].ToString() + "Kategori 3: " + allaResultat[3].ToString();

            

            //double procent = (double)resultat / 25 * 100;

            //if (procent > 69)
            //{
            //    godkänd = "GODKÄND";
            //}
            //else
            //{
            //    godkänd = "ICKE GODKÄND";
            //}

            //frågenummer.InnerText = "Ditt resultat är " + resultat + " av 25 poäng vilket betyder " + procent + "%";

            //p.LäggTillProv(provId, aktuellPerson.anställningsID, "LICENS", DateTime.Today, godkänd, resultat, 22);
        }
    }
}

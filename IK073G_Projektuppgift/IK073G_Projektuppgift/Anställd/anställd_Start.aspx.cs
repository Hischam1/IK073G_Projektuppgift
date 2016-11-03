using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace IK073G_Projektuppgift.Anställd
{
    public partial class anställd_Start : System.Web.UI.Page
    {
        List<QA> AllaFrågor = new List<QA>();
        List<QA> AllaQALista = new List<QA>();
        List<QA> PåbörjadFråga = new List<QA>();
        List<Person> AllaAnställda = new List<Person>();
        QA aktuellFråga = new QA();
        Person aktuellPerson = new Person();
        int nuvarandeProvId = new int();

        Postgres p = new Postgres();

        int resultat;
        int[] allaResultat = new int[4];
        int fråganummer = 1;
        string användarenSvar1;
        string användarenSvar2;
        string användarenSvar3;
        XDocument aktuelltProv = new XDocument();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AllaAnställda = p.HämtaAnställda();
                anställningsLista.DataSource = AllaAnställda;
                anställningsLista.DataBind();
                Session["AllaAnställda"] = AllaAnställda;

                XElement xmlElement = new XElement("Frågor");
                aktuelltProv.Add(xmlElement);
                Session["aktuelltProv"] = aktuelltProv;
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
        public void VisaResultat(List<QA> QALista)
        {

            foreach (QA qa in QALista)
            {
                HtmlGenericControl varjeFråga = new HtmlGenericControl("div id=varjeFråga");
                fråga.Controls.Add(varjeFråga);

                HtmlGenericControl kategoriFråga = new HtmlGenericControl("p class=frågaKategori");
                kategoriFråga.InnerText = qa.kategori;
                varjeFråga.Controls.Add(kategoriFråga);

                HtmlGenericControl bildFråga = new HtmlGenericControl("img src= '" + qa.bild + "' class=bildFråga width=20%");
                varjeFråga.Controls.Add(bildFråga);

                HtmlGenericControl rubrikFråga = new HtmlGenericControl("p class=frågaRubrik id=hej runat=server");
                rubrikFråga.InnerText = qa.fråga;
                varjeFråga.Controls.Add(rubrikFråga);

                HtmlGenericControl divText = new HtmlGenericControl("div class=text");
                divText.InnerText = qa.text;
                varjeFråga.Controls.Add(divText);

                HtmlGenericControl ul = new HtmlGenericControl("ul");
                varjeFråga.Controls.Add(ul);

                HtmlGenericControl li1 = new HtmlGenericControl("li");
                ul.Controls.Add(li1);

                HtmlGenericControl li2 = new HtmlGenericControl("li");
                ul.Controls.Add(li2);

                HtmlGenericControl li3 = new HtmlGenericControl("li");
                ul.Controls.Add(li3);

                HtmlGenericControl li4 = new HtmlGenericControl("li");
                ul.Controls.Add(li4);

                HtmlGenericControl checkboxSvar1 = new HtmlGenericControl("input type = checkbox");
                checkboxSvar1.InnerText = qa.svar1;
                li1.Controls.Add(checkboxSvar1);

                HtmlGenericControl checkboxSvar2 = new HtmlGenericControl("input type = checkbox");
                checkboxSvar2.InnerText = qa.svar2;
                li2.Controls.Add(checkboxSvar2);

                HtmlGenericControl checkboxSvar3 = new HtmlGenericControl("input type = checkbox");
                checkboxSvar3.InnerText = qa.svar3;
                li3.Controls.Add(checkboxSvar3);

                HtmlGenericControl checkboxSvar4 = new HtmlGenericControl("input type = checkbox");
                checkboxSvar4.InnerText = qa.svar4;
                li4.Controls.Add(checkboxSvar4);

                HtmlGenericControl rättaSvar = new HtmlGenericControl("div id=rättaSvar");
                rättaSvar.InnerText = "Korrekta svar: " + qa.rättSvar1 + " - " + qa.rättSvar2;
                varjeFråga.Controls.Add(rättaSvar);

                HtmlGenericControl användarensSvar = new HtmlGenericControl("div id=användarensSvar");
                användarensSvar.InnerText = "Dina svar: " + qa.användarensSvar1 + " - " + qa.användarensSvar2 + " - " + qa.användarensSvar3;
                varjeFråga.Controls.Add(användarensSvar);

            }
        }
        public List<QA> XmlTillLista()
        {
            AllaQALista.Clear();

            string path = Server.MapPath("../Q&A.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList allaFrågorOchSvar = doc.SelectNodes("/bank/Frågor/FrågorÅKU/Frågenummer");


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

            XmlDocument doc = xmldoc;
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
                qa.användarensSvar1 = node["användarenSvar1"].InnerXml;
                qa.användarensSvar2 = node["användarenSvar2"].InnerXml;
                qa.användarensSvar3 = node["användarenSvar3"].InnerXml;


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
            AllaFrågor.RemoveAt(randomIndex);

        }

        protected void startaNyttTest_Click(object sender, EventArgs e)
        {
            AllaFrågor = XmlTillLista();
            Session["AllaFrågor"] = AllaFrågor;
            fråganummer = 1;
            Session["fråganummer"] = fråganummer;
            for (int i = 0; i < allaResultat.Length; i++)
            {
                allaResultat[i] = 0;
            }

            Session["allaResultat"] = allaResultat;

            if (Session["AllaAnställda"] != null)
            {
                AllaAnställda = (List<Person>)Session["AllaAnställda"];
            }

            for (int i = 0; i < AllaAnställda.Count; i++)
            {
                if (AllaAnställda[i].förnamn + " " + AllaAnställda[i].efternamn == anställningsLista.SelectedValue.ToString())
                {
                    aktuellPerson = AllaAnställda[i];
                }
            }

            Session["AktuellPerson"] = aktuellPerson;

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
            frågeform.Visible = true;
            startaNyttTest.Visible = false;
            anställningsLista.Visible = false;
            namnet.Visible = false;
            provText.InnerHtml = "";
            frågenummer.InnerHtml = "Fråga: " + fråganummer + " av 15";

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

            if (Session["användarenSvar1"] != null)
            {
                användarenSvar1 = (string)Session["användarenSvar1"];
            }

            if (Session["användarenSvar2"] != null)
            {
                användarenSvar2 = (string)Session["användarenSvar2"];
            }

            if (Session["användarenSvar3"] != null)
            {
                användarenSvar3 = (string)Session["användarenSvar3"];
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
                    provText.InnerHtml = "";

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
                            }

                            användarenSvar1 = checkbox.Text;
                            Session["användarenSvar1"] = användarenSvar1;
                            AvslutAvFråga();
                        }
                    }
                }

            }

            else if (aktuellFråga.typ == "TvåRätt")
            {
                int kollaCheckbox = 0;
                string[] användarenSvar1och2 = new string[4];

                for (int i = 0; i < checkArray.Length; i++)
                {
                    if (checkArray[i].Checked == true)
                    {
                        kollaCheckbox += 1;
                        användarenSvar1och2[i] = checkArray[i].Text;
                    }
                }

                List<string> rättaSvar = new List<string>();

                foreach (string svar in användarenSvar1och2)
                {
                    if (svar != null)
                    {
                        rättaSvar.Add(svar);
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

                    användarenSvar2 = rättaSvar[0];
                    användarenSvar3 = rättaSvar[1];

                    Session["användarenSvar2"] = användarenSvar2;
                    Session["användarenSvar3"] = användarenSvar3;


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
                    }

                    AvslutAvFråga();
                }
            }
        }

        protected void avslutaProv_Click(object sender, EventArgs e)
        {
            Postgres p = new Postgres();
            Postgres p1 = new Postgres();

            seDetaljer.Visible = true;
            avslutaProv.Visible = false;

            Random rnd = new Random();
            nuvarandeProvId = rnd.Next(1, 1000);

            Session["provId"] = nuvarandeProvId;


            string godkänd;

            double procent = (double)allaResultat[0] / 15 * 100;
            double procentKat1 = (double)allaResultat[1] / 5 * 100;
            double procentKat2 = (double)allaResultat[2] / 5 * 100;
            double procentKat3 = (double)allaResultat[3] / 5 * 100;

            kategori1.Visible = true;
            kategori2.Visible = true;
            kategori3.Visible = true;
            status.Visible = true;

            if (procent > 69 && procentKat1 > 59 && procentKat2 > 59 && procentKat3 > 59)
            {
                godkänd = "GODKÄND";
                frågenummer.InnerText = "Ditt totalresultat är " + allaResultat[0].ToString() + " av 15 poäng vilket är " + procent + "%";
                status.InnerHtml = godkänd;
                kategori1.InnerHtml = "Kategori 1: " + allaResultat[1].ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                kategori2.InnerHtml = "Kategori 2: " + allaResultat[2].ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                kategori3.InnerHtml = "Kategori 3: " + allaResultat[3].ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                aktuellPerson.klaratProv = true;
                aktuellPerson.anställd = true;
                aktuellPerson.nyanställd = false;
                p1.UppdateraPerson(aktuellPerson.klaratProv, aktuellPerson.anställd, aktuellPerson.nyanställd, aktuellPerson.anställningsID);
                p1.StängConnection();
            }
            else
            {
                godkänd = "ICKE GODKÄND";
                frågenummer.InnerText = "Ditt totalresultat är " + allaResultat[0].ToString() + " av 15 poäng vilket är " + procent + "%";
                status.InnerHtml = godkänd;
                kategori1.InnerHtml = "Kategori 1: " + allaResultat[1].ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                kategori2.InnerHtml = "Kategori 2: " + allaResultat[2].ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                kategori3.InnerHtml = "Kategori 3: " + allaResultat[3].ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                görOm.Visible = true;
                aktuellPerson.klaratProv = false;
                p1.UppdateraPerson(aktuellPerson.klaratProv, aktuellPerson.anställd, aktuellPerson.nyanställd, aktuellPerson.anställningsID);
                p1.StängConnection();
            }

            p.LäggTillProv(nuvarandeProvId, aktuellPerson.anställningsID, "ÅKU", DateTime.Today, godkänd, allaResultat[0], 22, allaResultat[1], allaResultat[2], allaResultat[3], XmlTillDataBas());
            p.StängConnection();

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
            if (Session["användarenSvar3"] != null)
            {
                användarenSvar3 = (string)Session["användarenSvar3"];
            }
            if (Session["aktuelltProv"] != null)
            {
                aktuelltProv = (XDocument)Session["aktuelltProv"];
            }

            string path = Server.MapPath("../aktuelltprov.xml");

            try
            {

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
                    new XElement("användarenSvar2", användarenSvar2),
                    new XElement("användarenSvar3", användarenSvar3));

                aktuelltProv.Element("Frågor").Add(xmlElement);


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
                frågenummer.InnerHtml = "Fråga: " + fråganummer + " av 15";
            }
            else
            {
                CheckBox1.Visible = false; CheckBox2.Visible = false; CheckBox3.Visible = false; CheckBox4.Visible = false;
                frågenummer.InnerText = "Nu är du klar med provet. Tryck på rätta för att få reda på ditt resultat";
                nästaSida1.Visible = false;
                avslutaProv.Visible = true;
                avslutaProv.Text = "Rätta";              

            }
            användarenSvar1 = "";
            Session["användarenSvar1"] = användarenSvar1;
            användarenSvar2 = "";
            Session["användarenSvar2"] = användarenSvar1;
            användarenSvar3 = "";
            Session["användarenSvar3"] = användarenSvar1;


        }

        public string XmlTillDataBas()
        {
            if (Session["aktuelltProv"] != null)
            {
                aktuelltProv = (XDocument)Session["aktuelltProv"];
            }

            string xmlstring = aktuelltProv.ToString();

            return xmlstring;
        }

        public XmlDocument DatabasTillXml()
        {
            XmlDocument visaAktuelltProv = new XmlDocument();
            Postgres p = new Postgres();

            if (Session["provId"] != null)
            {
                nuvarandeProvId = (int)Session["provId"];
            }

            p.HämtaXmlFrånDatabas(visaAktuelltProv, aktuellPerson.anställningsID, nuvarandeProvId);

            return visaAktuelltProv;
        }

        protected void seDetaljer_Click(object sender, EventArgs e)
        {


            VisaResultat(XmlTillLista2(DatabasTillXml()));

            avslutaAllt.Visible = true;
            seDetaljer.Visible = false;
        }

        protected void avslutaAllt_Click(object sender, EventArgs e)
        {
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;

namespace IK073G_Projektuppgift.Nyanställd
{
    public partial class nyanställd_MittKonto : System.Web.UI.Page
    {
        XDocument aktuelltProv = new XDocument();
        List<Person> AllaNyAnställda = new List<Person>();
        List<QA> AllaQALista = new List<QA>();
        Postgres p = new Postgres();
        Person aktuellPerson = new Person();
        Prov provResultat = new Prov();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AllaNyAnställda = p.HämtaNyanställda();
                nyanställningsLista.DataSource = AllaNyAnställda;
                nyanställningsLista.DataBind();
                nyanställningsLista.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                Session["AllaNyanställda"] = AllaNyAnställda;

                XElement xmlElement = new XElement("Frågor");
                aktuelltProv.Add(xmlElement);
                Session["aktuelltProv"] = aktuelltProv;
            }
        }

        protected void nyanställningsLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Postgres pp = new Postgres();

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

            provResultat = pp.HämtaResultat(aktuellPerson.anställningsID);

            if (provResultat.efternamn == null)
            {
                provTyp.Visible = true;
                provTyp.InnerHtml = "Personen har inte utfört ett prov.";
            }
            else
            {

                string godkänd;

            double procent = (double)provResultat.antalRätt / 25 * 100;
            double procentKat1 = (double)provResultat.kat1Rätt/ 8 * 100;
            double procentKat2 = (double)provResultat.kat2Rätt / 8 * 100;
            double procentKat3 = (double)provResultat.kat3Rätt / 9 * 100;

            kategori1.Visible = true;
            kategori2.Visible = true;
            kategori3.Visible = true;
            frågenummer.Visible = true;
            status.Visible = true;
            provTyp.Visible = true;

            if (procent > 69 && procentKat1 > 59 && procentKat2 > 59 && procentKat3 > 59)
            {

                godkänd = "GODKÄND";
                frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 25 poäng vilket är " + procent + "%";
                provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                status.InnerHtml = godkänd;
                kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 8 poäng vilket är " + procentKat1 + "%";
                kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 8 poäng vilket är " + procentKat2 + "%";
                kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 9 poäng vilket är " + procentKat3 + "%";
            }
            else
            {
                godkänd = "ICKE GODKÄND";
                frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 25 poäng vilket är " + procent + "%";
                provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                status.InnerHtml = godkänd;
                kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 8 poäng vilket är " + procentKat1 + "%";
                kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 8 poäng vilket är " + procentKat2 + "%";
                kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 9 poäng vilket är " + procentKat3 + "%";
            }

            VisaResultat(XmlTillLista2(DatabasTillXml()));
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
        public XmlDocument DatabasTillXml()
        {
            XmlDocument visaAktuelltProv = new XmlDocument();
            Postgres p = new Postgres();

            p.HämtaXmlFrånDatabas2(visaAktuelltProv, aktuellPerson.anställningsID);

            return visaAktuelltProv;
        }
    }
}
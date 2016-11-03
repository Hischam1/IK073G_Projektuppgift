using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Linq;

namespace IK073G_Projektuppgift.Admin
{
    public partial class admin_Start : System.Web.UI.Page
    {
        Postgres p = new Postgres();
        Person aktuellPerson = new Person();
        Person aktuellDeltagare = new Person();
        List<Person> AllaAdmin = new List<Person>();
        List<QA> AllaQALista = new List<QA>();
        List<Person> inteKlaratLicens = new List<Person>();
        List<Person> domAnställda = new List<Person>();
        List<Person> inteKlaratÅKU = new List<Person>();
        List<Person> klaratProv = new List<Person>();
        Prov provResultat = new Prov();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AllaAdmin = p.HämtaAdmin();
                AdminLista.DataSource = AllaAdmin;
                AdminLista.DataBind();
                AdminLista.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                Session["AllaAdmin"] = AllaAdmin;
                p.StängConnection();
            }
        }

        protected void AdminLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["AllaAdmin"] != null)
            {
                AllaAdmin = (List<Person>)Session["AllaAdmin"];
            }

            for (int i = 0; i < AllaAdmin.Count; i++)
            {
                if (AllaAdmin[i].förnamn + " " + AllaAdmin[i].efternamn == AdminLista.SelectedValue.ToString())
                {
                    aktuellPerson = AllaAdmin[i];
                }
            }

            namnet.InnerHtml = "Välkommen " + aktuellPerson.förnamn + " " + aktuellPerson.efternamn;
            AdminLista.Visible = false;
            Session["AktuellPerson"] = aktuellPerson;

            lista1.Visible = true;
            lista3.Visible = true;
            lista4.Visible = true;

            SorteraPersonerIListor();


            LicensInteKlarat.DataSource = inteKlaratLicens;
            LicensInteKlarat.DataBind();
            LicensInteKlarat.Items.Insert(0, new ListItem("Välj person", string.Empty));
            ÅKUinteKlarat.DataSource = inteKlaratÅKU;
            ÅKUinteKlarat.DataBind();
            ÅKUinteKlarat.Items.Insert(0, new ListItem("Välj person", string.Empty));
            PROVKlarat.DataSource = klaratProv;
            PROVKlarat.DataBind();
            PROVKlarat.Items.Insert(0, new ListItem("Välj person", string.Empty));

        }

        public void SorteraPersonerIListor()
        {
            Postgres p = new Postgres();
            Postgres p1 = new Postgres();

            inteKlaratLicens = p.HämtaNyanställda();
            domAnställda = p1.HämtaAnställda();
            p.StängConnection();
            p1.StängConnection();

            foreach (Person person in domAnställda)
            {
                if (person.klaratProv == true)
                {
                    klaratProv.Add(person);
                }
                else
                {
                    inteKlaratÅKU.Add(person);
                }
            }

            Session["inteKlaratLicens"] = inteKlaratLicens;
            Session["inteKlaratÅKU"] = inteKlaratÅKU;
            Session["klaratProv"] = klaratProv;
        }

        protected void LicensInteKlarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ÅKUinteKlarat.SelectedIndex = 0;
            PROVKlarat.SelectedIndex = 0;

            Postgres pp = new Postgres();

            if (Session["inteKlaratLicens"] != null)
            {
                inteKlaratLicens = (List<Person>)Session["inteKlaratLicens"];
            }

            for (int i = 0; i < inteKlaratLicens.Count; i++)
            {
                if (inteKlaratLicens[i].förnamn + " " + inteKlaratLicens[i].efternamn == LicensInteKlarat.SelectedValue.ToString())
                {
                    aktuellDeltagare = inteKlaratLicens[i];
                }
            }

            Session["aktuellDeltagare"] = aktuellDeltagare;

            provResultat = pp.HämtaResultat(aktuellDeltagare.anställningsID);
            pp.StängConnection();

            if (provResultat.provTyp == null)
            {
                kategori1.InnerHtml = "";
                kategori2.InnerHtml = "";
                kategori3.InnerHtml = "";
                frågenummer.InnerHtml = "";
                status.InnerHtml = "";

                provTyp.Visible = true;
                provTyp.InnerHtml = "Personen har inte utfört ett prov.";
            }
            else
            {
                string godkänd;

                double procent = (double)provResultat.antalRätt / 25 * 100;
                double procentKat1 = (double)provResultat.kat1Rätt / 8 * 100;
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
            }
        }

        protected void ÅKUinteKlarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicensInteKlarat.SelectedIndex = 0;
            PROVKlarat.SelectedIndex = 0;

            Postgres pp = new Postgres();

            if (Session["inteKlaratÅKU"] != null)
            {
                inteKlaratÅKU = (List<Person>)Session["inteKlaratÅKU"];
            }

            for (int i = 0; i < inteKlaratÅKU.Count; i++)
            {
                if (inteKlaratÅKU[i].förnamn + " " + inteKlaratÅKU[i].efternamn == ÅKUinteKlarat.SelectedValue.ToString())
                {
                    aktuellDeltagare = inteKlaratÅKU[i];
                }
            }

            Session["aktuellDeltagare"] = aktuellDeltagare;

            provResultat = pp.HämtaResultat(aktuellDeltagare.anställningsID);

            if (provResultat.provTyp == null)
            {
                kategori1.InnerHtml = "";
                kategori2.InnerHtml = "";
                kategori3.InnerHtml = "";
                frågenummer.InnerHtml = "";
                status.InnerHtml = "";
                provTyp.Visible = true;
                provTyp.InnerHtml = "Personen har inte utfört ett prov.";
            }
            else
            {
                string godkänd;

                double procent = (double)provResultat.antalRätt / 15 * 100;
                double procentKat1 = (double)provResultat.kat1Rätt / 5 * 100;
                double procentKat2 = (double)provResultat.kat2Rätt / 5 * 100;
                double procentKat3 = (double)provResultat.kat3Rätt / 5 * 100;

                kategori1.Visible = true;
                kategori2.Visible = true;
                kategori3.Visible = true;
                frågenummer.Visible = true;
                status.Visible = true;
                provTyp.Visible = true;

                if (procent > 69 && procentKat1 > 59 && procentKat2 > 59 && procentKat3 > 59)
                {

                    godkänd = "GODKÄND";
                    frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 15 poäng vilket är " + procent + "%";
                    provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                    status.InnerHtml = godkänd;
                    kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                    kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                    kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                }
                else
                {
                    godkänd = "ICKE GODKÄND";
                    frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 15 poäng vilket är " + procent + "%";
                    provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                    status.InnerHtml = godkänd;
                    kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                    kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                    kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                }
            }
        }

        protected void PROVKlarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicensInteKlarat.SelectedIndex = 0;
            ÅKUinteKlarat.SelectedIndex = 0;

            Postgres pp = new Postgres();

            if (Session["klaratProv"] != null)
            {
                klaratProv = (List<Person>)Session["klaratProv"];
            }

            for (int i = 0; i < klaratProv.Count; i++)
            {
                if (klaratProv[i].förnamn + " " + klaratProv[i].efternamn == PROVKlarat.SelectedValue.ToString())
                {
                    aktuellDeltagare = klaratProv[i];
                }
            }

            Session["aktuellDeltagare"] = aktuellDeltagare;

            provResultat = pp.HämtaResultat(aktuellDeltagare.anställningsID);

            if (provResultat.provTyp == null)
            {
                kategori1.InnerHtml = "";
                kategori2.InnerHtml = "";
                kategori3.InnerHtml = "";
                frågenummer.InnerHtml = "";
                status.InnerHtml = "";
                provTyp.Visible = true;
                provTyp.InnerHtml = "Personen har inte utfört ett prov.";
            }
            else
            {
                if (provResultat.provTyp == "LICENS")
                {
                    string godkänd;

                    double procent = (double)provResultat.antalRätt / 25 * 100;
                    double procentKat1 = (double)provResultat.kat1Rätt / 8 * 100;
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

                }
                else
                {
                    string godkänd;

                    double procent = (double)provResultat.antalRätt / 15 * 100;
                    double procentKat1 = (double)provResultat.kat1Rätt / 5 * 100;
                    double procentKat2 = (double)provResultat.kat2Rätt / 5 * 100;
                    double procentKat3 = (double)provResultat.kat3Rätt / 5 * 100;

                    kategori1.Visible = true;
                    kategori2.Visible = true;
                    kategori3.Visible = true;
                    frågenummer.Visible = true;
                    status.Visible = true;
                    provTyp.Visible = true;

                    if (procent > 69 && procentKat1 > 59 && procentKat2 > 59 && procentKat3 > 59)
                    {

                        godkänd = "GODKÄND";
                        frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 15 poäng vilket är " + procent + "%";
                        provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                        status.InnerHtml = godkänd;
                        kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                        kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                        kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                    }
                    else
                    {
                        godkänd = "ICKE GODKÄND";
                        frågenummer.InnerText = "Ditt totalresultat är " + provResultat.antalRätt.ToString() + " av 15 poäng vilket är " + procent + "%";
                        provTyp.InnerHtml = provResultat.provTyp + " - " + provResultat.datum.ToShortDateString();
                        status.InnerHtml = godkänd;
                        kategori1.InnerHtml = "Kategori 1: " + provResultat.kat1Rätt.ToString() + " av 5 poäng vilket är " + procentKat1 + "%";
                        kategori2.InnerHtml = "Kategori 2: " + provResultat.kat2Rätt.ToString() + " av 5 poäng vilket är " + procentKat2 + "%";
                        kategori3.InnerHtml = "Kategori 3: " + provResultat.kat3Rätt.ToString() + " av 5 poäng vilket är " + procentKat3 + "%";
                    }

                }

            }
        }
    }
}
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
        QA aktuellFråga = new QA();

        Postgres p = new Postgres();


       int resultat;
        int fråganummer;


    protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nyanställningsLista.DataSource = p.HämtaNyanställda();
                nyanställningsLista.DataBind();

            }
            if (Session["AllaFrågor"] != null)
            {
                AllaFrågor = (List<QA>)Session["AllaFrågor"];
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

            fråganummer += 100;

            Session["fråganummer"] = fråganummer;

            if (Session["fråganummer"] != null)
            {
                fråganummer = (int)Session["fråganummer"];
            }

            frågenummer.InnerHtml = "Fråga: " + fråganummer + " av 25";


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

            TaUtEnFråga();
            VisaAllt(PåbörjadFråga);
            //kategori1 = true;
            //kategori2 = false;
            //kategori3 = false;

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
            provText.InnerHtml = "";



        }

        protected void nästaSida1_Click(object sender, EventArgs e)
        {
            if (Session["resultat"] != null)
            {
                resultat = (int)Session["resultat"];
            }


            if (Session["AktuellFråga"] != null)
            {
                aktuellFråga = (QA)Session["AktuellFråga"];
            }


            if (CheckBox1.Checked == true)
            {
                if (CheckBox1.Text == aktuellFråga.rättSvar1)
                {
                    namnet.InnerHtml = "Rätt, checkbox1";
                    resultat+=1;
                }
                else if (CheckBox1.Text == aktuellFråga.rättSvar2)
                {
                    namnet.InnerHtml = "Rätt, checkbox1";
                    resultat += 1;
                }
                else if (CheckBox1.Text == aktuellFråga.rättSvar3)
                {
                    namnet.InnerHtml = "Rätt, checkbox1";
                    resultat += 1;
                }
                else
                {
                    namnet.InnerHtml = "Checkbox1 är checked men inte rättsvar";
                }
                provText.InnerHtml = resultat.ToString();
            }
            else if (CheckBox2.Checked == true)
            {
                if (CheckBox2.Text == aktuellFråga.rättSvar1)
                {
                    namnet.InnerHtml = "Rätt, checkbox2";
                    resultat += 1;
                }
                else if (CheckBox2.Text == aktuellFråga.rättSvar2)
                {
                    namnet.InnerHtml = "Rätt, checkbox2";
                    resultat += 1;
                }
                else if (CheckBox2.Text == aktuellFråga.rättSvar3)
                {
                    namnet.InnerHtml = "Rätt, checkbox2";
                    resultat += 1;
                }
                else
                {
                    namnet.InnerHtml = "Checkbox2 är checked men inte rättsvar";
                }
                provText.InnerHtml = resultat.ToString();
            }
            else if (CheckBox3.Checked == true)
            {
                if (CheckBox3.Text == aktuellFråga.rättSvar1)
                {
                    namnet.InnerHtml = "Rätt, checkbox3";
                    resultat += 1;
                }
                else if (CheckBox3.Text == aktuellFråga.rättSvar2)
                {
                    namnet.InnerHtml = "Rätt, checkbox3";
                    resultat += 1;
                }
                else if (CheckBox3.Text == aktuellFråga.rättSvar3)
                {
                    namnet.InnerHtml = "Rätt, checkbox3";
                    resultat += 1;
                }
                else
                {
                    namnet.InnerHtml = "Checkbox3 är checked men inte rättsvar";
                }
                provText.InnerHtml = resultat.ToString();
            }
            else if (CheckBox4.Checked == true)
            {
                if (CheckBox4.Text == aktuellFråga.rättSvar1)
                {
                    namnet.InnerHtml = "Rätt, checkbox4";
                    resultat += 1;
                }
                else if (CheckBox4.Text == aktuellFråga.rättSvar2)
                {
                    namnet.InnerHtml = "Rätt, checkbox4";
                    resultat += 1;
                }
                else if (CheckBox4.Text == aktuellFråga.rättSvar3)
                {
                    namnet.InnerHtml = "Rätt, checkbox4";
                    resultat += 1;
                }
                else
                {
                    namnet.InnerHtml = "Checkbox4 är checked men inte rättsvar";
                }
                provText.InnerHtml = resultat.ToString();
            }

            if (CheckBox1.Checked == false && CheckBox2.Checked == false && CheckBox3.Checked == false && CheckBox4.Checked == false)
            {
                if (Session["PåbörjadFråga"] != null)
                {
                    PåbörjadFråga = (List<QA>)Session["PåbörjadFråga"];
                }
                provText.InnerHtml = "Dra åt helvete! \n Markera ett svarsalternativ";
                VisaAllt(PåbörjadFråga);
            }
            else
            {
                
                Session["resultat"] = resultat;

                TaUtEnFråga();
                VisaAllt(PåbörjadFråga);


            }

        }


        protected void avslutaProv_Click(object sender, EventArgs e)
        {

        }
    }
}
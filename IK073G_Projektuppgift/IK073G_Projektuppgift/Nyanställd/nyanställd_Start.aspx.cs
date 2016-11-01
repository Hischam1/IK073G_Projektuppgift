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

                frågeform.Controls.Add(kategoriFråga);

                HtmlGenericControl bildFråga = new HtmlGenericControl("img src= '" + qa.bild + "' class=bildFråga width=20%");
                frågeform.Controls.Add(bildFråga);

                HtmlGenericControl rubrikFråga = new HtmlGenericControl("p class=frågaRubrik id=hej runat=server");
                rubrikFråga.InnerText = qa.fråga;
                frågeform.Controls.Add(rubrikFråga);

                HtmlGenericControl divText = new HtmlGenericControl("div class=text");
                divText.InnerText = qa.text;
                frågeform.Controls.Add(divText);
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
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, AllaFrågor.Count);

            CheckBox1.ID = AllaFrågor[randomIndex].svar1;
            CheckBox1.Text = AllaFrågor[randomIndex].svar1;
            CheckBox2.ID = AllaFrågor[randomIndex].svar2;
            CheckBox2.Text = AllaFrågor[randomIndex].svar2;
            CheckBox3.ID = AllaFrågor[randomIndex].svar3;
            CheckBox3.Text = AllaFrågor[randomIndex].svar3;
            CheckBox4.ID = AllaFrågor[randomIndex].svar4;
            CheckBox4.Text = AllaFrågor[randomIndex].svar4;

            aktuellFråga = AllaFrågor[randomIndex];
            PåbörjadFråga.Add(AllaFrågor[randomIndex]);
            namnet.InnerHtml = randomIndex.ToString() + " " + AllaFrågor.Count.ToString() + " " + AllaFrågor[randomIndex].rättSvar1;
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

            nästaSida1.Visible = true;
            //provText.Visible = false;
            ////startaNyttTest.Visible = false;
            //nyanställningsLista.Visible = false;
            ////namnet.InnerHtml = nyanställningsLista.SelectedItem.Value;

        }

        protected void nästaSida1_Click(object sender, EventArgs e)
        {
            //BesvaradeFrågor.Add(PåbörjadFråga[0]);
            PåbörjadFråga.Clear();
            aktuellFråga = null;

            TaUtEnFråga();

            VisaAllt(PåbörjadFråga);

            if (CheckBox1.Checked == true)
            {
                hej.InnerHtml = "Rätt";
            }
            if (CheckBox2.Checked == true && CheckBox2.ID == aktuellFråga.rättSvar1)
            {
                hej.InnerHtml = "Rätt";
            }

        }


        protected void avslutaProv_Click(object sender, EventArgs e)
        {

        }
    }
}
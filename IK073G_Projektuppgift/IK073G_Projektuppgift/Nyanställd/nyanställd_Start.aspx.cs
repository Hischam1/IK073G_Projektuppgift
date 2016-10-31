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
        List<QA> AllaQALista = new List<QA>();
        List<QA> Kategori1QALista = new List<QA>();
        List<QA> Kategori2QALista = new List<QA>();
        List<QA> Kategori3QALista = new List<QA>();
        List<QA> PåbörjadFråga = new List<QA>();
        List<QA> BesvaradeFrågor = new List<QA>();

        Postgres p = new Postgres();

        private bool kategori1;
        private bool kategori2;
        private bool kategori3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nyanställningsLista.DataSource = p.HämtaNyanställda();
                nyanställningsLista.DataBind();
            }
            XmlTillLista();

        }
        public void VisaAllt(List<QA> QALista)
        {
            Random r = new Random();
            var blandadLista = QALista.OrderBy(c => r.Next()).ToList();
                        
            foreach (QA qa in blandadLista)
            {

                HtmlGenericControl divFråga = new HtmlGenericControl("div class=fråga");
                frågeform.Controls.Add(divFråga);

                HtmlGenericControl kategoriFråga = new HtmlGenericControl("p class=frågaKategori");
                kategoriFråga.InnerText = qa.kategori;
                divFråga.Controls.Add(kategoriFråga);

                HtmlGenericControl bildFråga = new HtmlGenericControl("img src= '" + qa.bild + "' class=bildFråga width=20%");
                //bildFråga.InnerText = qa.bild;
                divFråga.Controls.Add(bildFråga);

                HtmlGenericControl rubrikFråga = new HtmlGenericControl("p class=frågaRubrik id=hej runat=server");
                rubrikFråga.InnerText = qa.fråga;
                divFråga.Controls.Add(rubrikFråga);

                HtmlGenericControl divText = new HtmlGenericControl("div class=text");
                divText.InnerText = qa.text;
                divFråga.Controls.Add(divText);

                HtmlGenericControl svarsalternativDivFråga = new HtmlGenericControl("div class=svarsalternativ");
                divFråga.Controls.Add(svarsalternativDivFråga);

                HtmlGenericControl svar1Fråga = new HtmlGenericControl("li class=svar1");
                svarsalternativDivFråga.Controls.Add(svar1Fråga);
                HtmlGenericControl checkBox1Fråga = new HtmlGenericControl("input type = checkbox name = svar1");
                svar1Fråga.Controls.Add(checkBox1Fråga);
                HtmlGenericControl svar1TextFråga = new HtmlGenericControl("p");
                svar1TextFråga.InnerText = qa.svar1;
                svar1Fråga.Controls.Add(svar1TextFråga);

                HtmlGenericControl svar2Fråga = new HtmlGenericControl("li class=svar2");
                svarsalternativDivFråga.Controls.Add(svar2Fråga);
                HtmlGenericControl checkBox2Fråga = new HtmlGenericControl("input type = checkbox name = svar2");
                svar2Fråga.Controls.Add(checkBox2Fråga);
                HtmlGenericControl svar2TextFråga = new HtmlGenericControl("p");
                svar2TextFråga.InnerText = qa.svar2;
                svar2Fråga.Controls.Add(svar2TextFråga);

                HtmlGenericControl svar3Fråga = new HtmlGenericControl("li class=svar3");
                svarsalternativDivFråga.Controls.Add(svar3Fråga);
                HtmlGenericControl checkBox3Fråga = new HtmlGenericControl("input type = checkbox name = svar3" );
                svar3Fråga.Controls.Add(checkBox3Fråga);
                HtmlGenericControl svar3TextFråga = new HtmlGenericControl("p");
                svar3TextFråga.InnerText = qa.svar3;
                svar3Fråga.Controls.Add(svar3TextFråga);

                HtmlGenericControl svar4Fråga = new HtmlGenericControl("li class=svar4");
                svarsalternativDivFråga.Controls.Add(svar4Fråga);
                HtmlGenericControl checkBox4Fråga = new HtmlGenericControl("input type = checkbox name = svar4");
                svar4Fråga.Controls.Add(checkBox4Fråga);
                HtmlGenericControl svar4TextFråga = new HtmlGenericControl("p");
                svar4TextFråga.InnerText = qa.svar4;
                svar4Fråga.Controls.Add(svar4TextFråga);
            }
        }
        public List<QA> XmlTillLista()
        {
            AllaQALista.Clear();
            Kategori1QALista.Clear();
            Kategori2QALista.Clear();
            Kategori3QALista.Clear();


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

//            if (kategori1 == true)
//            {
//                for (int i = 0; i<AllaQALista.Count; i++)
//                {
//                    if (AllaQALista[i].kategori == "Produkter och hantering av kundens affärer")
//                    {

//                        Kategori1QALista.Add(AllaQALista[i]);

//                    }
//}

//                return Kategori1QALista;

//            }
//            else if (kategori2 == true)
//            {
//                for (int i = 0; i<AllaQALista.Count; i++)
//                {
//                    if (AllaQALista[i].kategori == "Ekonomi – nationalekonomi, finansiell ekonomi och privatekonomi")
//                    {
//                        Kategori2QALista.Add(AllaQALista[i]);
//                    }
//                }

//                return Kategori2QALista;
//            }
//            else if (kategori3 == true)
//            {
//                for (int i = 0; i<AllaQALista.Count; i++)
//                {
//                    if (AllaQALista[i].kategori == "Etik och regelverk")
//                    {
//                        Kategori3QALista.Add(AllaQALista[i]);
//                    }
//                }

//                return Kategori3QALista;
//            }
//            else
//            {
//                return AllaQALista;
//            }

        public void TaUtEnFråga()
        {
            if (PåbörjadFråga.Count < 1)
            {
                Random rnd = new Random();

                int randomIndex = rnd.Next(10);

                PåbörjadFråga.Add(AllaQALista[randomIndex]);
                AllaQALista.RemoveAt(randomIndex);
            }
        }


        protected void startaNyttTest_Click(object sender, EventArgs e)
        {

            kategori1 = true;
            kategori2 = false;
            kategori3 = false;

            nästaSida1.Visible = true;
            provText.Visible = false;
            startaNyttTest.Visible = false;
            nyanställningsLista.Visible = false;
            namnet.InnerHtml = nyanställningsLista.SelectedItem.Value;

            TaUtEnFråga();

            VisaAllt(PåbörjadFråga);
        }

        protected void nästaSida1_Click(object sender, EventArgs e)
        {
            //BesvaradeFrågor.Add(PåbörjadFråga[0]);
            PåbörjadFråga.Clear();

            kategori1 = false;
            kategori2 = true;
            kategori3 = false;

            TaUtEnFråga();

            VisaAllt(PåbörjadFråga);
        }


        protected void avslutaProv_Click(object sender, EventArgs e)
        {

        }
    }
}
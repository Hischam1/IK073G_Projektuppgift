using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace IK073G_Projektuppgift
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VisaAllt(XmlTillLista());
        }
        public void VisaAllt(List<QA> QALista)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            sorteraFrågor.Controls.Add(div);
              

            foreach (QA qa in QALista)
            {
                div.InnerHtml = qa.kategori;
                //sorteraFrågor.Controls.Add(div); 
            }
        }
        public List<QA> XmlTillLista()
        {
            List<QA> QALista = new List<QA>();
            string path = Server.MapPath("Q&A.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList allaFrågorOchSvar = doc.SelectNodes("/Frågor/FrågorLicensiering/Frågenummer");

            foreach (XmlNode node in allaFrågorOchSvar)
            {
                QA qa = new QA();
                qa.kategori = node["Kategori"].InnerXml;
                qa.typ = node["Typ"].InnerXml;
                qa.text = node["Text"].InnerXml;
                qa.fråga = node["fråga"].InnerXml;

                QALista.Add(qa);
            }

            return QALista;
        }
    }
}
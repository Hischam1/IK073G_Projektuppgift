using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IK073G_Projektuppgift.Admin
{
    public partial class admin_Start : System.Web.UI.Page
    {
        Postgres p = new Postgres();
        Postgres p1 = new Postgres();

        protected void Page_Load(object sender, EventArgs e)
        {
            AdminLista.DataSource = p.HämtaAdmin();
            AdminLista.DataBind();

            //DropDownList1.DataSource = p1.HämtaProvResultat();
            //DropDownList1.DataBind();

            GridViewDeltagare.DataSource = p1.HämtaProvResultat();
            GridViewDeltagare.DataBind();
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    Entities.User LoggedInUser = Session["User"] as Entities.User;
                    if (LoggedInUser != null)
                    {
                        BLL.LogInHelper logInHelper = new BLL.LogInHelper();
                        Entities.User result = new Entities.User();
                        try
                        {
                            result = logInHelper.LoginAtPageLoad(LoggedInUser);
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("/SignIn");
                        }
                        if (result != null)
                        {
                            //akkoord om op de pagina te zijn
                            IngelogdAls.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName + " BSN:" + LoggedInUser.BsnNumber;
                            
                        }
                        else
                        {

                            Response.Redirect("/SignIn");
                        }
                    }
                    else
                    {

                        Response.Redirect("/SignIn");
                    }
                }
                else
                {
                    Response.Redirect("/SignIn");
                }
            }
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("/SignIn");
        }

        protected void ToPortal_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PortalContent/DashBoard");
        }
    }
}
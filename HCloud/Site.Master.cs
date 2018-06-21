using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DAL.DBTestConnection dBTestConnection = new DAL.DBTestConnection();
                try
                {
                    dBTestConnection.TestDBConnection();
                }
                catch (Exception ex)
                {
                    if (Request.Url.AbsolutePath != "/ErrorPage")
                    { Response.Redirect("/ErrorPage.aspx"); }
                }

                if (Session["User"] != null)
                {
                    Entities.User LoggedInUser = Session["User"] as Entities.User;
                    if (LoggedInUser != null)
                    {
                        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                        Entities.UserData data = dBUserConnection.GetUserData(LoggedInUser);
                        if (data.ProfilePicUrl == null)
                        {
                            ProfileImg.Visible = false;
                        }
                        else
                        {
                            ProfileImg.ImageUrl = data.ProfilePicUrl;
                        }
                        BLL.LogInHelper logInHelper = new BLL.LogInHelper();
                        Entities.User result = new Entities.User();
                        try
                        {
                            result = logInHelper.LoginAtPageLoad(LoggedInUser);
                        }
                        catch (Exception)
                        {
                            LoggedInUserLbl.Text = "Inloggen";
                            LogginLink.HRef = "/SignIn";
                            ProfileImg.Visible = false;
                            Logout.Visible = false;
                        }
                        if (result != null)
                        {
                            LoggedInUserLbl.Text = LoggedInUser.EmailAdress;
                            LogginLink.HRef = "/Account";
                            
                        }
                        else
                        {
                            LoggedInUserLbl.Text = "Inloggen";
                            LogginLink.HRef = "/SignIn";
                            ProfileImg.Visible = false;
                            Logout.Visible = false;
                        }
                    }
                    else
                    {
                        LoggedInUserLbl.Text = "Inloggen";
                        LogginLink.HRef = "/SignIn";
                        ProfileImg.Visible = false;
                        Logout.Visible = false;
                    }
                }
                else
                {
                    LoggedInUserLbl.Text = "Inloggen";
                    LogginLink.HRef = "/SignIn";
                    Logout.Visible = false;
                    ProfileImg.Visible = false;
                }
            }

        }
        /// <summary>
        /// Set the link and text of the navlogin
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        public void setLoggedInText(string text,string url)
        {
            try
            {
                LoggedInUserLbl.Text = "Inloggen";
                LogginLink.HRef = "/SignIn";
            }
            catch
            {
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Session["User"] = null;
                Response.Redirect("/SignIn");
            }
        }
    }
}
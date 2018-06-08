using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class NewDesease : System.Web.UI.Page
    {
        Entities.User LoggedInUser;
        protected void Page_Load(object sender, EventArgs e)
        {

            BLL.ClientHelper clientHelper = new BLL.ClientHelper();
            if (Session["User"] != null)
            {
                LoggedInUser = Session["User"] as Entities.User;
                if (LoggedInUser != null)
                {
                    if (!IsPostBack)
                    {
                        NewDeseaseClient.DataSource = clientHelper.GetClientList(LoggedInUser);
                        NewDeseaseClient.DataTextField = "FirstName";
                        NewDeseaseClient.DataValueField = "BsnNumber";
                        NewDeseaseClient.DataBind();


                       
                        NewDeseaseTherapist.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName;
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


        protected void SaveTherapy_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Desease desease = new Entities.Desease();
                desease.date = Convert.ToDateTime(NewDeseaseDate.Text);
                desease.Description = NewDeseaseDescriptionTB.Text;
                desease.determinerFirstName = LoggedInUser.FirstName;
                desease.determinerLastName = LoggedInUser.LastName;
                desease.determiner = LoggedInUser.ID;
                desease.DeclaredHealthy = false;
                DAL.DBDeseaseConnection dBDesease = new DAL.DBDeseaseConnection();
                bool result = dBDesease.Save(LoggedInUser, desease, Convert.ToInt32(NewDeseaseClient.SelectedValue));
                if(result)
                lbl.Text = "Aandoening is succesvol opgeslagen";
                else
                    lbl.Text = "Er is iets fout gegaan";

            }
            catch (Exception ex)
            {
                lbl.Text = "Er is iets fout gegaan";
            }
        }
    }

}
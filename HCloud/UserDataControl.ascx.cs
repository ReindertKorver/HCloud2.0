using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class UserDataControl : System.Web.UI.UserControl
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

                            //get the user data from database
                            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
                            PostCode.Text = data.PostCode??"Geen";
                            Bloedgroep.Text = data.Bloedgroep ?? "Geen";
                            if (data.GeboorteDatum != DateTime.MinValue)
                            {
                                GeboorteDatum.Text = data.GeboorteDatum.ToString("dd-MM-yyyy");
                            }
                            else
                            {
                                GeboorteDatum.Text = "Geen";
                            }
                            Geboorteplaats.Text = data.Geboorteplaats ?? "Geen";
                            Huisnummer.Text = data.Huisnummer ?? "Geen";
                            Nationaliteit.Text = data.Nationaliteit ?? "Geen";
                            Provincie.Text = data.Provincie ?? "Geen";
                            Straat.Text = data.Straat ?? "Geen";
                            Woonplaats.Text = data.Woonplaats ?? "Geen";
                            Bankrekeningnummer.Text = data.Bankrekeningnummer ?? "Geen";
;                        }
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

        protected void SaveValue_Click(object sender, EventArgs e)
        {
            string value = CurrentValueEdit.Text;

        }
        protected void ClickHandler_Click(object sender, EventArgs e)
        {
            var btn = sender as LinkButton;
            var theValue = btn.Attributes["myParam"].ToString();
            EditValueText.InnerText ="Wijzig "+theValue;
            CurrentValueEdit.Text = theValue;
            FilterGrid.Attributes["style"] = "display: block;";
        }
    }
}
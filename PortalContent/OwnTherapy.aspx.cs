using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class OwnTherapy : System.Web.UI.Page
    {
        Entities.User LoggedInUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                LoggedInUser = Session["User"] as Entities.User;
                if (LoggedInUser != null)
                {
                   
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
            DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
            List<Therapy> therapy = dBTherapy.GetOwn(LoggedInUser);
            TherapyCards.InnerHtml = "";
            if (therapy != null)
            {
                foreach (var item in therapy)
                {
                    try
                    {
                        string MedDescription = string.Empty;
                        if (item.Medication != null) {
                            MedDescription =  item.Medication.FirstOrDefault().Description;
                        }
                        string DesDescription = string.Empty;
                        if (item.Deseases != null)
                        {
                            DesDescription = item.Deseases.FirstOrDefault().Description;
                        }
                        TherapyCards.InnerHtml += "<div class='card-small'><div class='card-small-title'>" + (item.description ?? "") + " </div><hr/>Behandeld door: " + (item.therapistLastName ?? "") + "<hr/>" + (item.date.ToString("dd-MM-yyyy") ?? "") + "<br />Aandoening: " + DesDescription + "<br />Medicatie: " + MedDescription + "</div>";
                    }
                    catch (Exception ex)
                    {

                    }
                    }
            }
            else
            {
                TherapyCards.InnerHtml = "Geen behandelingen om weer te geven";
            }
        }
    }
}
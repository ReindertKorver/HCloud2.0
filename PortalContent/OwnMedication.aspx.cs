using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class OwnMedication : System.Web.UI.Page
    {
        DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
        Entities.User LoggedInUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                LoggedInUser = Session["User"] as Entities.User;
                if (LoggedInUser != null)
                {
                    if (!IsPostBack)
                    {
                        var resultMedications = dBMedication.GetOwn(LoggedInUser);

                        foreach (var item in resultMedications)
                        {
                            MedicationCards.InnerHtml += "<div class='card-small'><div class='card-small-title'>" + (item.Description ?? "") + "</div>" + (item.HandedOut ? "Deze medicatie is afgegeven door " + item.getHandedOutByName() + "<br/>Afgegeven op: " + item.HandedOutDate.ToString("dd/MM/yyyy") + "<br/>Met houdbaarheidsdatum van: " + item.ExpirationDate.ToString("dd/MM/yyyy") : "Deze medicatie is nog niet opgehaald") + "</div>";
                        }
                    }
                }
            }
        }
    }
}
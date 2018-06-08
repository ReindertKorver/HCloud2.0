using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class OwnDesease : System.Web.UI.Page
    {
        Entities.Role role;
        DAL.DBDeseaseConnection dBDesease = new DAL.DBDeseaseConnection();
        DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
        DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
        DAL.DBRapportConnection dBRapport = new DAL.DBRapportConnection();
        DAL.DBUserConnection dBUser = new DAL.DBUserConnection();
        Entities.User LoggedInUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"] as Entities.User;
            if (user != null)
            {
                DeseaseCards.InnerHtml = "";
                var resultDeseases = dBDesease.GetOwn(user);
               
                foreach (var item in resultDeseases)
                {
                    DeseaseCards.InnerHtml += "<div class='card-small'><div class='card-small-title'>" + (item.Description ?? "") + "</div><hr/>Gevonden door: " + (item.determinerLastName ?? "") + "<hr/>" + (item.date.ToString("dd-MM-yyyy") ?? "") + "<br />Beter: " + (item.DeclaredHealthy? "Ja<br />Beter verklaard op: " + item.DeclaredHealthyDate.ToString("dd-MM-yyyy") + "</div>" : "Nee") + "</div>";
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class AllTherapies : System.Web.UI.Page
    {
        DAL.DBTherapyConnection DBTherapy = new DAL.DBTherapyConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"] as Entities.User;
            if (user != null)
            {
                TherapyCards.InnerHtml = "";
                var resultTherapies = DBTherapy.GetAll(user);

                foreach (var item in resultTherapies)
                {
                    try
                    {
                        string MedDescription = string.Empty;
                        if (item.Medication != null)
                        {
                            MedDescription = item.Medication.FirstOrDefault().Description;
                        }
                        string DesDescription = string.Empty;
                        if (item.Deseases != null)
                        {
                            DesDescription = item.Deseases.FirstOrDefault().Description;
                        }
                        TherapyCards.InnerHtml += "<div class='card-small'>" +"<div class='card-small-title'>" +(item.date.ToString("dd-MM-yyyy") ?? "") +"</div><hr/>" + (item.description ?? "") +"<br/>"+(item.Location??"")+"<br/>Kosten:"+(item.CostsInEuro)+"<hr/>Behandeld door: " + (item.therapistLastName ?? "") + "<br />Aandoening: " + (DesDescription??"") + "<br />Medicatie: " +( MedDescription??"") +"</div>";
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
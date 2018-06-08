using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Web.UI.DataVisualization.Charting;

namespace HCloud.PortalContent
{
    public partial class NewRapport : System.Web.UI.Page
    {
        DAL.DBTherapyConnection DBTherapyConnection = new DAL.DBTherapyConnection();
        BLL.ClientHelper clientHelper = new BLL.ClientHelper();
        List<Therapy> therapies = new List<Therapy>();
        protected void Page_Load(object sender, EventArgs e)
        {

            var user = Session["User"] as Entities.User;
            if (user != null)
            {
                if (!IsPostBack)
                {


                    therapies = DBTherapyConnection.GetAll(user) ?? throw new Exception("DBTherapyConnection.GetAll()->null exception");
                    TherapiesGrid.DataSource = therapies;
                    TherapiesGrid.DataBind();
                    FilterTherapyClient.DataSource = clientHelper.GetClientList(user) ?? throw new Exception("clientHelper.GetClientList()->null exception");
                    FilterTherapyClient.DataTextField = "FirstName";
                    FilterTherapyClient.DataValueField = "BsnNumber";
                    FilterTherapyClient.DataBind();
                    Filtername_date.InnerHtml = "";
                    Messaging.InnerHtml = "";
                    string woonplaats = "nieuw-lekkerland";
                    DAL.DBDeseaseConnection dBDeseaseConnection = new DAL.DBDeseaseConnection();
                    
                    
                }
            }
        }
        protected void SaveFilter_Click(object sender, EventArgs e)
        {
            string bsnstring = FilterTherapyClient.SelectedValue;
            int bsn = 0;
            if (!string.IsNullOrEmpty(bsnstring))
            {
                bsn = int.Parse(bsnstring);
            }
            try
            {
                if (!string.IsNullOrEmpty(FilterTherapyDate.Text) && bsn == 0)
                {
                    var date = DateTime.Parse(FilterTherapyDate.Text);
                    therapies = DBTherapyConnection.GetFilteredTherapies(date) ?? throw new Exception("Geen resultaten gevonden");

                    Filtername_date.InnerHtml = "op datum:<b>" + date + "</b>";
                }
                else if (string.IsNullOrEmpty(FilterTherapyDate.Text) && bsn != 0)
                {
                    therapies = DBTherapyConnection.GetFilteredTherapies(bsn) ?? throw new Exception("Geen resultaten gevonden");
                    Filtername_date.InnerHtml = "op Client:<b>" + FilterTherapyClient.SelectedItem.Text + "</b>";
                }
                else if (!string.IsNullOrEmpty(FilterTherapyDate.Text) && bsn != 0)
                {
                    var date = DateTime.Parse(FilterTherapyDate.Text);
                    therapies = DBTherapyConnection.GetFilteredTherapies(bsn, date) ?? throw new Exception("Geen resultaten gevonden");
                    Filtername_date.InnerHtml = "op Client:<b>" + FilterTherapyClient.SelectedItem.Text + "</b> en datum:<b>" + date.ToString("dd-MM-yyyy") + "</b>";
                }
                if (therapies != null)
                {
                    TherapiesGrid.DataSource = therapies;
                    TherapiesGrid.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
                Messaging.InnerHtml = "<span style='color:red;'><i class='glyphicon glyphicon-exclamation-sign'></i>" + exc + "</span>";
            }
        }
       

    }
}
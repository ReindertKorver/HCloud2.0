using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class AllMedications : System.Web.UI.Page
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
                        Messager.Text = "";
                        Messager.Visible = false;
                        ListBoxMedication.DataSource = dBMedication.GetAll(LoggedInUser);
                        ListBoxMedication.DataTextField = "Description";
                        ListBoxMedication.DataValueField = "ID";
                        ListBoxMedication.DataBind();

                        MedicationDoneBY.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName;

                        var resultMedications = dBMedication.GetAll(LoggedInUser);

                        foreach (var item in resultMedications)
                        {
                            MedicationCards.InnerHtml += "<div class='card-small'><div class='card-small-title'>" + (item.Description ?? "") + "</div>" + (item.HandedOut ? "Deze medicatie is afgegeven door " + item.getHandedOutByName() + "<br/>Afgegeven op: " + item.HandedOutDate.ToString("dd/MM/yyyy")+"<br/>Met houdbaarheidsdatum van: "+item.ExpirationDate.ToString("dd/MM/yyyy"):"Deze medicatie is nog niet opgehaald")+"</div>";
                        }
                    }
                }
            }
        
        }
        public void showMessage(string text)
        {
            Messager.Visible = true;
            Messager.Text = text;
        }
        protected void SaveMedication_Click(object sender, EventArgs e)
        {
            try
            {
                int DoneByID = LoggedInUser.ID;
                DateTime dateTimeHandedOut = DateTime.Parse(NewMedicationDate.Text);
                DateTime expirationdate = DateTime.Parse(ExpirationDate.Text);
                int medicationID = Convert.ToInt32(ListBoxMedication.SelectedValue);
                Entities.Medication medication = new Entities.Medication();
                medication.ExpirationDate = expirationdate;
                medication.HandedOut = true;
                medication.HandedOutDate = dateTimeHandedOut;
                medication.ID = medicationID;
                medication.HandedOutByID = DoneByID;

                string result = dBMedication.HandOutMedication(LoggedInUser, medication);
                showMessage(result);
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCloud.Entities;

namespace HCloud.PortalContent
{
    public partial class NewMedication : System.Web.UI.Page
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
                        NewMedicationClient.DataSource = clientHelper.GetClientList(LoggedInUser);
                        NewMedicationClient.DataTextField = "FirstName";
                        NewMedicationClient.DataValueField = "BsnNumber";
                        NewMedicationClient.DataBind();


                        NewMedicationTherapist.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName;
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

        protected void SaveMedication_Click(object sender, EventArgs e)
        {
            try
            {
                Medication medication = new Medication();
                medication.Description = NewMedicationDescriptionTB.Text;
                medication.Determiner = LoggedInUser.ID;
                DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
                bool result =dBMedication.Save(LoggedInUser,medication, int.Parse(NewMedicationClient.SelectedValue));
                if (result == true)
                    Label1.Text = "Medicatie succesvol opgeslagen";
                else
                    Label1.Text = "Er is iets fout gegaan";
            }
            catch (Exception ex)
            {
                Label1.Text = "Er is iets fout gegaan";
            }
        }
    }
}
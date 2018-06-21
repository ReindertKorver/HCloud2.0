using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCloud.Entities;
namespace HCloud
{
    public partial class NewTherapy : System.Web.UI.Page
    {
        Entities.User LoggedInUser;
        DAL.DBDeseaseConnection dBDesease = new DAL.DBDeseaseConnection();
        DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
        DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
        DAL.DBRapportConnection dBRapport = new DAL.DBRapportConnection();
        DAL.DBUserConnection dBUser = new DAL.DBUserConnection();
        BLL.ClientHelper clientHelper = new BLL.ClientHelper();
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
                        NewTherapyClient.DataSource = clientHelper.GetClientList(LoggedInUser);
                        NewTherapyClient.DataTextField = "FirstName";
                        NewTherapyClient.DataValueField = "BsnNumber";
                        NewTherapyClient.DataBind();

                        BLL.DeseaseHelper deseaseHelper = new BLL.DeseaseHelper();
                        List<Entities.Desease> listdesease = new List<Entities.Desease>();
                        listdesease.Add(new Entities.Desease { ID = 0, Description = "Geen" });
                        listdesease.AddRange(deseaseHelper.GetDeseaseList(LoggedInUser));
                        NewDesease.DataSource = listdesease;
                        NewDesease.DataTextField = "Description";
                        NewDesease.DataValueField = "ID";
                        NewDesease.DataBind();

                        BLL.MedicationHelper medicationHelper = new BLL.MedicationHelper();
                        List<Entities.Medication> result = new List<Entities.Medication>();
                        result.Add(new Entities.Medication { ID = 0, Description = "Geen" });
                        result.AddRange(medicationHelper.GetMedicationList(LoggedInUser));
                        NewMedicationDDL.DataSource = result;
                        NewMedicationDDL.DataTextField = "Description";
                        NewMedicationDDL.DataValueField = "ID";
                        NewMedicationDDL.DataBind();

                        NewTherapyTherapist.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName;
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
                DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
                Therapy therapy = new Therapy();
                therapy.date = Convert.ToDateTime(NewTherapyDate.Text);
                therapy.Time = TimeSpan.Parse(NewTherapyEndTime.Text);
                therapy.description = NewTherapyDescriptionTB.Text;
                therapy.Location = Location.Text ?? "";
                therapy.CostsInEuro = Convert.ToDecimal(Costs.Text ?? "0", CultureInfo.InvariantCulture);
                Desease desease = new Desease();
                desease.ID = Convert.ToInt32(NewDesease.SelectedValue);
                Medication medication = new Medication();
                medication.ID = Convert.ToInt32(NewMedicationDDL.SelectedValue);

               
                if (dBTherapy.IsTherapyAllowed(LoggedInUser, therapy, NewTherapyClient.SelectedValue))
                {
                    bool result = dBTherapy.Save(LoggedInUser, therapy, medication, desease, NewTherapyClient.SelectedValue);
                    if (result)
                        lbl.Text = "Behandeling is succesvol opgeslagen";
                    else
                        lbl.Text = "Er is iets fout gegaan";
                }
                else
                {
                    lbl.Text = "De behandelaar of de cliënt heeft al een behandeling gepland in het ingevuld tijdsbestek";
                }

            }
            catch (Exception ex)
            {
                lbl.Text = "Er is iets fout gegaan";
            }
        }
    }
}
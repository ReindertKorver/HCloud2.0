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
        public Entities.User LoggedInUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                LoggedInUser = Session["User"] as Entities.User;
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
                        //get user measures from database
                        if (!IsPostBack)
                        {
                            List<Measure> measures = dBUserConnection.GetUserMeasures(LoggedInUser);
                            List<string> items = new List<string>();
                            List<string> itemsCategories = new List<string>();
                            List<decimal> itemSeries = new List<decimal>();
                            foreach (var measure in measures)
                            {
                                items.Add(measure.Date.ToString("dd/MM/yyyy hh:mm") + " | Temperatuur: " + measure.Temperature.ToString() + " Bloeddruk: " + measure.BloodPressure);
                                itemSeries.Add(Convert.ToDecimal(measure.Temperature));
                                itemsCategories.Add(measure.Date.ToString("dd MMMM hh:mm"));

                            }
                            CareControlMeasuresLineChart.Series.Add(new AjaxControlToolkit.LineChartSeries() { Data = itemSeries.ToArray(), Name = "Temperatuur in Celsius", LineColor = "#127a7b" });

                            CareControlMeasures.DataSource = items;
                            CareControlMeasures.DataBind();
                            CareControlMeasuresLineChart.CategoriesAxis = string.Join(",", itemsCategories.ToArray());
                            CareControlMeasuresLineChart.DataBind();
                            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
                            fillUserData(data);
                        }
                        //get the user data from database


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
            else
            {
                Response.Redirect("/SignIn");
            }


        }
        public void fillUserData(UserData data)
        {
            PostCode.Text = data.PostCode ?? "Geen";
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
        }
        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
        protected void PostCodeSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(PostCodeTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.PostCode, PostCodeTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void StraatSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(PostCodeTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Straat, StraatTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void HuisnummerSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(HuisnummerTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Huisnummer, HuisnummerTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void WoonplaatsSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(WoonplaatsTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Woonplaats, WoonplaatsTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void GeboorteplaatsSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GeboorteplaatsTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Geboorteplaats, GeboorteplaatsTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void BloedgroepSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(BloedgroepTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Bloedgroep, BloedgroepTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void GeboorteDatumSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GeboorteDatumTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.GeboorteDatum, GeboorteDatumTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void BankrekeningSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(BankrekeningnummerTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Bankrekeningnummer, BankrekeningnummerTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }
        void showMessage(string text)
        {
            messager.InnerHtml = "Resultaat: " + text;
        }

        protected void ProvincieSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ProvincieTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Provincie, ProvincieTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }

        protected void NationaliteitSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(NationaliteitTXT.Text))
                {
                    showMessage(dBUserConnection.SetUserData(Session["User"] as Entities.User, UserData.Types.Nationaliteit, NationaliteitTXT.Text));
                }
                else
                {
                    throw new Exception("Vul wel iets in voor op opslaan te klikken");
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
            UserData data = UserData.GetUserDataFromDB(LoggedInUser);
            fillUserData(data);
        }
    }
}
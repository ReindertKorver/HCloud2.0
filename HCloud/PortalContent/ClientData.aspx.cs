using HCloud.DAL;
using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class ClientData : System.Web.UI.Page
    {
        public User UserDataUser;
        Entities.User LoggedInUser;
        public static User Data { get; set; }
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
                        List<User> clients = new List<User>();
                        clients.Add(new User() { FirstName = "geen", ID = 0 });
                        var dbusers = clientHelper.GetClientList(LoggedInUser);
                        clients.AddRange(dbusers);
                        NewUserData.DataSource = clients;

                        NewUserData.DataTextField = "FirstName";
                        NewUserData.DataValueField = "ID";
                        NewUserData.DataBind();
                        //DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                        //User UserData = dBUserConnection.GetUser(Convert.ToInt32(NewUserData.SelectedValue+1));
                        //UserDataController.Data = UserData;
                        //UserDataController.DataBind();
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

        protected void NewUserData_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBUserConnection dBUserConnection = new DBUserConnection();
            User UserDataData = dBUserConnection.GetUser(Convert.ToInt32(NewUserData.SelectedValue));
            Data = UserDataData;
            if (NewUserData.SelectedValue != "0")
            {

                if (Data != null)
                {

                    //get user measures from database

                    BSNNumberQR.Text = Data.BsnNumber;
                    List<Measure> measures = dBUserConnection.GetUserMeasures(Data);
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
                    UserData data = UserData.GetUserDataFromDB(Data);
                    fillUserData(data);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>makeCode(" + (Data.BsnNumber ?? "0") + ");</script>", false);
                }
            }

        }
        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
        protected void PostCodeSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(PostCodeTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.PostCode, PostCodeTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void StraatSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(StraatTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Straat, StraatTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void HuisnummerSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(HuisnummerTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Huisnummer, HuisnummerTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void WoonplaatsSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(WoonplaatsTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Woonplaats, WoonplaatsTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void GeboorteplaatsSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(GeboorteplaatsTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Geboorteplaats, GeboorteplaatsTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void BloedgroepSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(BloedgroepTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Bloedgroep, BloedgroepTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void GeboorteDatumSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(GeboorteDatumTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.GeboorteDatum, GeboorteDatumTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void BankrekeningSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(BankrekeningnummerTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Bankrekeningnummer, BankrekeningnummerTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }
        void showMessage(string text)
        {
            messager.InnerHtml = "Resultaat: " + text;
        }

        protected void ProvincieSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(ProvincieTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Provincie, ProvincieTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
            }
        }

        protected void NationaliteitSave_Click(object sender, EventArgs e)
        {
            if (Data != null)
            {
                try
                {

                    if (!string.IsNullOrEmpty(NationaliteitTXT.Text))
                    {
                        showMessage(dBUserConnection.SetUserData(Data, UserData.Types.Nationaliteit, NationaliteitTXT.Text));
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
                UserData data = UserData.GetUserDataFromDB(Data);
                fillUserData(data);
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
    }
}
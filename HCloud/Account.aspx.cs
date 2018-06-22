using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class Account : System.Web.UI.Page
    {
        public static string qrBSN;
        public static User Data { get; set; }
        public static User LoggedInUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProfileImageUpload.Visible = false;
            if (IsPostBack && ProfileImageUpload.PostedFile != null)
            {
                if (ProfileImageUpload.PostedFile.FileName.Length > 0)
                {
                    try
                    {

                        bool DirExists = System.IO.Directory.Exists(Server.MapPath("/Files/" + LoggedInUser.UniqueUserID + "/PF"));
                        if (!DirExists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("/Files/" + LoggedInUser.UniqueUserID + "/PF"));
                        }

                        ProfileImageUpload.SaveAs(Server.MapPath("/Files/" + LoggedInUser.UniqueUserID + "/PF/" + ProfileImageUpload.FileName));
                        string location = "/Files/" + LoggedInUser.UniqueUserID + "/PF/" + ProfileImageUpload.FileName;
                        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                        string result = dBUserConnection.SetUserData(LoggedInUser,UserData.Types.ProfilePicUrl,location);
                        ShowPFMessage(result+"<br/>Wanneer u opnieuw inlogd zal de foto zichtbaar zijn.","Resultaat");
                        ProfileImageUpload.Dispose();
                        ProfileImageUpload.PostedFile.InputStream.Dispose();
                        ProfileImageUpload.Attributes.Clear();
                    }
                    catch (Exception ex)
                    {
                        ShowPFMessage("Fout:"+ex.Message, "Resultaat");
                    }
                }
            }
            if (!IsPostBack)
            {
                messagerPF.Style.Add("display", "none!important");
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
                            //akkoord om op de pagina te zijn
                            IngelogdAls.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName + " BSN:" + LoggedInUser.BsnNumber;
                            Data = LoggedInUser;
                            if (Data != null)
                            {

                                //get user measures from database
                                if (!IsPostBack)
                                {
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
                                    DAL.DBRoleConnection dBRoleConnection = new DAL.DBRoleConnection();
                                    string roleDescription = null;
                                    try
                                    {
                                        var resultRights = dBRoleConnection.GetUserRights(LoggedInUser);
                                        roleDescription = "<br/>Rol: " + resultRights.Description;
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    ProfileInformation.InnerHtml = "";
                                    ProfileInformation.InnerHtml = "E-mailadress: " + LoggedInUser.EmailAdress + "<br/>Telefoonummer: " + LoggedInUser.PhoneNumber + "<br/>BSN nummer: " + LoggedInUser.BsnNumber + (roleDescription ?? "");
                                    UserName.Text = LoggedInUser.FirstName + " " + LoggedInUser.LastName;
                                    fillUserData(data);
                                }

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
                else
                {
                    Response.Redirect("/SignIn");
                }
            }

        }
        public void ShowPFMessage(string message, string title)
        {
            messagerPF.InnerHtml = "<b>"+title+"</b><br/>"+message;
            messagerPF.Style.Add("display", "block!important");
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("/SignIn");
        }

        protected void ToPortal_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PortalContent/DashBoard");
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
            ProfileImage.ImageUrl = data.ProfilePicUrl ?? "Resources/Account.png";
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
    }
}
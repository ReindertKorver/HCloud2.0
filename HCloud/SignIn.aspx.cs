using HCloud.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class SignIn : System.Web.UI.Page
    {
        
        private Entities.User UserCredentials;
        DAL.DBUserConnection userDB = new DAL.DBUserConnection();
        HCloud.SiteMaster SiteMaster = new SiteMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            InvisibleControls();
        }
        void InvisibleControls()
        {
            if (!IsPostBack)
            {
                MessageAlert.Visible = false;
            }
        }
        protected void SignInButton_Click(object sender, EventArgs e)
        {
            string Emailadress = SignInEmailTB.Text.ToLower();
            string PassWord = SignInPasswordTB.Text;
            Entities.User result1 = new Entities.User();
            //vul user credentials met email en wachtwoord+hashtouniqueid
            string uniqueId="";
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(Emailadress));
                 uniqueId = PassWordSecurity.NameUUIDFromBytes(hash);
            }
            UserCredentials = new Entities.User();
            UserCredentials.PassWordHash = uniqueId + BLL.PassWordSecurity.Hash(PassWord);
            UserCredentials.EmailAdress = Emailadress;
            if (UserCredentials != null)
            {
                if (!string.IsNullOrEmpty(UserCredentials.EmailAdress) && !string.IsNullOrEmpty(UserCredentials.PassWordHash))
                {
                    BLL.LogInHelper logInHelper = new BLL.LogInHelper();
                    try
                    {
                        //probeer in te loggen met ingevulde gegevens

                        result1 = logInHelper.LoginAtPageLoad(UserCredentials);
                        if (result1 != null || result1.ID != 0)
                        {
                            //inlog geaccepteerd
                            ShowMessagerAlert("U bent ingelogd");
                            Session["User"] = result1;
                            SiteMaster.setLoggedInText(UserCredentials.EmailAdress, "/Account");
                            Response.Redirect("/Account.aspx");


                        }
                        else
                        {
                            SiteMaster.setLoggedInText("Inloggen", "/SignIn");
                            ShowMessagerAlert("Deze combinatie van gebruikersnaam en wachtwoord is niet gevonden");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessagerAlert("Probleem bij ophalen van gebruikergegevens: " + ex.Message);
                    }

                }
                else
                {
                    ShowMessagerAlert("Er zijn geen gebruikersnaam of wachtwoord ingevuld");
                }
            }
            else
            {
                SiteMaster.setLoggedInText("Inloggen", "/SignIn");
                ShowMessagerAlert("Er zijn geen gebruikersnaam of wachtwoord ingevuld");
            }
            //do postback zodat inloggen vervangen wordt door emailadress
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);

        }
        void ShowMessagerAlert(string text)
        {
            Messager.Text = text;
            if (text == "")
            {
                MessageAlert.Visible = false;
            }
            else
            {

                MessageAlert.Visible = true;
            }
        }
        private string Login(string Emailadress, string PassWord)
        {
            try
            {
                try
                {
                    string uniqueId;
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(Emailadress));
                        uniqueId = PassWordSecurity.NameUUIDFromBytes(hash);
                    }
                    UserCredentials = userDB.GetUserSignInCredentials(Emailadress, uniqueId + BLL.PassWordSecurity.Hash(PassWord));
                }
                catch (Exception)
                {
                    return "Er is een fout opgetreden bij het ophalen van de gebruiker gegevens uit het database";
                }
                if (UserCredentials != null)
                {
                    string DBemail = UserCredentials.EmailAdress;
                    string DBpassword = UserCredentials.PassWordHash;
                    bool DBconfirmed = UserCredentials.Confirmed;
                    string DBUniqueId = UserCredentials.UniqueUserID;

                    if (DBconfirmed)
                    {
                        string TBPassword = PassWordSecurity.Hash(PassWord);
                        string uniqueId;
                        using (MD5 md5 = MD5.Create())
                        {
                            byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(DBemail));
                            uniqueId = PassWordSecurity.NameUUIDFromBytes(hash);
                        }
                        if (TBPassword == DBpassword && DBUniqueId == uniqueId)
                        {
                            //wachtwoord is gecontroleerd en goedgekeurd gebruiker mag worden ingelogd.
                            Session["User"] = UserCredentials;
                            Response.Redirect("/");
                            return "U wordt ingelogd";

                        }
                        else
                        {
                            // wachtwoord fout
                            return "Combinatie van gebruikersnaam en wachtwoord is niet goed";
                        }
                    }
                    else
                    {
                        //gebruiker mag niet inloggen!!
                        return "U bent nog niet geaccepteerd door één van de beheerders";
                    }
                }
                else
                {
                    return "Er is een fout opgetreden: 'UserCredentials was null'";
                }
            }
            catch (Exception)
            {
                return "Er is een fout opgetreden, probeer later opnieuw";
            }
        }



    }
}
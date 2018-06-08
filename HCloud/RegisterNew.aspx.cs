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
    public partial class RegisterNew : System.Web.UI.Page
    {
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
        void ShowRegisterController(string text, MessageType messageType)
        {
            RegisterControler.Text = text;
            if (text == "")
            {
                MessageAlert.Visible = false;
            }
            else
            {
                MessageAlert.Visible = true;
                if (messageType == MessageType.error)
                {
                    RegisterControler.ForeColor = System.Drawing.Color.Red;
                }
                else if (messageType == MessageType.info)
                {
                    RegisterControler.ForeColor = System.Drawing.Color.LightBlue;
                }
                else
                    if (messageType == MessageType.succes)
                {
                    RegisterControler.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
        protected void Register_Click(object sender, EventArgs e)
        {
            if (RegisterPasswordTB.Text == RegisterPasswordExtraTB.Text)
            {
                if(PhoneNumberTB.Text.Length>10)
                {
                    ShowRegisterController("Telefoonnummer is te lang", MessageType.error);
                }
                try
                {
                    
                    AddUser(RegisterNameTB.Text, RegisterLastNameTB.Text, RegisterPasswordTB.Text, RegisterEmailTB.Text.ToLower(), BsnNumberTB.Text, PhoneNumberTB.Text);
                    ShowRegisterController("U bent geregistreerd u ontvangt een email wanneer uw account beschikbaar is.", MessageType.succes);
                }
                catch (Exception ex)
                {
                    ShowRegisterController(ex.Message,MessageType.error);
                }
            }
            else
            {
                ShowRegisterController("De wachtwoorden zijn niet gelijk", MessageType.error);
            }
        }
        void AddUser(string FirstName, string LastName, string Password, string EmailAdress, string BsnNumber, string PhoneNumber)
        {
            Entities.User newUser = new Entities.User();
            newUser.FirstName = FirstName;
            newUser.LastName = LastName;
            newUser.PassWordHash = Password;
            newUser.EmailAdress = EmailAdress;
            newUser.BsnNumber = BsnNumber;
            newUser.PhoneNumber = PhoneNumber;
            string input = EmailAdress;
            string uniqueId;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(input.ToLower()));
                uniqueId = PassWordSecurity.NameUUIDFromBytes(hash);
            }
            newUser.UniqueUserID = uniqueId;

            DAL.DBUserConnection userDB = new DAL.DBUserConnection();
            userDB.AddUser(newUser);
            
        }
        enum MessageType {error,info,succes }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HCloud.BLL
{
    public class LogInHelper
    {
        private Entities.User UserCredentials;
        DAL.DBUserConnection userDB = new DAL.DBUserConnection();
        public Entities.User LoginAtPageLoad(Entities.User LoggedInUser)
        {
            try
            {

                try
                {
                    UserCredentials = userDB.GetUserSignInCredentials(LoggedInUser.EmailAdress, LoggedInUser.PassWordHash);
                }
                catch (Exception)
                {
                    throw new Exception("Er is een fout opgetreden bij het ophalen van de gebruiker gegevens uit het database");
                }
                if (UserCredentials != null)
                {
                    string DBemail = UserCredentials.EmailAdress;
                    string DBpassword = UserCredentials.PassWordHash;
                    bool DBconfirmed = UserCredentials.Confirmed;
                    string DBUniqueId = UserCredentials.UniqueUserID;

                    if (DBconfirmed)
                    {
                        string TBPassword = LoggedInUser.PassWordHash;
                        Guid result;
                        using (MD5 md5 = MD5.Create())
                        {
                            byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(DBemail));
                            result = new Guid(hash);
                        }
                        Guid NewUniqueID = result;
                        if (TBPassword == DBpassword)
                        {
                            //wachtwoord is gecontroleerd en goedgekeurd gebruiker mag worden ingelogd.
                            
                            return UserCredentials;

                        }
                        else
                        {
                            // wachtwoord fout
                            throw new Exception("Combinatie van gebruikersnaam en wachtwoord is niet goed");
                        }
                    }
                    else
                    {
                        //gebruiker mag niet inloggen!!
                        throw new Exception("U bent nog niet geaccepteerd door één van de beheerders");
                    }
                }
                else
                {
                    throw new Exception("Geen gebruiker gevonden met de combinatie van gebruikersnaam en wachtwoord");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
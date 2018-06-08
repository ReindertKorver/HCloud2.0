using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.BLL
{
    public class ClientHelper
    {

        DAL.DBUserConnection dBUser = new DAL.DBUserConnection();
        public List<Entities.User> GetClientList(Entities.User LoggedInUser)
        {
            List<Entities.User> resultClients;
            try
            {
                resultClients = dBUser.getClients(LoggedInUser);
                
                
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }


            return resultClients;
        }
    }
}
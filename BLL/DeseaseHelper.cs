using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.BLL
{
    public class DeseaseHelper
    {
        DAL.DBDeseaseConnection dBDesease = new DAL.DBDeseaseConnection();
        public List<Entities.Desease> GetDeseaseList(Entities.User LoggedInUser)
        {
            List<Entities.Desease> resultDeseases;
            try
            {
                resultDeseases = dBDesease.getDeseases(LoggedInUser);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultDeseases;
        }
    }
}
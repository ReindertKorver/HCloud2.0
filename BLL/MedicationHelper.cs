using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.BLL
{
    public class MedicationHelper
    {
        DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
        public List<Entities.Medication> GetMedicationList(Entities.User LoggedInUser)
        {
            List<Entities.Medication> resultMedication;
            try
            {
                resultMedication = dBMedication.getMedications(LoggedInUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultMedication;
        }
    }
}
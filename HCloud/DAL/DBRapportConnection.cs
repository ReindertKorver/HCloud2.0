using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCloud.Entities;

namespace HCloud.DAL
{
    public class DBRapportConnection : Interfaces.Display
    {
        public override void Delete(User user)
        {
        }

        public override void GetAll(User user)
        {
        }

        public  User GetOwn(User user)
        {
            return new User();
        }

        public override void New(User user)
        {
        }
    }
}
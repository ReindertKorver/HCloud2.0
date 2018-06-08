using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCloud.Interfaces
{
    abstract public class Display
    {
        public abstract void GetAll(Entities.User user);
        public abstract void New(Entities.User user);
        public abstract void Delete(Entities.User user);
    }
}
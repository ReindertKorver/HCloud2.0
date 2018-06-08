using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Exception)Session["UnhandledExceptions"] != null)
                {
                    
                    DetailedErrorPanel.Visible = true;
                    var exception = (Exception)Session["UnhandledExceptions"];
                    FriendlyErrorMsg.Text = exception.Message;
                        InnerMessage.Text = exception.InnerException.ToString();
                    ErrorHandler.Text = "Global.Application_Error()";
                    ErrorDetailedMsg.Text = exception.InnerException.StackTrace;
                }
            }
        }
        
    }
}
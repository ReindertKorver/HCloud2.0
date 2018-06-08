using HCloud.DAL;
using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class AllFiles : System.Web.UI.Page
    {
        Entities.User LoggedInUser;
        protected void Page_Load(object sender, EventArgs e)
        {

            BLL.ClientHelper clientHelper = new BLL.ClientHelper();
            if (Session["User"] != null)
            {
                LoggedInUser = Session["User"] as Entities.User;
                if (LoggedInUser != null)
                {
                    if (!IsPostBack)
                    {
                        List<User> clients = new List<User>();
                        clients.Add(new User() { FirstName = "geen", ID = 0 });
                        var dbusers = clientHelper.GetClientList(LoggedInUser);
                        clients.AddRange(dbusers);
                        NewDeseaseClient.DataSource = clients;

                        NewDeseaseClient.DataTextField = "FirstName";
                        NewDeseaseClient.DataValueField = "ID";
                        NewDeseaseClient.DataBind();




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

        protected void NewDeseaseClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewDeseaseClient.SelectedValue != "0")
            {
                if ((User)Session["User"] != null)
                {
                    FilesMain.InnerHtml = "";
                    DBFileConnection dBFileConnection = new DBFileConnection();
                    DBUserConnection dBUserConnection = new DBUserConnection();
                    User FileUser = dBUserConnection.GetUser(Convert.ToInt32(NewDeseaseClient.SelectedValue));
                    List<File> Files = dBFileConnection.GetFiles(FileUser);
                    foreach (var file in Files)
                    {
                        string fileExt = System.IO.Path.GetExtension(file.FilePath);
                        string imgSource = "/Resources/file.png";
                        if (fileExt == ".docx")
                        {
                            imgSource = "/Resources/docx.png";
                        }
                        else if (fileExt == ".pdf")
                        {
                            imgSource = "/Resources/pdf.png";
                        }
                        else if (fileExt == ".png")
                        {
                            imgSource = "/Resources/png.png";
                        }
                        else if (fileExt == ".jpg")
                        {
                            imgSource = "/Resources/jpg.png";
                        }
                        FilesMain.InnerHtml += "<a href=" + file.FilePath + " target='_blank' dowload><div class='filecard' id=" + file.ID + "><div class='filetext'>" + file.Description + "</div><img class='filecardimg' src=" + imgSource + " /></div></a> ";
                    }
                }
            }
            else
            {
                FilesMain.InnerHtml = "";
            }
        }
    }
}
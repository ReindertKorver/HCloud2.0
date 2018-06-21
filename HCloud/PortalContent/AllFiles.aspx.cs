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
        public static List<File> UserFiles;
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
                        UserFiles = null;
                        List<User> clients = new List<User>();
                        clients.Add(new User() { FirstName = "geen", ID = 0 });
                        var dbusers = clientHelper.GetClientList(LoggedInUser);
                        clients.AddRange(dbusers);
                        NewDeseaseClient.DataSource = clients;

                        NewDeseaseClient.DataTextField = "FirstName";
                        NewDeseaseClient.DataValueField = "ID";
                        NewDeseaseClient.DataBind();

                        SortBy.Items.Add(new ListItem() { Text = "Datum aflopend", Value = File.Sortby.dateDESC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Datum oplopend", Value = File.Sortby.dateASC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Beschrijving aflopend", Value = File.Sortby.descDESC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Beschrijving oplopend", Value = File.Sortby.descASC.ToString() });

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
                    UserFiles = null;
                    DBFileConnection dBFileConnection = new DBFileConnection();
                    DBUserConnection dBUserConnection = new DBUserConnection();
                    User FileUser = dBUserConnection.GetUser(Convert.ToInt32(NewDeseaseClient.SelectedValue));
                    List<File> files = dBFileConnection.GetFiles(FileUser);
                    UserFiles = files.OrderByDescending(o => o.Date).ToList();
                    string html = setHTMLFiles(UserFiles);
                    FilesMain.InnerHtml = html;
                }
            }
            else
            {
                FilesMain.InnerHtml = "";
            }
        }
        public string setHTMLFiles(List<File> files)
        {
            string HTML = "";
            foreach (var file in files)
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
                HTML += "<a href=" + file.FilePath + " target='_blank' dowload><div class='filecard' id=" + file.ID + "><div class='filetext'>" + file.Description + "</div><img class='filecardimg' src=" + imgSource + " /></div></a> ";
            }
            return HTML;
        }

        protected void SortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FilesMain.InnerHtml != "" && UserFiles != null)
            {
                List<File> files = new List<File>();

                switch (SortBy.SelectedValue.ToString())
                {
                    case "dateDESC":
                        files = UserFiles.OrderByDescending(o => o.Date).ToList();
                        break;
                    case "dateASC":
                        files = UserFiles.OrderBy(o => o.Date).ToList();

                        break;
                    case "descDESC":
                        files = UserFiles.OrderByDescending(o => o.Description).ToList();
                        break;
                    case "descASC":
                        files = UserFiles.OrderBy(o => o.Description).ToList();

                        break;
                    default:
                        files = UserFiles.OrderByDescending(o => o.Date).ToList();
                        break;
                }
                UserFiles = files;
                string html = setHTMLFiles(files);
                FilesMain.InnerHtml = html;
            }
        }
    }
}
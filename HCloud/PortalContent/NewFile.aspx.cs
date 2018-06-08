using HCloud.DAL;
using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class NewFile : System.Web.UI.Page
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
                        NewFileClient.DataSource = clientHelper.GetClientList(LoggedInUser);
                        NewFileClient.DataTextField = "FirstName";
                        NewFileClient.DataValueField = "ID";
                        NewFileClient.DataBind();



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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        public void file_Click(object sender, EventArgs e)
        {
            FileResult.InnerHtml = "";
            if (file.HasFile)
            {

                try
                {
                    string filename = Path.GetFileName(file.FileName);
                    string fileExt = System.IO.Path.GetExtension(file.FileName);
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
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void StatusLabel_Click(object sender, EventArgs e)
        {

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (file.HasFile)
            {
                if (!string.IsNullOrEmpty(FileDescription.Text))
                {
                    if (NewFileClient.SelectedItem != null)
                    {
                        try
                        {
                            DBUserConnection dBUserConnection = new DBUserConnection();
                            User FileUser = dBUserConnection.GetUser(Convert.ToInt32(NewFileClient.SelectedValue));

                            bool DirExists = System.IO.Directory.Exists(Server.MapPath("/Files/" + FileUser.UniqueUserID + "/"));
                            if (!DirExists)
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("/Files/" + FileUser.UniqueUserID + "/"));
                            }
                            file.SaveAs(Server.MapPath("/Files/" + FileUser.UniqueUserID + "/" + file.FileName));
                            DBFileConnection dBFileConnection = new DBFileConnection();
                            Entities.File NewFile = new Entities.File();
                            NewFile.Description = FileDescription.Text;
                            NewFile.FileName = file.FileName;
                            NewFile.FilePath = "/Files/" + FileUser.UniqueUserID + "/" + file.FileName;
                            dBFileConnection.Save(LoggedInUser,NewFile,FileUser.ID);
                            Message.Text = "Bestand is opgeslagen";
                        }
                        catch (Exception ex)
                        {
                            Message.Text = "Er is een uitzondering opgetreden bij het opslaan van " + file.FileName + "<br/>" + ex.Message;
                        }
                    }
                    else
                    {
                        Message.Text = "Er is geen gebruiker geselecteerd";
                    }
                }
                else
                {
                    Message.Text = "Er is geen beschrijving ingevuld";
                }
            }
            else
            {
                Message.Text = "Er is geen bestand gekozen";
            }
        }
    }
}
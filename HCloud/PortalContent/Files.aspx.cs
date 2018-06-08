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
    public partial class Files : System.Web.UI.Page
    {
        static List<File> DBFiles { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((User)Session["User"] != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        User user = (User)Session["User"];
                        FilesFrom.Text = "Bestanden van <b>" + (user.FirstName??"")+" "+ (user.LastName??"")+"</b>";
                        DBFileConnection dBFileConnection = new DBFileConnection();
                        DBFiles = dBFileConnection.GetFiles((User)Session["User"]);
                        AppendFiles(DBFiles);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        void AppendFiles(List<File> files)
        {
            FilesMain.InnerHtml = "";
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
                FilesMain.InnerHtml += "<a href=" + file.FilePath + " target='_blank' dowload><div class='filecard' id=" + file.ID + "><div class='filetext'>" + file.Description + "</div><img class='filecardimg' src=" + imgSource + " /></div></a> ";
            }
        }
        void FilterFiles()
        {
            try
            {
                //wel datum niet format
                if (!string.IsNullOrEmpty(FilterFileDate.Text) && string.IsNullOrEmpty(FilterFileFormat.Text))
                {
                    List<File> bkfiles = new List<File>();
                    foreach (var fileitem in DBFiles)
                    {
                        DateTime date = DateTime.Parse(FilterFileDate.Text);
                        if ( fileitem.Date.Date == date.Date)
                        {
                            bkfiles.Add(fileitem);
                        }
                    }
                    AppendFiles(bkfiles);
                }
                //wel datum wel format
                else if (!string.IsNullOrEmpty(FilterFileDate.Text) && !string.IsNullOrEmpty(FilterFileFormat.Text))
                {
                    List<File> bkfiles = new List<File>();
                    foreach (var fileitem in DBFiles)
                    {
                        string filepath = fileitem.FilePath;
                        string fileext = System.IO.Path.GetExtension(filepath);
                        DateTime date = DateTime.Parse(FilterFileDate.Text);
                        if (fileext == FilterFileFormat.Text && fileitem.Date.Date == date.Date)
                        {
                            bkfiles.Add(fileitem);
                        }
                    }
                    AppendFiles(bkfiles);
                }
                //niet datum wel format
                else if (string.IsNullOrEmpty(FilterFileDate.Text) && !string.IsNullOrEmpty(FilterFileFormat.Text))
                {
                    List<File> bkfiles = new List<File>();
                    foreach (var fileitem in DBFiles)
                    {
                        string filepath = fileitem.FilePath;
                        string fileext = System.IO.Path.GetExtension(filepath);
                        if (fileext == FilterFileFormat.Text)
                        {
                            bkfiles.Add(fileitem);
                        }
                    }
                    AppendFiles(bkfiles);
                }
                //niet datum niet format
                else if (string.IsNullOrEmpty(FilterFileDate.Text) && string.IsNullOrEmpty(FilterFileFormat.Text))
                {

                }
            }
            catch (Exception ex)
            {
                Messager.Text = "Filterfout: " + ex.Message;
            }
        }

        protected void SaveFilter_Click(object sender, EventArgs e)
        {
            FilterFiles();
        }
    }
}
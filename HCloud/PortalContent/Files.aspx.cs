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

                        SortBy.Items.Add(new ListItem() { Text = "Datum aflopend", Value = File.Sortby.dateDESC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Datum oplopend", Value = File.Sortby.dateASC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Beschrijving aflopend", Value = File.Sortby.descDESC.ToString() });
                        SortBy.Items.Add(new ListItem() { Text = "Beschrijving oplopend", Value = File.Sortby.descASC.ToString() });
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
            FilesMain.InnerHtml = setHTMLFiles(files);
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
            if (FilesMain.InnerHtml != "" && DBFiles != null)
            {
                List<File> files = new List<File>();

                switch (SortBy.SelectedValue.ToString())
                {
                    case "dateDESC":
                        files = DBFiles.OrderByDescending(o => o.Date).ToList();
                        break;
                    case "dateASC":
                        files = DBFiles.OrderBy(o => o.Date).ToList();

                        break;
                    case "descDESC":
                        files = DBFiles.OrderByDescending(o => o.Description).ToList();
                        break;
                    case "descASC":
                        files = DBFiles.OrderBy(o => o.Description).ToList();

                        break;
                    default:
                        files = DBFiles.OrderByDescending(o => o.Date).ToList();
                        break;
                }
                DBFiles = files;
                string html = setHTMLFiles(files);
                FilesMain.InnerHtml = html;
            }
        }

        protected void DelFilter_Click(object sender, EventArgs e)
        {
            FilterFileDate.Text = "";
            FilterFileFormat.Text = "";
            FilesMain.InnerHtml = "";
            User user = (User)Session["User"];
            FilesFrom.Text = "Bestanden van <b>" + (user.FirstName ?? "") + " " + (user.LastName ?? "") + "</b>";
            DBFileConnection dBFileConnection = new DBFileConnection();
            DBFiles = dBFileConnection.GetFiles((User)Session["User"]);
            AppendFiles(DBFiles);
        }
    }
}
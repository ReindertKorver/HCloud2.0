
using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class Portal : MasterPage
    {
        Entities.Role role;
        DAL.DBDeseaseConnection dBDesease = new DAL.DBDeseaseConnection();
        DAL.DBMedicationConnection dBMedication = new DAL.DBMedicationConnection();
        DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
        DAL.DBRapportConnection dBRapport = new DAL.DBRapportConnection();
        DAL.DBUserConnection dBUser = new DAL.DBUserConnection();
        Entities.User LoggedInUser;
        bool ShowWorkbar { get { return (Convert.ToBoolean(Application["ShowToolBar"]??1)); } set { Application["ShowToolBar"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ShowWorkbar)
            {
                SideNavH.Visible = true;
                CollapseButton.Text = "<span class='glyphicon glyphicon-chevron-left'></span>";
                Collumn2.Style.Value = "";

                CollapseButton.ToolTip = "Werkbalk verbergen";
                ShowWorkbar = true;
            }
            else
            {
                SideNavH.Visible = false;
                CollapseButton.Text = "<span class='glyphicon glyphicon-chevron-right'></span>";
                Collumn2.Style.Value = "width:100%;";
                CollapseButton.ToolTip = "Werkbalk uitvouwen";
                ShowWorkbar = false;
            }
            Page.Title = "Portaal " + System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            if (!IsPostBack)
            {
                
                HideVisibles();
                if (Session["User"] != null)
                {
                    LoggedInUser = Session["User"] as Entities.User;
                    if (LoggedInUser != null)
                    {
                        BLL.LogInHelper logInHelper = new BLL.LogInHelper();
                        Entities.User result = new Entities.User();
                        try
                        {
                            result = logInHelper.LoginAtPageLoad(LoggedInUser);
                        }
                        catch (Exception)
                        {
                            Response.Redirect("/SignIn");
                        }
                        if (result != null)
                        {
                            //akkoord om op de pagina te zijn+controle rechten rollen
                            //Select user.RoleID, rights.ShowOwnDeseases, rights.ShowOwnTherapies, rights.ShowAllDeseases, rights.ShowAllTherapies, rights.ShowNewTherapy, rights.ShowNewDesease, rights.ShowNewMedication, rights.ShowOwnMedication, rights.ShowNewRapport, rights.ShowOwnRapports, rights.ShowAllRapports, rights.ChangeClientNAW, rights.ShowAllMedications from user inner join roles on roles.ID = user.RoleID inner join rights on rights.ID= roles.RightsID  where user.BsnNumber='0123456790'and user.UniqueID='82a7969a-3570-f75b-a2cd-76c7ff0a396d'
                            DAL.DBRoleConnection dBRoleConnection = new DAL.DBRoleConnection();

                            role = dBRoleConnection.GetUserRights(LoggedInUser);
                            //haal alle gegevens aan het begin op met laad scherm
                            string previousPageName = "";
                            if (Request.UrlReferrer != null)
                                previousPageName = Request.UrlReferrer.AbsolutePath;

                            string url = Request.Url.AbsolutePath;
                            string currentPage = Path.GetFileName(Request.PhysicalPath);
                            List<string> listurl = url.Split('/').ToList();
                            List<string> Finallisturl = new List<string>();
                            {
                                Finallisturl.Add(previousPageName);
                                Finallisturl.AddRange(listurl);
                            }
                            foreach (string item in Finallisturl)
                            {
                                if (item != "" && item != "PortalContent" && !item.StartsWith("/"))
                                    BreadCrumb.InnerHtml += "<li><a href='" + item + "'>" + item + "</a></li>";
                                else if (item == "PortalContent")
                                {
                                    BreadCrumb.InnerHtml += "<li><a href='DashBoard'>" + item + "</a></li>";
                                }
                                else if (item.StartsWith("/"))
                                {
                                    BreadCrumb.InnerHtml += "<li><a href='" + item + "'><span class='glyphicon glyphicon-arrow-left'></span></a></li>";
                                }
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
                else
                {
                    Response.Redirect("/SignIn");
                }
                if (role != null)
                {
                    ShowOwnDeseases.Visible = role.ShowOwnDeseases;


                    ShowOwnTherapies.Visible = role.ShowOwnTherapies;

                    ShowOwnMedications.Visible = role.ShowOwnMedication;

                    ShowAllDeseases.Visible = role.ShowAllDeseases;

                    ShowAllTherapies.Visible = role.ShowAllTherapies;

                    ShowAllMedications.Visible = role.ShowAllMedications;

                    ShowNewDeseases.Visible = role.ShowNewDesease;

                    ShowNewTherapies.Visible = role.ShowNewTherapy;

                    ShowNewMedication.Visible = role.ShowNewMedication;

                    Rapports.Visible = role.ShowNewRapport;

                    Management.Visible = role.Management;

                    ShowAllFiles.Visible = role.ShowAllFiles;

                    ShowNewFile.Visible = role.ShowNewFile;

                    ShowOwnFiles.Visible = role.ShowOwnFiles;

                    ShowClientData.Visible = role.ShowClientData;
                }
                else
                {
                    Response.Redirect("/ErrorPage");
                }
            }

        }
        void HideVisibles()
        {
            ShowOwnDeseases.Visible = false;
            ShowOwnTherapies.Visible = false;
            ShowOwnMedications.Visible = false;
            ShowOwnRaports.Visible = false;
            ShowAllDeseases.Visible = false;
            ShowAllTherapies.Visible = false;
            ShowAllMedications.Visible = false;
            ShowAllRapports.Visible = false;
            ShowNewDeseases.Visible = false;
            ShowNewTherapies.Visible = false;
            ShowNewMedication.Visible = false;
            Rapports.Visible = false;
            Management.Visible = false;
            ShowAllFiles.Visible = false;
            ShowNewFile.Visible = false;
            ShowOwnFiles.Visible = false;
            ShowClientData.Visible = false;
        }
        public void ShowCard(string url)
        {
            Response.Redirect(url);
        }

        protected void ShowOwnDeseases_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/OwnDesease");
        }

        protected void ShowOwnTherapies_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/OwnTherapy");

        }

        protected void ShowOwnMedications_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/OwnMedication");
        }

        protected void ShowOwnRaports_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/OwnRapport");
        }

        protected void ShowAllDeseases_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/AllDeseases");
        }

        protected void ShowAllTherapies_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/AllTherapies");
        }

        protected void ShowAllMedications_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/AllMedications");
        }

        protected void ShowAllRapports_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/AllRapports");
        }

        protected void ShowNewDeseases_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/NewDesease");
        }

        protected void ShowNewTherapies_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/NewTherapy");
        }

        protected void ShowNewMedication_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/NewMedication");
        }

        protected void ShowNewRapport_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/Rapport");
        }

        protected void ShowNewNAW_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/Management");
        }
        protected void ShowFiles_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/Files");
        }

        protected void CollapseButton_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (SideNavH.Visible == true)
                {
                    SideNavH.Visible = false;
                    CollapseButton.Text = "<span class='glyphicon glyphicon-chevron-right'></span>";
                    Collumn2.Style.Value = "width:100%;";
                    CollapseButton.ToolTip = "Werkbalk uitvouwen";
                    ShowWorkbar = false;
                }
                else
                {
                    SideNavH.Visible = true;
                    CollapseButton.Text = "<span class='glyphicon glyphicon-chevron-left'></span>";
                    Collumn2.Style.Value = "";

                    CollapseButton.ToolTip = "Werkbalk verbergen";
                    ShowWorkbar = true;

                }
            }
        }

        protected void AgendaModule_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/Agenda");
        }

        protected void ShowNewFile_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/NewFile");
        }

        protected void ShowAllFiles_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/AllFiles");
        }

        protected void ShowClientData_Click(object sender, EventArgs e)
        {
            ShowCard("/PortalContent/ClientData");
        }
    }
}

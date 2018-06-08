using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class Management : System.Web.UI.Page
    {
        Entities.User loggedinUser;
        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

            //SELECT user.BsnNumber,user.EmailAdress, user.FirstName,user.LastName from user  where user.Confirmed=0
            if (!IsPostBack)
            {
                Messager.Text = "";
                Messager.Visible = false;

                loggedinUser = Session["User"] as Entities.User;
                if (loggedinUser != null)
                {
                    try
                    {
                        UserGrid.DataSource = dBUserConnection.getPendingUsers(loggedinUser);
                        UserGrid.DataBind();
                        ListBoxUsers.DataSource = dBUserConnection.GetAllUsers(loggedinUser);
                        ListBoxUsers.DataTextField = "FirstName";
                        ListBoxUsers.DataValueField = "BsnNumber";
                        ListBoxUsers.DataBind();

                        DAL.DBRoleConnection dBRoleConnection = new DAL.DBRoleConnection();
                        ListBoxRoles.DataSource = dBRoleConnection.GetRoles();
                        ListBoxRoles.DataTextField = "Description";
                        ListBoxRoles.DataValueField = "RoleID";
                        ListBoxRoles.DataBind();

                        ListBoxUserTherapist.DataSource = ListBoxUsers.DataSource;
                        ListBoxUserTherapist.DataTextField = "FirstName";
                        ListBoxUserTherapist.DataValueField = "BsnNumber";
                        ListBoxUserTherapist.DataBind();

                        ListBoxTherapist.DataSource = dBUserConnection.GetAllUsersWithRole(loggedinUser);
                        ListBoxTherapist.DataTextField = "FirstName";
                        ListBoxTherapist.DataValueField = "ID";
                        ListBoxTherapist.DataBind();

                        ListboxFunctionRoles.DataSource = dBRoleConnection.GetRoles();
                        ListboxFunctionRoles.DataTextField = "Description";
                        ListboxFunctionRoles.DataValueField = "RoleID";
                        ListboxFunctionRoles.DataBind();

                        RoleGrid.DataSource = dBRoleConnection.GetRights();
                        RoleGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        showMessage(ex.Message);
                    }
                }
            }
        }

        protected void SaveAllowUser_Click(object sender, EventArgs e)
        {
            loggedinUser = Session["User"] as Entities.User;
            int result = 0;
            foreach (GridViewRow row in UserGrid.Rows)
            {
                CheckBox ck = ((CheckBox)row.FindControl("ItemCheckBox"));
                if (ck.Checked && ck.Visible)
                {
                    try
                    {
                        var a = row.Cells[1].Text;
                        DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                        int result1 = dBUserConnection.ConfirmUser(loggedinUser, row.Cells[2].Text, row.Cells[3].Text);
                        result = +result1;
                    }
                    catch (Exception ex)
                    {
                        showMessage(ex.Message);
                    }
                }
                else
                {
                    //Code if it is not checked ......may not be required
                }
            }
            if (result != 1)
                showMessage("Er zijn " + result + " gebruikers goedgekeurd");
            else
                showMessage("Er is " + result + " gebruiker goedgekeurd");
        }
        public void showMessage(string text)
        {
            Messager.Visible = true;
            Messager.Text = text;
        }

        protected void SaveRole_Click(object sender, EventArgs e)
        {
            var bsn = ListBoxUsers.SelectedValue;
            var roleid = ListBoxRoles.SelectedValue;
            if (!string.IsNullOrEmpty(bsn) && !string.IsNullOrEmpty(roleid))
            {
                DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                var result = dBUserConnection.ChangeRole(loggedinUser, bsn, Convert.ToInt32(roleid));
                showMessage(result);
            }
            else
            {
                showMessage("FOUT: Er is of geen rol of geen gebruiker geselecteerd!");
            }

        }

        protected void SaveTherapistChange_Click(object sender, EventArgs e)
        {
            var bsn = ListBoxUserTherapist.SelectedValue;
            var Id = ListBoxTherapist.SelectedValue;
            if (!string.IsNullOrEmpty(bsn) && !string.IsNullOrEmpty(Id))
            {
                DAL.DBUserConnection dBUserConnection = new DAL.DBUserConnection();
                var result = dBUserConnection.ChangeTherapist(loggedinUser, bsn, Convert.ToInt32(Id));
                showMessage(result);
            }
            else
            {
                showMessage("FOUT: Er is of geen behandelaar of geen gebruiker geselecteerd!");
            }
        }

        protected void ChangeRoleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Role role = new Role();
                foreach (GridViewRow row in RoleGrid.Rows)
                {

                    CheckBox ck = ((CheckBox)row.FindControl("ItemCheckBoxRole"));
                    if (ck.Checked && ck.Visible)
                    {
                        var a = row.Cells[1].Text;
                        if (a == "ShowOwnDeseases")
                            role.ShowOwnDeseases = true;
                        if (a == "ShowOwnTherapies")
                            role.ShowOwnTherapies = true;
                        if (a == "ShowAllDeseases")
                            role.ShowAllDeseases = true;
                        if (a == "ShowAllTherapies")
                            role.ShowAllTherapies = true;
                        if (a == "ShowNewTherapy")
                            role.ShowNewTherapy = true;
                        if (a == "ShowNewDesease")
                            role.ShowNewDesease = true;
                        if (a == "ShowNewMedication")
                            role.ShowNewMedication = true;
                        if (a == "ShowOwnMedication")
                            role.ShowOwnMedication = true;
                        if (a == "ShowNewRapport")
                            role.ShowNewRapport = true;
                        if (a == "ShowOwnRapports")
                            role.ShowOwnRapports = true;
                        if (a == "ShowAllRapports")
                            role.ShowAllRapports = true;
                        if (a == "ChangeClientNAW")
                            role.Management = true;
                        if (a == "ShowAllMedications")
                            role.ShowAllMedications = true;


                        role.Description = ListboxFunctionRoles.SelectedItem.Text;
                        role.RoleID = Convert.ToInt32(ListboxFunctionRoles.SelectedValue);
                    }
                    else
                    {
                        //Code if it is not checked ......may not be required
                    }
                }
                DAL.DBRoleConnection dBRoleConnection = new DAL.DBRoleConnection();
               
                string result = dBRoleConnection.ChangeRole(Session["User"] as Entities.User, role);
                showMessage(result);
            }
            catch (Exception ex)
            {
                showMessage(ex.Message);
            }
        }
    }
}
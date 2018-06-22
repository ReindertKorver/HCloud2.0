using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((User)Session["User"] != null)
            {
                DAL.DBRoleConnection dBRoleConnection = new DAL.DBRoleConnection();
                Entities.Role role= dBRoleConnection.GetUserRights((User)Session["User"]);
                if (role.ShowNewTherapy == true)
                {
                    if (!IsPostBack)
                    {
                        DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
                        List<Therapy> therapy = dBTherapy.GetTherapiesFromTherapist((User)Session["User"]);
                        List<AgendaItem> agendaItems = ConvertTherapyToAcceptedAgendaItems(therapy);

                        ActivityAgendaTherapist.DataSource = agendaItems;
                        ActivityAgendaTherapist.TimeFormat = DayPilot.Web.Ui.Enums.TimeFormat.Clock24Hours;
                        ActivityAgendaTherapist.StartDate = DateTime.Now;
                        ActivityAgendaTherapist.Days = 7;
                        ActivityAgendaTherapist.DataTextField = "Description";
                        ActivityAgendaTherapist.DataStartField = "StartTime";
                        ActivityAgendaTherapist.DataEndField = "EndTime";
                        ActivityAgendaTherapist.DataIdField = "ID";
                        ActivityAgendaTherapist.ShowToolTip = true;
                        ActivityAgendaTherapist.DataBind();
                        List<Therapy> newListTherapies = new List<Therapy>();
                        foreach (var item in therapy)
                        {
                            if (!item.Accepted)
                            {
                                newListTherapies.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    ActivityAgendaTherapist.Visible = false;
                    ActivityAgendaTherapist.Enabled = false;
                }
            }
            else
            {
                ActivityAgendaTherapist.Visible = false;
                ActivityAgendaTherapist.Enabled = false;
            }
        }

        protected void NextDay_Click(object sender, EventArgs e)
        {
            ActivityAgendaTherapist.StartDate = ActivityAgendaTherapist.StartDate.AddDays(1);
        }

        protected void BackDay_Click(object sender, EventArgs e)
        {
            ActivityAgendaTherapist.StartDate = ActivityAgendaTherapist.StartDate.AddDays(-1);
        }

        protected void Today_Click(object sender, EventArgs e)
        {
            ActivityAgendaTherapist.StartDate = DateTime.Now;
        }

        protected void countdays_TextChanged(object sender, EventArgs e)
        {
            if (countdays.Text != "")
            {
                if (countdays.Text != "0")
                {
                    if (!countdays.Text.Contains("-"))
                    {
                        ActivityAgendaTherapist.Days = Convert.ToInt32(countdays.Text);
                    }
                }
            }
        }

        public List<AgendaItem> ConvertTherapyToAcceptedAgendaItems(List<Therapy> therapies)
        {
            List<AgendaItem> agendaItems = new List<AgendaItem>();
            foreach (var therapy in therapies)
            {
                if (therapy.Accepted)
                {
                    AgendaItem agendaItem = new AgendaItem();
                    agendaItem.Description = (therapy.Time.ToString("h'h 'm'm 's's'")) + " <br/>" + (therapy.description ?? "") + ("<br/>" + therapy.Location ?? "") + ("<br/>Behandelaar: " + therapy.therapistFirstName ?? "") + (" " + therapy.therapistLastName ?? "");
                    agendaItem.StartTime = therapy.date;
                    agendaItem.EndTime = therapy.date + therapy.Time;
                    agendaItem.ID = therapy.ID;
                    agendaItems.Add(agendaItem);
                }
            }
            return agendaItems;
        }

    }
}
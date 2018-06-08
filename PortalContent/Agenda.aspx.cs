using HCloud.DAL;
using HCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud.PortalContent
{
    public partial class Agenda : System.Web.UI.Page
    {
        DBAgendaConnection dBAgendaConnection = new DBAgendaConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((User)Session["User"] != null)
            {
                if (!IsPostBack)
                {


                    //Bind de tingywingyringylingy



                    DAL.DBTherapyConnection dBTherapy = new DAL.DBTherapyConnection();
                    List<Therapy> therapy = dBTherapy.GetOwn((User)Session["User"]);
                    List<AgendaItem> agendaItems = ConvertTherapyToAcceptedAgendaItems(therapy);

                    ActivityAgenda.DataSource = agendaItems;
                    ActivityAgenda.TimeFormat = DayPilot.Web.Ui.Enums.TimeFormat.Clock24Hours;
                    ActivityAgenda.StartDate = DateTime.Now;
                    ActivityAgenda.Days = 7;
                    ActivityAgenda.DataTextField = "Description";
                    ActivityAgenda.DataStartField = "StartTime";
                    ActivityAgenda.DataEndField = "EndTime";
                    ActivityAgenda.DataIdField = "ID";
                    ActivityAgenda.ShowToolTip = true;
                    ActivityAgenda.DataBind();
                    List<Therapy> newListTherapies = new List<Therapy>();
                    foreach (var item in therapy)
                    {
                        if (!item.Accepted)
                        {
                            newListTherapies.Add(item);
                        }
                    }
                    TherapyGrid.DataSource = newListTherapies;
                    TherapyGrid.DataBind();


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
                    agendaItem.Description = (therapy.Time.ToString("h'h 'm'm 's's'")) +" <br/>"+ (therapy.description ?? "") + ("<br/>" + therapy.Location ?? "") + ("<br/>Behandelaar: " + therapy.therapistFirstName ?? "") + (" " + therapy.therapistLastName ?? "");
                    agendaItem.StartTime = therapy.date;
                    agendaItem.EndTime = therapy.date + therapy.Time;
                    agendaItem.ID = therapy.ID;
                    agendaItems.Add(agendaItem);
                }
            }
            return agendaItems;
        }
        protected void NextDay_Click(object sender, EventArgs e)
        {
            ActivityAgenda.StartDate = ActivityAgenda.StartDate.AddDays(1);
        }

        protected void BackDay_Click(object sender, EventArgs e)
        {
            ActivityAgenda.StartDate = ActivityAgenda.StartDate.AddDays(-1);
        }

        protected void Today_Click(object sender, EventArgs e)
        {
            ActivityAgenda.StartDate = DateTime.Now;
        }

        protected void countdays_TextChanged(object sender, EventArgs e)
        {
            if (countdays.Text != "")
            {
                if (countdays.Text != "0")
                {
                    if (!countdays.Text.Contains("-"))
                    {
                        ActivityAgenda.Days = Convert.ToInt32(countdays.Text);
                    }
                }
            }
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            var loggedinUser = Session["User"] as Entities.User;
            int result = 0;
            foreach (GridViewRow row in TherapyGrid.Rows)
            {
                CheckBox ck = ((CheckBox)row.FindControl("ItemCheckBox"));
                if (ck.Checked && ck.Visible)
                {
                    try
                    {
                        var a = row.Cells[1].Text;
                        DAL.DBAgendaConnection dBAgendaConnection = new DAL.DBAgendaConnection();
                        int result1 = dBAgendaConnection.AcceptItem(loggedinUser, int.Parse(row.Cells[1].Text));
                        result = +result1;
                    }
                    catch (Exception ex)
                    {
                        showMessage(ex.Message, System.Drawing.Color.Red);
                    }
                }
                else
                {
                    //Code if it is not checked ......may not be required
                }
            }
            if (result > 1)
                showMessage("Er zijn " + result + " afspraken geaccepteerd",System.Drawing.Color.Green);
            else if(result==1)
                showMessage("Er is " + result + " afspraak geaccepteerd", System.Drawing.Color.Green);
            else if(result==0)
            {
                showMessage("Er zijn " + result + " afspraken geaccepteerd", System.Drawing.Color.Red);
            }
        }
        public void showMessage(string text, System.Drawing.Color color)
        {
            Messager.Visible = true;
            Messager.Text = text;
            Messager.ForeColor = color;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HCloud
{
    public partial class Graphs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LineChart1.CategoriesAxis = "datum1,datum2";
            decimal[] dec =
            {
                10,10
            };
            decimal[] dec1 =
            {
                9,11
            };
            LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries() { Data=dec,LineColor="red",Name="Reindert"});
            LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries() { Data = dec1, LineColor = "blue", Name = "Arco" });
        }
    }
}
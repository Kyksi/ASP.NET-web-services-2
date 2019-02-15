using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pogoda.GlobalWeather gw = new pogoda.GlobalWeather();
            string cities = gw.GetCitiesByCountry("Poland");
            XmlDocument x = new XmlDocument();
            x.LoadXml(cities);
            XmlNodeList list = x.GetElementsByTagName("City");
            foreach (XmlNode xn in list)
            {
                ListItem li = new ListItem(xn.InnerText, xn.InnerText);
                DropDownList1.Items.Add(li);
            }

            kantor.CurrencyConvertor c = new kantor.CurrencyConvertor();
            int i=0;
            foreach (string item in Enum.GetNames(typeof(kantor.Currency)))
            {
               ListItem li = new ListItem(item, i.ToString());
               DropDownList2.Items.Add(li);
               DropDownList3.Items.Add(li);
               i++;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pogoda.GlobalWeather gw = new pogoda.GlobalWeather();
        Label1.Text = gw.GetWeather(DropDownList1.SelectedItem.Text, "Poland");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        kantor.CurrencyConvertor c = new kantor.CurrencyConvertor();
        double a = c.ConversionRate((kantor.Currency)Convert.ToInt32(DropDownList2.SelectedValue), (kantor.Currency)Convert.ToInt32(DropDownList3.SelectedValue));
        Label2.Text = (a*Convert.ToDouble(TextBox1.Text)).ToString();
    }
}

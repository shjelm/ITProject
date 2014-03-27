using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITProject14.Shared.UserControls;
using ITProject14.App_Code.BLL;

namespace ITProject14
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Member customer = null;

                try
                {
                    // ...hämta kundnumret från "query string"-variabeln,...
                    int customerId = Convert.ToInt32(Request.QueryString["id"]);

                    // ...hämta kunduppgifterna och...
                    Service service = new Service();
                    customer = service.GetMember(customerId);
                }
                catch
                {
                    // Tom! "Äter upp" bara upp undantaget!
                }

                // ...kontrollera om det verkligen finns några kunduppgifter, i så fall så...
                if (customer != null)
                {
                    MemberEdit MyCustomerEdit = new MemberEdit();

                    // ...presentera dem.
                    MyCustomerEdit.CustomerId = customer.MemberId;
                    MyCustomerEdit.Name = customer.Name;
                    MyCustomerEdit.Mail = customer.Mail;
                    MyCustomerEdit.Username = customer.Username;
                    MyCustomerEdit.Password = customer.Password;
                }
                else
                {
                    // ...om inga kunduppgifter kunde hittas dirigeras 
                    // användaren till en meddelandesida.
                    Response.Redirect("~/NotFound.aspx", false);
                }
            }
        }

        protected void MemberEdit_Saved(object sender, SavedEventArgs e)
        {
            // Kunduppgifterna sparade varför användaren dirigeras till en
            // rättmeddelandesida.
            Response.Redirect(String.Format("~/Success.aspx?returnUrl=Details.aspx?id={0}&action=Customer_Saved",
                e.Member.MemberId), false);
        }

        protected void MemberEdit_Canceled(object sender, EventArgs e)
        {
            // Kunduppgifterna inte sparade varför användaren dirigeras till detaljsidan.
            Response.Redirect(String.Format("~/Details.aspx?id={0}", Convert.ToInt32(Request.QueryString["id"])), false);
        }
    }
}
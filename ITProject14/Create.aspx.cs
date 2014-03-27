using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITProject14.Shared.UserControls;

namespace ITProject14
{
    public partial class Create : System.Web.UI.Page
    {
        protected void MemberEdit_Saved(object sender, SavedEventArgs e)
        {
            // Kunduppgifterna sparade varför användaren dirigeras till en
            // rättmeddelandesida.
            string url = String.Format("~/Success.aspx?returnUrl=~/Details.aspx?id={0}&action=Member_Saved",
                e.Member.MemberId);
            Response.Redirect(url, false);
        }

        protected void MemberEdit_Canceled(object sender, EventArgs e)
        {
            // Kunduppgifterna inte sparade varför användaren dirigeras till startsidan.
            Response.Redirect("~/", false);
        }
    }
}
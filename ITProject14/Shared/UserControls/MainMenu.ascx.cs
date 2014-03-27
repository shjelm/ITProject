using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITProject14.Shared.UserControls
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ser till att en navigeringstabb aktiveras om sidan den är 
            // assoccierad med är den aktuella sidan.
            if (String.Compare(HomeHyperLink.NavigateUrl, Page.AppRelativeVirtualPath,
                StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                HomeLi.Attributes["class"] = "selected";
            }
            else if (String.Compare(CreateHyperLink.NavigateUrl, Page.AppRelativeVirtualPath,
                StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                CreateLi.Attributes["class"] = "selected";
            }
        }
    }
}
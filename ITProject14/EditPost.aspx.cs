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
    public partial class EditPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Post post = null;

                try
                {
                    // ...hämta kundnumret från "query string"-variabeln,...
                    int postId = Convert.ToInt32(Request.QueryString["id"]);

                    // ...hämta kunduppgifterna och...
                    Service service = new Service();
                    post = service.GetPostByPostId(postId);
                }
                catch
                {
                    // Tom! "Äter upp" bara upp undantaget!
                }

                // ...kontrollera om det verkligen finns några kunduppgifter, i så fall så...
                if (post != null)
                {
                    PostEdit MyPostEdit = new PostEdit();

                    // ...presentera dem.
                    MyPostEdit.MemberId = post.MemberId;
                    MyPostEdit.PostId = post.PostId;
                    MyPostEdit.Value = post.Value;
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
            Response.Redirect(String.Format("~/Success.aspx?returnUrl=Details.aspx?id={0}&action=Post_Saved",
                e.Member.MemberId), false);
        }

        protected void MemberEdit_Canceled(object sender, EventArgs e)
        {
            // Kunduppgifterna inte sparade varför användaren dirigeras till detaljsidan.
            Response.Redirect(String.Format("~/Details.aspx?id={0}", Convert.ToInt32(Request.QueryString["id"])), false);
        }
    }
}
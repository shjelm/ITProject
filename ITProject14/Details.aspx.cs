using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITProject14.App_Code.BLL;
using Resources;

namespace ITProject14
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Member member = null;

            try
            {
                // ...hämta kundnumret från "query string"-variabeln,...
                int memberId = Convert.ToInt32(Request.QueryString["id"]);

                // ...hämta kunduppgifterna och...
                Service service = new Service();
                member = service.GetMember(memberId);
            }
            catch
            {
                // Tom! "Äter upp" bara upp det eventuella undantaget!
            }

            // ...kontrollera om det verkligen finns några kunduppgifter, i så fall så...
            if (member != null)
            {

                // ...presentera dem.
                NameLabel.Text = Server.HtmlEncode(member.Name);
                MailLabel.Text = Server.HtmlEncode(member.Mail);
                UsernameLabel.Text = Server.HtmlEncode(member.Username);
                PasswordLabel.Text = Server.HtmlEncode(member.Password);

                // Kundnumret skickas med som en "querystring"-variabel.
                EditButton.PostBackUrl = String.Format("~/Edit.aspx?id={0}", member.MemberId);
                EditButton.Enabled = true;

                // Användaren måste bekräfta att kunduppgifterna ska tas bort. Kundnumret skickas med
                // som ett argument till händelsen Command (inte Click!).
                // *** Skulle kunna ersättas med ett dolt fält - RegisterHiddenField. ***
                DeleteButton.CommandArgument = member.MemberId.ToString();
                DeleteButton.Enabled = true;

                // Användar unobtrusive JavaScript istället för följande två rader.
                //   string prompt = String.Format("return confirm(\"{0}\");", Strings.Customer_Delete_Confirm);
                //   DeleteButton.OnClientClick = String.Format(prompt, customer.Name);
                DeleteButton.CssClass = "delete-action";
                DeleteButton.Attributes.Add("data-type", Strings.Data_Type_Customer);
                DeleteButton.Attributes.Add("data-value", member.Name);
            }
            else
            {
                // ...om inga kunduppgifter kunde hittas dirigeras 
                // användaren till en meddelandesida.
                Response.Redirect("~/NotFound.aspx", false);
            }
        }

        protected void DeleteButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                // Kunduppgifterna tas bort och användaren dirigeras till en
                // rättmeddelandesida, eller så...
                Service service = new Service();
                service.DeleteMember(Convert.ToInt32(e.CommandArgument));
                Response.Redirect("~/Success.aspx?returnUrl=~/Default.aspx&action=Customer_Deleted");
            }
            catch
            {
                // ...visas ett felmeddelande.
                var validator = new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = Strings.Customer_Deleting_Error
                };

                Page.Validators.Add(validator);
            }
        }
    }
}
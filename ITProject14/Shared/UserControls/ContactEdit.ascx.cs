using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using Resources;
using ITProject14.App_Code.BLL;

namespace ITProject14.Shared.UserControls
{
    public partial class ContactEdit : System.Web.UI.UserControl
    {
        #region Händelser

        public event EventHandler ContactChanged;

        #endregion

        #region Egenskaper

        /// <summary>
        /// Bestämmer om kontaktuppgifterna ska kunna redigeras eller inte.
        /// Vyn med index 0 är för redigering; vyn med index 1 är "readonly".
        /// </summary>
        //public bool ReadOnly
        //{
        //    get
        //    {
        //        return ContactMultiView.ActiveViewIndex == 1;
        //    }

        //    set
        //    {
        //        ContactMultiView.ActiveViewIndex = value ? 1 : 0;
        //    }
        //}

        #endregion

        #region ContactDataSource

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Contact_Selecting_Error);
                e.ExceptionHandled = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Inserting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var contact = e.InputParameters[0] as Post;

            if (contact == null)
            {
                AddErrorMessage(Strings.Contact_Inserting_Unexpected_Error);
                e.Cancel = true;
            }
            else if (!contact.IsValid)
            {
                AddErrorMessage(contact);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Contact_Inserting_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (ContactChanged != null)
                {
                    ContactChanged(this, EventArgs.Empty);
                }

                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Contact_Saved",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Updating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var contact = e.InputParameters[0] as Post;

            if (contact == null)
            {
                AddErrorMessage(Strings.Contact_Updating_Unexpected_Error);
                e.Cancel = true;
            }
            else if (!contact.IsValid)
            {
                AddErrorMessage(contact);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Contact_Updating_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (ContactChanged != null)
                {
                    ContactChanged(this, EventArgs.Empty);
                }

                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Contact_Saved",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till ContactDataSource_Deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Contact_Deleting_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (ContactChanged != null)
                {
                    ContactChanged(this, EventArgs.Empty);
                }
                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Contact_Deleted",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        #endregion

        #region ContactTypeDataSource

        /// <summary>
        /// TODO: Skriv beskrivning till ContactTypeDataSource_Selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactTypeDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.ContactType_Selecting_Error);
                e.ExceptionHandled = true;
            }
        }

        #endregion

        #region ContactListView

        /// <summary>
        /// TODO: Skriv beskrivning till ContactListView_ItemInserting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactListView_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["CustomerId"] = Request.QueryString["id"];
        }

        #endregion

        #region ContactReadOnlyListView

        /// <summary>
        /// TODO: Skriv beskrivning till ContactReadOnlyListView_ItemDataBound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void ContactReadOnlyListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        // Typar om e.Item så att...
        //        ListViewDataItem currentItem = (ListViewDataItem)e.Item;

        //        // ...egenskapen DataItem kan användas vilket gör det möjligt att 
        //        // hämta primärnyckelns värde...
        //        Post currentContact = (Post)currentItem.DataItem;

        //        // ...som sedan kan användas för att hämta ett kontakttypobjekt...
        //        Service service = new Service();
        //        ContactType contactType = service.GetContactTypes()
        //            .Single(ct => ct.ContactTypeId == currentContact.ContactTypeId);

        //        // ...så att en beskrivning av kontaktypen kan presenteras; ex: Arbete: 012-345 67 89
        //        Label label = (Label)e.Item.FindControl("ContactTypeNameLabel");
        //        label.Text = contactType.Name + ": ";
        //    }
        //}

        #endregion

        #region Privata hjälpmetoder

        /// <summary>
        /// Lägger till ett CustomValidator-objekt till samlingen ValidatorCollection.
        /// </summary>
        /// <param name="message">Felmeddelande som ska visas av en ValidationSummary-kontroll.</param>
        private void AddErrorMessage(string message)
        {
            var validator = new CustomValidator
            {
                IsValid = false,
                ErrorMessage = message
            };

            Page.Validators.Add(validator);
        }

        /// <summary>
        /// Går igenom samtliga publika egenskaper för obj och undersöker om felmeddelande finns som i så
        /// fall läggs till samlingen ValidatorCollection.
        /// </summary>
        /// <param name="obj">Referens till affärslogikobjekt.</param>
        private void AddErrorMessage(IDataErrorInfo obj)
        {
            // Hämtar och loopar igenom samtliga publika, icke statiska, egenskaper objektet har där det
            // finns det ett felmeddelande associerat med egenskapens namn.
            var propertyNames = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !String.IsNullOrWhiteSpace(obj[p.Name]))
                .Select(p => p.Name);

            foreach (var propertyName in propertyNames)
            {
                // Överför meddelandet till samlingen ValidatorCollection.
                AddErrorMessage(obj[propertyName]);
            }
        }

        #endregion
        protected void ContactReadOnlyListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
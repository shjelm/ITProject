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
    public partial class CommentEdit : System.Web.UI.UserControl
    {
        #region Händelser

        public event EventHandler CommentChanged;

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
        //        return CommentMultiView.ActiveViewIndex == 1;
        //    }

        //    set
        //    {
        //        CommentMultiView.ActiveViewIndex = value ? 1 : 0;
        //    }
        //}

        #endregion

        #region CommentDataSource

        /// <summary>
        /// TODO: Skriv beskrivning till CommentDataSource_Selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Post_Selecting_Error);
                e.ExceptionHandled = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till PostDataSource_Inserting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommenttDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var contact = e.InputParameters[0] as Comment;

            if (contact == null)
            {
                AddErrorMessage(Strings.Post_Inserting_Unexpected_Error);
                e.Cancel = true;
            }
            else if (!contact.IsValid)
            {
                AddErrorMessage(contact);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till PostDataSource_Inserted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Post_Inserting_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (CommentChanged != null)
                {
                    CommentChanged(this, EventArgs.Empty);
                }

                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Comment_Saved",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till PostDataSource_Updating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var comment = e.InputParameters[0] as Comment;

            if (comment == null)
            {
                AddErrorMessage(Strings.Post_Updating_Unexpected_Error);
                e.Cancel = true;
            }
            else if (!comment.IsValid)
            {
                AddErrorMessage(comment);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till PostDataSource_Updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Post_Updating_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (CommentChanged != null)
                {
                    CommentChanged(this, EventArgs.Empty);
                }

                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Comment_Saved",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        /// <summary>
        /// TODO: Skriv beskrivning till CommentDataSource_Deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                AddErrorMessage(Strings.Post_Deleting_Error);
                e.ExceptionHandled = true;
            }
            else
            {
                if (CommentChanged != null)
                {
                    CommentChanged(this, EventArgs.Empty);
                }
                string url = String.Format("~/Success.aspx?returnUrl=~/Edit.aspx?id={0}&action=Comment_Deleted",
                    Request.QueryString["id"]);
                Response.Redirect(url, false);
            }
        }

        #endregion

        #region CommentTypeDataSource

        ///// <summary>
        ///// TODO: Skriv beskrivning till CommentTypeDataSource_Selected.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void CommentTypeDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        AddErrorMessage(Strings.PostType_Selecting_Error);
        //        e.ExceptionHandled = true;
        //    }
        //}

        #endregion

        #region CommentListView

        /// <summary>
        /// TODO: Skriv beskrivning till CommentListView_ItemInserting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CommentListView_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            e.Values["PostId"] = Request.QueryString["id"];
        }

        #endregion

        #region CommentReadOnlyListView

        /// <summary>
        /// TODO: Skriv beskrivning till CommentReadOnlyListView_ItemDataBound.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void CommentReadOnlyListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        // Typar om e.Item så att...
        //        ListViewDataItem currentItem = (ListViewDataItem)e.Item;

        //        // ...egenskapen DataItem kan användas vilket gör det möjligt att 
        //        // hämta primärnyckelns värde...
        //        Comment currentComment = (Comment)currentItem.DataItem;

        //        // ...som sedan kan användas för att hämta ett kontakttypobjekt...
        //        Service service = new Service();
        //        CommentType contactType = service.GetCommentTypes()
        //            .Single(ct => ct.CommentTypeId == currentComment.CommentTypeId);

        //        // ...så att en beskrivning av kontaktypen kan presenteras; ex: Arbete: 012-345 67 89
        //        Label label = (Label)e.Item.FindControl("CommentTypeNameLabel");
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
        protected void CommentReadOnlyListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
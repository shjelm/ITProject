using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using ITProject14.App_Code.BLL;
using Resources;

namespace ITProject14.Shared.UserControls
{
    public partial class PostEdit : System.Web.UI.UserControl
    {
        #region Händelser

        // Definierar ett nytt delegat som representerar signaturen som
        // händelsen Saved har.
        public delegate void SavedEventHandler(object sender, SavedEventArgs e);

        // Definierar publika händelsemedlemmar.
        public event SavedEventHandler Saved;
        public event EventHandler Canceled;

        #endregion

        #region Fält

        private int _postId;
        private int _memberId;

        #endregion

        #region Egenskaper

        //public bool PostVisible
        //{
        //    get { return CommentEdit1.Visible; }
        //    set { CommentEdit1.Visible = value; }
        //}

        public int PostId
        {
            get { return this._postId; }
            set { this._postId = value; }
        }

        public int MemberId
        {
            get { return this._memberId; }
            set { this._memberId = value; }
        }

        public string Value
        {
            get { return ValueTextBox.Text; }
            set { ValueTextBox.Text = value; }
        }
        #endregion

        #region Hanterarmetoder

        protected override void OnInit(EventArgs e)
        {
            // Låt sidan veta att "control state" används.
            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Om valideringen är OK så...
            if (Page.IsValid)
            {
                try
                {
                    // ...skapa ett nytt Member-objekt och initiera det
                    // med värdena från textfälten och...
                    Post post = new Post
                    {
                        MemberId = MemberId,
                        Value = Value,
                        PostId = PostId
                    };

                    // ...veriferera att objektet uppfyller affärsreglerna...
                    if (!post.IsValid)
                    {
                        // ...visa felmeddelanden om vad som
                        // orsakade att valideringen misslyckades.
                        AddErrorMessage(post);
                        return;
                    }

                    // ...spara objektet.
                    Service service = new Service();
                    service.SavePost(post);

                    // Om någon abbonerar på händelsen Saved...
                    if (Saved != null)
                    {
                        // ...utlöses händelsen Saved och skickar med
                        // en referens till kunduppgifterna som sparats.
                        Saved(this, new SavedEventArgs(post));
                    }
                }
                catch
                {
                    // ...visas ett felmeddelande.
                    AddErrorMessage(Strings.Post_Inserting_Error);
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Om någon abbonerar på händelsen Canceled...
            if (Canceled != null)
            {
                // ...utlöses händelsen Canceled.
                Canceled(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Överskuggningar

        // Ser till att MemberId alltid sparas genom att spara den i 
        // "constrol state" istället för "view state" som ju kan stängas av.

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
            {
                Pair p = savedState as Pair;
                if (p != null)
                {
                    base.LoadControlState(p.First);
                    this._postId = (int)p.Second;
                }
                else
                {
                    if (savedState is int)
                    {
                        this._postId = (int)savedState;
                    }
                    else
                    {
                        base.LoadControlState(savedState);
                    }
                }
            }
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();

            if (this._postId != 0)
            {
                if (obj != null)
                {
                    return new Pair(obj, this._postId);
                }
                else
                {
                    return (this._postId);
                }
            }
            else
            {
                return obj;
            }
        }

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
                ErrorMessage = message,
                ValidationGroup = "vgPost"
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
            // Hämtar och loopar igenom samtliga publika, icke statiska, egenskaper objektet har.
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                // Finns det ett felmeddelande associerat med egenskapens namn?
                if (!String.IsNullOrWhiteSpace(obj[property.Name]))
                {
                    // Överför meddelandet till samlingen ValidatorCollection.
                    AddErrorMessage(obj[property.Name]);
                }
            }
        }

        #endregion
    }
}
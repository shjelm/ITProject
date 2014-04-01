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
    public partial class MemberEdit : System.Web.UI.UserControl
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

        private int _memberId;

        #endregion

        #region Egenskaper

        public bool PostVisible
        {
            get { return PostEdit1.Visible; }
            set { PostEdit1.Visible = value; }
        }

        public int MemberId
        {
            get { return this._memberId; }
            set { this._memberId = value; }
        }

        public string Name
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }

        //@TODO: Rätt textbox
        public string Username
        {
            get { return UsernameTextBox.Text; }
            set { UsernameTextBox.Text = value; }
        }
        //@TODO: Rätt textbox
        public string Mail
        {
            get { return MailTextBox.Text; }
            set { MailTextBox.Text = value; }
        }
        //@TODO: Rätt textbox
        public string Password
        {
            get { return PasswordTextBox.Text; }
            set { PasswordTextBox.Text = value; }
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
                    Member member = new Member
                    {
                        MemberId = MemberId,
                        Name = Name,
                        Username = Username,
                        Mail = Mail,
                        Password = Password
                    };

                    // ...veriferera att objektet uppfyller affärsreglerna...
                    if (!member.IsValid)
                    {
                        // ...visa felmeddelanden om vad som
                        // orsakade att valideringen misslyckades.
                        AddErrorMessage(member);
                        return;
                    }

                    // ...spara objektet.
                    Service service = new Service();
                    service.SaveMember(member);

                    // Om någon abbonerar på händelsen Saved...
                    if (Saved != null)
                    {
                        // ...utlöses händelsen Saved och skickar med
                        // en referens till kunduppgifterna som sparats.
                        Saved(this, new SavedEventArgs(member));
                    }
                }
                catch
                {
                    // ...visas ett felmeddelande.
                    AddErrorMessage(Strings.Member_Saving_Error);
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
                    this._memberId = (int)p.Second;
                }
                else
                {
                    if (savedState is int)
                    {
                        this._memberId = (int)savedState;
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

            if (this._memberId != 0)
            {
                if (obj != null)
                {
                    return new Pair(obj, this._memberId);
                }
                else
                {
                    return (this._memberId);
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

    public class SavedEventArgs : EventArgs
    {
        #region Fält

        private Member _member;
        private Post _post;

        #endregion

        #region Egenskaper

        public Member Member
        {
            get
            {
                // Skapar en kopia av objektet som _member refererar till. Undviker 
                // på så sätt en "privacy leak".
                return this._member != null ? this._member.Clone() as Member : null;
            }

            private set
            {
                // Skapar en kopia av objektet som value refererar till. Undviker 
                // på så sätt en "privacy leak".
                this._member = value != null ? value.Clone() as Member : null;
            }
        }
        public Post Post
        {
            get
            {
                // Skapar en kopia av objektet som _member refererar till. Undviker 
                // på så sätt en "privacy leak".
                return this._post != null ? this._member.Clone() as Post : null;
            }

            private set
            {
                // Skapar en kopia av objektet som value refererar till. Undviker 
                // på så sätt en "privacy leak".
                this._post = value != null ? value.Clone() as Post : null;
            }
        }

        #endregion

        #region Konstruktorer

        public SavedEventArgs(Member member)
        {
            Member = member;
        }

        public SavedEventArgs(Post post)
        {
            Post = post;
        }

        #endregion
    }
}
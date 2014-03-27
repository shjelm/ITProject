using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Caching;
using ITProject14.App_Code.DAL;


namespace ITProject14.App_Code.BLL
{
    /// <summary>
    /// Klassen tillhandahåller metoder presentationslogiklagret
    /// anropar för att hantera data. Främst innehåller klassen
    /// metoder som använder sig av klasser i dataåtkomstlagret.
    /// </summary>
    [DataObject(true)]
    public class Service
    {    
        #region Fält

        private PostDAL _postDAL;
        private CommentDAL _commentDAL;
        private ContactTypeDAL _contactTypeDAL;
        private MemberDAL _memberDAL;

        #endregion

        #region Egenskaper

        private PostDAL ContactDAL
        {
            // Ett ContactDAL-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _postDAL ?? (_postDAL = new PostDAL()); }
        }

        private ContactTypeDAL ContactTypeDAL
        {
            get { return _contactTypeDAL ?? (_contactTypeDAL = new ContactTypeDAL()); }
        }

        private MemberDAL MemberDAL
        {
            get { return _memberDAL ?? (_memberDAL = new MemberDAL()); }
        }

        private CommentDAL CommentDAL
        {
            get { return _commentDAL ?? (_commentDAL = new CommentDAL()); }
        }

        #endregion

        #region Post CRUD-metoder
        // http://en.wikipedia.org/wiki/Create,_read,_update_and_delete

        /// <summary>
        /// Tar bort specifierad kontaktuppgift ur databasen.
        /// </summary>
        /// <param name="post">Kontaktuppgift som ska tas bort.</param>
        public void DeletePost(Post post) // ObjectDataSource kräver att en Delete-metod
        {                                             // med en parameter av typen Customer.
            ContactDAL.DeletePost(post.PostId);
        }

        /// <summary>
        /// Hämtar kontaktuppgift med ett specifikt nummer från databasen.
        /// </summary>
        /// <param name="postId">Kontaktuppgiftens nummer.</param>
        /// <returns>Ett Contact-objekt innehållande kontaktuppgifter.</returns>
        public Post GetPostByPostId(int postId)
        {
            return ContactDAL.GetPostByPostId(postId);
        }

        /// <summary>
        /// Hämtar en kunds kontaktuppgifter som finns lagrade i databasen.
        /// </summary>
        /// <returns>Lista med referenser till Contact-objekt innehållande kontaktuppgifter.</returns>
        public List<Post> GetPosts()
        {
            return ContactDAL.GetPosts();
        }

        /// <summary>
        /// Spara en kontaktuppgift i databasen.
        /// </summary>
        /// <param name="customer">KOntaktuppgifter som ska sparas.</param>
        public void SavePost(Post post)
        {
            if (post.IsValid)
            {
                // Contact-objektet sparas antingen genom att en ny post 
                // skapas eller genom att en befintlig post uppdateras.
                if (post.PostId == 0) // Ny post om ContactID är 0!
                {
                    ContactDAL.InsertPost(post);
                }
                else
                {
                    ContactDAL.UpdatePost(post);
                }
            }
            else
            {
                // Uppfyller inte objektet affärsreglerna kastas ett undantag med
                // ett allmänt felmeddelande samt en referens till objektet som 
                // inte klarade valideringen.
                ApplicationException ex = new ApplicationException(post.Error);
                ex.Data.Add("Post", post);
                throw ex;
            }
        }

        #endregion

        #region ContactType (C)R(UD)-metoder

        /// <summary>
        /// Hämtar alla kontakttyper från databasen.
        /// </summary>
        /// <returns>Ett List-objekt innehållande referenser till ContactType-objekt.</returns>
        private List<ContactType> GetContactTypesFromDAL()
        {
            return ContactTypeDAL.GetContactTypes();
        }

        ///// <summary>
        ///// Hämtar alla kontakttyper.
        ///// </summary>
        ///// <returns>Ett List-objekt innehållande referenser till ContactType-objekt.</returns>
        //public List<ContactType> GetContactTypes()
        //{
        //    // Försöker hämta lista med kontakttyper från cachen.
        //    List<ContactType> contactTypes = HttpContext.Current.Cache["ContactTypes"] as List<ContactType>;

        //    // Om det inte finns det en lista med kontakttyper...
        //    if (contactTypes == null)
        //    {
        //        // ...hämtar då lista med kontakttyper...
        //        contactTypes = GetContactTypesFromDAL();

        //        // ...och cachar dessa. List-objektet, inklusive alla ContactType-objekt, kommer att cachas 
        //        // under 10 minuter, varefter de automatiskt avallokeras från webbserverns primärminne.
        //        HttpContext.Current.Cache.Insert("ContactTypes", contactTypes, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
        //    }

        //    // Returnerar listan med kontakttyper.
        //    return contactTypes;
        //}

        #endregion

        #region Member CRUD-metoder

        /// <summary>
        /// Tar bort specifierad kund ur databasen.
        /// </summary>
        /// <param name="customerId">Kund med kundnummer som ska tas bort.</param>
        public void DeleteMember(int customerId)
        {
            MemberDAL.DeleteMember(customerId);
        }

        /// <summary>
        /// Hämtar en kund med ett specifikt kundnummer från databasen.
        /// </summary>
        /// <param name="memberId">Kundens kundnummer.</param>
        /// <returns>Ett Customer-objekt innehållande kunduppgifter.</returns>
        public Member GetMember(int memberId)
        {
            return MemberDAL.GetMemberById(memberId);
        }

        /// <summary>
        /// Hämtar alla kunder som finns lagrade i databasen.
        /// </summary>
        /// <returns>Lista med referenser till Customer-objekt innehållande kunduppgifter.</returns>
        public List<Member> GetMembers()
        {
            return MemberDAL.GetMembers();
        }

        /// <summary>
        /// Spara en kunds kunduppgifter i databasen.
        /// </summary>
        /// <param name="customer">Kunduppgifter som ska sparas.</param>
        public void SaveCustomer(Member customer)
        {
            // Klarar objektet validering i affärslogiklagret?
            if (customer.IsValid)
            {
                // Customer-objektet sparas antingen genom att en ny post 
                // skapas eller genom att en befintlig post uppdateras.
                if (customer.MemberId == 0) // Ny post om CustomerId är 0!
                {
                    MemberDAL.InsertMember(customer);
                }
                else
                {
                    MemberDAL.UpdateMember(customer);
                }
            }
            else
            {
                // Uppfyller inte objektet affärsreglerna kastas ett undantag med
                // ett allmänt felmeddelande samt en referens till objektet som 
                // inte klarade valideringen.
                ApplicationException ex = new ApplicationException(customer.Error);
                ex.Data.Add("Customer", customer);
                throw ex;
            }
        }

        #endregion


        #region Comment CRUD-metoder

        /// <summary>
        /// Tar bort specifierad comment ur databasen.
        /// </summary>
        /// <param name="comment">Kontaktuppgift som ska tas bort.</param>
        public void DeleteComment(Comment comment) // ObjectDataSource kräver att en Delete-metod
        {                                             // med en parameter av typen Customer.
            CommentDAL.DeleteComment(comment.CommentId);
        }

        /// <summary>
        /// Hämtar kontaktuppgift med ett specifikt nummer från databasen.
        /// </summary>
        /// <param name="commentId">Kontaktuppgiftens nummer.</param>
        /// <returns>Ett Contact-objekt innehållande kontaktuppgifter.</returns>
        public Comment GetCommentByCommentId(int commentId)
        {
            return CommentDAL.GetCommentByCommentId(commentId);
        }

        /// <summary>
        /// Spara en kontaktuppgift i databasen.
        /// </summary>
        /// <param name="customer">KOntaktuppgifter som ska sparas.</param>
        public void SaveComment(Comment comment)
        {
            if (comment.IsValid)
            {
                // Contact-objektet sparas antingen genom att en ny post 
                // skapas eller genom att en befintlig post uppdateras.
                if (comment.PostId == 0) // Ny post om ContactID är 0!
                {
                    CommentDAL.InsertComment(comment);
                }
                else
                {
                    CommentDAL.UpdateComment(comment);
                }
            }
            else
            {
                // Uppfyller inte objektet affärsreglerna kastas ett undantag med
                // ett allmänt felmeddelande samt en referens till objektet som 
                // inte klarade valideringen.
                ApplicationException ex = new ApplicationException(comment.Error);
                ex.Data.Add("Comment", comment);
                throw ex;
            }
        }


        #endregion
    }
}
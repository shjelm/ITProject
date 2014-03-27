using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Resources;

namespace ITProject14.App_Code.BLL
{
    public class ContactType : BusinessObjectBase, ICloneable
    {
        #region Fält

        // Autoimplementerade egenskaper kan inte användas i och med implementeringen
        // av valideringen varför privata fält kopplade till egenskaperna behövs.
        private string _name;

        #endregion

        #region Konstruktorer

        public ContactType()
        {
            // Ombesörjer att objektets alla värden valideras genom att använda
            // egenskaperna istället för fälten direkt.
            this.ContactTypeId = 0;
            this.Name = null;
            this.SortOrder = (byte)0;
        }

        #endregion

        #region Egenskaper - data

        // Primärnycklens värde, eller sorteringsordningens värde, behöver inte 
        // valideras varför det går bra att använda autoimplementerade egenskaper.
        public int ContactTypeId { get; set; }
        public byte SortOrder { get; set; }

        public string Name
        {
            get { return this._name; }
            set
            {
                // Antar att värdet är korrekt.
                base.ValidationErrors.Remove("Name");

                // Undersöker om värdet är korrekt beträffande om strängen är null
                // eller tom för i så fall...
                if (String.IsNullOrWhiteSpace(value))
                {
                    // ...är det ett fel varför nyckeln Name (namnet på egenskapen)
                    // mappas mot ett felmeddelande.
                    base.ValidationErrors.Add("Name", Strings.ContactType_Name_Required);
                }
                else if (value.Length > 50)
                {
                    // Om strängen innehåller fler än 50 tecken kan inte det fullständiga 
                    // datat inte sparas i databastabellen vilket är att betrakta som ett fel.
                    base.ValidationErrors.Add("Name", Strings.ContactType_Name_MaxLength);
                }

                // Tilldelar fältet värdet, oavsett om det är ett korrekt värde 
                // enligt affärsreglerna eller inte.
                this._name = value != null ? value.Trim() : null;
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
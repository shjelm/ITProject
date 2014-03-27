using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using ITProject14.App_Code.BLL;

namespace ITProject14.App_Code.DAL
{
    /// <summary>
    /// Klass för (C)R(UD)-funktionalitet mot tabellen ContactType.
    /// </summary>
    [DataObject(false)]
    public class ContactTypeDAL : DALBase
    {
        #region CRUD-metoder

        /// <summary>
        /// Hämtar alla kontakttyper i databasen.
        /// </summary>
        /// <returns>Lista med referenser till ContactType-objekt.</returns>
        public List<ContactType> GetContactTypes()
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspGetContactTypes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar det List-objekt som initialt har plats för 10 referenser till ContactType-objekt.
                    List<ContactType> contactTypes = new List<ContactType>(10);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                        // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                        // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                        int contactTypeIdIndex = reader.GetOrdinal("ContactTypeId");
                        int nameIndex = reader.GetOrdinal("Name");
                        int sortOrderIndex = reader.GetOrdinal("SortOrder");

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            contactTypes.Add(new ContactType
                            {
                                ContactTypeId = reader.GetInt32(contactTypeIdIndex),
                                Name = reader.GetString(nameIndex),
                                SortOrder = reader.GetByte(sortOrderIndex)
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    contactTypes.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med ContactType-objekt.
                    return contactTypes;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        #endregion
    }
}
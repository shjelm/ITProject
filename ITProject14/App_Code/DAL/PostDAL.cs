﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITProject14.App_Code.BLL;
using System.ComponentModel;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ITProject14.App_Code.DAL
{
    public class PostDAL : DALBase
    {
        #region CRUD-metoder

        /// <summary>
        /// Hämtar en kunds kontaktuppgifter i databasen.
        /// </summary>
        /// <returns>Lista med referenser till Contact-objekt.</returns>
        //public List<Post> GetPostByMemberId(int memberId)
        //{
        //    // Skapar ett anslutningsobjekt.
        //    using (SqlConnection conn = CreateConnection())
        //    {
        //        try
        //        {
        //            // Skapar och initierar ett SqlCommand-objekt som används till att 
        //            // exekveras specifierad lagrad procedur.
        //            SqlCommand cmd = new SqlCommand("app.uspGetPostByMemberId", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            // Lägger till den paramter den lagrade proceduren kräver. Använder här det MINDRE effektiva 
        //            // sätttet att göra det på - enkelt, men ASP.NET behöver "jobba" rätt mycket.
        //            cmd.Parameters.AddWithValue("@MemberId", memberId);

        //            // Skapar det List-objekt som initialt har plats för 10 referenser till Contact-objekt.
        //            List<Post> posts = new List<Post>(10);

        //            // Öppnar anslutningen till databasen.
        //            conn.Open();

        //            // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
        //            // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
        //            // SqlDataReader-objekt och returnerar en referens till objektet.
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
        //                // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
        //                // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
        //                int memberIdIndex = reader.GetOrdinal("MemberId");
        //                int postIdIndex = reader.GetOrdinal("PostId");
        //                int contactTypeIdIndex = reader.GetOrdinal("ContactTypeId");
        //                int valueIndex = reader.GetOrdinal("Value");

        //                // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
        //                // poster returnerar Read false.
        //                while (reader.Read())
        //                {
        //                    // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
        //                    // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
        //                    posts.Add(new Post
        //                    {
        //                        MemberId = reader.GetInt32(memberIdIndex),
        //                        PostId = reader.GetInt32(postIdIndex),
        //                        ContactTypeId = reader.GetInt32(contactTypeIdIndex),
        //                        Value = reader.GetString(valueIndex)
        //                    });
        //                }
        //            }

        //            // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
        //            // som inte används.
        //            posts.TrimExcess();

        //            // Returnerar referensen till List-objektet med referenser med Contact-objekt.
        //            return posts;
        //        }
        //        catch
        //        {
        //            // Kastar ett eget undantag om ett undantag kastas.
        //            throw new ApplicationException(GenericErrorMessage);
        //        }
        //    }
        //}

        /// <summary>
        /// Hämtar alla posts i databasen.
        /// </summary>
        /// <returns>Lista med referenser till post-objekt.</returns>
        public List<Post> GetPosts()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar det List-objekt som initialt har plats för 100 referenser till Member-objekt.
                    var posts = new List<Post>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new MySqlCommand("app.uspGetPosts", conn); //@TODO: Här ska en lagrad procedur som hämtar ut alla medlemmar skrivas
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                        // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                        // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                        var memberIdIndex = reader.GetOrdinal("MemberId");
                        var valueIndex = reader.GetOrdinal("Value");
                        int postIdIndex = reader.GetOrdinal("PostId");

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            posts.Add(new Post
                            {
                                MemberId = reader.GetInt32(memberIdIndex),
                                Value = reader.GetString(valueIndex),
                                PostId = reader.GetInt32(postIdIndex)
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    posts.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                    return posts;
                }
                catch
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Hämtar en kontaktuppgift.
        /// </summary>
        /// <param name="customerId">En kontaktuppgifts nummer.</param>
        /// <returns>Ett Contact-objekt med en kontaktuppgifter.</returns>
        public Post GetPostByPostId(int postId)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspGetPost", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver. Använder här det MINDRE effektiva 
                    // sätttet att göra det på - enkelt, men ASP.NET behöver "jobba" rätt mycket.
                    cmd.Parameters.AddWithValue("@PostId", postId);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returner en post varför
                    // ett SqlDataReader-objekt måste ta hand om posten. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Om det finns en post att läsa returnerar Read true. Finns ingen post returnerar
                        // Read false.
                        if (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har. Genom att använda 
                            // GetOrdinal behöver du inte känna till i vilken ordning de olika 
                            // kolumnerna kommer, bara vad de heter.
                            int memberIdIndex = reader.GetOrdinal("MemberId");
                            int postIdIndex = reader.GetOrdinal("PostId");
                            int valueIndex = reader.GetOrdinal("Value");

                            // Returnerar referensen till de skapade Contact-objektet.
                            return new Post
                            {
                                MemberId = reader.GetInt32(memberIdIndex),
                                PostId = reader.GetInt32(postIdIndex),
                                //ContactTypeId = reader.GetInt32(contactTypeIdIndex),
                                Value = reader.GetString(valueIndex)
                            };
                        }
                    }

                    // Istället för att returnera null kan du välja att kasta ett undatag om du 
                    // inte får "träff" på en kontaktuppgift. I denna applikation väljer jag att *inte* betrakta 
                    // det som ett fel om det inte går att hitta en kontaktuppgift. Vad du väljer är en smaksak...
                    return null;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Skapar en ny post i tabellen Contact.
        /// </summary>
        /// <param name="customer">Kontaktuppgift som ska läggas till.</param>
        public void InsertPost(Post post)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspInsertPost", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@MemberId", MySqlDbType.Int32, 4).Value = post.MemberId;
                    cmd.Parameters.Add("@Value", MySqlDbType.VarChar, 500).Value = post.Value;

                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@PostId", MySqlDbType.Int32, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    post.MemberId = (int)cmd.Parameters["@PostId"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Uppdaterar en kunds kontaktuppgifter i tabellen Contact.
        /// </summary>
        /// <param name="customer">KOntaktuppgift som ska uppdateras.</param>
        public void UpdatePost(Post post)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspUpdatePost", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@PostId", MySqlDbType.Int32, 4).Value = post.PostId;
                    cmd.Parameters.Add("@MemberId", MySqlDbType.Int32, 4).Value = post.MemberId;
                    cmd.Parameters.Add("@Value", MySqlDbType.VarChar, 500).Value = post.Value;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en UPDATE-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Tar bort en kontaktuppgift.
        /// </summary>
        /// <param name="customerId">Kontaktuppgifts nummer.</param>
        public void DeletePost(int postId)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspDeletePost", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@PostId", MySqlDbType.Int32, 4).Value = postId;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en DELETE-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
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
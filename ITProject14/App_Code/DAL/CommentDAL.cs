﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resources;
using ITProject14.App_Code.BLL;
using System.ComponentModel;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ITProject14.App_Code.DAL
{
    public class CommentDAL : DALBase
    {
        #region CRUD-metoder


        /// <summary>
        /// Hämtar en kontaktuppgift.
        /// </summary>
        /// <param name="memberId">En kontaktuppgifts nummer.</param>
        /// <returns>Ett Post-objekt med en kontaktuppgifter.</returns>
        public Comment GetCommentsByPostId(int postId)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspGetCommentsByPostId", conn);
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
                            int commentIdIndex = reader.GetOrdinal("CommentId");
                            //int contactTypeIdIndex = reader.GetOrdinal("PostTypeId");
                            int valueIndex = reader.GetOrdinal("Value");

                            // Returnerar referensen till de skapade Post-objektet.
                            return new Comment
                            {
                                MemberId = reader.GetInt32(memberIdIndex),
                                PostId = reader.GetInt32(postIdIndex),
                                CommentId = reader.GetInt32(commentIdIndex),
                                //PostTypeId = reader.GetInt32(contactTypeIdIndex),
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
        /// Skapar en ny post i tabellen Post.
        /// </summary>
        /// <param name="member">Kontaktuppgift som ska läggas till.</param>
        public void InsertComment(Comment comment)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspInsertComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@MemberId", MySqlDbType.Int32, 4).Value = comment.MemberId;
                    cmd.Parameters.Add("@PostId", MySqlDbType.Int32, 4).Value = comment.PostId;
                    cmd.Parameters.Add("@Value", MySqlDbType.VarChar, 500).Value = comment.Value;

                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@CommentId", MySqlDbType.Int32, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Member-objektet värdet.
                    comment.CommentId = (int)cmd.Parameters["@CommentId"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Uppdaterar en kunds kontaktuppgifter i tabellen Post.
        /// </summary>
        /// <param name="member">KOntaktuppgift som ska uppdateras.</param>
        public void UpdateComment(Comment comment)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspUpdateComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@CommentId", MySqlDbType.Int32, 4).Value = comment.CommentId;
                    cmd.Parameters.Add("@PostId", MySqlDbType.Int32, 4).Value = comment.PostId;
                    cmd.Parameters.Add("@MemberId", MySqlDbType.Int32, 4).Value = comment.MemberId;
                    cmd.Parameters.Add("@Value", MySqlDbType.VarChar, 500).Value = comment.Value;

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
        /// <param name="memberId">Kontaktuppgifts nummer.</param>
        public void DeleteComment(int commentId)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspDeleteComment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@CommentId", MySqlDbType.Int32, 4).Value = commentId;

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

        public Comment GetCommentByCommentId(int commentId)
        {
            // Skapar ett anslutningsobjekt.
            using (MySqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    MySqlCommand cmd = new MySqlCommand("app.uspGetCommentByCommentId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver. Använder här det MINDRE effektiva 
                    // sätttet att göra det på - enkelt, men ASP.NET behöver "jobba" rätt mycket.
                    cmd.Parameters.AddWithValue("@CommentId", commentId);

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
                            int commentIdIndex = reader.GetOrdinal("CommentId");
                            int valueIndex = reader.GetOrdinal("Value");

                            // Returnerar referensen till de skapade Post-objektet.
                            return new Comment
                            {
                                MemberId = reader.GetInt32(memberIdIndex),
                                PostId = reader.GetInt32(postIdIndex),
                                CommentId = reader.GetInt32(commentIdIndex),
                                //PostTypeId = reader.GetInt32(contactTypeIdIndex),
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
    }
}

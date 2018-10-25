using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HighscoreModel;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HighscoreRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private static List<Highscore> highscoreList = new List<Highscore>();

        //string that connects to the DB. DON'T UPLOAD THIS TO GITHUB!
        private static string conString = "";



        // GET: api/Highscores
        [HttpGet]
        public IEnumerable<Highscore> Get()
        {
            string connectionString = conString;

            //Sets up a new connection with the server. 
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //opens the connection. 
                con.Open();

                //Gives the command the server uses, as well as the connection string. 
                using (SqlCommand command = new SqlCommand("SELECT * FROM Highscore", con))

                //Sets up a reader for the selected items in the DB.
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //makes a new object of the Highscore class, to be used as a temp holder of data from the DB.
                        Highscore obj = new Highscore();
                        obj.ID = Convert.ToInt32(reader["ID"]);
                        obj.Name = reader["PersonName"].ToString();
                        obj.Score = Convert.ToInt32(reader["Score"]);

                        //Filters items out that have been loaded once. And if the item is not in the list then it add it. 
                        if (highscoreList.All(I => I.ID != Convert.ToInt32(reader["ID"])))
                        {
                            highscoreList.Add(obj);
                        }
                    }
                }
            }

            return highscoreList;
        }

        // POST: api/Highscores
        [HttpPost("add", Name="add")]
        public void Post([FromBody] Highscore obj)
        {
            string connectionString = conString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                //makes a new command to be used to add a new item to the DB. And then gives it the string it uses to complete its task. 
                using (SqlCommand command =
                    new SqlCommand("INSERT INTO HighScore(ID, PersonName, Score) VALUES(@ID, @Name, @Score)", con))
                {
                    //Adds the different values the the correct columns in the DB. 
                    obj.ID = highscoreList.Count;
                    command.Parameters.AddWithValue("@ID", obj.ID);
                    command.Parameters.AddWithValue("@Name", obj.Name);
                    command.Parameters.AddWithValue("@Score", obj.Score);
                    command.ExecuteNonQuery();

                    //obj.ID = highscoreList.Count;
                    //highscoreList.Add(obj);
                    //highscoreList.OrderByDescending(o => o.Score).ToList();
                }

            }
                
        }

        // PUT: api/Highscores/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

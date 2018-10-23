using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HighscoreModel;

namespace HighscoreRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private static List<Highscore> highscoreList = new List<Highscore>();

        // GET: api/Highscores
        [HttpGet]
        public IEnumerable<Highscore> Get()
        {
            return highscoreList;
        }

        // POST: api/Highscores
        [HttpPost]
        public void Post([FromBody] Highscore obj)
        {
            obj.ID = highscoreList.Count;
            highscoreList.Add(obj);
            highscoreList.OrderByDescending(o => o.Score).ToList();
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

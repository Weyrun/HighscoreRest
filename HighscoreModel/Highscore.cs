using System;

namespace HighscoreModel
{
    public class Highscore
    {
        private int _id;
        private string _name;
        private int _score;

        public Highscore() { }

        public Highscore(int id, string name, int score)
        {
            _id = id;
            _name = name;
            _score = score;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value;  }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public override string ToString()
        {
            return $"{Name}: {Score}";
        }
    }
}

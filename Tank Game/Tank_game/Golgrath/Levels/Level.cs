using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine.Golgrath
{
    public abstract class Level : GameObject
    {
        protected string identifier, levelToLoad;
        public Level(string identifier, string levelToLoad)
        {
            this.identifier = identifier;
            this.levelToLoad = levelToLoad;
        }
        public string GetIdentifier()
        {
            return this.identifier;
        }
        public void SetIdentifier(string identifier)
        {
            this.identifier = identifier;
        }

        public string GetLevelToLoad()
        {
            return this.levelToLoad;
        }

        public void SetLevelToLoad(string levelToLoad)
        {
            this.levelToLoad = "Assets/" + levelToLoad;
        }

        public abstract void Construct();

    }
}

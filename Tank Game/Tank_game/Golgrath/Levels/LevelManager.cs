using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Levels
{
    public class LevelManager : GameObject
    {
        private List<Level> levels;
        public LevelManager()
        {
            this.levels = new List<Level>();
            this.levels.Add(new LevelBottom());
            this.SetActiveLevel(this.levels[0].GetIdentifier());
        }

        public void SetActiveLevel(string levelIdentifier)
        {
            if (this.levels.Count != 0)
            {
                foreach (Level level in this.levels)
                {
                    if (level.GetIdentifier().Equals(levelIdentifier))
                    {
                        if (this.GetChildCount() != 0)
                        {
                            this.GetChildren()[0].Remove();
                        }
                        level.Construct();
                        AddChild(level);
                    }
                }
            }
        }
        public Level GetActiveLevel()
        {
            return (Level)this.GetChildren()[0];
        }
        public Level GetLevel(int index)
        {
            if (index < this.levels.Count && index >= 0)
            {
                return this.levels[index];
            }
            return null;
        }
        public Level GetLevel(string levelIdentifier)
        {
            if (this.levels.Count != 0)
            {
                foreach (Level level in this.levels)
                {
                    if (level.GetIdentifier().Equals(levelIdentifier))
                    {
                        return level;
                    }
                }
            }
            return null;
        }
    }
}

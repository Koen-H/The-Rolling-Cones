using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;

namespace GXPEngine.Coolgrath
{
    public class NextLevelBlock : AnimationSprite
    {
        public NextLevelBlock(Vec2 position, string filename = "Sprite_sheet_coin.png", int cols = 8, int rows = 1, int frames = -1) : base(filename, cols, rows, frames)
        {
            SetXY(position.x, position.y);
            MyGame myGame = (MyGame)Game.main;
            myGame.coins.Add(this);
            SetOrigin(width / 2, height / 2);
        }

        public new void Update()
        {
            Animate(0.05f);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Coolgrath
{
    public class Geyser : AnimationSprite
    {
        public float strength { get; set; }

        public Geyser(float _strength, Vec2 position,  int cols, int rows, int frames = -1, string filename = "cyan_block.png") : base(filename, cols, rows, frames)
        {
            strength = _strength;
            x = position.x;
            y = position.y;
            MyGame myGame = (MyGame)Game.main;
            myGame.geysers.Add(this);

            //Temporary
            SetScaleXY(1,3);
        }


    }
}

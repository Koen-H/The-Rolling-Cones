using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Coolgrath
{
    public class BushShot : AnimationSprite
    {
        public AnimationSprite target = new AnimationSprite("target.png",1,1,-1,false);
        private Vec2 position;
        public Vec2 Position
        {
            set
            {
                this.position = value;
                this.x = value.x;
                this.y = value.y;
            }
            get
            {
                return this.position;
            }
        }
        private bool targetDirectionLeft = false;

        public BushShot(Vec2 position,  int cols = 1, int rows = 1, string filename = "BushShot.png", int frames = -1) : base(filename, cols, rows, frames)
        {
            this.AddChild(target);
            target.SetOrigin(64, 64);
            target.SetScaleXY(0.5f,0.5f);
            target.rotation = -90;
            target.alpha = 0f;
            Position = position;
            SetOrigin(width / 2, height / 2);
            MyGame myGame = (MyGame)Game.main;
            myGame.bushes.Add(this);
        }

        public void Aiming()
        {
            if(targetDirectionLeft) target.rotation--;
            else target.rotation++;
            if (target.rotation > 95 - 90 || target.rotation < -95 - 90) targetDirectionLeft = !targetDirectionLeft;
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public class Barrel : Sprite
    {
        Tank playerTank;

        public Barrel() : base("Barrel.png")
        {
            SetOrigin(width / 4, height / 2);
            
        }

        void Aim()
        {
            Vec2 aimPosition = new Vec2(0,0);
            
            if (playerTank == (Tank)parent)
            {
                aimPosition = new Vec2(Input.mouseX, Input.mouseY);//If the parent is playertank, follow the mouse
            }
            else
            {
                aimPosition = new Vec2(playerTank._position.x, playerTank._position.y);//Else aim at the player
            }
            

            Vec2 toPoint = aimPosition - new Vec2(parent.x, parent.y);

            float rot = rotation + parent.rotation;
            float diff = AngleDifference(rot, toPoint.GetAngleDegrees());
            if (diff > 2.5f)
            {
                rotation += 2;
            }
            else if (diff < -2.5f)
            {
                rotation -= 2;
            }
        }

        void Update()
        {
            if(playerTank == null)SetPlayerTank();//Has to be set in update, as the playertank is set after it's inistialized.
            Aim();
        }

        float AngleDifference(float angle1, float angle2)// Get the shortest angle
        {
            float diff = (angle2 - angle1 + 180) % 360 - 180;
            return diff < -180 ? diff + 360 : diff;
        }
        void SetPlayerTank()
        {
            MyGame myGame = (MyGame)Game.main;
            playerTank = myGame.playerTank;
        }

    }
   
}

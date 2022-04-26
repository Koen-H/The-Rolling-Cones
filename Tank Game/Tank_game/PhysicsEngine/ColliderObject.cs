using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

namespace Physics
{
    //This is a easydraw which has a collider. All the information to resolve a collision is stored here.
    //Question: Is extending a class the right way? Or should I store this inside of a gameobject? 
    public class ColliderObject : EasyDraw
    {


        public int radius;
        public Vec2 _position
        {
            get
            {
                return position;
            }
            set
            {
                //Set the position
                x = value.x;
                y = value.y;
                position = value;
            }
        }
        public Vec2 oldPosition;
        public ColliderManager colManager = new ColliderManager();
        public Vec2 velocity;
        public Vec2 oldVelocity;

        Vec2 position;

        public ColliderObject(int width, int height) : base (width, height)
        {

        }

        public void DrawLine()
        {

            if (((MyGame)game).drawDebugLine)
            {
                ((MyGame)game).DrawLine(oldPosition, position);
            }
        }
        
    }
}

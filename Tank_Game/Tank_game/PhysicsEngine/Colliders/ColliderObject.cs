using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;

namespace GXPEngine.PhysicsEngine.Colliders
{
    public class ColliderObject 
    {
        public CanvasBall ball;

        public int radius;
        public Vec2 _position
        {
            get
            {
                return position;
            }
            set
            {

                position = value;
            }
        }
        public Vec2 oldPosition;
        //public ColliderManager colManager = new ColliderManager();
        public Vec2 velocity;
        public Vec2 oldVelocity;

        Vec2 position;

        public ColliderObject(CanvasBall _ball)
        {
            ball = _ball;
        }

    }
}

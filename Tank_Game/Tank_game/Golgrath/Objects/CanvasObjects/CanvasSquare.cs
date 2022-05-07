using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasSquare : CanvasRectangle, Moveable
    {
        protected Vec2 oldPosition, gravity, velocity;
        protected SquareCollider squareCollider;
        public CanvasSquare(Vec2 position, Vec2 velocity, Vec2 gravity, int width, int height): base(position, width, height)
        {
            this.velocity = velocity;
            this.gravity = gravity;
            this.oldPosition = position;
            this.myCollider = new SquareCollider(this);
            
        }

        public Vec2 OldPosition
        {
            get
            {
                return this.oldPosition;
            }
        }

        public Vec2 Gravity
        {
            set
            {
                this.gravity = value;
            }
            get
            {
                return this.gravity;
            }
        }

        public Vec2 Velocity
        {
            set
            {
                this.velocity = value;
            }
            get
            {
                return this.velocity;
            }
        }

        public void Update()
        {
            this.Step();
        }

        public void Step()
        {
            this.oldPosition = this.position;
            this.velocity += MyGame.collisionManager.FirstTime == true ? gravity : new Vec2(-gravity.x, -gravity.y);
            this.Position += velocity;

            MyGame.collisionManager.CollideWith(this.myCollider);
        }
    }
}

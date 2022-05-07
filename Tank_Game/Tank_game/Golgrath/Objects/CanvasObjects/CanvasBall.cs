using GXPEngine.PhysicsEngine.Colliders;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasBall : CanvasCircle, Moveable
    {
        protected Vec2 oldPosition, gravity, velocity;
        public CanvasBall(int radius, Vec2 position, Vec2 gravity, Vec2 velocity): base(radius, position)
        {
            this.radius = radius;
            this.velocity = velocity;
            this.gravity = gravity;
            this.oldPosition = position;
        }

        public Vec2 OldPosition
        {
            get
            {
                return this.oldPosition;
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

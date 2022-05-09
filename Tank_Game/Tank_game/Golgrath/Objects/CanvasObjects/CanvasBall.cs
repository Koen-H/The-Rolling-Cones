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

        public new Vec2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                this.x = value.x;
                this.y = value.y;
                CircleCollider circleCollider = (CircleCollider)this.myCollider;
                circleCollider.Position = value;
            }
        }

        public Vec2 OldPosition
        {
            get
            {
                return this.oldPosition;
            }
            set
            {
                this.oldPosition = value;
                CircleCollider circleCollider = (CircleCollider)this.myCollider;
                circleCollider.OldPosition = value;
            }
        }

        public Vec2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value; 
                CircleCollider circleCollider = (CircleCollider)this.myCollider;
                circleCollider.Velocity = value;
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
            this.OldPosition = this.Position;
            this.Velocity += MyGame.collisionManager.FirstTime == true ? gravity : new Vec2(-gravity.x, -gravity.y);
            this.Position += velocity;

            MyGame.collisionManager.CollideWith(this.myCollider);
        }
    }
}

using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class PlayerLine : Line
    {
        private Vec2 velocity;
        private Pinball target;
        private float moveSpeed;
        public PlayerLine(Vec2 start, Vec2 end, uint pColor = 0xffffffff) : base(start, end, pColor)
        {
            this.moveSpeed = 5.0F;
            this.velocity = new Vec2(0, 0);
            MyGame.collisionManager.RemoveCollider(this.lineCollider);
            this.lineCollider = new PlayerLineCollider(this);
            MyGame.collisionManager.AddCollider(this.lineCollider);
        }
        public Pinball Target
        {
            set
            {
                this.target = value;
            }
            get
            {
                return this.target;
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
            float length = this.Length();
            if (Input.GetKey(Key.RIGHT) && this.End.x + velocity.x < 678 && this.Start.x + velocity.x < 678 - length)
            {
                this.velocity.x = this.moveSpeed;
            }
            else if (Input.GetKey(Key.LEFT) && this.Start.x + velocity.x > 121 && this.End.x + velocity.x > 121 + length)
            {
                this.velocity.x = -this.moveSpeed;
            }
            else
            {
                this.velocity.x = 0;
            }
            this.Start += velocity;
            this.End += velocity;
            //Console.WriteLine(length);
            //Console.WriteLine(this.Start + " " + this.End);
            //121, 678
        }
    }
}

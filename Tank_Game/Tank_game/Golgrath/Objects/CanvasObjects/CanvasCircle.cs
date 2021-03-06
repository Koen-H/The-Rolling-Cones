using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasCircle : MyCanvas
    {
        protected float radius;
        public CanvasCircle(int radius, Vec2 position, int canvasWidth, int canvasHeight): base(position, canvasWidth, canvasHeight)
        {
            this.radius = radius;
            this.SetOrigin(radius, radius);
           // Draw(230, 200, 0);
            this.myCollider = new CircleCollider(this, this.position, this.radius);
        }

        public CanvasCircle(int radius, Vec2 position) : base(position, radius * 2 + 1, radius * 2 + 1)
        {
            this.radius = radius;
            this.SetOrigin(radius, radius);
            //Draw(230, 200, 0);
            this.myCollider = new CircleCollider(this, this.position, this.radius);
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
                CircleCollider collider = (CircleCollider)this.myCollider;
                collider.Position = value;
                collider.OldPosition = value;
            }
        }

        public float Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                this.radius = value;
                CircleCollider collider = (CircleCollider)this.myCollider;
                collider.Radius = value;
            }
        }

        private void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
        }
    }
}

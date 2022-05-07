using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class Circle : MyAnimationSprite
    {
        protected float radius;
        public Circle(int radius, Vec2 position, string filename, int columns, int rows) : base(position, filename, columns, rows)
        {
            this.radius = radius;
            this.width = radius;
            this.height = radius;
            this.SetOrigin(radius, radius);
            this.myCollider = new CircleCollider(this);
            MyGame.collisionManager.AddCollider(this.myCollider);
        }

        public float Radius
        {
            get
            {
                return this.radius;
            }
        }
    }
}


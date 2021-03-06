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
        public bool isOrbital;
        public Circle(int radius, Vec2 position, string filename, int columns, int rows, bool _isOrbital = false) : base(position, filename, columns, rows)
        {
            isOrbital = _isOrbital;
            this.radius = radius;
            this.width = radius;
            this.height = radius;
            this.SetOrigin(radius, radius);
            this.myCollider = new CircleCollider(this, this.position, this.radius);
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


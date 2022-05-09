using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects.AnimationObjects
{
    public class Rectangle : MyAnimationSprite
    {
        protected CanvasLine topSide, rightSide, bottomSide, leftSide;
        public Rectangle(Vec2 position, string filename, int columns, int rows) : base(position, filename, columns, rows)
        {
            this.MyCollider = new SquareCollider(this);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.AddCollider(new LineCollider(this, sides[0], sides[1]));
            this.AddCollider(new CircleCollider(this, sides[0], 0));
            this.AddCollider(new LineCollider(this, sides[1], sides[2]));
            this.AddCollider(new CircleCollider(this, sides[1], 0));
            this.AddCollider(new LineCollider(this, sides[2], sides[3]));
            this.AddCollider(new CircleCollider(this, sides[2], 0));
            this.AddCollider(new LineCollider(this, sides[3], sides[0]));
            this.AddCollider(new CircleCollider(this, sides[3], 0));
        }

        public Rectangle(Vec2 position, string filename, int columns, int rows, int frames) : base(position, filename, columns, rows, frames)
        {
            this.MyCollider = new SquareCollider(this);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.AddCollider(new LineCollider(this, sides[0], sides[1]));
            this.AddCollider(new CircleCollider(this, sides[0], 0));
            this.AddCollider(new LineCollider(this, sides[1], sides[2]));
            this.AddCollider(new CircleCollider(this, sides[1], 0));
            this.AddCollider(new LineCollider(this, sides[2], sides[3]));
            this.AddCollider(new CircleCollider(this, sides[2], 0));
            this.AddCollider(new LineCollider(this, sides[3], sides[0]));
            this.AddCollider(new CircleCollider(this, sides[3], 0));
        }

        public Rectangle(Vec2 position, string filename, int width, int height, int columns, int rows) : base(position, filename, width, height, columns, rows)
        {
            this.MyCollider = new SquareCollider(this);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.AddCollider(new LineCollider(this, sides[0], sides[1]));
            this.AddCollider(new CircleCollider(this, sides[0], 0));
            this.AddCollider(new LineCollider(this, sides[1], sides[2]));
            this.AddCollider(new CircleCollider(this, sides[1], 0));
            this.AddCollider(new LineCollider(this, sides[2], sides[3]));
            this.AddCollider(new CircleCollider(this, sides[2], 0));
            this.AddCollider(new LineCollider(this, sides[3], sides[0]));
            this.AddCollider(new CircleCollider(this, sides[3], 0));
        }

        public CanvasLine TopSide
        {
            get
            {
                return this.topSide;
            }
        }
        public CanvasLine RightSide
        {
            get
            {
                return this.rightSide;
            }
        }
        public CanvasLine BottomSide
        {
            get
            {
                return this.bottomSide;
            }
        }
        public CanvasLine LeftSide
        {
            get
            {
                return this.leftSide;
            }
        }

    }
}

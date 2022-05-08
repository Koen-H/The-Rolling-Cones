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
            this.myCollider = new SquareCollider(this);
            MyGame.collisionManager.AddCollider(this.myCollider);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.topSide = new CanvasLine(sides[0], sides[1], 0xffffffff, false, false, true);
            this.rightSide = new CanvasLine(sides[1], sides[2], 0xffffffff, false, false, true);
            this.bottomSide = new CanvasLine(sides[2], sides[3], 0xffffffff, false, false, true);
            this.leftSide = new CanvasLine(sides[3], sides[0], 0xffffffff, false, false, true);
        }

        public Rectangle(Vec2 position, string filename, int columns, int rows, int frames) : base(position, filename, columns, rows, frames)
        {
            this.myCollider = new SquareCollider(this);
            MyGame.collisionManager.AddCollider(this.myCollider);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.topSide = new CanvasLine(sides[0], sides[1], 0xffffffff, false, false, true);
            this.rightSide = new CanvasLine(sides[1], sides[2], 0xffffffff, false, false, true);
            this.bottomSide = new CanvasLine(sides[2], sides[3], 0xffffffff, false, false, true);
            this.leftSide = new CanvasLine(sides[3], sides[0], 0xffffffff, false, false, true);
        }

        public Rectangle(Vec2 position, string filename, int width, int height, int columns, int rows) : base(position, filename, width, height, columns, rows)
        {
            this.myCollider = new SquareCollider(this);
            MyGame.collisionManager.AddCollider(this.myCollider);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            this.topSide = new CanvasLine(sides[0], sides[1], 0xffffffff, false, false, true);
            this.rightSide = new CanvasLine(sides[1], sides[2], 0xffffffff, false, false, true);
            this.bottomSide = new CanvasLine(sides[2], sides[3], 0xffffffff, false, false, true);
            this.leftSide = new CanvasLine(sides[3], sides[0], 0xffffffff, false, false, true);
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

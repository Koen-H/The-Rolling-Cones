using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Coolgrath;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasRectangle : MyCanvas
    {
        protected CanvasLine topSide, rightSide, bottomSide, leftSide;
        public CanvasRectangle(Vec2 position, int width, int height, string type = null): base(position, width, height)
        {
            if (type.Equals("InteractableEnvironment"))this.Draw(0, 100, 200, 50);
            this.myCollider = new SquareCollider(this);
            MyGame.collisionManager.AddCollider(this.myCollider);
            Vec2[] sides = { new Vec2(position.x, position.y), new Vec2(position.x + width, position.y), new Vec2(position.x + width, position.y + height), new Vec2(position.x, position.y + height) };
            /*this.topSide = new CanvasLine(sides[0], sides[1], 0xffffffff, false, false, true);
            this.rightSide = new CanvasLine(sides[1], sides[2], 0xffffffff, false, false, true);
            this.bottomSide = new CanvasLine(sides[2], sides[3], 0xffffffff, false, false, true);
            this.leftSide = new CanvasLine(sides[3], sides[0], 0xffffffff, false, false, true);*/
        }

        private void Draw(byte red, byte green, byte blue, byte alpha = 255)
        {
            Fill(red, green, blue, alpha);
            Stroke(red, green, blue, alpha);
            Rect(0, 0, this.width * 2, this.height * 2);
        }
    }
}

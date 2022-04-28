using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine.Colliders;

namespace GXPEngine.Golgrath.Objects
{
    public class Line : MyGameObject
    {
        protected Vec2 start, end;
        protected uint color;
        protected LineCollider lineCollider;
        protected Cap cap1, cap2;
        public Line(Vec2 start, Vec2 end, uint pColor = 0xffffffff): base(new Vec2(0, 0), 800, 1080)
        {
            this.start = start;
            this.end = end;
            this.lineCollider = new LineCollider(this);
            this.cap1 = new Cap(0, start);
            this.cap2 = new Cap(0, end);
            this.Stroke(System.Drawing.Color.Aqua);
            this.Line(this.start.x, this.start.y, this.end.x, this.end.y);
            MyGame.collisionManager.AddCollider(this.lineCollider);
            MyGame.collisionManager.AddCollider(this.cap1.MyCollider);
            MyGame.collisionManager.AddCollider(this.cap2.MyCollider);
        }
        public Vec2 Start
        {
            get
            {
                return start;
            }
            set
            {
                this.start = value;
                this.cap1.Position = value;
            }
        }
        public Vec2 End
        {
            get
            {
                return end;
            }
            set
            {
                this.end = value;
                this.cap2.Position = value;
            }
        }

        public float Length()
        {
            Vec2 length = this.start - this.end;
            return length.Length();
        }

        /*override protected void RenderSelf(GLContext glContext)
        {
            Gizmos.DrawLine(this.Start.x, this.Start.y, this.End.x, this.End.y, this.parent, this.color);
        }*/
    }
}

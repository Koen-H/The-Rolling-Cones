using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine.Colliders;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasLine : MyCanvas
    {
        protected Vec2 start, end;
        protected uint color;
        protected CanvasCap cap1, cap2;
        protected bool render;
        public CanvasLine(Vec2 start, Vec2 end, uint pColor = 0xffffffff, bool render = true, bool disableStartCap = false, bool disableEndCap = false): base(new Vec2(0, 0), 3000, 3000)
        {
            this.start = start;
            this.end = end;
            this.render = render;
            this.myCollider = new LineCollider(this);
            if (!disableStartCap)
            {
                this.cap1 = new CanvasCap(0, start);
                MyGame.collisionManager.AddCollider(this.cap1.MyCollider);
            }
            if (!disableEndCap)
            {
                this.cap2 = new CanvasCap(0, end);
                MyGame.collisionManager.AddCollider(this.cap2.MyCollider);
            }
            this.Stroke(System.Drawing.Color.Red);
            if (this.render)
            {
                this.Line(start.x, start.y, end.x, end.y);
            }
            MyGame.collisionManager.AddCollider(this.myCollider);
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
                if (this.cap1 != null)
                {
                    this.cap1.Position = value;
                }
                if (this.render)
                {
                    this.ClearTransparent();
                    this.Line(start.x, start.y, end.x, end.y);
                }
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
                if (this.cap2 != null)
                {
                    this.cap2.Position = value;
                }
                if (this.render)
                {
                    this.ClearTransparent();
                    this.Line(start.x, start.y, end.x, end.y);
                }
            }
        }

        public float Length()
        {
            Vec2 length = this.start - this.end;
            return length.Length();
        }

        public CanvasCap StartCap
        {
            get
            {
                return this.cap1;
            }
        }

        public CanvasCap EndCap
        {
            get
            {
                return this.cap2;
            }
        }

        /*override protected void RenderSelf(GLContext glContext)
        {
            Gizmos.DrawLine(this.Start.x, this.Start.y, this.End.x, this.End.y, this.parent, this.color);
        }*/
    }
}

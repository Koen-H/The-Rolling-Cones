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
        protected CircleCollider cap1, cap2;
        protected bool render;
        protected bool oneWay;
        public CanvasLine(Vec2 start, Vec2 end, bool _oneWay = false, uint pColor = 0xffffffff, bool render = false, bool disableStartCap = false, bool disableEndCap = false): base(new Vec2(0, 0), 2000, 2000)
        {
            this.start = start;
            this.end = end;
            this.oneWay = _oneWay;
            this.render = render;
            this.MyCollider = new LineCollider(this,oneWay);
            if (!disableStartCap)
            {
                this.cap1 = new CircleCollider(this, this.start, 0);
                this.childColliders.Add(this.cap1);
            }
            if (!disableEndCap)
            {
                this.cap2 = new CircleCollider(this, this.end, 0);
                this.childColliders.Add(this.cap2);
            }
            this.Stroke(System.Drawing.Color.Red);
            if (this.render)
            {
                this.Line(start.x, start.y, end.x, end.y);
            }
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

        public CircleCollider StartCap
        {
            get
            {
                return this.cap1;
            }
        }

        public CircleCollider EndCap
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

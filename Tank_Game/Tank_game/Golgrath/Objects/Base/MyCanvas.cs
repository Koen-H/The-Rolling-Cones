using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public abstract class MyCanvas : EasyDraw
    {
        protected Vec2 position;
        protected Collider myCollider;
        /// <summary>
        /// A blank MyGameObject, which extends from EasyDraw. Contains a custom Vec2 position attribute and a custom Collider.
        /// </summary>
        /// <param name="position">Vec2, a point in a 2 dimensional space.</param>
        /// <param name="canvasWidth">Canvas width</param>
        /// <param name="canvasHeight">Canvas height</param>
        public MyCanvas(Vec2 position, int canvasWidth, int canvasHeight): base(canvasWidth, canvasHeight, false)
        {
            this.Position = position;
        }

        public Vec2 Position
        {
            set
            {
                this.position = value;
                this.x = value.x;
                this.y = value.y;
            }
            get
            {
                return this.position;
            }
        }

        public Collider MyCollider
        {
            get
            {
                return this.myCollider;
            }
        }
    }
}

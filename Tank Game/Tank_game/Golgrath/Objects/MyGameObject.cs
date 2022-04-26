using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public abstract class MyGameObject : EasyDraw
    {
        protected Vec2 position;
        protected Collider myCollider;
        /// <summary>
        /// A blank MyGameObject, which extends from EasyDraw. Contains a custom Vec2 position attribute and a custom Collider.
        /// </summary>
        /// <param name="position">Vec2, a point in a 2 dimensional space.</param>
        /// <param name="canvasWidth">Canvas width</param>
        /// <param name="canvasHeight">Canvas height</param>
        public MyGameObject(Vec2 position, int canvasWidth, int canvasHeight): base(canvasWidth, canvasHeight)
        {
            this.position = position;
            this.UpdateScreenPosition();
        }

        public Vec2 Position
        {
            set
            {
                this.position = value;
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
        
        public void UpdateScreenPosition()
        {
            this.x = position.x;
            this.y = position.y;
        }
    }
}

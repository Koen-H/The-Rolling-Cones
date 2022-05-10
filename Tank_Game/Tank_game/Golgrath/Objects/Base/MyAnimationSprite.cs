using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public abstract class MyAnimationSprite : AnimationSprite
    {
        protected Vec2 position;
        protected Collider myCollider;
        protected List<Collider> childColliders;
        /// <summary>
        /// A blank MyGameObject, which extends from EasyDraw. Contains a custom Vec2 position attribute and a custom Collider.
        /// </summary>
        /// <param name="position">Vec2, a point in a 2 dimensional space.</param>
        /// <param name="canvasWidth">Canvas width</param>
        /// <param name="canvasHeight">Canvas height</param>
        public MyAnimationSprite(Vec2 position, string filename, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = true): base(filename, columns, rows, frames, keepInCache, addCollider)
        {
            this.Position = position;
            this.childColliders = new List<Collider>();
        }

        public MyAnimationSprite(Vec2 position, string filename, int width, int height, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, columns, rows, frames, keepInCache, addCollider)
        {
            this.width = width;
            this.height = height;
            this.Position = position;
            this.childColliders = new List<Collider>();
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
            set
            {
                this.myCollider = value;
            }
        }

        public List<Collider> ChildColliders
        {
            get
            {
                return this.childColliders;
            }
        }

        public void AddCollider(Collider collider)
        {
            if (!this.childColliders.Contains(collider))
            {
                this.childColliders.Add(collider);
                MyGame.collisionManager.AddCollider(collider);
            }
        }

        public void RemoveCollider(Collider collider)
        {
            if (this.childColliders.Contains(collider))
            {
                this.childColliders.Remove(collider);
                MyGame.collisionManager.RemoveCollider(collider);
            }
        }

        public virtual void Trigger(GameObject other)
        {

        }
    }
}

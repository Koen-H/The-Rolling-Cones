using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;
using Physics;

namespace GXPEngine.PhysicsEngine.Colliders
{
    public abstract class Collider
    {
        public bool trigger;
        protected Vec2 position;
        protected GameObject owner;
        /// <summary>
        /// Constructor for the Collider class, requires an owner to be attatched to.
        /// </summary>
        /// <param name="owner">MyGameObject, a custom variation of GameObject.</param>
        public Collider(MyCanvas owner)
        {
            this.owner = owner;
        }
        public Collider(MyCanvas owner, bool trigger)
        {
            this.owner = owner;
            this.trigger = trigger;
        }
        /// <summary>
        /// Constructor for the Collider class, requires an owner to be attatched to.
        /// </summary>
        /// <param name="owner">MyAnimationSprite</param>
        public Collider(MyAnimationSprite owner)
        {
            this.owner = owner;
        }
        public Collider(MyAnimationSprite owner, bool trigger)
        {
            this.owner = owner;
            this.trigger = trigger;
        }
        /// <summary>
        /// Constructor for the Collider class, requires an owner to be attatched to.
        /// </summary>
        /// <param name="owner">GameObject</param>
        public Collider(GameObject owner)
        {
            this.owner = owner;
        }
        public Collider(GameObject owner, bool trigger)
        {
            this.owner = owner;
            this.trigger = trigger;
        }
        /// <summary>
        /// Gets the collision info if a collision happens with the collider that is given.
        /// </summary>
        /// <param name="colideWith">The collider to check with.</param>
        /// <returns>Null or CollisionInfo object, information about the collision that happened.</returns>
        public abstract CollisionInfo Collision(Collider colideWith);
        /// <summary>
        /// Resolves the collision. NEEDS TO BE OVERRIDEN TO MAKE AN ACTUAL RESOLUTION HAPPEN!
        /// </summary>
        /// <param name="info">CollisionInfo object, information about the collision that happened.</param>
        public abstract void Resolve(CollisionInfo info);
        /// <summary>
        /// A getter and setter for the 'owner' property in my own Collider class.
        /// </summary>
        public GameObject Owner
        {
            get
            {
                return this.owner;
            }
        }

        public Vec2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
    }
}

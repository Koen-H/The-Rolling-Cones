using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;
using GXPEngine.PhysicsEngine.Colliders;
using Physics;

namespace GXPEngine.PhysicsEngine
{
    public class MyCollisionManager
    {
        private List<Collider> colliders;
        private bool firstTime;
        /// <summary>
        /// My Own CollisionManager constructor.
        /// </summary>
        public MyCollisionManager()
        {
            this.colliders = new List<Collider>();
            this.firstTime = true;
        }
        /// <summary>
        /// A method that is run every update for every object that you want to check it for (Preferably one that moves.)
        /// It also looks for the earliest ToI.
        /// </summary>
        /// <param name="collideWith">The collider of which owner is currently moving.</param>
        public void CollideWith(Collider collideWith)
        {
            Collider other = null;
            CollisionInfo info = null;
            foreach (Collider collider in colliders)
            {
                if (collider != collideWith)
                {
                    CollisionInfo newInfo = collider.Collision(collideWith);
                    if (info == null)
                    {
                        if (newInfo != null)
                        {
                            info = newInfo;
                            other = collider;
                        }
                    }
                    else
                    {
                        if (newInfo != null)
                        {
                            if (newInfo.timeOfImpact < info.timeOfImpact)
                            {
                                info = newInfo;
                                other = collider;
                            }
                        }
                    }
                }
            }
            if (other != null && info != null)
            {
                other.Resolve(info);
                if (info.timeOfImpact <= 0.00001 && firstTime)
                {
                    this.firstTime = false;
                    if (collideWith.Owner is Moveable)
                    {
                        if (collideWith.Owner is CanvasPlayerBall)
                        {
                            CanvasPlayerBall move = (CanvasPlayerBall)collideWith.Owner;
                            move.Step();
                        }
                    }
                }
            }
            this.firstTime = true;
        }

        public bool FirstTime
        {
            set
            {
                this.firstTime = value;
            }
            get
            {
                return this.firstTime;
            }
        }
        /// <summary>
        /// Adds a collider to the list to loop through
        /// </summary>
        /// <param name="collider">A Collider object</param>
        public void AddCollider(Collider collider)
        {
            this.colliders.Add(collider);
        }
        /// <summary>
        /// Removes a collider from the list if present.
        /// </summary>
        /// <param name="collider">A Collider object</param>
        public void RemoveCollider(Collider collider)
        {
            if (this.colliders.Contains(collider))
            {
                this.colliders.Remove(collider);
            }
        }
    }
}

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
        public List<Collider> colliders;
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
                            if (!collider.trigger)
                            {
                                info = newInfo;
                                other = collider;
                            }
                            else
                            {
                                if (collider.Owner is MyCanvas && collideWith.Owner is MyCanvas)
                                {
                                    MyCanvas otherOne = (MyCanvas)collider.Owner;
                                    MyCanvas mainOne = (MyCanvas)collideWith.Owner;
                                    mainOne.Trigger(otherOne);
                                }
                                else if (collider.Owner is MyAnimationSprite && collideWith.Owner is MyCanvas)
                                {
                                    MyAnimationSprite otherOne = (MyAnimationSprite)collider.Owner;
                                    MyCanvas mainOne = (MyCanvas)collideWith.Owner;
                                    mainOne.Trigger(otherOne);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (newInfo != null)
                        {
                            if (newInfo.timeOfImpact < info.timeOfImpact)
                            {
                                if (!collider.trigger)
                                {
                                    info = newInfo;
                                    other = collider;
                                }
                                else
                                {
                                    if (collider.Owner is MyCanvas && collideWith.Owner is MyCanvas)
                                    {
                                        MyCanvas otherOne = (MyCanvas)collider.Owner;
                                        MyCanvas mainOne = (MyCanvas)collideWith.Owner;
                                        mainOne.Trigger(otherOne);
                                    }
                                    else if (collider.Owner is MyAnimationSprite && collideWith.Owner is MyCanvas)
                                    {
                                        MyAnimationSprite otherOne = (MyAnimationSprite)collider.Owner;
                                        MyCanvas mainOne = (MyCanvas)collideWith.Owner;
                                        mainOne.Trigger(otherOne);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (other != null && info != null)
            {
                other.Resolve(info);
                //It was 0.00001
                if (info.timeOfImpact <= 0.01 && firstTime)
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

        /* public CollisionInfo CapsCollisionsTOI(ColliderObject collider)
         {
             MyGame myGame = (MyGame)Game.main;

             CollisionInfo earliestCollision = null;

             for (int i = 0; i < myGame.caps.Count(); i++)
             {
                  ColliderObject cap = myGame.GetCap(i);
                 if (cap != collider)
                 {
                     CollisionInfo colCheck = CircleCheckTOI(collider, cap);
                     if (colCheck != null) if ((earliestCollision == null || earliestCollision.timeOfImpact > colCheck.timeOfImpact)) earliestCollision = colCheck;
                 }
             }
             return earliestCollision;
         }

         public Vec2 CapsCollisions(ColliderObject collider)
         {
             MyGame myGame = (MyGame)Game.main;

             CollisionInfo earliestCollision = null;
             for (int i = 0; i < myGame.caps.Count(); i++)
             {
                 ColliderObject cap = myGame.GetCap(i);
                 if (cap != collider)
                 {
                     Vec2 colCheck = CircleCheck(collider, cap);
                     if (colCheck.x != 0 && colCheck.y != 0) return colCheck;
                     //if (colCheck != null) if ((earliestCollision == null || earliestCollision.timeOfImpact > colCheck.timeOfImpact)) earliestCollision = colCheck;
                 }
             }
             return new Vec2(0,0);
         }
          */
        CollisionInfo CircleCheckTOI(ColliderObject collider, ColliderObject other)//Get the earliest collision between two circles
        {
            Vec2 relativePosition = collider._position - other._position;
            if (relativePosition.Length() < collider.radius + other.radius)
            {
                //Struggling with the sldies, this finally worked, and I should NO LONGER TOUCH IT...
                Vec2 normal = (collider.oldPosition - other._position);//Get the normal.
                float a = Mathf.Pow(collider.velocity.Length(), 2);
                float b = 2 * normal.Dot(collider.velocity);//The dot product between the normal and velocity.
                float c = Mathf.Pow(normal.Length(), 2) - Mathf.Pow(collider.radius + other.radius, 2);
                Console.WriteLine($"A: {a}");
                Console.WriteLine($"B: {b}");
                Console.WriteLine($"C: {c}");
                if (c < 0)
                {
                    if (b < 0) return new CollisionInfo(normal, other.ball, 0);
                    else return null;
                }
                if (a == 0) return null;
                float d = Mathf.Pow(b, 2) - (4 * a * c);
                if (d < 0) return null;
                float toi = (-b - Mathf.Sqrt(d)) / (2 * a);
                if (0 <= toi && toi < 1) return new CollisionInfo(normal, other.ball, toi);
                else return null;
               
                Console.WriteLine($"D: {d}");
                Console.WriteLine($"toi: {toi}");
            }
            return null;
        }

        Vec2 CircleCheck(ColliderObject collider, ColliderObject other)//Get the earliest collision between two circles
        {
            Vec2 relativePosition = collider.ball.Position - other.ball.Position;

            if (relativePosition.Length() < collider.ball.Radius + other.ball.Radius)
            {
                return (collider.ball.Position - other.ball.Position).Normal();
                
            }
            return new Vec2(0,0);
        }

        public void ResolveCollision(ColliderObject col, CollisionInfo colInfo)//Resolves the collision
        {
            Console.WriteLine("A COLLISION HAPPEND!!");
            Vec2 poi = col.oldPosition + colInfo.timeOfImpact * col.velocity; //Get the point of impact 
            col._position = poi;//Set the position to point of impact
            col.velocity.Reflect(colInfo.normal.Normal()); //Reflect the velocity
            col.velocity *= -1;// And make it go the other way.
        }
    }
}

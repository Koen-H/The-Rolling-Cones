using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using GXPEngine.Core;
using Physics;

namespace Physics
{
    public class ColliderManager
    {
        MyGame myGame = (MyGame)Game.main;

        public CollisionInfo TankCollisions(ColliderObject tank)//Get the earliest collision between tanks
        {

            CollisionInfo earliestCollision = null;

            for (int i = 0; i < myGame.GetNumberOfTanks(); i++)
            {
                Tank other = myGame.GetTank(i);
                if (other != tank)
                {
                    CollisionInfo colCheck = CircleCheck(tank, other);
                    if (colCheck != null) if ((earliestCollision == null || earliestCollision.timeOfImpact > colCheck.timeOfImpact)) earliestCollision = colCheck;
                }
            }
            return earliestCollision;

        }
        public CollisionInfo AccelerationCheck(ColliderObject collider)// Gets the earliest collision with a accelerationcheck.
        {

            CollisionInfo earliestCollision = null;

            for (int i = 0; i < myGame.GetNumberOfAccelerationFields(); i++)
            {
                AccelerationField other = myGame.GetAccelerationField(i);
                if (other != collider)
                {
                    CollisionInfo colCheck = CircleCheck(collider, other);
                    if (colCheck != null) if ((earliestCollision == null || earliestCollision.timeOfImpact > colCheck.timeOfImpact)) earliestCollision = colCheck;
                }
            }
            return earliestCollision;

        }

        CollisionInfo CircleCheck(ColliderObject collider, ColliderObject other)//Get the earliest collision between two circles
        {
            Vec2 relativePosition = collider._position - other._position;
            if (relativePosition.Length() < collider.radius + other.radius)
            {
                //Struggling with the sldies, this finally worked, and I should NO LONGER TOUCH IT...
                Vec2 normal = (collider.oldPosition - other._position);//Get the normal.
                float a = Mathf.Pow(collider.velocity.Length(), 2);
                float b = 2 * normal.Dot(collider.velocity);//The dot product between the normal and velocity.
                float c = Mathf.Pow(normal.Length(), 2) - Mathf.Pow(collider.radius + other.radius, 2);
                if (c < 0)
                {
                    if (b < 0) return new CollisionInfo(normal, other, 0);
                    else return null;
                }
                if (a == 0) return null;
                float d = Mathf.Pow(b, 2) - (4 * a * c);
                if (d < 0) return null;
                float toi = (-b - Mathf.Sqrt(d)) / (2 * a);
                if (0 <= toi && toi < 1) return new CollisionInfo(normal, other, toi);
                else return null;
            }
            return null;
        }

        public Tank BulletHitTank(ColliderObject bullet)//if the distance is smaller than two radius, it's colliding and the tank + bullet should be destroyed.
        {
            for (int i = 0; i < myGame.GetNumberOfTanks(); i++)
            {
                Tank other = myGame.GetTank(i);
                if (bullet._position.Distance(other._position) < bullet.radius + other.radius)// If the distance between the two objects are smaller than the two radius combined, there is a collision!
                {
                    Console.WriteLine("Hit a tank!");

                    return other;
                }
            }
            return null;
        }

        public CollisionInfo LineCollision(ColliderObject col)//Gets and returns the earliest lineCollision
        {
            CollisionInfo earliestCollision = null;

            for (int i = 0; i < myGame.GetNumberOfLines(); i++)
            {

                float toi = float.NaN;
                LineSegment line = myGame.GetLine(i);
                Vec2 lineVec = line.end - line.start;//The vector of the line
                Vec2 normal = lineVec.Normal();//The normal of the line
                Vec2 oldDifferenceVec = col.oldPosition - line.start; // The difference between the oldposition and the line start.
                float a = normal.Dot(oldDifferenceVec) - col.radius;
                float b = -normal.Dot(col.velocity);
                if (b >= 0)
                {
                    if (a >= 0)
                    {
                        toi = a / b;
                    }
                    else if (a >= -col.radius)
                    {
                        toi = 0;
                    }
                    if (toi <= 1)
                    {

                        Vec2 poi = col.oldPosition + col.velocity * toi;
                        Vec2 diffVec2 = poi - line.start;
                        float distance = diffVec2.Dot(lineVec.Normalized());
                        if (distance >= 0 && distance < lineVec.Length())//If the distance is smaller than the linevec, it's not on the linevec.
                        {
                            Console.WriteLine("HIT");
                            if ((earliestCollision == null || earliestCollision.timeOfImpact > toi)) earliestCollision = new CollisionInfo(normal, line, toi);
                        }

                    }

                }

            }
            if (earliestCollision == null)// If there wasn't a collision with a line, check the caps as they are still part of the line.
            {
                for (int i = 0; i < myGame.GetNumberOfCaps(); i++)
                {
                    Caps cap = myGame.GetCap(i);
                    CollisionInfo loopCollision = CircleCheck(col, cap);
                    if (loopCollision != null && (earliestCollision == null || earliestCollision.timeOfImpact > loopCollision.timeOfImpact)) earliestCollision = loopCollision;
                }
            }
            return earliestCollision;
        }

        public void ResolveCollision(ColliderObject col, CollisionInfo colInfo)//Resolves the collision
        {
            Vec2 poi = col.oldPosition + colInfo.timeOfImpact * col.velocity; //Get the point of impact 
            col._position = poi;//Set the position to point of impact
            col.velocity.Reflect(colInfo.normal.Normal()); //Reflect the velocity
            col.velocity *= -1;// And make it go the other way.
        }
    }
   
}

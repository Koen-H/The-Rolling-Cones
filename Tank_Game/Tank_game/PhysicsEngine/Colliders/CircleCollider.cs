using GXPEngine.Coolgrath;
using GXPEngine.Golgrath.Objects;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.PhysicsEngine.Colliders
{
    public class CircleCollider : Collider
    {
        public CircleCollider(Circle circle): base(circle)
        {
            
        }

        //Handle Circle to Circle collision.
        public override CollisionInfo Collision(Collider collideWith)
        {
            CollisionInfo info = null;
            if (collideWith != this)
            {
                if (collideWith.Owner is Ball && this.Owner is OrbitalField)
                {
                    OrbitalField ownCircle = (OrbitalField)this.Owner;
                    Ball incBall = (Ball)collideWith.Owner;
                    Vec2 relative = incBall.Position - ownCircle.Position;
                    if (relative.Length() < ownCircle.Radius + incBall.Radius)
                    {
                        float gravity = incBall.Gravity.Length();
                        Vec2 pullDirection = ownCircle.Position - incBall.Position; //Draws a line from the bullet position to the center of the acceleration field.
                        float oldLength = incBall.Velocity.Length();//Gets the old lenght (speed)
                        incBall.Velocity = incBall.Velocity + pullDirection * ownCircle.PullStrength;//Set the velocity to the direction of the center of the acceleration field based on the pullstrength.
                        Console.WriteLine(incBall.Velocity);
                        incBall.Velocity = incBall.Velocity.Normalized();//Set speed to 1
                        Console.WriteLine(incBall.Velocity);
                        //  incBall.Velocity = (incBall.Velocity * (float)((oldLength + gravity) * 1.09));//Set speed back to original.
                        incBall.Velocity = incBall.Velocity * (oldLength + gravity);
                        Console.WriteLine(incBall.Velocity);
                        Console.WriteLine("Veloicity: " + incBall.Velocity);
                        //incBall.Gravity = new Vec2(0, 0);
                    }
                    
                }
                else if (collideWith.Owner is Ball && this.Owner is Circle)
                {
                    Circle ownCircle = (Circle)this.Owner;
                    Ball incBall = (Ball)collideWith.Owner;
                    Vec2 relative = incBall.Position - ownCircle.Position;
                    if (relative.Length() < ownCircle.Radius + incBall.Radius)
                    {
                        Vec2 u = incBall.OldPosition - ownCircle.Position;
                        float a = Mathf.Pow(incBall.Velocity.Length(), 2);
                        float b = (u * 2).Dot(incBall.Velocity);
                        float c = Mathf.Pow(u.Length(), 2.0F) - Mathf.Pow(ownCircle.Radius + incBall.Radius, 2.0F);
                        float discrete = Mathf.Pow(b, 2) - (4 * a * c);
                        if (c >= 0)
                        {
                            if (a > 0.1F && discrete >= 0)
                            {
                                float toi = (-b - Mathf.Sqrt(discrete)) / (a * 2);
                                if (toi >= 0.0F && toi <= 1.0F)
                                {
                                    if (info != null && toi < info.timeOfImpact)
                                        info = new CollisionInfo(u.Normalized(), incBall, toi);
                                    else if (info == null)
                                        info = new CollisionInfo(u.Normalized(), incBall, toi);
                                }
                            }
                        }
                        else
                        {
                            if (b < 0)
                                return new CollisionInfo(u.Normalized(), incBall, 0.0F);
                        }
                    }
                }
            }
            return info;
        }

        //Resolution of the collision between circles and balls.
        public override void Resolve(CollisionInfo info)
        {
            if (info != null)
            {
                Ball ball = (Ball)info.other;
                ball.Position = ball.OldPosition + ball.Velocity * info.timeOfImpact;
                if (MyGame.collisionManager.FirstTime)
                {
                    Vec2 velocity = ball.Velocity;
                    velocity.Reflect(info.normal, 1.0F);
                    ball.Velocity = velocity;
                }
            }
        }
    }
}

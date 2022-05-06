using GXPEngine.Golgrath.Objects;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.PhysicsEngine.Colliders
{
    public class LineCollider : Collider
    {

        public LineCollider(CanvasLine line) : base(line)
        {
            
        }
        //Detect ball to line collision.
        public override CollisionInfo Collision(Collider collideWith)
        {
			CollisionInfo info = null;
            if (collideWith is CircleCollider && collideWith.Owner is CanvasBall)
            {
                CanvasLine line = (CanvasLine)this.Owner;
                CanvasBall ball = (CanvasBall)collideWith.Owner;
                Vec2 totalLine1 = line.End - line.Start;
                Vec2 totalLine2 = line.Start - line.End;
                Vec2 ballPosition = ball.Position;
                Vec2 oldBallPosition = ball.OldPosition;

                Vec2 normal1 = totalLine1.Normal();
                Vec2 normal2 = totalLine2.Normal();
                Vec2 newLine1 = oldBallPosition - line.Start;
                Vec2 newLine2 = oldBallPosition - line.End;

                float a1 = normal1.Dot(newLine1) - ball.Radius;
                float b1 = -normal1.Dot(ball.Velocity);
                float a2 = normal2.Dot(newLine2) - ball.Radius;
                float b2 = -normal2.Dot(ball.Velocity);
                //Console.WriteLine(a2);
                if (b1 >= 0)
                {
                    if (a1 >= 0 || a1 >= -ball.Radius)
                    {
                        float toi = a1 >= 0 ? a1 / b1 : 0.0F;
                        if (toi >= 0.00F && toi <= 1.00F)
                        {
                            float lengthLine = totalLine1.Length();
                            Vec2 newLines = ballPosition - line.Start;
                            float dot = newLines.Dot(totalLine1.Normalized());
                            if (dot > 0 && dot < lengthLine)
                            {
                                if (info != null && toi < info.timeOfImpact)
                                    info = new CollisionInfo(normal1, ball, toi);
                                else if (info == null)
                                    info = new CollisionInfo(normal1, ball, toi);
                            }
                        }
                    }
                }
                if (b2 >= 0)
                {
                    if (a2 >= 0 || a2 >= -ball.Radius)
                    {
                        float toi = a2 >= 0 ? a2 / b2 : 0.0F;
                        if (toi >= 0.00F && toi <= 1.00F)
                        {
                            float lengthLine = totalLine1.Length();
                            Vec2 newLines = ballPosition - line.Start;
                            float dot = newLines.Dot(totalLine1.Normalized());
                            if (dot > 0 && dot < lengthLine)
                            {
                                if (info != null && toi < info.timeOfImpact)
                                    info = new CollisionInfo(normal2, ball, toi);
                                else if (info == null)
                                    info = new CollisionInfo(normal2, ball, toi);
                            }
                        }
                    }
                }
            }
            return info;
		}
        //Resolve ball to line collision
        public override void Resolve(CollisionInfo info)
        {
            if (info != null)
            {
                CanvasBall ball = (CanvasBall)info.other;
                ball.Position = ball.OldPosition + ball.Velocity * info.timeOfImpact;
                if (MyGame.collisionManager.FirstTime)
                {
                    Vec2 velocity = ball.Velocity;
                    velocity.Reflect(info.normal, 0.7F);
                    ball.Velocity = velocity;
                }
            }
        }
    }
}

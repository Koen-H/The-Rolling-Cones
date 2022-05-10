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
        protected Vec2 start, end;
        public LineCollider(CanvasLine line) : base(line)
        {
            this.start = line.Start;
            this.end = line.End;
        }
        public LineCollider(MyCanvas myCanvas, Vec2 start, Vec2 end) : base(myCanvas)
        {
            this.start = start;
            this.end = end;
        }
        public LineCollider(MyAnimationSprite myAnimationSprite, Vec2 start, Vec2 end) : base(myAnimationSprite)
        {
            this.start = start;
            this.end = end;
        }

        public Vec2 Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }

        public Vec2 End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
            }
        }

        //Detect ball to line collision.
        public override CollisionInfo Collision(Collider collideWith)
        {
			CollisionInfo info = null;
            if (collideWith.Owner is CanvasBall || collideWith.Owner is Ball)
            {
                CircleCollider ball = (CircleCollider)collideWith;
                Vec2 totalLine1 = this.End - this.Start;
                Vec2 totalLine2 = this.Start - this.End;
                Vec2 ballPosition = ball.Position;
                Vec2 oldBallPosition = ball.OldPosition;

                Vec2 normal1 = totalLine1.Normal();
                Vec2 normal2 = totalLine2.Normal();
                Vec2 newLine1 = oldBallPosition - this.Start;
                Vec2 newLine2 = oldBallPosition - this.End;

                float a1 = normal1.Dot(newLine1) - ball.Radius;
                float b1 = -normal1.Dot(ball.Velocity);
                float a2 = normal2.Dot(newLine2) - ball.Radius;
                float b2 = -normal2.Dot(ball.Velocity);
                //Console.WriteLine(a2);
                if (b1 > 0)
                {
                    if (a1 >= 0 || a1 >= -ball.Radius)
                    {
                        float toi = a1 >= 0 ? a1 / b1 : 0.0F;
                        if (toi >= 0.00F && toi <= 1.00F)
                        {
                            float lengthLine = totalLine1.Length();
                            Vec2 newLines = ballPosition - this.Start;
                            float dot = newLines.Dot(totalLine1.Normalized());
                            if (dot > 0 && dot < lengthLine)
                            {
                                if (info != null && toi < info.timeOfImpact)
                                    info = new CollisionInfo(normal1, ball.Owner, toi);
                                else if (info == null)
                                    info = new CollisionInfo(normal1, ball.Owner, toi);
                            }
                        }
                    }
                }
                if (b2 > 0)
                {
                    if (a2 >= 0 || a2 >= -ball.Radius)
                    {
                        float toi = a2 >= 0 ? a2 / b2 : 0.0F;
                        if (toi >= 0.00F && toi <= 1.00F)
                        {
                            float lengthLine = totalLine1.Length();
                            Vec2 newLines = ballPosition - this.Start;
                            float dot = newLines.Dot(totalLine1.Normalized());
                            if (dot > 0 && dot < lengthLine)
                            {
                                if (info != null && toi < info.timeOfImpact)
                                    info = new CollisionInfo(normal2, ball.Owner, toi);
                                else if (info == null)
                                    info = new CollisionInfo(normal2, ball.Owner, toi);
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
                if (info.other is Moveable && info.other is MyCanvas)
                {
                    MyCanvas myCanvas = (MyCanvas)info.other;
                    Moveable moveable = (Moveable)info.other;
                    myCanvas.Position = moveable.OldPosition + moveable.Velocity * info.timeOfImpact;
                    if (MyGame.collisionManager.FirstTime)
                    {
                        Vec2 velocity = moveable.Velocity;
                        velocity.Reflect(info.normal, 0.7F);
                        moveable.Velocity = velocity;
                    }
                }
            }
        }
    }
}

using GXPEngine.Golgrath.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics;

namespace GXPEngine.PhysicsEngine.Colliders
{
    public class PlayerLineCollider : LineCollider
    {
        public PlayerLineCollider(Line line): base(line)
        {
            
        }

        public override void Resolve(CollisionInfo info)
        {
            if (info != null && info.other is Pinball)
            {
                Pinball ball = (Pinball)info.other;
                ball.Position = ball.OldPosition + ball.Velocity * info.timeOfImpact;
                if (MyGame.collisionManager.FirstTime)
                {
                    if (Input.GetKey(Key.W) && info.normal.y == -1)
                    {
                        PlayerLine playerLine = (PlayerLine)this.Owner;
                        ball.LockOn = playerLine;
                        playerLine.Target = ball;
                    }
                    else
                    {
                        Vec2 velocity = ball.Velocity;
                        velocity.Reflect(info.normal, 1.0F);
                        ball.Velocity = velocity;
                    }
                }
            }
        }
    }
}

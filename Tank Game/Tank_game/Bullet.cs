using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics;

namespace GXPEngine
{
    public class Bullet : ColliderObject
    {

        readonly Tank shooter;
        readonly float rotationSpeed = 10f;

        readonly bool playerBullet;
        bool guided = false;
        bool bounced = false;

        public Bullet(Vec2 pPosition, Vec2 pVelocity,Tank pShooter, bool pPlayerBullet = false, int pRadius = 4) : base(pRadius * 2 + 1, pRadius * 2 + 1)
        {
            SetOrigin(width / 4, height / 2);
            _position = pPosition;
            velocity = pVelocity;
            shooter = pShooter;
            playerBullet = pPlayerBullet;
            guided = pPlayerBullet;
            radius = pRadius;
            Draw(255,0,0);
        }

        void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
        }

        void Guided()
        {
            Vec2 mousePosition = new Vec2(Input.mouseX, Input.mouseY);
            Vec2 toMouse = mousePosition - new Vec2(x, y);

            float rot = rotation;
            float diff = AngleDifference(rot, toMouse.GetAngleDegrees());
            Vec2 diffVec = Vec2.GetUnitVectorDeg(diff);

            //Check if the ball is at the cursor of the mouse. The offset is added to help prevent infinite turning.
            if ((mousePosition - _position).Length() < radius + 3 || !Input.GetMouseButton(0)) 
            {
                guided = false;
            }

            if (diff > rotationSpeed)
            {
                rotation += rotationSpeed;
            }
            else if (diff < -rotationSpeed)
            {
                rotation -= rotationSpeed;
            }
            velocity.SetAngleDegrees(rotation);//Change the velocity direction based on the new rotation
            
        }

        float AngleDifference(float angle1, float angle2) //Get the difference between two angles.
        {
            float diff = (angle2 - angle1 + 180) % 360 - 180;
            return diff < -180 ? diff + 360 : diff;
        }

        public void Update()
        {
            oldPosition = _position;
            _position += velocity;
            if (guided) Guided();
            CheckCollisions();
            BoundaryPrevention();

            DrawLine();

        }

        void CheckCollisions()
        {
            //colManager.lineCollision(this);
            CollisionInfo collision = colManager.LineCollision(this);//Check for a collision with a line.
            if (collision != null)
            {
                if (bounced) Destroy();
                else if(collision.other != shooter)
                guided = false;
                colManager.ResolveCollision(this, collision);//Resolve the collision
                bounced = true;
            }
            Tank hitTank = colManager.BulletHitTank(this); //Check for a collision with a tank
            if (hitTank != null)
            {//If the bullet hit a tank
                if (bounced || hitTank != shooter)
                {//and it isn't the shooter or already bounced before (so the else already happend.)
                    ((MyGame)Game.main).RemoveTank(hitTank);
                    Destroy();
                }
                else//If it hit itself due to movement speed. Reflect velocity and turn of guided.
                {
                    guided = false;
                    velocity *= -1;
                }
            }
            CollisionInfo gravityCollision = colManager.AccelerationCheck(this);
            if(gravityCollision != null)
            {
                
                float pullStrength = 0.05f;
                ColliderObject other = (ColliderObject)gravityCollision.other;
                guided = false;
                Vec2 pullDirection = other._position - _position; //Draws a line from the bullet position to the center of the acceleration field.
                float oldLength = velocity.Length();//Gets the old lenght (speed)
                velocity += pullDirection * pullStrength;//Set the velocity to the direction of the center of the acceleration field based on the pullstrength.
                velocity.Normalize();//Set speed to 1
                velocity *= oldLength;//Set speed back to original.
            }
        }

        void BoundaryPrevention()//Incase it somehow ends up outside of the view, destroy the object.
        {
            if (_position.x < 0 || _position.x > game.width || _position.y < 0 || _position.y > game.height) Destroy();
        }
    }
}

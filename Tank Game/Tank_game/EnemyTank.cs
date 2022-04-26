using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics;

namespace GXPEngine
{
    class EnemyTank : Tank
    {
        readonly float bulletSpeed = 2.5f;
        readonly float bulletInterval = 5000f;
        float bullets = 5000;
        float bulletTime;

        public EnemyTank(Vec2 position) : base(20,position,255,0,0)
        {
            bulletTime = Time.time + bulletInterval;
        }

        void Shoot()
        {
            if (bulletTime < Time.time)
            {
                bulletTime = Time.time + bulletInterval;
                Bullet instance = new Bullet((new Vec2(x, y) + Vec2.GetUnitVectorDeg(_barrel.rotation + rotation) * (_barrel.height + 12)), (Vec2.GetUnitVectorDeg(_barrel.rotation + rotation)) * bulletSpeed, this);
                //Create a new bullet, the position is defined by getting the position of the tank, rotating it based on the barrel + barrel length (and a offset of 12 to make it look better)
                //Defines the velocity by getting the angle of the barrel + the tank and scaling it by bullet speed.
                parent.AddChild(instance);
                instance.rotation = _barrel.rotation + rotation;
                bullets--;
            }
        }

        void Move()
        {
            //Move has been disabled, as I can't make AI, some basic stuff is still here for future development?
            oldPosition = _position;
            oldVelocity = velocity;
            Vec2 dir = Vec2.GetUnitVectorDeg(rotation);
            _position += velocity;

            CollisionInfo collision = colManager.LineCollision(this);//Check for a collision with the line
            if (collision == null) collision = colManager.TankCollisions(this);//If none, Check for a collision with another tank.
            if (collision != null)
            {
                colManager.ResolveCollision(this, collision);
            }
        }

        void Update()
        {
           // Move();
            //Shoot();
        }
    }
}

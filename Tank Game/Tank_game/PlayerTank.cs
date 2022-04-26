using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class PlayerTank : Tank
    {
        //This is the player tank

        readonly float bulletSpeed = 6.5f;
        float  bullets = 5000;

        public PlayerTank(Vec2 position) : base(20,position,0,255,0)
        {

        }

        void Shoot()
        {
            if (Input.GetMouseButtonDown(0) && bullets > 0)
            {
                Bullet instance = new Bullet((new Vec2(x, y) + Vec2.GetUnitVectorDeg(_barrel.rotation + rotation) * (_barrel.height + 12)), (Vec2.GetUnitVectorDeg(_barrel.rotation + rotation)) * bulletSpeed, this, true);
                //Create a new bullet, the position is defined by getting the position of the tank, rotating it based on the barrel + barrel length (and a offset of 12 to make it look better)
                //Defines the velocity by getting the angle of the barrel + the tank and scaling it by bullet speed.
                parent.AddChild(instance);
                instance.rotation = _barrel.rotation + rotation;//set the gameobject's rotation correctly.
                bullets--;
            }
        }

        

        void Update()
        {
            Controls();
            Shoot();
        }
    }
}

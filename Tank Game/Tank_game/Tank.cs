using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using Physics;

namespace GXPEngine
{
     public class Tank : ColliderObject
    {
        public Barrel _barrel
        {
            get
            {
                return barrel;
            }
        }

        public float accelerationStrength = 0.2f;
        public float drag = 0.91f;
        public float rotationSpeed = 0.9f;

        readonly Barrel barrel;
        

        public Tank(int pRadius, Vec2 pPosition,byte red, byte green, byte blue) : base (pRadius*2 + 1, pRadius*2 + 1)
        {
            radius = pRadius;
            _position = pPosition;
            barrel = new Barrel();
            SetOrigin(radius, radius);
            Draw(red, green, blue);
            AddChild(barrel);
        }

        void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
            Fill(0, 0, 0);
            Stroke(0, 0, 0);
            Rect(radius + radius/2, radius, radius/2, radius);// put a rectangle at the front side, to keep track on which direction is forward
        }


        public void Controls()
        {
            oldPosition = _position;
            oldVelocity = velocity;
            Vec2 dir = Vec2.GetUnitVectorDeg(rotation);
            if (Input.GetKey(Key.LEFT))
            {
                rotation -= velocity.Length() * rotationSpeed;
            }
            if (Input.GetKey(Key.RIGHT))
            {
                rotation += velocity.Length() * rotationSpeed;
            }
            if (Input.GetKey(Key.UP))
            {
                velocity += dir * accelerationStrength;
            }
            else if (Input.GetKey(Key.DOWN))
            {
                velocity -= dir * accelerationStrength;
            }
            velocity = velocity.Dot(dir) * dir * drag;

            _position += velocity;

            //Check for collisions, and resolve them if there are any.
            CollisionInfo collision = colManager.LineCollision(this);
            if(collision == null) collision = colManager.TankCollisions(this);
            if (collision != null) colManager.ResolveCollision(this,collision);
            
        }
    }
}

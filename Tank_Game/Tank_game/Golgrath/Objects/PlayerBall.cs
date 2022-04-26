﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class PlayerBall : Ball
    {
        private float drag, acceleration, maxSpeed;
        public PlayerBall(int radius, Vec2 position, Vec2 gravity, Vec2 velocity) : base(radius, position, gravity, velocity)
        {
            this.DrawRect(0, 200, 0);
            this.drag = 0.03F;
            this.acceleration = 0.05F;
            this.maxSpeed = 3F;
        }

        public new void Update()
        {
            this.HandleInput();
            this.Step();
        }

        private void HandleInput()
        {
            Vec2 velocity = this.Velocity;
            if (Input.GetKey(Key.A) && velocity.x > -this.maxSpeed)
            {
                velocity.x -= acceleration;
            }
            else if (Input.GetKey(Key.D) && velocity.x < this.maxSpeed)
            {
                velocity.x += acceleration;
            }
            if (velocity.x > drag)
            {
                velocity.x -= drag;
            }
            else if (velocity.x < -drag)
            {
                velocity.x += drag;
            }
            else
            {
                velocity.x = 0;
            }
            this.Velocity = velocity;
        }
        private void DrawRect(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Rect(radius, radius * 2, radius / 2, radius / 2);
            //Testssss
        }

        public new void Step()
        {
            this.oldPosition = this.position;
            this.velocity += MyGame.collisionManager.FirstTime == true ? gravity : new Vec2(-gravity.x, -gravity.y);
            this.Position += velocity;
            MyGame.collisionManager.CollideWith(this.myCollider);
            this.UpdateScreenPosition();
        }
    }
}
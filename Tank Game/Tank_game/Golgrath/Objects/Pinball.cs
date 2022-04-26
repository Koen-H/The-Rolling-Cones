using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects
{
    public class Pinball : Ball
    {
        private PlayerLine lockOn;
        private bool firstInLoop, mouseFollow;
        private Vec2 shootArrow;
        private float rotationSpeed;
        public Pinball(int radius, Vec2 position, Vec2 gravity, Vec2 velocity): base(radius, position, gravity, velocity)
        {
            this.firstInLoop = true;
            this.rotationSpeed = 1;
            this.DrawRect(0, 200, 0);
        }

        public PlayerLine LockOn
        {
            set
            {
                this.lockOn = value;
            }
            get
            {
                return this.lockOn;
            }
        }

        public new void Update()
        {
            if (Input.GetKeyDown(Key.E))
            {
                this.mouseFollow = !this.mouseFollow;
            }
            this.Step();
        }
        private void DrawRect(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Rect(radius, radius * 2, radius / 2, radius / 2);
        }

        public new void Step()
        {
            this.oldPosition = this.position;
            if (this.lockOn == null)
            {
                this.velocity += MyGame.collisionManager.FirstTime == true ? gravity : new Vec2(-gravity.x, -gravity.y);
            }
            else
            {
                if (this.firstInLoop)
                {
                    this.firstInLoop = false;
                    this.shootArrow = new Vec2(0, -100);
                }
                this.velocity = lockOn.Velocity;
                if (!this.mouseFollow)
                {
                    float degrees = this.shootArrow.GetAngleDegrees();
                    if (Input.GetKey(Key.D) && (degrees - this.rotationSpeed >= 110 || degrees - this.rotationSpeed <= -110))
                    {
                        this.shootArrow.RotateDegrees(this.rotationSpeed);
                        this.rotation = -degrees;
                    }
                    else if (Input.GetKey(Key.A) && (degrees + this.rotationSpeed >= 110 || degrees + this.rotationSpeed <= -110))
                    {
                        this.shootArrow.RotateDegrees(-this.rotationSpeed);
                        this.rotation = -degrees;
                    }
                }
                else
                {
                    Vec2 toMouse = new Vec2(Input.mouseX, Input.mouseY) - this.position;
                    float degrees = toMouse.GetAngleDegrees();
                    if (degrees >= 110 || degrees <= -110)
                    {
                        this.shootArrow = toMouse.Normalized() * this.shootArrow.Length();
                        this.rotation = -degrees;
                    }
                }
                Gizmos.DrawArrow(this.position.x, this.position.y, this.shootArrow.x, this.shootArrow.y);
                if (Input.GetKeyDown(Key.W))
                {
                    this.lockOn = null;
                    this.firstInLoop = true;
                    this.velocity = this.shootArrow.Normalized() * 22;
                }
            }
            this.Position += velocity;
            MyGame.collisionManager.CollideWith(this.myCollider);
            this.UpdateScreenPosition();
        }
    }
}

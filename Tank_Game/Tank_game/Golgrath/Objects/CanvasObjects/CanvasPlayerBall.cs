using GXPEngine.Golgrath.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Coolgrath;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.Core;

namespace GXPEngine.Golgrath.Objects
{
    public class CanvasPlayerBall : CanvasBall
    {
        private float drag, acceleration, maxSpeed;
        private bool umbrella;
        private AnimationSprite playerSprite;
        bool goingLeft = false;
        bool inBush = false;
        public BushShot currentBush;
        BushShot lastBush;
        float bushInterval = 0;


        public bool pausePlayer;
        public new bool PausePlayer
        {
            get
            {
                return this.pausePlayer;
            }
            set
            {
                this.pausePlayer = value;
                this.pausedVelocity = this.velocity;
            }
        }
        private Vec2 pausedVelocity;

        private AnimationSprite umbrellaSprite;


        private AnimationSprite snowSprite;

        private Vec2 umbrellaGravity;
        private PlayerCamera camera;
        public CanvasPlayerBall(int radius, Vec2 position, Vec2 gravity, Vec2 velocity) : base(radius, position, gravity, velocity)
        {
            playerSprite = new AnimationSprite("RollingPineCone.png", 8,1,-1,false,false);
            playerSprite.SetOrigin(this.radius,this.radius);
            
            this.DrawRect(0, 200, 0);
            this.drag = 0.08F;
            this.acceleration = 0.4F;
            this.maxSpeed = 5F;
            this.SetOrigin(this.radius, this.radius);
            this.umbrellaSprite = new AnimationSprite("Umbrella.png", 8, 1, -1, false, false);
           // this.umbrellaGravity = gravity / 8;
            this.umbrellaSprite.alpha = 0.0F;
            this.umbrellaSprite.SetOrigin(this.umbrellaSprite.width / 2, this.umbrellaSprite.height / 2);
            this.AddChild(playerSprite);
            this.AddChild(umbrellaSprite);
            //this.AddChild(snowSprite);
            
        }

        public new void Update()
        {
            if (!pausePlayer)
            {
                this.Step();
            }
            /*Gizmos.DrawRectangle(this.x + _bounds.x, this.y + _bounds.y, 20, 20);
            Gizmos.DrawRectangle(this.x + width + _bounds.x, this.y + _bounds.y, 20, 20);
            Gizmos.DrawRectangle(this.x + _bounds.x, this.y + height + _bounds.y, 20, 20);
            Gizmos.DrawRectangle(this.x + width + _bounds.x, this.y + height + _bounds.y, 20, 20);*/
            if (camera != null)
            {
                this.camera.SetXY(this.position.x, this.position.y - 200);
            }
            //umbrellaSprite.rotation = -rotation;
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
            if (Input.GetKeyDown(Key.W))
            {
                this.umbrella = !this.umbrella;
                if (this.umbrella)
                {
                    this.umbrellaSprite.alpha = 1.0F;
                    playerSprite.alpha = 0.0f;
                }
                else
                {
                    this.umbrellaSprite.alpha = 0.0F;
                    playerSprite.alpha = 1f;
                }
            }
            /*if (velocity.x > drag)
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
            }*/


            this.Velocity = velocity;
            //Rotate the sprite based on the direction of the velocity.

            //Old rotation,it was actual ball rotation!
            if (velocity.Normalized().x < 0) rotation -= velocity.Length();
            else rotation += velocity.Length();
            if (rotation >= 360 || rotation <= -360) rotation = 0;

            //Rotatin sprite instead of ball...
            /* if (velocity.Normalized().x < 0 && !goingLeft)// To left
             {
                 playerSprite.initializeFromTexture(Texture2D.GetInstance("RollingPineConeLeft.png", false));
                 playerSprite.initializeAnimFrames(8, 1, -1);
                 playerSprite.SetOrigin(this.radius, this.radius);
                 goingLeft = true;
             }
             else if(velocity.Normalized().x > 0 && goingLeft)//To right
             {
                 playerSprite.initializeFromTexture(Texture2D.GetInstance("RollingPineCone.png", false));
                 playerSprite.initializeAnimFrames(8, 1, -1);
                 playerSprite.SetOrigin(this.radius, this.radius);
                 goingLeft = false;
             }
             playerSprite.Animate(velocity.Length()/50);*/
            playerSprite.Animate(0.05f);
            umbrellaSprite.Animate(0.05f);

        }
        private void DrawRect(byte red, byte green, byte blue)
        {
         /*   Fill(red, green, blue);
            Stroke(red, green, blue);
            Rect(radius, radius * 2, radius / 2, radius / 2);*/
        }

        public new void Step()
        {
            this.OldPosition = this.Position;
            if (MyGame.collisionManager.FirstTime)
            {
                this.HandleInput();
            }
            this.OldPosition = this.position;

            //new Vec2(-gravity.x, -gravity.y) I know i put this here, for a reason. KEEP THIS!
            if (MyGame.collisionManager.FirstTime == false)
            {
                this.umbrella = false;
                this.umbrellaSprite.alpha = 0.0F;
                playerSprite.alpha = 1f;
            }
            ApplyGravity();
            
            MyGame.collisionManager.CollideWith(this.myCollider);
            OnGeyser();
            InOrbital();
            OnCoin();
            OnBushShot();
            if(currentBush != null && inBush)
            {
                InBush();
            }
            //this.Position += velocity.Normalized() * (velocity.Length()/17 * Time.deltaTime);
            this.Position += velocity;
        }
        private void ApplyGravity()
        {
            
            if (umbrella && velocity.Normalized().y > 0)
            {
                //Do umbrella stuff
                Console.WriteLine("umbrella thigns" + rotation );
                float oldRotation = rotation * 0.90f;

                rotation *= 0.1f;
                rotation += oldRotation; // now it's 0.01* desired + 0.99 * old
                if(rotation <= 10 && rotation >= -10)
                {
                    rotation = 0;
                }

                //rotation = SinDamp(rotation);
                
                velocity += new Vec2(0, 0.2F);//Gravity, but less
                //Console.WriteLine(rotation);
                if (velocity.Length() > 7.5f )
                {
                  velocity = velocity.Normalized() * 7.5f;
                }
                


            }
            else
            {
               // playerSprite.Animate(0.1f);
                Velocity += MyGame.collisionManager.FirstTime == true ? gravity : new Vec2(0,0);

            }
        }
        private void OnGeyser()
        {
            MyGame myGame = (MyGame)Game.main;
            foreach (Geyser geyser in myGame.geysers)
            {
                if (HitTest(geyser))
                {
                    Console.WriteLine("On Geyser!");
                    velocity = Vec2.GetUnitVectorDeg(-90 + geyser.rotation) * 25;
                    geyser.DoAnimate();
                }
            }
        }

        private void OnBushShot()
        {
            if (!inBush){ 
                MyGame myGame = (MyGame)Game.main;
                foreach (BushShot bush in myGame.bushes)
                {
                    if(Time.time > bushInterval || bush != lastBush)
                    if (HitTest(bush))
                    {
                        this.umbrella = false;
                        this.umbrellaSprite.alpha = 0.0F;
                        playerSprite.alpha = 1f;
                        Console.WriteLine("On bush");
                        this.Position = bush.Position;// TODO: Make fancy?
                        inBush = true;
                        currentBush = bush;
                        bush.target.rotation = -90;
                        bush.target.alpha = 1f;
                    }
                }
            }
        }
        private void InBush()
        {
            currentBush.Aiming();
            rotation = currentBush.target.rotation;
            if (Input.GetKeyDown(Key.S))
            {
                velocity = Vec2.GetUnitVectorDeg(rotation) * 20; //Strength of Bushshot
                lastBush = currentBush;
                currentBush.target.alpha = 0f;
                currentBush = null;
                inBush = false;
                bushInterval = Time.time + 1200; //1.2 seconds
                Console.WriteLine(bushInterval);
            }
            else
            {
                velocity = new Vec2(0,0);
            }
        }

        private void InOrbital()
        {
            MyGame myGame = (MyGame)Game.main;
            foreach (OrbitalField other in myGame.fields)
            {
                if (HitTest(other))
                {
                    Console.WriteLine("TRIGGER WITH ORBITALFIELD");
                    OrbitalField ownCircle = (OrbitalField)other;
                    CanvasBall incBall = this;
                    Vec2 relative = incBall.Position - ownCircle.Position;
                    //if (relative.Length() < ownCircle.Radius + incBall.Radius)
                    //{
                        Console.WriteLine("ORBITAL 2");
                        float gravity = incBall.Gravity.Length();
                        Vec2 pullDirection = ownCircle.Position - incBall.Position; //Draws a line from the bullet position to the center of the acceleration field.
                        float oldLength = incBall.Velocity.Length();//Gets the old lenght (speed)
                        incBall.Velocity = incBall.Velocity + pullDirection * ownCircle.PullStrength;//Set the velocity to the direction of the center of the acceleration field based on the pullstrength.
                        incBall.Velocity = incBall.Velocity.Normalized();//Set speed to 1
                                                                         //  incBall.Velocity = (incBall.Velocity * (float)((oldLength + gravity) * 1.09));//Set speed back to original.
                        incBall.Velocity = incBall.Velocity * (oldLength + gravity);
                        //incBall.Gravity = new Vec2(0, 0);
                    //}
                }
                
            }
        }

        private void OnCoin()
        {
            MyGame myGame = (MyGame)Game.main;
            foreach (NextLevelBlock other in myGame.coins)
            {
                if (HitTest(other))
                {
                    Console.WriteLine("ON A COIN!");
                    myGame.currentLevel++;
                    myGame.LoadLevel("NEWLEVEL_" + myGame.currentLevel + ".tmx");

                }

            }
        }

        public void SetPlayerCamera(PlayerCamera camera)
        {
            this.camera = camera;
        }

        float SinDamp(float t)
        {
            return Mathf.Sin(Mathf.PI * 4 * t) * (1-t);
        }

      /*  public override void Trigger(GameObject other)
        {
            if (other is Geyser)
            {
                Geyser geyser = (Geyser)other;
                velocity += Vec2.GetUnitVectorDeg(-90 + geyser.rotation) * geyser.strength;
            }
            if(other is OrbitalField)
            {
                Console.WriteLine("TRIGGER WITH ORBITALFIELD");
                OrbitalField ownCircle = (OrbitalField)other;
                CanvasBall incBall = this;
                Vec2 relative = incBall.Position - ownCircle.Position;
                if (relative.Length() < ownCircle.Radius + incBall.Radius)
                {
                    float gravity = incBall.Gravity.Length();
                    Vec2 pullDirection = ownCircle.Position - incBall.Position; //Draws a line from the bullet position to the center of the acceleration field.
                    float oldLength = incBall.Velocity.Length();//Gets the old lenght (speed)
                    incBall.Velocity = incBall.Velocity + pullDirection * ownCircle.PullStrength;//Set the velocity to the direction of the center of the acceleration field based on the pullstrength.
                    incBall.Velocity = incBall.Velocity.Normalized();//Set speed to 1
                                                                     //  incBall.Velocity = (incBall.Velocity * (float)((oldLength + gravity) * 1.09));//Set speed back to original.
                    incBall.Velocity = incBall.Velocity * (oldLength + gravity);
                    //incBall.Gravity = new Vec2(0, 0);
                }
            }
        }*/
    }


}

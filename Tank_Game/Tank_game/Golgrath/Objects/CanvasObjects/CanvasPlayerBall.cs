using GXPEngine.Golgrath.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Coolgrath;
using GXPEngine.PhysicsEngine;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.Core;
using Physics;
using GXPEngine.Golgrath.Objects.CanvasObjects;

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
        SoundChannel orbit = new SoundChannel(1);
        public MyGame myGame = (MyGame) Game.main;




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
        private Sprite shadow = new Sprite("Shadow.png",false);


        private AnimationSprite snowSprite;

        private Vec2 umbrellaGravity;
        private PlayerCamera camera;
        public CanvasPlayerBall(int radius, Vec2 position, Vec2 gravity, Vec2 velocity) : base(radius, position, gravity, velocity)
        {
            playerSprite = new AnimationSprite("RollingPineCone.png", 8,1,-1,false,false);
            playerSprite.SetOrigin(this.radius,this.radius);
            shadow.SetOrigin(width/2,height/2);
            
            
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
            this.AddChild(shadow);
            shadow.alpha = 0.5f;
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

            if (Input.GetKeyDown(Key.K)) {
              MyGame myGame = (MyGame)Game.main;
                myGame.newGamePlus = !myGame.newGamePlus;
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
            if(velocity.Length() > 0.5f) {
                if (velocity.Normalized().x < 0) rotation -= velocity.Length();
                else rotation += velocity.Length();
            }
            else
            {
                velocity = new Vec2(0,0);
            }
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
            shadow.rotation = -rotation;
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
            CapCheck();

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
                    velocity = Vec2.GetUnitVectorDeg(-90 + geyser.rotation) * 24;
                    geyser.doAnimate = true;
                }
            }
        }

        private void OnBushShot()
        {
            if (!inBush){ 
                MyGame myGame = (MyGame)Game.main;
                foreach (BushShot bush in myGame.bushes)
                {
                    
                    if (Time.time > bushInterval || bush != lastBush)
                    if (HitTest(bush))
                    {
                            
                        new Sound("Branch_bend.wav").Play();
                        
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
                Console.WriteLine("BranchGo.wav");
                new Sound("BranchGo.wav").Play();
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
                    if (!orbit.IsPlaying)
                    {
                        orbit = new Sound("Orbit.wav").Play();
                    }

                        //if (relative.Length() < ownCircle.Radius + incBall.Radius)
                        //{
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
                    new Sound("Coin.wav").Play();
                    myGame.currentLevel++;
                    if(myGame.currentLevel == 8)
                    {
                        
                        myGame.MainMenu();
                    }
                    else myGame.LoadLevel("NEWLEVEL_" + myGame.currentLevel + ".tmx");


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

        private void ResolveCollisions()
        {
           // CollisionInfo collision = MyGame.collisionManager.CapsCollisionsTOI(this.koenCollider);
            //if (collision != null) MyGame.collisionManager.ResolveCollision(this.koenCollider, collision);
            //if (collision.x != 0 && collision.y != 0) velocity.Reflect(collision);
            /*CollisionInfo collision = MyGame.collisionManager.CapsCollisions(this.koenCollider);
            Console.WriteLine(collision);
            if (collision != null) MyGame.collisionManager.ResolveCollision(this.koenCollider, collision);*/
        }

        private void CapCheck()
        {
            CollisionInfo earliestCollision = null;
            for (int i = 0; i < myGame.caps.Count(); i++)
            {
                Cap cap = myGame.GetCap(i);
                    CollisionInfo colCheck = BallCheck(cap);
                    if (colCheck != null) if ((earliestCollision == null || earliestCollision.timeOfImpact > colCheck.timeOfImpact)) earliestCollision = colCheck;
            }
            if (earliestCollision != null)
            {
                Console.WriteLine($"Time of impact: {0}", earliestCollision.timeOfImpact);
                ResolveCollision(earliestCollision);
            }
        }

        CollisionInfo BallCheck(Cap other)
        {

            Vec2 relativePosition = position - other.position;
            if (relativePosition.Length() < radius + other.radius)
            {
                // DONE: compute correct normal and time of impact, and 
                // 		 return *earliest* collision instead of *first detected collision*:

                Vec2 normal = (OldPosition - other.position);
                float a = Mathf.Pow(velocity.Length(), 2);
                float b = 2 * normal.Dot(velocity);
                float c = Mathf.Pow(normal.Length(), 2) - Mathf.Pow(radius + other.radius, 2);
                Console.WriteLine($"A: {a}");
                Console.WriteLine($"B: {b}");
                Console.WriteLine($"C: {c}");
                //float d = Mathf.Pow(b, 2) - (4 * a * c);
                if (c < 0)
                {
                    if (b < 0) return new CollisionInfo(normal, other, 0);
                    else return null;
                }
                if (a == 0) return null;
                float d = Mathf.Pow(b, 2) - (4 * a * c);
                if (d < 0) return null;
                float toi = (-b - Mathf.Sqrt(d)) / (2 * a);
                if (0 <= toi && toi < 1) return new CollisionInfo(normal, other, toi);
                else return null;
            }
            return null;
        }

        public void ResolveCollision(CollisionInfo col)
        {
            // DONE: resolve the collision correctly: position reset & velocity reflection.
            if (col.other is Cap)
            {
                Cap otherBall = (Cap)col.other;
                Vec2 poi = OldPosition + col.timeOfImpact * velocity;
                position = poi;
                velocity.Reflect(col.normal.Normal());
                velocity *= -1;

            }

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

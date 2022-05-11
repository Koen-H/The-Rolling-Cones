using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Objects.AnimationObjects;
using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Coolgrath
{
    public class Geyser : AnimationSprite
    {
        public float strength { get; set; }

        public float animationIntervalTime;
        public int currentFrame = 0;
        public bool doAnimate;
        private AnimationSprite animation = new AnimationSprite("Geyser_Animated.png", 8, 1, -1, false,false);

        private Vec2 position;
        public Vec2 Position
        {
            set
            {
                this.position = value;
                this.x = value.x;
                this.y = value.y;
            }
            get
            {
                return this.position;
            }
        }

        public Geyser(float _strength, Vec2 position, string filename, int cols, int rows, int frames = -1) : base(filename, cols, rows, frames)
        {
            Position = position;
            SetOrigin(width/2,height/2);
            strength = _strength;
            MyGame myGame = (MyGame)Game.main;
            myGame.geysers.Add(this);
            animation.SetOrigin(animation.width/2, height*2.7f);
            this.AddChild(animation);
            /*this.TopSide.MyCollider.trigger = true;
            this.TopSide.StartCap.trigger = true;
            this.RightSide.MyCollider.trigger = true;
            this.RightSide.StartCap.trigger = true;
            this.BottomSide.MyCollider.trigger = true;
            this.BottomSide.StartCap.trigger = true;
            this.LeftSide.MyCollider.trigger = true;
            this.LeftSide.StartCap.trigger = true;*/
           // foreach (Collider collider in this.ChildColliders)
           // {
           //     collider.trigger = true;
           // }
            //Temporary
            //SetScaleXY(1,3);
        }

       /* public Geyser(float _strength, Vec2 position, string filename, int width, int height, int cols, int rows, int frames = -1) : base(filename, cols, rows, frames)
        {
            Position = position;
            SetOrigin(width / 2, height / 2);
            strength = _strength;
            MyGame myGame = (MyGame)Game.main;
            myGame.geysers.Add(this);
            animation.SetOrigin(animation.width, animation.height);
            this.AddChild(animation);
           
            // this.TopSide.MyCollider.trigger = true;
            /*this.TopSide.StartCap.trigger = true;
            this.RightSide.MyCollider.trigger = true;
            this.RightSide.StartCap.trigger = true;
            this.BottomSide.MyCollider.trigger = true;
            this.BottomSide.StartCap.trigger = true;
            this.LeftSide.MyCollider.trigger = true;
            this.LeftSide.StartCap.trigger = true;*/
            //foreach (Collider collider in this.ChildColliders)
            // {
            //     collider.trigger = true;
            // }
            //Temporary
            //SetScaleXY(1,3);
        //}*/
        
        public new void Update()
        {
            if (doAnimate) DoAnimate();
        }

        public void DoAnimate()
        {
            if(Time.time > animationIntervalTime)
            {
                animation.Animate();
                currentFrame++;
                animationIntervalTime = Time.time + 125;
                if (currentFrame == 1) new Sound("Geyser.wav").Play();
            }
            

            if (currentFrame == 8)
            {
                doAnimate = false;
                currentFrame = 0;
            }
        }
    }
}

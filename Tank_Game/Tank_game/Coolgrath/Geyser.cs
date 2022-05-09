using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Objects.AnimationObjects;
using GXPEngine.PhysicsEngine.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Coolgrath
{
    public class Geyser : Rectangle
    {
        public float strength { get; set; }

        public Geyser(float _strength, Vec2 position, string filename, int cols, int rows, int frames = -1) : base(position, filename, cols, rows, frames)
        {
            strength = _strength;
            MyGame myGame = (MyGame)Game.main;
            //myGame.geysers.Add(this);
            /*this.TopSide.MyCollider.trigger = true;
            this.TopSide.StartCap.trigger = true;
            this.RightSide.MyCollider.trigger = true;
            this.RightSide.StartCap.trigger = true;
            this.BottomSide.MyCollider.trigger = true;
            this.BottomSide.StartCap.trigger = true;
            this.LeftSide.MyCollider.trigger = true;
            this.LeftSide.StartCap.trigger = true;*/
            foreach (Collider collider in this.ChildColliders)
            {
                Console.WriteLine("test");
                collider.trigger = true;
            }
            //Temporary
            //SetScaleXY(1,3);
        }

        public Geyser(float _strength, Vec2 position, string filename, int width, int height, int cols, int rows) : base(position, filename, width, height, cols, rows)
        {
            strength = _strength;
            MyGame myGame = (MyGame)Game.main;
            //myGame.geysers.Add(this);
            /*this.TopSide.MyCollider.trigger = true;
            this.TopSide.StartCap.trigger = true;
            this.RightSide.MyCollider.trigger = true;
            this.RightSide.StartCap.trigger = true;
            this.BottomSide.MyCollider.trigger = true;
            this.BottomSide.StartCap.trigger = true;
            this.LeftSide.MyCollider.trigger = true;
            this.LeftSide.StartCap.trigger = true;*/
            foreach (Collider collider in this.ChildColliders)
            {
                Console.WriteLine("test");
                collider.trigger = true;
            }
            //Temporary
            //SetScaleXY(1,3);
        }

    }
}

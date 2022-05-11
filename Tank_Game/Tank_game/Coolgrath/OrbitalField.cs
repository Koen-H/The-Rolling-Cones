using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.Golgrath.Objects;
using GXPEngine;

namespace GXPEngine.Coolgrath
{
    public class OrbitalField : AnimationSprite
    {
        public float PullStrength { get; set; }
        public Vec2 position;
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
        public float radius;

        public OrbitalField(float pPullStrength, int pRadius, Vec2 pPosition) : base("Magnet.png", 8, 1, -1, true)
        {
            Position = pPosition;
            //collider = new Circle(pRadius * 2 + 1, pPosition, "BallTest.png", 1, 1, true);
            //this.AddChild(collider);
            PullStrength = pPullStrength;
            SetXY(pPosition.x, pPosition.y);
            radius = pRadius;
            MyGame myGame = (MyGame)Game.main;
            myGame.fields.Add(this);
            SetOrigin(width/2,height/2);

        }

        public new void Update()
        {
            this.Animate(0.1f);
            //this.rotation += 0.3f;
        }

        /* public class OrbitalField : CanvasCircle
         {
             public float PullStrength {get;set;}
             public Circle collider;

             public OrbitalField(float pPullStrength, int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pPosition)
             {
                 //collider = new Circle(pRadius * 2 + 1, pPosition, "BallTest.png", 1, 1, true);
                 //this.AddChild(collider);
                 PullStrength = pPullStrength;
                 Draw(0, 0, 180);
                 SetXY(pPosition.x,pPosition.y);
                 MyGame myGame = (MyGame)Game.main;
                 myGame.fields.Add(this);

             }
             public new void Update()
             {
                 //collider.Position = this.position;
                 //collider.MyCollider.Position = this.position;
                 myCollider.Position = this.position;
             }
             void Draw(byte red, byte green, byte blue)
             {
                 //Fill(red, green, blue);
                 //Stroke(red, green, blue);
                 //Ellipse(radius, radius, 2 * radius, 2 * radius);
             }
         }
         */
    }
}

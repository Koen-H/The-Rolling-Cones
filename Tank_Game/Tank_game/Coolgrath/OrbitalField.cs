using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.Golgrath.Objects;
using GXPEngine;

namespace GXPEngine.Coolgrath
{
    public class OrbitalField : CanvasCircle
    {
        public float PullStrength {get;set;}
        public Circle collider;

        public OrbitalField(float pPullStrength, int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pPosition)
        {
            collider = new Circle(pRadius * 2 + 1, pPosition, "BallTest.png", 1, 1, true);
            this.AddChild(collider);
            PullStrength = pPullStrength;
            Draw(0, 0, 180);
            SetXY(pPosition.x,pPosition.y);
            MyGame myGame = (MyGame)Game.main;
            myGame.fields.Add(this);

        }

        void Draw(byte red, byte green, byte blue)
        {
            //Fill(red, green, blue);
            //Stroke(red, green, blue);
            //Ellipse(radius, radius, 2 * radius, 2 * radius);
        }
    }
}

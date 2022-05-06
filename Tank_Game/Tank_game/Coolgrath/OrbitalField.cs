using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.Golgrath.Objects;
using GXPEngine;

namespace GXPEngine.Coolgrath
{
    class OrbitalField : CanvasCircle
    {
        public float PullStrength {get;set;}
        public OrbitalField(float pPullStrength, int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pPosition)
        {
            PullStrength = pPullStrength;
            Draw(0, 0, 180);
            SetXY(pPosition.x,pPosition.y);
        }

        void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
        }
    }
}

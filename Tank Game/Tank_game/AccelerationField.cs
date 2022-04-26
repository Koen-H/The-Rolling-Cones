using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using Physics;

namespace GXPEngine
{
    //This is just an object.
    public class AccelerationField : ColliderObject
    {
        public AccelerationField(int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1)
        {
            radius = pRadius;
            _position = pPosition;
            SetOrigin(radius, radius);
            Draw(0,0,180);
        }

        void Draw(byte red, byte green, byte blue)
        {
            Fill(red, green, blue);
            Stroke(red, green, blue);
            Ellipse(radius, radius, 2 * radius, 2 * radius);
        }
    }
}

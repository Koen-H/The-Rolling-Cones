using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics;

namespace GXPEngine
{
    public class Caps : ColliderObject
    {
        //Caps are placed on the end of lines to make them collidable.
        public Caps(Vec2 pPosition, int pRadius = 0) : base(pRadius* 2 + 1, pRadius* 2 + 1)
        {
            _position = pPosition;
            radius = pRadius;
        }
    }
}

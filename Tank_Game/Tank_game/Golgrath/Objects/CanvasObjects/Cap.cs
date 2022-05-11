using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Objects.CanvasObjects
{
    public class Cap : EasyDraw
    {
        // These four public static fields are changed from MyGame, based on key input (see Console):
        public static bool drawDebugLine = false;
        public static bool wordy = false;
        public static float bounciness = 0.98f;
        // For ease of testing / changing, we assume every ball has the same acceleration (gravity):
        public static Vec2 acceleration = new Vec2(0, 0);


        public Vec2 velocity;
        public Vec2 position;

        public readonly int radius;
        public readonly bool moving;

        // Mass = density * volume.
        // In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
        public float Mass
        {
            get
            {
                return radius * radius * _density;
            }
        }

        Vec2 _oldPosition;
        //Arrow _velocityIndicator;

        float _density = 1;

        public Cap(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = false) : base(pRadius * 2 + 1, pRadius * 2 + 1)
        {
            radius = pRadius;
            position = pPosition;
            velocity = pVelocity;
            this.moving = moving;

            position = pPosition;
           // UpdateScreenPosition();
            SetOrigin(radius, radius);

            //Draw(230, 200, 0);

            //_velocityIndicator = new Arrow(position, new Vec2(0, 0), 10);
            //AddChild(_velocityIndicator);
        }
    }
}

using System;
using GXPEngine;
using Physics;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Golgrath.Objects;
using GXPEngine.PhysicsEngine;

public class MyGame : Game
{
    /*List<Tank> tanks;
    List<LineSegment> lines;
    List<Caps> caps;
    List<AccelerationField> accelerationFields;
    public Tank playerTank;*/

    public bool drawDebugLine;
    Canvas lineContainer = null;
    public static MyCollisionManager collisionManager;

    /*static void DoTests()
    {
        //Start Week 1
        // - Operator
        Vec2 v = new Vec2(3, 8);
        v = v - new Vec2(2, 3);
        Console.WriteLine("Test - Operator: {0}, Expected: 1,5", v.ToString());

        // * Operator
        v = new Vec2(-6, 8);
        v = v * 3;
        Console.WriteLine("Test * Operator: {0}, Expected: 30", v.Length());

        //Normalize
        v = new Vec2(3, 4);
        v.Normalize();
        Console.WriteLine("Test Normalize: {0}, Expected: 0.6, 0.8", v.ToString());

        //Normalized
        v = new Vec2(3, 4);
        Vec2 w = v.Normalized();
        Console.WriteLine("Test Normalized: {0}, Expected: 0.6, 0.8. Original: {1} (should be 3,4) ", w, v);

        //Length
        v = new Vec2(-6, 8);
        Console.WriteLine("Test Length: {0}, Expected: 10", v.Length());

        //SetXY
        v = new Vec2(-6, 8);
        v.SetXY(3, 5);
        Console.WriteLine("Test SetXY: {0}, Expected: 3,5", v.ToString());


        //Start Week 2

        //Deg to Rad
        float deg = 180;
        Console.WriteLine("Test Deg2Rad: {0}, expected: 3.141593f", Vec2.Deg2Rad(deg));

        //Rad to Deg
        float rad = 3.141593f;
        Console.WriteLine("Test Rad2Deg: {0}, expected: 180", Vec2.Rad2Deg(rad));

        //Get Random Vector
        for (int i = 0; i < 5; i++)
        {
            Vec2 r = Vec2.RandomUnitVector();
            Console.WriteLine("Random unit vector: {0} length: {1}", r, r.Length());
        }

        //Get and set Angle Degrees
        Vec2 vecOne = new Vec2(13, 12);
        Console.WriteLine("Test Set angle and GetAngle Degrees: Vector's length and degree was: {0} and {1}", vecOne.Length(), vecOne.GetAngleDegrees());
        vecOne.SetAngleDegrees(90);
        Console.WriteLine("And after SetAngle(90), Vector's length and degree: {0} and {1} . expected same length, degrees 90", vecOne.Length(), vecOne.GetAngleDegrees());

        *//*//Get and set Angle
        Vec2 vecOne = new Vec2(13, 12);
        Console.WriteLine("Test Set angle and GetAngle Radians: Vector's length and degree was: {0} and {1}", vecOne.Length(), vecOne.GetAngleDegrees());
        vecOne.SetAngleDegrees(90);
        Console.WriteLine("And after SetAngle(90), Vector's length and degree: {0} and {1} . expected same length, degrees 90", vecOne.Length(), vecOne.GetAngleDegrees());*//*

        //Rotate
        vecOne = new Vec2(-4, -1);
        vecOne.RotateDegrees(90);
        Console.WriteLine("Test RotateDegrees: {0}, expected: 1,-4", vecOne);
        vecOne.RotateRadians(Vec2.Deg2Rad(-90));
        Console.WriteLine("Test RotateRadians: {0}, expected: -4,-1", vecOne);

        //Rotate Around a point
        vecOne = new Vec2(-4, -1);
        Vec2 vecRot = new Vec2(2, 1);
        vecOne.RotateAroundDegrees(vecRot, 90);
        Console.WriteLine("Test RotateAroundDegrees: {0}, expected: 4,-5", vecOne);
        vecOne.RotateAroundRadians(vecRot, Vec2.Deg2Rad(-90));
        Console.WriteLine("Test RotateAroundRadians: {0}, expected: -4, -1", vecOne);


        //Start week 4

        //normal
        Vec2 normalVec = new Vec2(6, -8);
        normalVec = normalVec.Normal();
        Console.WriteLine("Test Normal vector: {0}, expected: 0.8, 0.6", normalVec);

        //Dot product
        Vec2 pos1 = new Vec2(5, 12);
        Vec2 pos2 = new Vec2(3, 6);
        Console.WriteLine("Test dot product: {0}, expected: 87", pos1.Dot(pos2));

        //test for Vec2.Reflect() on straight and rotated lines;
        Vec2 pointA = new Vec2(1, 1);
        Vec2 pointB = new Vec2(5, 5);
        Vec2 lineLength = pointA - pointB;
        Vec2 vecNormal = lineLength.Normal();
        vecNormal.Normalize();
        Vec2 beforeReflect = new Vec2(5, 6);
        Vec2 afterReflect = beforeReflect;
        afterReflect.Reflect(vecNormal);
        Console.WriteLine("Reflect test: Before: {0}  After: {1}    Expected: 5,6 and 6,5 ", beforeReflect, afterReflect); //On a straight line
        Vec2 pointC = new Vec2(3, 1);
        Vec2 pointD = new Vec2(6, 9);
        Vec2 lineLength2 = pointC - pointD;
        Vec2 vecNormal2 = lineLength2.Normal();
        vecNormal2.Normalize();
        afterReflect.Reflect(vecNormal2);
        Console.WriteLine("Reflect test: Before: {0}  After: {1}    Expected: 6,5 and -1.232877,7.712329 ", beforeReflect, afterReflect); //On different line


        //Extra functionality
        Vec2 point1 = new Vec2(10, 12);
        Vec2 point2 = new Vec2(1, 4);
        Console.WriteLine("Distance between two vectors, {0} Expected: 12,04159", point1.Distance(point2));
    }*/

    public MyGame() : base(800, 1080, false,false)
	{
        collisionManager = new MyCollisionManager();

        PlayerBall ball = new PlayerBall(30, new Vec2(400, 500), new Vec2(0, 0.5F), new Vec2(0, 0));
        this.AddChild(ball);
        Line lineBottom = new Line(new Vec2(200, 1000), new Vec2(600, 1000));
        Line lineLeft1 = new Line(new Vec2(200, 1000), new Vec2(25, 550));
        Line lineLeft2 = new Line(new Vec2(25, 550), new Vec2(200, 100));
        Line lineRight1 = new Line(new Vec2(600, 1000), new Vec2(775, 550));
        Line lineRight2 = new Line(new Vec2(775, 550), new Vec2(600, 100));
        Line lineTop = new Line(new Vec2(200, 100), new Vec2(600, 100));
        this.AddChild(lineBottom);
        this.AddChild(lineLeft1);
        this.AddChild(lineLeft2);
        this.AddChild(lineRight1);
        this.AddChild(lineRight2);
        this.AddChild(lineTop);
        /*lineContainer = new Canvas(width, height);
        
        //AccelerationField()
        accelerationFields = new List<AccelerationField>();
        AddAccelerationField(50, new Vec2(460, 450));
        AddAccelerationField(32, new Vec2(180, 480));
        AddAccelerationField(24, new Vec2(520, 350));

        //Add Tanks
        tanks = new List<Tank>();
        playerTank = new PlayerTank(new Vec2(250, 250));
        tanks.Add(playerTank);
        AddChild(playerTank);

        EnemyTank enemyTank = new EnemyTank(new Vec2(500, 125));
        tanks.Add(enemyTank);
        AddChild(enemyTank);

        lines = new List<LineSegment>();
        caps = new List<Caps>();
        //Border aroudn the map
        AddLine(new Vec2(50, 550), new Vec2(50,50), false);
        AddLine(new Vec2(50, 50), new Vec2(750, 50),  false);
        AddLine(new Vec2(750, 50), new Vec2(750, 550), false);
        AddLine( new Vec2(750, 550), new Vec2(50, 550), false);

        // Obstacles/lines in the map.
        AddLine(new Vec2(460,450), new Vec2(350,430));
        AddLine(new Vec2(350, 430), new Vec2(420, 220));
        AddLine(new Vec2(420, 220), new Vec2(250, 120));


        //Add the linecontainer as last
        AddChild(lineContainer);*/
    }

	static void Main()
	{
        //DoTests();//Unit tests, mainly copied from previous assignments
		new MyGame().Start();
	}

    void Update()
    {
        if (Input.GetKeyDown(Key.D)) drawDebugLine ^= true;
        if (Input.GetKeyDown(Key.C)) lineContainer.graphics.Clear(Color.Black);

        this.HandleInput();
        targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;//Lower the framerate.
    }

    /*public int GetNumberOfTanks()
    {
        return tanks.Count;
    }
    public Tank GetTank(int index)
    {
        if (index >= 0 && index < tanks.Count)
        {
            return tanks[index];
        }
        return null;
    }
    public void RemoveTank(Tank tank)
    {
        tanks.Remove(tank);//Remove the tank from the list
        tank.Destroy();//And the game.
    }*/

    /*void AddLine(Vec2 start, Vec2 end, Boolean dualSided = true)//Add a line with caps
    {
        LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
        Caps startCap = new Caps(start);
        Caps endCap = new Caps(end);
        caps.Add(startCap);
        caps.Add(endCap);
        line.AddChild(startCap);
        line.AddChild(endCap);
        AddChild(line);
        lines.Add(line);
        if (dualSided)//If true, there should be a second line, but with the opposite normal. 
        {
            LineSegment line2 = new LineSegment(end, start, 0xff00ff00, 4);
            AddChild(line2);
            lines.Add(line2);
            //This doesn't need any caps as they share the same coordinates as the one above.
        }
    }*/
    /*public int GetNumberOfLines()
    {
        return lines.Count;
    }*/
    /*public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < lines.Count)
        {
            return lines[index];
        }
        return null;
    }
    public int GetNumberOfCaps()
    {
        return caps.Count;
    }
    public Caps GetCap(int index)
    {
        if (index >= 0 && index < caps.Count)
        {
            return caps[index];
        }
        return null;
    }*/

    /*void AddAccelerationField(int radius, Vec2 position)
    {
        AccelerationField accelerationField = new AccelerationField(radius,position);
        accelerationFields.Add(accelerationField);
        AddChild(accelerationField);
    }
    public int GetNumberOfAccelerationFields()
    {
        return accelerationFields.Count;
    }
    public AccelerationField GetAccelerationField(int index)
    {

        if (index >= 0 && index < accelerationFields.Count)
        {
            return accelerationFields[index];
        }
        return null;
    }*/


    public void DrawLine(Vec2 start, Vec2 end)
    {
        lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
    }
    public static bool Approximate(Vec2 a, Vec2 b, float errorMargin = 0.01f)
    {
        return Approximate(a.x, b.x, errorMargin) && Approximate(a.y, b.y, errorMargin);
    }

    public static bool Approximate(float a, float b, float errorMargin = 0.01f)
    {
        return Math.Abs(a - b) < errorMargin;
    }

    private void HandleInput()
    {
        this.targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;
    }
}
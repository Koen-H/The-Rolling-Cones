using System;
using GXPEngine;
using Physics;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Coolgrath;
using GXPEngine.PhysicsEngine;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.TiledLoader;

public class MyGame : Game
{
    /*List<Tank> tanks;
    List<LineSegment> lines;
    List<Caps> caps;
    List<AccelerationField> accelerationFields;
    public Tank playerTank;*/

    public List<Geyser> geysers = new List<Geyser>();

    public bool drawDebugLine;
    Canvas lineContainer = null;
    public static MyCollisionManager collisionManager;

    public MyGame() : base(1920, 1080, false,false, 1920, 1080)
	{
        collisionManager = new MyCollisionManager();
        PlayerBall ball = new PlayerBall(30, new Vec2(400, 500), new Vec2(0, 0.5F), new Vec2(0, 0));
        Circle newBall = new Circle(50, new Vec2(0, 0), 100, 100);
        Line lineBottom = new Line(new Vec2(200, 1000), new Vec2(600, 1000));
        /*Line lineLeft1 = new Line(new Vec2(200, 1000), new Vec2(25, 800));
        Line lineLeft2 = new Line(new Vec2(25, 550), new Vec2(200, 100));
        Line lineRight1 = new Line(new Vec2(600, 1000), new Vec2(775, 550));
        Line lineRight2 = new Line(new Vec2(775, 550), new Vec2(600, 100));
        Line lineTop = new Line(new Vec2(200, 100), new Vec2(600, 100));*/
        PlayerCamera playerCamera = new PlayerCamera(0, 0, this.width, this.height);
        this.AddChild(playerCamera);
        this.AddChild(lineBottom);
        /*this.AddChild(lineLeft1);
        this.AddChild(lineLeft2);
        this.AddChild(lineRight1);
        this.AddChild(lineRight2);
        this.AddChild(lineTop);*/
        //Geyser geyserTest = new Geyser(2, new Vec2(220,900), "cyan_block.png",1,1,1);
        //AddChild(geyserTest);
        this.AddChild(ball);
        ball.SetPlayerCamera(playerCamera);
        this.AddChild(newBall);

        Level level = new Level("MapTest.tmx");
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
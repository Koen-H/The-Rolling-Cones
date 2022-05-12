using System;
using GXPEngine;
using Physics;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Coolgrath;
using GXPEngine.PhysicsEngine;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.PhysicsEngine.Colliders;
using GXPEngine.TiledLoader;
using GXPEngine.Golgrath.Objects.CanvasObjects;

public class MyGame : Game
{
    /*List<Tank> tanks;
    List<LineSegment> lines;
    List<Caps> caps;
    List<AccelerationField> accelerationFields;
    public Tank playerTank;*/

    public List<Geyser> geysers = new List<Geyser>();
    public List<OrbitalField> fields = new List<OrbitalField>();
    public List<NextLevelBlock> coins = new List<NextLevelBlock>();
    public List<BushShot> bushes = new List<BushShot>();
    public List<Cap> caps = new List<Cap>();
    public PlayerCamera playerCamera;
    public Pivot objectLayer;
    public bool drawDebugLine;
    public bool shopOpen;
    public bool newGamePlus = false;
    Canvas lineContainer = null;
    public static MyCollisionManager collisionManager;
    public int currentLevel = 0;
    public CanvasPlayerBall player;

    public InteractableEnvironment interactableEnvironmentActive;

    public SoundChannel backgroundMusic = new SoundChannel(2);

    public MyGame() : base(1920, 1080, false, false, 1920, 1080, false)
	{
        collisionManager = new MyCollisionManager();
        playerCamera = new PlayerCamera(0, 0, this.width, this.height);
        playerCamera.SetXY(1920 / 2, 1080 / 2);
        AddChild(playerCamera);
        backgroundMusic = new Sound("background.wav",true,true).Play();
        backgroundMusic.Volume = 1f;

         AddChild(new ShopPopUp(null, new Vec2(0, 0), "mainMenu.png", 1, 1));
        //LoadLevel("NEWLEVEL_5.tmx");
    }

	static void Main()
	{
        //DoTests();//Unit tests, mainly copied from previous assignments
		new MyGame().Start();
	}

    void Update()
    {
        if (Input.GetKeyDown(Key.D)) drawDebugLine ^= true;
       // if (Input.GetKeyDown(Key.C)) lineContainer.graphics.Clear(Color.Black);
        this.HandleInput();
        targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;//Lower the framerate.

        if (Input.GetKeyDown(Key.F1))
        {
            Console.WriteLine(GetDiagnostics());
        }
        //Console.WriteLine( Time.deltaTime);

    }

    public Cap GetCap(int index)
    {
        if (index >= 0 && index < caps.Count)
        {
            return caps[index];
        }
        return null;
    }

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

    public void LoadLevel(string fileName = "NEWLEVEL.tmx")
    {
        
        DestroyAll();
        Level level = new Level(fileName);
        List<GameObject> children = level.GetChildren();
        foreach (GameObject child in children)
        {
            AddChild(child);
        }
        objectLayer = level.objectLayer;

    }
    public void MainMenu()
    {
        currentLevel = 0;
        
        newGamePlus = false;
        DestroyAll();
        AddChild(new ShopPopUp(null, new Vec2(0, 0), "mainMenu.png", 1, 1));
    }
   

    private void DestroyAll()
    {
        geysers = new List<Geyser>();
        fields = new List<OrbitalField>();
        coins = new List<NextLevelBlock>();
        bushes = new List<BushShot>();
        caps = new List<Cap>();
        collisionManager.colliders = new List<Collider>();
        player = null;
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
        playerCamera = new PlayerCamera(0, 0, this.width, this.height);
        playerCamera.SetXY(1920 / 2, 1080 / 2);
        AddChild(playerCamera);

    }
}
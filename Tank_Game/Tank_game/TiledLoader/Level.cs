using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Coolgrath;
using System.Globalization;
using GXPEngine.Golgrath.Cameras;

namespace GXPEngine.TiledLoader
{
    public class Level : GameObject
    {
        private MyGame myGame;

        readonly private string levelFileName;
        private Pivot colliders;
        private Pivot backgroundLayer;
        public Pivot objectLayer;
        private Pivot playerLayer;
        private Pivot frontgroundLayer;

        public Level(string filename)
        {
            myGame = (MyGame)game;
            levelFileName = filename;
            Construct();
        }

        void Construct()
        {
            Map mapData = MapParser.ReadMap(levelFileName);
            //CreateLevel(mapData);
            //Load the level in this order!
            colliders = new Pivot();
            backgroundLayer = new Pivot();
            objectLayer = new Pivot();
            playerLayer = new Pivot();
            frontgroundLayer = new Pivot();

            CreateLevel(mapData);
           // CreateInteractableEnvironments(mapData);

            this.AddChild(colliders);
            this.AddChild(backgroundLayer);
            this.AddChild(objectLayer);
            this.AddChild(playerLayer);
            this.AddChild(frontgroundLayer);
        }

        void CreateLevel(Map map)//This will make the lines/border from the lines layer of the tiled level.
        {
           //var group = map.ObjectGroups[0];

            foreach (var group in map.ObjectGroups)//Object Layers
            {
                if (group.Name.Equals("Lines"))
                {
                    foreach (TiledObject obj in group.Objects)
                    {
                        if (obj.polyline != null)
                        {
                            string data = obj.polyline.points;
                            Console.WriteLine("Found a polyline (" + obj.ID +"): " + data);
                            string[] subs = data.Split(' ', ',');
                            Vec2 objPos = new Vec2(obj.X, obj.Y); //Each point is relative of the parent, therefore it must be applied to each point to get the gamespace coordinates.
                            int coords = subs.Length;
                            for (int i = 0; (coords - 2) != i; i += 2)
                            {
                                Vec2 pos1 = new Vec2(float.Parse(subs[i], CultureInfo.InvariantCulture.NumberFormat), float.Parse(subs[i + 1], CultureInfo.InvariantCulture.NumberFormat)) + objPos;
                                Vec2 pos2 = new Vec2(float.Parse(subs[i + 2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(subs[i + 3], CultureInfo.InvariantCulture.NumberFormat)) + objPos;
                                CanvasLine line = new CanvasLine(pos1, pos2);
                                colliders.AddChild(line);
                            }
                        }
                    }
                }

                if (group.Name.Equals("InteractableEnvironments"))
                {
                    foreach (TiledObject obj in group.Objects)
                    {
                        Console.WriteLine("interactableEnvironment found: " + obj.Width + " and " + obj.Height);
                        Vec2 objPos = new Vec2(obj.X, obj.Y);
                        InteractableEnvironment interactableEnvironment = new InteractableEnvironment(objPos, (int)obj.Width, (int)obj.Height);
                        objectLayer.AddChild(interactableEnvironment);
                    }
                }
                else if (group.Name.Equals("PreDefinedObjects"))
                {
                    foreach (TiledObject obj in group.Objects)
                    {
                        Console.WriteLine("Pre defined object found: " + obj.Width + " and " + obj.Height);
                        String objName = obj.Name;
                        switch (objName)
                        {
                            case "Player":
                                CanvasPlayerBall ball = new CanvasPlayerBall(30, new Vec2(obj.X, obj.Y), new Vec2(0, 0.5F), new Vec2(0, 0));
                                ball.SetPlayerCamera(myGame.playerCamera);
                                playerLayer.AddChild(ball);
                                break;
                            case "Geyser":
                                Geyser geyser = new Geyser(5,new Vec2(obj.X,obj.Y),"cyan_block.png",1,1);
                                objectLayer.AddChild(geyser);
                                geyser.rotation = obj.Rotation;
                                break;
                            case "MagneticField":
                                OrbitalField magneticField = new OrbitalField(0.025F, 44, new Vec2(obj.X, obj.Y));
                                objectLayer.AddChild(magneticField);
                                break;
                            case "Other stuff here":

                                break;
                            default:
                                Console.WriteLine("This predefinedobject doesn't exist: " + objName); //shouldn't happen, but let the game continue anyway
                                break;
                        }
                       
                    }
                }
            }
            foreach(var group in map.ImageLayers)//Image layers
            {
                if (group.Name.Equals("Background"))
                {
                    Sprite backgroundSprite = new Sprite(group.Image.FileName, false, false);
                    backgroundSprite.SetXY(group.offsetX,group.offsetY);
                    backgroundLayer.AddChild(backgroundSprite); //group.Image.FileName
                }
                if (group.Name.Equals("Frontground"))
                {
                    Sprite frontgroundSprite = new Sprite(group.Image.FileName, false, false);
                    frontgroundSprite.SetXY(group.offsetX, group.offsetY);
                    frontgroundLayer.AddChild(frontgroundSprite); //group.Image.FileName
                }
            }
        }

        void CreateInteractableEnvironments(Map map)
        {

        }

        /*void CreateLevel(Map mapData)
        {

            if (mapData.Layers == null || mapData.Layers.Length == 0)
                return;
            Layer mainLayer = mapData.Layers[0];
            short[,] tileArray = mainLayer.GetTileArray();

            //We don't need to have colliders on textures, textures should be a different layer in tiled, and have their own pivot where they can look pretty.
            //If we can get a collision layer/pivot which only exists out of polygon lines, we can easily create colliders.

            for (int row = 0; row < mainLayer.Height; row++)
            {
                for (int col = 0; col < mainLayer.Width; col++)
                {
                    int tileNumber = tileArray[col, row];
                    TileSet tiles = mapData.GetTileSet(tileNumber);

                    AnimationSprite tile = null;
                    switch (tileNumber)
                    {
                        case 0://Woah, this is worthless! (Empty space)
                            tile = null;
                            break;
                        case 1:
                            tile = null;
                            break;
                    }
                    if(tile != null)
                    {
                        tile.SetFrame(tileNumber - tiles.FirstGId);
                        tile.SetXY(col * tile.width, row * tile.height);
                        frontgroundLayer.AddChild(tile);
                    }
                }
            }
        }
        */
    }
}

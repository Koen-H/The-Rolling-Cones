using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
using GXPEngine.Golgrath.Objects;
using System.Globalization;

namespace GXPEngine.TiledLoader
{
    public class Level : GameObject
    {
        private MyGame myGame;

        readonly private string levelFileName;
        private Pivot colliders;
        private Pivot textures;

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
            colliders = new Pivot();
            textures = new Pivot();

            CreateLines(mapData);
        }

        void CreateLines(Map map)//This will make the lines/border from the lines layer of the tiled level.
        {
            var group = map.ObjectGroups[0];
            if (group.Name == "Lines")
            {
                foreach (TiledObject obj in group.Objects)
                {
                    if (obj.polyline != null)
                    {
                        string data = obj.polyline.points;
                        Console.WriteLine("Found a polyline: " + data);
                        string[] subs = data.Split(' ', ',');
                        Vec2 objPos = new Vec2(obj.X,obj.Y); //Each point is relative of the parent, therefore it must be applied to each point to get the gamespace coordinates.
                        int coords = subs.Length;
                        for (int i = 0; (coords - 2) != i; i += 2)
                        {
                            Console.WriteLine(float.Parse(subs[i], CultureInfo.InvariantCulture.NumberFormat));
                            Vec2 pos1 = new Vec2(float.Parse(subs[i], CultureInfo.InvariantCulture.NumberFormat), float.Parse(subs[i + 1], CultureInfo.InvariantCulture.NumberFormat)) + objPos;
                            Vec2 pos2 = new Vec2(float.Parse(subs[i + 2], CultureInfo.InvariantCulture.NumberFormat), float.Parse(subs[i + 3], CultureInfo.InvariantCulture.NumberFormat)) + objPos;
                            Console.WriteLine(" pos1:" + pos1 + " pos2:" + pos2);
                            Console.WriteLine(coords + " and " + "i =" +i);
                            CanvasLine line = new CanvasLine(pos1, pos2 );
                            AddChild(line);
                        }
                    }
                }
            }
        }

        void CreateLevel(Map mapData)
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
                        textures.AddChild(tile);
                    }
                }
            }
            /* for ()//This is for objects, where we get a property linepos 1 and linepos2 and draw a linesegment on it + caps.
                {
                
                }*/

        }


    }
}

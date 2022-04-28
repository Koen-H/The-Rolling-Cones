using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

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

            VerifyMap(mapData);
        }

        void VerifyMap(Map map)
        {
            var group = map.ObjectGroups[0];
            foreach (TiledObject obj in group.Objects)
            {
                if (obj.polyline !=null)
                {
                    string data = obj.polyline.points;
                    Console.WriteLine("Found a polyline: "+data);
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

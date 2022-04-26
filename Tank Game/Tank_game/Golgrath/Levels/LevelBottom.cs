using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Tiles;
using TiledMapParser;
using System.Drawing;

namespace GXPEngine.Golgrath.Levels
{
    public class LevelBottom : Level
    {
        private Map mapData;
        public LevelBottom(): base("LEVEL_BOTTOM", "Assets/FirstMap.tmx")
        {
            Map mapData = MapParser.ReadMap(this.levelToLoad);
            this.mapData = mapData;
            SpawnTiles(mapData);
            SpawnObjects(mapData);

        }

        public void ReadMap()
        {
            this.mapData = MapParser.ReadMap(this.levelToLoad);
        }

        public void LoadMap()
        {
            this.SpawnTiles(this.mapData);
            this.SpawnObjects(this.mapData);
        }

        public void Clear()
        {
            foreach (GameObject child in this.GetChildren())
            {
                child.LateDestroy();
            }
        }

        public void SpawnTiles(Map levelData)
        {
            
            if (levelData.Layers == null || levelData.Layers.Length == 0)
                return;
            Layer mainLayer = levelData.Layers[0];
            short[,] tileArray = mainLayer.GetTileArray();
            for (int row = 0; row < mainLayer.Height; row++)
            {
                for (int col = 0; col < mainLayer.Width; col++)
                {
                    int tileNumber = tileArray[col, row];
                    TileSet tiles = levelData.GetTileSet(tileNumber);
                    if (tileNumber > 0)
                    {
                        CollisionTile tile = new CollisionTile("Assets/" + tiles.Image.FileName, tiles.Columns, tiles.Rows);
                        tile.SetFrame(tileNumber - tiles.FirstGId);
                        tile.SetXY(col * tile.width, row * tile.height);
                        AddChild(tile);
                    }
                }
            }
        }

        public void SpawnObjects(Map levelData)
        {
            if (levelData.ObjectGroups == null || levelData.ObjectGroups.Length == 0)
                return;
            ObjectGroup group = levelData.ObjectGroups[0];
            if (group.Objects == null || group.Objects.Length == 0)
                return;
            foreach (TiledObject tiledObject in group.Objects)
            {
                switch (tiledObject.Type)
                {
                    case "Player":
                        break;
                }
            }
        }

        public override void Construct()
        {
            
        }
    }
}

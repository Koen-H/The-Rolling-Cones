using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Golgrath.Tiles
{
    public class Tile : AnimationSprite
    {
        private string identifier;

        public Tile(string tileAsset, int column, int row) : base(tileAsset, column, row, -1, false, true)
        {
            this.identifier = tileAsset.Split('.')[0];
            this.collider.isTrigger = true;
        }

        public string GetIdentifier()
        {
            return this.identifier;
        }
        public void SetIdentifier(string identifier)
        {
            this.identifier = identifier;
        }
    }
}

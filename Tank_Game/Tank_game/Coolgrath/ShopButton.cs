using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Coolgrath;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.Core;

namespace GXPEngine.Coolgrath
{
    class ShopButton : MyAnimationSprite
    {
        ShopButtonProperty buttonProperty;
        ShopPopUp shopPopUp;
        MyGame myGame;

        public ShopButton(ShopButtonProperty _buttonProperty, ShopPopUp _shopPopUp, Vec2 position, string filename, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(position, filename, columns, rows, frames, keepInCache, addCollider)
        {
            buttonProperty = _buttonProperty;
            shopPopUp = _shopPopUp;
            myGame = (MyGame)Game.main;
        }

        public new void Update()
        {
            Vector2 worldSpaceMousePos = myGame.playerCamera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
            if (HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y) && Input.GetMouseButtonDown(0)) shopPopUp.ClickedButton(buttonProperty);
        }
    }
}

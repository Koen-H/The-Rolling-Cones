using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.Core;
namespace GXPEngine.Coolgrath
{
    public class QuestionShop : MyAnimationSprite
    {
        PlayerCamera mainCamera;
        MyGame myGame;

        public QuestionShop(Vec2 position, string filename, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(position, filename, columns, rows, frames, keepInCache, addCollider)
        {
           myGame = (MyGame)Game.main;
           mainCamera = myGame.playerCamera;
        }

        public new void Update()
        {
            Vector2 worldSpaceMousePos = mainCamera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
            if (HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y) && Input.GetMouseButtonDown(0)) OpenShop();
            
        }

        void OpenShop()
        {
            // Pause game?
            mainCamera.AddChild(new ShopPopUp(this,new Vec2(-game.width / 2, -game.height / 2),"ShopBackground.png",1,1));
            Console.WriteLine("Shop opened");
        }
    }
}

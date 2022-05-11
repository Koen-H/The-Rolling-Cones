using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.Core;
namespace GXPEngine.Coolgrath
{
    public class InteractableEnvironment : CanvasRectangle
    {
        PlayerCamera mainCamera;
        MyGame myGame;

        public GameObject interactableObject;

        public InteractableEnvironment(Vec2 position, int width, int height) : base(position, width, height, "InteractableEnvironment")
        {
           myGame = (MyGame)Game.main;
           mainCamera = myGame.playerCamera;
        }

        public new void Update()
        {
            Vector2 worldSpaceMousePos = mainCamera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
            if (HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y) && Input.GetMouseButtonDown(0) && interactableObject == null) OpenShop();
            else if (HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y)  && interactableObject != null && interactableObject.HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y))
            {
                if (Input.GetMouseButton(0))
                {
                    myGame.player.pausePlayer = true;
                    interactableObject.SetXY(worldSpaceMousePos.x, worldSpaceMousePos.y);
                    if (interactableObject is OrbitalField) {
                        OrbitalField temp = (OrbitalField)interactableObject;
                        temp.Position = new Vec2(worldSpaceMousePos.x,worldSpaceMousePos.y);
                    }
                }else if (Input.GetMouseButton(1) && !myGame.shopOpen) OpenShop();
                else
                {
                    myGame.player.pausePlayer = false;
                }
            }

            
        }

        void OpenShop()
        {
            // Pause game?
            mainCamera.AddChild(new ShopPopUp(this,new Vec2(-game.width / 2, -game.height / 2),"ShopBackground.png",1,1));
            myGame.player.pausePlayer = true;
            myGame.shopOpen = true;
            Console.WriteLine("Shop opened");
        }
    }
}

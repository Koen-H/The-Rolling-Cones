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
        bool isDragging;

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
            else if (HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y) && interactableObject != null && interactableObject.HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y))
            {
                bool dontContinue = false;
                if (interactableObject is BushShot)
                {
                    BushShot temp = (BushShot)interactableObject;
                    if (myGame.player.currentBush != null && temp == myGame.player.currentBush)
                    {
                        dontContinue = true;
                    }
                }
                if (Input.GetMouseButton(0) && !dontContinue)
                {
                    myGame.player.pausePlayer = true;
                    myGame.interactableEnvironmentActive = this;
                    isDragging = true;
                    Dragging(worldSpaceMousePos);
                }
                else if (Input.GetMouseButton(1) && !myGame.shopOpen && !dontContinue) OpenShop();
                else if(!myGame.shopOpen)
                {
                    myGame.player.pausePlayer = false;
                    isDragging = false;
                }
            }
            if (this == myGame.interactableEnvironmentActive && !myGame.shopOpen) {
                if(!HitTestPoint(worldSpaceMousePos.x, worldSpaceMousePos.y))
                {
                    myGame.player.pausePlayer = false;
                    isDragging = false;
                }
                else if(isDragging)
                {
                    Dragging(worldSpaceMousePos);
                }
                
            }

        }
        void Dragging(Vector2 worldSpaceMousePos)
        {
            /*if (interactableObject is Geyser) interactableObject.SetXY(worldSpaceMousePos.x, worldSpaceMousePos.y - 40);
            else*/
                interactableObject.SetXY(worldSpaceMousePos.x, worldSpaceMousePos.y);
            if (interactableObject is OrbitalField)
            {
                OrbitalField temp = (OrbitalField)interactableObject;
                temp.Position = new Vec2(worldSpaceMousePos.x, worldSpaceMousePos.y);
            }
            else if (interactableObject is BushShot)
            {
                BushShot temp = (BushShot)interactableObject;
                temp.Position = new Vec2(worldSpaceMousePos.x, worldSpaceMousePos.y);
            }
        }

        void OpenShop()
        {
            if (!myGame.shopOpen) { 
                myGame.AddChild(new ShopPopUp(this,new Vec2(0, 0), "menuBG.png", 1,1));
                myGame.player.pausePlayer = true;
                myGame.shopOpen = true;
                Console.WriteLine("Shop opened");
            }
        }
    }
}

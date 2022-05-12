using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Golgrath.Objects;
using GXPEngine.Golgrath.Cameras;
using GXPEngine.Core;

namespace GXPEngine.Coolgrath
{
    enum ShopButtonProperty //Each button has a property, for example, a close button has the close property.
    {
        Close,
        CloseGame,
        Geyser,
        OrbitalField,
        BushShot,
        Play,
        PlayNewGamePlus,
    }

    class ShopPopUp : MyAnimationSprite
    {
        MyGame myGame;
        InteractableEnvironment questionShopObject;
        public ShopPopUp(InteractableEnvironment _questionShopObject, Vec2 position, string filename, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = false) : base(position, filename, columns, rows, frames, keepInCache, addCollider)
        {
            questionShopObject = _questionShopObject;
            myGame = (MyGame)Game.main;

            if (myGame.currentLevel != 0) AddChild(new ShopButton(ShopButtonProperty.Close, this, new Vec2(835, 600), "CloseButton.png", 1, 1));
            if (myGame.newGamePlus || (myGame.currentLevel != 6 && myGame.currentLevel != 0)) AddChild(new ShopButton(ShopButtonProperty.Geyser, this, new Vec2(440, 300), "GeyserButton.png", 1, 1));
            if (myGame.newGamePlus || myGame.currentLevel == 4 || myGame.currentLevel == 5 || myGame.currentLevel == 7) AddChild(new ShopButton(ShopButtonProperty.OrbitalField, this, new Vec2(835, 300), "OrbitalField.png", 1, 1));
            if (myGame.newGamePlus || myGame.currentLevel == 6 || myGame.currentLevel == 7) AddChild(new ShopButton(ShopButtonProperty.BushShot, this, new Vec2(1224, 300), "BushShotButton.png", 1, 1));

            if(myGame.currentLevel == 0)
            {
                AddChild(new ShopButton(ShopButtonProperty.Play, this, new Vec2(580, 350), "PlayButton.png", 1, 1));
                AddChild(new ShopButton(ShopButtonProperty.PlayNewGamePlus, this, new Vec2(1095, 350), "PlayButtonPlus.png", 1, 1));
                AddChild(new ShopButton(ShopButtonProperty.CloseGame, this, new Vec2(835, 650), "CloseButton.png", 1, 1));
            }
        }

        public void ClickedButton(ShopButtonProperty buttonProperty)
        {
            new Sound("button.wav").Play();
            
            
            switch (buttonProperty)
            {
                case ShopButtonProperty.Close:
                    {
                        Console.WriteLine("Close sellected");
                        KillShop();
                        break;
                    }

                case ShopButtonProperty.Geyser:
                    {
                        DeleteObject();
                        Console.WriteLine("Geyser sellected and placed!");
                        Vec2 objectPos = new Vec2(questionShopObject.Position.x + (questionShopObject.width/2), questionShopObject.Position.y + (questionShopObject.height / 2));
                        questionShopObject.interactableObject = new Geyser(5, objectPos, "cyan_block.png", 8, 1);
                        myGame.objectLayer.AddChild(questionShopObject.interactableObject);
                        KillShop();
                        break;
                    }

                case ShopButtonProperty.OrbitalField:
                    {
                        DeleteObject();
                        Console.WriteLine("OrbitalField sellected and placed!");
                        Vec2 objectPos = new Vec2(questionShopObject.Position.x + (questionShopObject.width / 2), questionShopObject.Position.y + (questionShopObject.height / 2));
                        questionShopObject.interactableObject = new OrbitalField(0.025F, 40, objectPos);
                        myGame.objectLayer.AddChild(questionShopObject.interactableObject);
                        RemoveCoins(10);
                        KillShop();
                        break;
                    }
                case ShopButtonProperty.BushShot:
                    {
                        DeleteObject();
                        Console.WriteLine("BushShot sellected and placed!");
                        Vec2 objectPos = new Vec2(questionShopObject.Position.x + (questionShopObject.width / 2), questionShopObject.Position.y + (questionShopObject.height / 2));
                        questionShopObject.interactableObject = new BushShot(objectPos);
                        myGame.objectLayer.AddChild(questionShopObject.interactableObject);
                        RemoveCoins(10);
                        KillShop();
                        break;
                    }
                case ShopButtonProperty.Play:
                    {
                        myGame.LoadLevel();//load the first template, should be on by default.
                        myGame.currentLevel = 1;
                        myGame.shopOpen = false;
                        this.Destroy(); ;
                        break;
                    }
                case ShopButtonProperty.PlayNewGamePlus:
                    {
                        myGame.LoadLevel();//load the first template, should be on by default.
                        myGame.currentLevel = 1;
                        myGame.newGamePlus = true;
                        myGame.shopOpen = false;
                        this.Destroy();
                        break;
                    }
                case ShopButtonProperty.CloseGame:
                    {
                        myGame.Destroy();
                        break;
                    }

                default:
                    {
                        Console.WriteLine("This button hasn't been setup yet, " + buttonProperty);
                        KillShop();
                        break;
                    }

                    
            }
        }

        public void DeleteObject()
        {
            if(questionShopObject.interactableObject != null)
            {
                Console.WriteLine("Deleted!");
                questionShopObject.interactableObject.Destroy();
                if (questionShopObject.interactableObject is Geyser) myGame.geysers.Remove((Geyser)questionShopObject.interactableObject);
                else if (questionShopObject.interactableObject is OrbitalField) myGame.fields.Remove((OrbitalField)questionShopObject.interactableObject);
                else if (questionShopObject.interactableObject is BushShot)
                {
                    BushShot temp = (BushShot)questionShopObject.interactableObject;
                    temp.leaves.Destroy();
                    myGame.bushes.Remove((BushShot)questionShopObject.interactableObject);
                }
            }
        }

        public void RemoveCoins(int coins)
        {
            //remove coins here
        }
        public void KillShop()
        {
            //questionShopObject.Destroy();
            myGame.player.pausePlayer = false;
            myGame.shopOpen = false;
            this.Destroy();
            
        }
    }

}

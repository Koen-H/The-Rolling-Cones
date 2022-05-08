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
        Geyser,
        OrbitalField,
    }

    class ShopPopUp : MyAnimationSprite
    {
        MyGame myGame;
        QuestionShop questionShopObject;
        public ShopPopUp(QuestionShop _questionShopObject, Vec2 position, string filename, int columns, int rows, int frames = -1, bool keepInCache = false, bool addCollider = false) : base(position, filename, columns, rows, frames, keepInCache, addCollider)
        {
            questionShopObject = _questionShopObject;
            myGame = (MyGame)Game.main;

            AddChild(new ShopButton(ShopButtonProperty.Close, this, new Vec2(0, 0), "CloseButton.png", 1, 1));
            AddChild(new ShopButton(ShopButtonProperty.Geyser, this, new Vec2(500, 500), "GeyserButton.png", 1, 1));
            AddChild(new ShopButton(ShopButtonProperty.OrbitalField, this, new Vec2(150, 500), "OrbitalField.png", 1, 1));

        }

        public void ClickedButton(ShopButtonProperty buttonProperty)
        {
            switch (buttonProperty)
            {
                case ShopButtonProperty.Close:
                    {
                        Console.WriteLine("Close sellected");
                        this.Destroy();
                        break;
                    }
                case ShopButtonProperty.Geyser:
                    {

                        //if(coins >= enough){

                        Console.WriteLine("Geyser sellected and placed!");
                        Vec2 objectPos = new Vec2(questionShopObject.Position.x + (questionShopObject.width/2), questionShopObject.Position.y + (questionShopObject.height / 2));
                        myGame.AddChild(new Geyser(10, objectPos, 1,1));
                        RemoveCoins(10);
                        KillShop();
                        //} else {  YOU ARE BROKE LOL 
                        //Console.WriteLine("Geyser sellected but not placed!"); }
                        break;
                    }
                case ShopButtonProperty.OrbitalField:
                    {

                        //if(coins >= enough){

                        Console.WriteLine("OrbitalField sellected and placed!");
                        Vec2 objectPos = new Vec2(questionShopObject.Position.x + (questionShopObject.width / 2), questionShopObject.Position.y + (questionShopObject.height / 2));
                        myGame.AddChild(new OrbitalField(0.025F, 40, objectPos));
                        RemoveCoins(10);
                        KillShop();
                        //} else {  YOU ARE BROKE LOL 
                        //Console.WriteLine("Geyser sellected but not placed!"); }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("This button hasn't been setup yet, " + buttonProperty);

                        this.Destroy();
                        break;
                    }

                    
            }
        }

        public void RemoveCoins(int coins)
        {
            //remove coins here
        }
        public void KillShop()
        {
            questionShopObject.Destroy();
            this.Destroy();
            
        }
    }

}

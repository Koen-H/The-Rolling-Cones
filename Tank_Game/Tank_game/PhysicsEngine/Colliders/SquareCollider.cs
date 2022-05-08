using GXPEngine.Golgrath.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics;
using GXPEngine.Golgrath.Objects.AnimationObjects;

namespace GXPEngine.PhysicsEngine.Colliders
{
	public class SquareCollider : Collider
	{
		public SquareCollider(CanvasRectangle rect) : base(rect)
		{

		}
		public SquareCollider(Rectangle rect) : base(rect)
		{

		}

		//Handle Circle to Circle collision.
		public override CollisionInfo Collision(Collider collideWith)
		{
			CollisionInfo info = null;
			if (collideWith != this)
			{
				if (collideWith.Owner is CanvasSquare && this.Owner is CanvasRectangle)
				{
					CanvasRectangle rectMe = (CanvasRectangle)this.Owner;
					CanvasSquare rectOt = (CanvasSquare)collideWith.Owner;

					Vec2 tlcMe = new Vec2(rectMe.x, rectMe.y);
					Vec2 trcMe = new Vec2(rectMe.x + rectMe.width, rectMe.y);
					Vec2 blcMe = new Vec2(rectMe.x, rectMe.y + rectMe.height);
					Vec2 brcMe = new Vec2(rectMe.x + rectMe.width, rectMe.y + rectMe.height);

					Vec2 tlcOt = new Vec2(rectOt.x, rectOt.y);
					Vec2 trcOt = new Vec2(rectOt.x + rectOt.width, rectOt.y);
					Vec2 blcOt = new Vec2(rectOt.x, rectOt.y + rectOt.height);
					Vec2 brcOt = new Vec2(rectOt.x + rectOt.width, rectOt.y + rectOt.height);
					//Collision Right
					if (((trcOt.x >= tlcMe.x && trcOt.x <= trcMe.x) && (brcOt.x >= blcMe.x && brcOt.x <= brcMe.x)) && this.SpecificYCalculation(rectOt.width, rectMe.width, trcOt, brcOt, tlcMe, blcMe))
					{
						float toi = this.CalculateTimeOfImpact("Right", rectMe, rectOt);
						//Console.WriteLine("TOI Right = " + toi);
						//Console.WriteLine("Right collision");
						if (toi >= 0.0F && toi <= 1.0F)
						{
							info = new CollisionInfo(new Vec2(-1.0F, 0), rectOt, toi);
						}

					}
					//Collision Left
					if (((tlcOt.x <= trcMe.x && tlcOt.x >= tlcMe.x) && (blcOt.x <= brcMe.x && blcOt.x >= blcMe.x)) && this.SpecificYCalculation(rectOt.width, rectMe.width, trcOt, brcOt, tlcMe, blcMe))
					{
						float toi = this.CalculateTimeOfImpact("Left", rectMe, rectOt);
						//Console.WriteLine("TOI Left = " + toi);
						//Console.WriteLine("Left collision");
						if (toi >= 0.0F && toi <= 1.0F)
						{
							if (info != null)
							{
								float otherToI = info.timeOfImpact;
								if (toi < otherToI)
								{
									info = new CollisionInfo(new Vec2(1.0F, 0), rectOt, toi);
								}
							}
							else
							{
								info = new CollisionInfo(new Vec2(1.0F, 0), rectOt, toi);
							}
						}
					}
					//Collision Top
					if (((tlcOt.y <= blcMe.y && tlcOt.y >= tlcMe.y) && (trcOt.y <= brcMe.y && trcOt.y >= trcMe.y)) && this.SpecificXCalculation(rectOt.height, rectMe.height, trcOt, tlcOt, brcMe, blcMe))
					{
						float toi = this.CalculateTimeOfImpact("Top", rectMe, rectOt);
						//Console.WriteLine("TOI Top = " + toi);
						//Console.WriteLine("Top collision");
						if (toi >= 0.0F && toi <= 1.0F)
						{
							if (info != null)
							{
								float otherToI = info.timeOfImpact;
								if (toi < otherToI)
								{
									info = new CollisionInfo(new Vec2(0.0F, 1.0F), rectOt, toi);
								}
							}
							else
							{
								info = new CollisionInfo(new Vec2(0.0F, 1.0F), rectOt, toi);
							}
						}
					}
					//Collision Bottom
					if (((blcOt.y >= tlcMe.y && blcOt.y <= blcMe.y) && (brcOt.y >= trcMe.y && brcOt.y <= brcMe.y)) && this.SpecificXCalculation(rectMe.height, rectOt.height, trcMe, tlcMe, brcOt, blcOt))
					{
						float toi = this.CalculateTimeOfImpact("Bottom", rectMe, rectOt);
						//Console.WriteLine("TOI Bottom = " + toi);
						//Console.WriteLine("Bottom collision");
						if (toi >= 0.0F && toi <= 1.0F)
						{
							if (info != null)
							{
								float otherToI = info.timeOfImpact;
								if (toi < otherToI)
								{
									info = new CollisionInfo(new Vec2(0.0F, -1.0F), rectOt, toi);
								}
							}
							else
							{
								info = new CollisionInfo(new Vec2(0.0F, -1.0F), rectOt, toi);
							}
						}
					}
				}
			}
			return info;
		}

		public override void Resolve(CollisionInfo info)
		{
			if (info != null)
			{
                if (info.other is Moveable && info.other is MyCanvas)
                {
					MyCanvas myCanvas = (MyCanvas)info.other;
                    if (this.trigger)
                    {
						myCanvas.Trigger(this.Owner);
                    }
                    else
					{
						Moveable moveable = (Moveable)info.other;
						myCanvas.Position = moveable.OldPosition + moveable.Velocity * info.timeOfImpact;
						if (MyGame.collisionManager.FirstTime)
						{
							Vec2 velocity = moveable.Velocity;
							velocity.Reflect(info.normal, 1.0F);
							moveable.Velocity = velocity;
						}
					}
				}
			}
		}
		private bool SpecificYCalculation(int widthMe, int widthOther, Vec2 trcMe, Vec2 brcMe, Vec2 tlcOt, Vec2 blcOt)
		{
			if (widthMe > widthOther)
			{
				return ((tlcOt.y >= trcMe.y && tlcOt.y <= brcMe.y) || (blcOt.y >= trcMe.y && blcOt.y <= brcMe.y));
			}
			else
			{
				return ((trcMe.y >= tlcOt.y && trcMe.y <= blcOt.y) || (brcMe.y >= tlcOt.y && brcMe.y <= blcOt.y));
			}
		}

		private bool SpecificXCalculation(int heightMe, int heightOther, Vec2 trcMe, Vec2 tlcMe, Vec2 brcOt, Vec2 blcOt)
		{
			if (heightMe > heightOther)
			{
				/*Console.WriteLine(this.radius);
				Console.WriteLine(radiusOther); */
				/*Console.WriteLine("Part of result 1: " + (tlcOt.x >= trcMe.x));
				Console.WriteLine("Part of result 1: " + (blcOt.x <= trcMe.x));
				Console.WriteLine("Part of result 1: " + (tlcOt.x >= brcMe.x));
				Console.WriteLine("Part of result 1: " + (blcOt.x <= brcMe.x));*/
				return ((blcOt.x >= tlcMe.x && blcOt.x <= trcMe.x) || (brcOt.x >= tlcMe.x && brcOt.x <= trcMe.x));
			}
			else
			{
				/*Console.WriteLine(this.radius);
				Console.WriteLine(radiusOther);*/
				/*Console.WriteLine("Part of result 2: " + (trcMe.y >= tlcOt.y));
				Console.WriteLine("Part of result 2: " + (trcMe.y <= blcOt.y));
				Console.WriteLine("Part of result 2: " + (brcMe.y >= tlcOt.y));
				Console.WriteLine("Part of result 2: " + (brcMe.y <= blcOt.y));*/
				return ((tlcMe.x >= blcOt.x && tlcMe.x <= brcOt.x) || (trcMe.x >= blcOt.x && trcMe.x <= brcOt.x));
			}
		}
		public float CalculateTimeOfImpact(string impactSide, CanvasRectangle rectMe, CanvasSquare rectOt)
		{
			MyGame myGame = (MyGame)MyGame.main;
			float impact = 0.0F;
			float distanceA = 0.0F;
			float distanceB = 0.0F;
			switch (impactSide)
			{
				case "Left":
					distanceA = (rectOt.OldPosition.x) - (rectMe.Position.x + rectMe.width);
					distanceB = rectOt.OldPosition.x - rectOt.Position.x;
					impact = distanceA / distanceB;
					break;
				case "Right":
					distanceA = (rectOt.OldPosition.x + rectOt.width) - (rectMe.Position.x);
					distanceB = rectOt.OldPosition.x - rectOt.Position.x;
					impact = distanceA / distanceB;
					break;
				case "Top":
					distanceA = (rectOt.OldPosition.y) - (rectMe.Position.y + rectMe.height);
					distanceB = rectOt.OldPosition.y - rectOt.Position.y;
					impact = distanceA / distanceB;
					break;
				case "Bottom":
					distanceA = (rectOt.OldPosition.y + rectOt.height) - (rectMe.Position.y);
					distanceB = rectOt.OldPosition.y - rectOt.Position.y;
					impact = distanceA / distanceB;
					break;
			}
			return impact;
		}
	}
}

		/*Vec2 tlcMe = new Vec2(this.position.x - radius, this.position.y - radius);
		Vec2 trcMe = new Vec2(this.position.x + radius, this.position.y - radius);
		Vec2 blcMe = new Vec2(this.position.x - radius, this.position.y + radius);
		Vec2 brcMe = new Vec2(this.position.x + radius, this.position.y + radius);

		Vec2 tlcOt = new Vec2(block.position.x - block.radius, block.position.y - block.radius);
		Vec2 trcOt = new Vec2(block.position.x + block.radius, block.position.y - block.radius);
		Vec2 blcOt = new Vec2(block.position.x - block.radius, block.position.y + block.radius);
		Vec2 brcOt = new Vec2(block.position.x + block.radius, block.position.y + block.radius);

		Gizmos.DrawRectangle(tlcMe.x, tlcMe.y, 5, 5);
        Gizmos.DrawRectangle(trcMe.x, trcMe.y, 5, 5);
        Gizmos.DrawRectangle(blcMe.x, blcMe.y, 5, 5);
        Gizmos.DrawRectangle(brcMe.x, brcMe.y, 5, 5);


        //if (distance.Length() < this.velocity.Length())
        //if(tlcMe.x + velocity.x > trcOt.x && trcOt.x + velocity.x < tlcMe.x)
        *//*Vec2 dTlcToTrc = tlcMe - trcOt;
		Vec2 dBlcToBrc = blcMe - brcOt;
		Vec2 distanceRightToLeft = trcMe - tlcOt;*//*
        //if (dTlcToTrc.Length() < this.velocity.Length() && dBlcToBrc.Length() < this.velocity.Length())
        *//*if (this.position.x + radius > block.position.x - radius && this.position.x - radius < block.position.x + radius)
        {
            float toi = this.CalculateTimeOfImpact("Right", block);
            info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
        }*/
/*Console.WriteLine("State 1: " + (trcMe.x >= tlcOt.x && trcMe.x <= trcOt.x));
Console.WriteLine("State 2: " + (brcMe.x >= blcOt.x && brcMe.x <= brcOt.x));
Console.WriteLine("State 3: " + (trcMe.y >= tlcOt.y && trcMe.y <= blcOt.y));
Console.WriteLine("State 4: " + (brcMe.y >= tlcOt.y && brcMe.y <= blcOt.y));*//*
//Collision Right
//if (((trcMe.x >= tlcOt.x && trcMe.x <= trcOt.x) || (brcMe.x >= blcOt.x && brcMe.x <= brcOt.x)) && ((trcMe.y >= tlcOt.y && trcMe.y <= blcOt.y) || (brcMe.y >= tlcOt.y && brcMe.y <= blcOt.y)))
if (((trcMe.x >= tlcOt.x && trcMe.x <= trcOt.x) && (brcMe.x >= blcOt.x && brcMe.x <= brcOt.x)) && this.SpecificYCalculation(block.radius, trcMe, brcMe, tlcOt, blcOt))
{
	float toi = this.CalculateTimeOfImpact("Right", block);
	//Console.WriteLine("TOI Right = " + toi);
	//Console.WriteLine("Right collision");
	if (toi >= 0.0F && toi <= 1.0F)
	{
		info = new CollisionInfo(new Vec2(-1.0F, 0), block, toi);
	}

}
//Collision Left
//else if (((tlcMe.x <= trcOt.x && tlcMe.x >= tlcOt.x) || (blcMe.x <= brcOt.x && blcMe.x >= blcOt.x)) && ((tlcMe.y >= trcOt.y && tlcMe.y <= brcOt.y) || (blcMe.y >= trcOt.y && blcMe.y <= brcOt.y)))
if (((tlcMe.x <= trcOt.x && tlcMe.x >= tlcOt.x) && (blcMe.x <= brcOt.x && blcMe.x >= blcOt.x)) && this.SpecificYCalculation(block.radius, trcMe, brcMe, tlcOt, blcOt))
{
float toi = this.CalculateTimeOfImpact("Left", block);
//Console.WriteLine("TOI Left = " + toi);
//Console.WriteLine("Left collision");
if (toi >= 0.0F && toi <= 1.0F)
{
if (info != null)
{
	float otherToI = info.timeOfImpact;
	if (toi < otherToI)
	{
		info = new CollisionInfo(new Vec2(1.0F, 0), block, toi);
	}
}
else
{
	info = new CollisionInfo(new Vec2(1.0F, 0), block, toi);
}
}
}
//TODO: Make top and bottom collisions.
//Collision Top
if (((tlcMe.y <= blcOt.y && tlcMe.y >= tlcOt.y) && (trcMe.y <= brcOt.y && trcMe.y >= trcOt.y)) && this.SpecificXCalculation(block.radius, trcMe, tlcMe, brcOt, blcOt))
{
float toi = this.CalculateTimeOfImpact("Top", block);
//Console.WriteLine("TOI Top = " + toi);
//Console.WriteLine("Top collision");
if (toi >= 0.0F && toi <= 1.0F)
{
if (info != null)
{
	float otherToI = info.timeOfImpact;
	if (toi < otherToI)
	{
		info = new CollisionInfo(new Vec2(0.0F, 1.0F), block, toi);
	}
}
else
{
	info = new CollisionInfo(new Vec2(0.0F, 1.0F), block, toi);
}
}
}
//Collision Bottom
if (((blcMe.y >= tlcOt.y && blcMe.y <= blcOt.y) && (brcMe.y >= trcOt.y && brcMe.y <= brcOt.y)) && this.SpecificXCalculation(block.radius, trcMe, tlcMe, brcOt, blcOt))
{
float toi = this.CalculateTimeOfImpact("Bottom", block);
//Console.WriteLine("TOI Bottom = " + toi);
//Console.WriteLine("Bottom collision");
if (toi >= 0.0F && toi <= 1.0F)
{
if (info != null)
{
	float otherToI = info.timeOfImpact;
	if (toi < otherToI)
	{
		info = new CollisionInfo(new Vec2(0.0F, -1.0F), block, toi);
	}
}
else
{
	info = new CollisionInfo(new Vec2(0.0F, -1.0F), block, toi);
}
}
}
*//*else if (((tlcMe.x <= trcOt.x && tlcMe.x >= tlcOt.x) || (blcMe.x <= brcOt.x && blcMe.x >= blcOt.x)) && ((tlcMe.y >= trcOt.y && tlcMe.y <= brcOt.y) || (blcMe.y >= trcOt.y && blcMe.y <= brcOt.y)))
{
	float toi = this.CalculateTimeOfImpact("Left", block);
	Console.WriteLine("TOI Left = " + toi);
	Console.WriteLine("Left collision");
	info = new CollisionInfo(new Vec2(-1.0F, 0), block, toi);
}*//*
if (info != null)
{
	Vec2 poi = _oldPosition + (velocity * info.timeOfImpact);
	_position = poi;
	if (info.normal.x == -1.0F)
	{
		float relativeX = velocity.x - block.velocity.x;
		if (trcMe.x - relativeX > tlcOt.x)
		{
			this.velocity.x *= -bounciness;
		}
	}
	else if (info.normal.x == 1.0F)
	{
		float relativeX = velocity.x - block.velocity.x;
		if (tlcMe.x + relativeX < trcOt.x)
		{
			this.velocity.x *= -bounciness;
		}
	}
	else if (info.normal.y == -1.0F)
	{
		float relativeY = velocity.y - block.velocity.y;
		if (blcMe.y - relativeY > tlcOt.y)
		{
			this.velocity.y *= -bounciness;
		}
	}
	else if (info.normal.y == 1.0F)
	{
		float relativeY = velocity.y - block.velocity.y;
		if (tlcMe.y + relativeY < blcOt.y)
		{
			this.velocity.y *= -bounciness;
		}
	}
}
//Collision for right
*//*if ((trcMe.x > tlcOt.x && brcMe.x > blcOt.x) && (trcMe.x < trcOt.x && brcMe.x < brcOt.x))
{
	Console.WriteLine("Right!");
	float toi = this.CalculateTimeOfImpact("Right", block);
	Console.WriteLine(toi);
	info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
}*/

/*float leftMe = this.position.x - radius;
float rightMe = this.position.x + radius;

float leftOt = block.position.x - radius;
float rightOt = block.position.x + radius;

if (rightMe > leftOt && leftMe < rightOt)
{
	Console.WriteLine("Right!");
	float toi = this.CalculateTimeOfImpact("Right", block);
	info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
}
else if (leftMe > rightOt && rightMe < leftOt)
{
	Console.WriteLine("Left!");
	float toi = this.CalculateTimeOfImpact("Left", block);
	info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
}*/
/*if (this.position.x + radius > block.position.x - radius && this.position.x - radius < block.position.x + radius)
{
	Console.WriteLine("Right!");
	float toi = this.CalculateTimeOfImpact("Right", block);
	info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
}*/
/*if (this.position.x - radius > block.position.x + radius && this.position.x + radius < block.position.x - radius)
{
	Console.WriteLine("Left!");
	float toi = this.CalculateTimeOfImpact("Right", block);
	info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
}*/

/*	else if (this.x - block.x < radius)
	{
		Console.WriteLine("Left!");
		float toi = this.CalculateTimeOfImpact("Right", block);
		info = new CollisionInfo(new Vec2(1.0F, 0.0F), block, toi);
	}*//*
return info;
    }

	private bool SpecificYCalculation(int radiusOther, Vec2 trcMe, Vec2 brcMe, Vec2 tlcOt, Vec2 blcOt)
{
	if (this.radius > radiusOther)
	{
		*//*Console.WriteLine(this.radius);
		Console.WriteLine(radiusOther);*//*
		return ((tlcOt.y >= trcMe.y && tlcOt.y <= brcMe.y) || (blcOt.y >= trcMe.y && blcOt.y <= brcMe.y));
	}
	else
	{
		*//*Console.WriteLine(this.radius);
		Console.WriteLine(radiusOther);*//*
		return ((trcMe.y >= tlcOt.y && trcMe.y <= blcOt.y) || (brcMe.y >= tlcOt.y && brcMe.y <= blcOt.y));
	}
}

private bool SpecificXCalculation(int radiusOther, Vec2 trcMe, Vec2 tlcMe, Vec2 brcOt, Vec2 blcOt)
{
	if (this.radius > radiusOther)
	{
		*//*Console.WriteLine(this.radius);
		Console.WriteLine(radiusOther);*/
/*Console.WriteLine("Part of result 1: " + (tlcOt.x >= trcMe.x));
Console.WriteLine("Part of result 1: " + (blcOt.x <= trcMe.x));
Console.WriteLine("Part of result 1: " + (tlcOt.x >= brcMe.x));
Console.WriteLine("Part of result 1: " + (blcOt.x <= brcMe.x));*//*
return ((blcOt.x >= tlcMe.x && blcOt.x <= trcMe.x) || (brcOt.x >= tlcMe.x && brcOt.x <= trcMe.x));
}
else
{
*//*Console.WriteLine(this.radius);
Console.WriteLine(radiusOther);*/
/*Console.WriteLine("Part of result 2: " + (trcMe.y >= tlcOt.y));
Console.WriteLine("Part of result 2: " + (trcMe.y <= blcOt.y));
Console.WriteLine("Part of result 2: " + (brcMe.y >= tlcOt.y));
Console.WriteLine("Part of result 2: " + (brcMe.y <= blcOt.y));*//*
return ((tlcMe.x >= blcOt.x && tlcMe.x <= brcOt.x) || (trcMe.x >= blcOt.x && trcMe.x <= brcOt.x));
}
}
public float CalculateTimeOfImpact(string impactSide, Block other)
{
MyGame myGame = (MyGame)game;
float impact = 0.0F;
float distanceA = 0.0F;
float distanceB = 0.0F;
switch (impactSide)
{
case "Left":
	if (other != null)
	{
		distanceA = (_oldPosition.x - radius) - (other.position.x + other.radius);
		distanceB = _oldPosition.x - position.x;
	}
	else
	{
		distanceA = _oldPosition.x - (myGame.LeftXBoundary + radius);
		distanceB = _oldPosition.x - position.x;
	}
	impact = distanceA / distanceB;
	break;
case "Right":
	if (other != null)
	{
		distanceA = (_oldPosition.x + radius) - (other.position.x - other.radius);
		distanceB = _oldPosition.x - position.x;
	}
	else
	{
		distanceA = _oldPosition.x - (myGame.RightXBoundary - radius);
		distanceB = _oldPosition.x - position.x;
	}
	impact = distanceA / distanceB;
	break;
case "Top":
	if (other != null)
	{
		distanceA = (_oldPosition.y - radius) - (other.position.y + other.radius);
		distanceB = _oldPosition.y - position.y;
	}
	else
	{
		distanceA = _oldPosition.y - (myGame.TopYBoundary + radius);
		distanceB = _oldPosition.y - position.y;
	}
	impact = distanceA / distanceB;
	break;
case "Bottom":
	if (other != null)
	{
		distanceA = (_oldPosition.y + radius) - (other.position.y - other.radius);
		distanceB = _oldPosition.y - position.y;
	}
	else
	{
		distanceA = _oldPosition.y - (myGame.BottomYBoundary - radius);
		distanceB = _oldPosition.y - position.y;
	}
	impact = distanceA / distanceB;
	break;
}
return impact;
}
}
}
*/
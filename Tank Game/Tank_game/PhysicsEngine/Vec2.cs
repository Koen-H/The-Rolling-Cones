using System;
using GXPEngine;	// For Mathf

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}

	public void SetXY(float pX, float pY) 
	{
		x = pX;
		y = pY;
	}

	public float Length() {
		return Mathf.Sqrt (x * x + y * y);
	}

	public void Normalize() {
		float len = Length ();
		if (len > 0) {
			x /= len;
			y /= len;
		}
	}

	public Vec2 Normalized() {
		Vec2 result = new Vec2 (x, y);
		result.Normalize ();
		return result;
	}

	public static Vec2 operator +(Vec2 left, Vec2 right) {
		return new Vec2 (left.x + right.x, left.y + right.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right) {
		return new Vec2 (left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 v, float scalar) {
		return new Vec2 (v.x * scalar, v.y * scalar);
	}

	public static Vec2 operator *(float scalar, Vec2 v) {
		return new Vec2 (v.x * scalar, v.y * scalar);
	}

    public static Vec2 operator *(Vec2 vec1, Vec2 vec2)
    {
        return new Vec2(vec1.x * vec2.x, vec1.y * vec2.y);
    }

    public static Vec2 operator /(Vec2 v, float scalar) {
		return new Vec2 (v.x / scalar, v.y / scalar);
	}

    public static Vec2 operator /(float other, Vec2 vec) //Scale
    {
        return new Vec2(vec.x / other, vec.y / other);
    }



    //Start week 2
    public static float Deg2Rad(float deg) // Converts the given degrees to radians
    {
        return (float)((Math.PI / 180) * deg);
    }
    public static float Rad2Deg(float rad) // Converts the given radians to degrees
    {
        return (float)((180 / Math.PI) * rad);
    }

    public static Vec2 GetUnitVectorDeg(float deg) // Returns a new vector pointing in the given direction in degrees
    {
        return GetUnitVectorRad(Deg2Rad(deg));
    }

    public static Vec2 GetUnitVectorRad(float rad) // Returns a new vector pointing in the given direction in radians
    {
        return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static Vec2 RandomUnitVector()
    {
        float random = Utils.Random(0, 360);
        return new Vec2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public void SetAngleDegrees(float degrees)
    {
        SetAngleRadians(Deg2Rad(degrees));
    }

    public void SetAngleRadians(float rad)
    {
        float currentLength = Length();
        x = (float)Math.Cos(rad) * currentLength;
        y = (float)Math.Sin(rad) * currentLength;
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(Mathf.Atan2(y, x));
    }

    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }

    public void RotateRadians(float rad)
    {

        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        float tx = x;
        float ty = y;
        x = (cos * tx) - (sin * ty);
        y = (sin * tx) + (cos * ty);
    }

    public void RotateAroundDegrees(Vec2 rotationPoint, float deg)
    {
        RotateAroundRadians(rotationPoint, Deg2Rad(deg));
    }

    public void RotateAroundRadians(Vec2 rotationPoint, float rad)
    {
        this -= rotationPoint;
        RotateRadians(rad);
        this += rotationPoint;
    }


    //Week 4

    public float Dot(Vec2 v2)
    {
        return this.x * v2.x + this.y * v2.y;
    }

    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 normalVector, float bounciness = 1)
    {
        this -= (1 + bounciness) * (Dot(normalVector) * normalVector);
    }

    //Extra
    public float Distance(Vec2 otherVec)
    {
        return (otherVec - this).Length();
    }
}
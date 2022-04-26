using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
	public float x;
	public float y;

	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	public float Length()
	{
		return Mathf.Sqrt(x * x + y * y);
	}

	public void Normalize()
	{
		float length = this.Length();
		this.x = this.x > 0 || this.x < 0 ? this.x / length : 0;
		this.y = this.y > 0 || this.y < 0 ? this.y / length : 0;
	}

	public Vec2 Normalized()
	{
		Vec2 normalized = new Vec2(x, y);
		normalized.Normalize();
		return normalized;
	}

	public float Dot(Vec2 other)
	{
		return this.x * other.x + this.y * other.y;
	}

	public Vec2 Normal()
	{
		Vec2 vector = new Vec2(this.x, this.y);
		vector.Normalize();
		vector.RotateDegrees(90);
		return vector;
	}

	public void Reflect(Vec2 angleBounce, float bounciness)
	{
		this = this - (1 + bounciness) * this.Dot(angleBounce) * angleBounce;
	}

	public void SetXY(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 vector, float scalar)
	{
		return new Vec2(vector.x * scalar, vector.y * scalar);
	}

	public static Vec2 operator *(float scalar, Vec2 vector)
	{
		return new Vec2(vector.x * scalar, vector.y * scalar);
	}

	public static Vec2 operator /(Vec2 vector, float scalar)
	{
		return new Vec2(vector.x / scalar, vector.y / scalar);
	}

	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}

	public static float Deg2Rad(float degrees)
	{
		return degrees * Mathf.PI / 180.0F;
	}

	public static float Rad2Deg(float radians)
	{
		return radians * 180.0F / Mathf.PI;
	}

	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		float radians = Deg2Rad(degrees);
		float x = Mathf.Cos(radians);
		float y = Mathf.Sin(radians);
		return new Vec2(x, y);
	}

	public static Vec2 GetUnitVectorRadians(float radians)
	{
		float x = Mathf.Cos(radians);
		float y = Mathf.Sin(radians);
		return new Vec2(x, y);
	}

	public static Vec2 GetRandomUnitVector()
	{
		float degrees = Utils.Random(0, 360);
		float radians = Deg2Rad(degrees);
		float x = Mathf.Cos(radians);
		float y = Mathf.Sin(radians);
		return new Vec2(x, y);
	}

	public void RotateDegrees(float degrees)
	{
		float radians = Deg2Rad(degrees);
		float x = this.x;
		float y = this.y;
		this.x = Mathf.Cos(radians) * x - Mathf.Sin(radians) * y;
		this.y = Mathf.Cos(radians) * y + Mathf.Sin(radians) * x;
	}

	public void RotateRadians(float radians)
	{
		float x = this.x;
		float y = this.y;
		this.x = Mathf.Cos(radians) * x - Mathf.Sin(radians) * y;
		this.y = Mathf.Cos(radians) * y + Mathf.Sin(radians) * x;
	}

	public void RotateAroundDegrees(Vec2 offset, float degrees)
	{
		this -= offset;
		RotateDegrees(degrees);
		this += offset;
	}

	public void RotateAroundRadians(Vec2 offset, float radians)
	{
		this -= offset;
		RotateRadians(radians);
		this += offset;
	}

	public float GetAngleRadians()
	{
		return Mathf.Atan2(this.x, this.y);
	}

	public float GetAngleDegrees()
	{
		return Rad2Deg(Mathf.Atan2(this.x, this.y));
	}

	public void SetAngleDegrees(float degrees)
	{
		this = GetUnitVectorDeg(degrees) * Length();
	}

	public void SetAngleRadians(float radians)
	{
		this = GetUnitVectorRadians(radians) * Length();
	}

	public override string ToString()
	{
		return String.Format("({0}, {1})", x, y);
	}

}


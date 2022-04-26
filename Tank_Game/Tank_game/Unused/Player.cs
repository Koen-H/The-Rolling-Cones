using System;
using GXPEngine;
using Physics;

class Player : AnimationSprite {
	Collider myCollider;
	float speed = 3;
	float colliderWidth = 0.7f;
	float colliderHeight = 0.7f;

	float lastVx = 0;
	float lastVy = 0;

	float currentDamage = 0;

	ColliderManager engine;

	public Player(Vec2 startPosition) : base("character.png",4,2) {
		float cWidthPixels = width*colliderWidth;
		float cHeightPixels = height*colliderHeight;

		SetOrigin(width/2, height-(cHeightPixels/2));

		// Create collider, and register it (as trigger!):
		myCollider=new AABB(this, startPosition, cWidthPixels/2, cHeightPixels/2);
		engine=ColliderManager.main;
		engine.AddTriggerCollider(myCollider);
	}

	protected override void OnDestroy() {
		// Remove the collider when the sprite is destroyed:
		engine.RemoveTriggerCollider(myCollider);
	}

	public void TakeDamage() {
		Console.WriteLine("Ouch!");
		currentDamage=1;
	}

	void Move() {
		float vx = 0;
		float vy = 0;
		if (Input.GetKey(Key.LEFT)) {
			vx-=speed;
		}
		if (Input.GetKey(Key.RIGHT)) {
			vx+=speed;
		}
		if (Input.GetKey(Key.UP)) {
			vy-=speed;
		}
		if (Input.GetKey(Key.DOWN)) {
			vy+=speed;
		}
		// Let the physics engine / collider manager move the collider:
		engine.MoveUntilCollision(myCollider, new Vec2(vx, 0));
		engine.MoveUntilCollision(myCollider, new Vec2(0, vy));

		// Don't forget to update the sprite position too!
		x=myCollider.position.x;
		y=myCollider.position.y;

		// This is just to determine shoot direction:
		if (vx!=0 || vy!=0) {
			lastVx=vx;
			lastVy=vy;
		}
	}

	void Shoot() {
		if (Input.GetKeyDown(Key.SPACE) && (lastVx!=0 || lastVy!=0)) {
			Vec2 bulletVelocity = new Vec2(lastVx*2, lastVy*2);
			parent.AddChild(new Projectile(myCollider.position, bulletVelocity));
		}
	}

	void ChangeColor() {
		if (currentDamage>0) {
			currentDamage-=0.01f;
			SetColor(1, 1-currentDamage, 1-currentDamage);
		}
	}

	void Update() {
		Move();
		Shoot();
		ChangeColor();
	}
}


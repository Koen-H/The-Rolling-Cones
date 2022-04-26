using System;
using System.Collections.Generic;
using GXPEngine;
using Physics;

class Projectile : AnimationSprite {
	Collider myCollider;
	Vec2 velocity;
	int bounces = 0;
	int maxBounces = 3;

	ColliderManager engine;

	bool isCollidingWithPlayer = true; // used to prevent hitting the player who fired this!

	public Projectile(Vec2 startPosition, Vec2 pVelocity) : base("tilesheet.png",14,7) {
		SetOrigin(width/2, height/2);
		SetFrame(25);
		scale=0.5f;

		// Create collider, and register it (as trigger!):
		// (Note: in the current code, this collider is not used, so it could be omitted.)
		myCollider=new AABB(this, startPosition, width/2, height/2);
		engine=ColliderManager.main;
		engine.AddTriggerCollider(myCollider);

		velocity=pVelocity;
	}

	protected override void OnDestroy() {
		// Remove the collider when the sprite is destroyed:
		engine.RemoveTriggerCollider(myCollider);
	}

	void Move() {
		CollisionInfo colInfo1 = engine.MoveUntilCollision(myCollider, new Vec2(velocity.x, 0));
		if (colInfo1!=null) {
			velocity.x *= -1;
			bounces++;
		}
		CollisionInfo colInfo2 = engine.MoveUntilCollision(myCollider, new Vec2(0, velocity.y));
		if (colInfo2!=null) {
			velocity.y *= -1;
			bounces++;
		}
		x=myCollider.position.x;
		y=myCollider.position.y;

		if (bounces>=maxBounces || x<0 || x>game.width || y<0 || y>game.height) {
			LateDestroy();
			Console.WriteLine("Removing projectile");
		}
	}

	void CheckPlayerHit() {
		// Check overlapping trigger colliders:
		// (Note: this is a discrete collision check - tunneling might occur with high velocities!)
		List<Collider> overlaps = engine.GetOverlaps(myCollider);

		// Deal with overlaps: in this case, if we overlap with a player, but didn't overlap last frame,
		//  then deal damage to that player:
		bool playerOverlap = false;
		foreach (Collider col in overlaps) {
			if (col.owner is Player) {
				if (!isCollidingWithPlayer) {
					((Player)col.owner).TakeDamage();
				} 
				playerOverlap=true;	
			}
		}
		isCollidingWithPlayer=playerOverlap;
	}

	void Update() {
		Move();
		CheckPlayerHit();
	}
}


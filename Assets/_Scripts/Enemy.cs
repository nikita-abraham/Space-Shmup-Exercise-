using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	public float speed = 10f; // the speed in m/s
	public float fireRate = 0.3f; // Seconds/shot (unused)
	public float health = 10;
	public int score = 100; //points earned for destroying this

	public bool _________________;

	public Bounds bounds; // the Bounds of this and its children
	public Vector3 boundsCenterOffset; // Dist of bounds.center from position

	void Awake () {
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	//update is called once per frame
	void Update(){
		Move ();
	}

	public virtual void Move () {
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}

	//this is a Property: a method that acts like a field
	public Vector3 pos {
		get {
			return (this.transform.position);
		}
		set {
			this.transform.position = value; 
		}
	}

	void CheckOffscreen() {
		//if bounds are still their default value ...
		if(bounds.size == Vector3.zero) {
			// then set them
			bounds = Utils.CombineBoundsOfChildren(this.gameObject);
			//also find the diff between bounds.center - transform.position;
			boundsCenterOffset = bounds.center - transform.position;
		}

		//every time, update the bounds to the current position
		bounds.center = transform.position + boundsCenterOffset;
		//check to see whether the bounds are completely offscreen
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
			//if this enemy has gone off the bottom edge of the screen 
			if(off.y < 0) {
				//then destroy it 
				Destroy (this.gameObject);
			}
		}
	}
}

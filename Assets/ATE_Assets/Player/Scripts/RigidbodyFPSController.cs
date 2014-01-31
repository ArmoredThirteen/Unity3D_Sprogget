// Forged by:
// ArmoredThirteen

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidbodyFPSController : MonoBehaviour {
	// stuff effecting movement speed
	public float maxSpeed = 7.5f;
	public float gravity = 9.8f;
	public float maxGroundAccel = 1.5f;
	public float maxAirAccel = 0.5f;
	public float speedDecayGround = 0.15f;
	public float speedDecayAir = 0.05f;

	// various jumping stats
	public float jumpHeight = 2.0f;
	public float holdJumpDuration = 5.0f;
	public float holdJumpForce = 100;
	// various jumping toggles
	private float holdJumpStartTime;
	private float holdJumpUsedForce;
	private bool jumpWasReleased = true;
	private bool isHoldingJump = false;
	
	// stuff for finding ground data
	private float distToGround = 0;
	private bool isGrounded = false;
	private RaycastHit groundRayHit;

	// slope steepness toggles
	public float maxUsableSlope = 45.0f;
	public float maxUnhinderedSlope = 25.0f;
	private float currentSteepness = 0.0f;
	

	void Awake () {
		// limit the default movement
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		// find the ground distance for rays
		distToGround = (float)(collider.bounds.extents.y + 0.1);
	}
	

	void FixedUpdate () {
		// update the ground ray information
		Ray tempGroundRay = new Ray(transform.position, -Vector3.up);
		isGrounded = Physics.SphereCast(tempGroundRay, 0.495f, out groundRayHit, distToGround);
		// update the current steepness of the ground
		Vector3 groundNormal = groundRayHit.normal;
		Vector3 angleChecker = new Vector3(groundNormal.x, 0, groundNormal.z);
		currentSteepness = (Vector3.Angle(groundNormal, angleChecker)*-1)+90;

		// do x,y movement
		xzMovement();
		// do jumping stuff
		jumpMovement();
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
	}


	/*
	 * Controls movement along the xz axis.
	 */
	private void xzMovement() {
		// set the actual velocity change and speed decay
		float actualAccel = isGrounded ? maxGroundAccel : maxAirAccel;
		float actualSpeedDecay = isGrounded ? speedDecayGround : speedDecayAir;

		// get current xz velocity
		Vector3 currentVelocity = new Vector3 (rigidbody.velocity.x, 0, rigidbody.velocity.z);
		// get the decay amount and apply it to the velocity
		float decayedMagnitude = currentVelocity.magnitude * (1-actualSpeedDecay);
		currentVelocity = Vector3.ClampMagnitude(currentVelocity, decayedMagnitude);

		// get and clamp the additional speed direction
		Vector3 additionalVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		additionalVelocity = transform.TransformDirection(additionalVelocity);
		additionalVelocity = Vector3.ClampMagnitude(additionalVelocity, actualAccel);

		// get the slope direction and angle with velocityChange
		Vector3 groundXZ = new Vector3(groundRayHit.normal.x, 0, groundRayHit.normal.z);
		float velocitySlopeAngle = Vector3.Angle(additionalVelocity, -groundXZ);
		
		// if it is going uphill and is very steep
		if(velocitySlopeAngle < 90 && currentSteepness > maxUsableSlope) {
			// push it off the slope a little
			additionalVelocity = Vector3.ClampMagnitude(groundXZ, 0.125f);
		}
		// if it is going uphill and is somewhat steep
		else if(velocitySlopeAngle < 90 && currentSteepness > maxUnhinderedSlope) {
			// reduce our changed velocity in half
			additionalVelocity.x *= 0.5f;
			additionalVelocity.z *= 0.5f;
		}

		// get the new speed vector
		Vector3 newVelocity = currentVelocity + additionalVelocity;

		// if it is going too fast
		if (currentVelocity.magnitude >= maxSpeed) {
			// if it is trying to speed up
			if(newVelocity.magnitude > currentVelocity.magnitude) {
				newVelocity = currentVelocity;
			}
			// otherwise let it slow down
		}
		// it isn't going too fast
		else {
			// if it wants to go too fast
			if(newVelocity.magnitude > maxSpeed) {
				newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
			}
			// otherwise let it speed up unhindered
		}

		// get the final target velocity
		Vector3 targetVelocity = new Vector3(newVelocity.x, rigidbody.velocity.y, newVelocity.z);
		Vector3 velocityChange = (targetVelocity - rigidbody.velocity);

		// add the actual force
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}


	/*
	 * Controls how the player jumps.
	 */
	private void jumpMovement() {
		// establish a couple variables
		bool jumpIsPressed = Input.GetButton("Jump");
		Vector3 velocity = rigidbody.velocity;

		// reset jumpWasReleased if applicable
		// has to be grounded otherwise player can repress jump while
		// in midair and get an automatic jump when they land.
		if(isGrounded && !jumpIsPressed)
			jumpWasReleased = true;
		// reset isHoldingJump if applicable
		if(!jumpIsPressed)
			isHoldingJump = false;

		// you can totally jump
		if(isGrounded && jumpIsPressed && jumpWasReleased) {
			// instantiate new velocity
			Vector3 newVelocity = Vector3.zero;
			// get the slope direction
			Vector3 groundXZ = new Vector3(groundRayHit.normal.x, 0, groundRayHit.normal.z);

			// if it is too steep, push it off the slope some
			if(currentSteepness > maxUsableSlope) {
				newVelocity = Vector3.ClampMagnitude(groundXZ, 0.25f);
			}
			// otherwise, do a normal jump
			else {
				newVelocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}

			// apply new velocity
			rigidbody.velocity = newVelocity;
			// reset jumping triggers
			jumpWasReleased = false;
			isHoldingJump = true;
			holdJumpStartTime = Time.time;
			holdJumpUsedForce = 0;
		}
		// you're totally in the middle of an increased jump
		else if(!isGrounded && isHoldingJump && Time.time < holdJumpStartTime+holdJumpDuration) {
			// get force per time
			float forcePerTime = holdJumpForce/holdJumpDuration;
			// found how much time has gone by
			float usedTime = Time.time - holdJumpStartTime;

			// amount of force for total used time, minus what's already been used
			float forceAmount = (forcePerTime*usedTime) - holdJumpUsedForce;
			// get the force direction and magnitude
			Vector3 forceToApply = Vector3.ClampMagnitude(rigidbody.velocity, forceAmount);
			rigidbody.AddForce(forceToApply*rigidbody.mass*gravity);

			// add to amount of applied force
			holdJumpUsedForce += forceAmount;
		}
	}


	float CalculateJumpVerticalSpeed() {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

}
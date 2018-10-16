using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ViveSwipeDetector : NetworkBehaviour {
	[SerializeField]
	SteamVR_TrackedObject trackedObj;

	private readonly Vector2 mXAxis = new Vector2(1, 0);
	private readonly Vector2 mYAxis = new Vector2(0, 1);
	private bool trackingSwipe = false;
	private bool checkSwipe = false;

	public GameObject sphereSelection;

	// The angle range for detecting swipe
	private const float mAngleRange = 30;

	// To recognize as swipe user should at lease swipe for this many pixels
	private const float mMinSwipeDist = 0.2f;

	// To recognize as a swipe the velocity of the swipe
	// should be at least mMinVelocity
	// Reduce or increase to control the swipe speed
	private const float mMinVelocity = 4.0f;

	private Vector2 mStartPosition;
	private Vector2 endPosition;

	private float mSwipeStartTime;


	void Update() {
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		// Touch down, possible chance for a swipe
		if ((int)trackedObj.index != -1 && device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_Axis0)) {
			trackingSwipe = true;
			// Record start time and position
			mStartPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
				device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
			mSwipeStartTime = Time.time;
		}
		// Touch up , possible chance for a swipe
		else if (device.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_Axis0)) {
			trackingSwipe = false;
			trackingSwipe = true;
			checkSwipe = true;
		}
		else if (trackingSwipe) {
			endPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
									  device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
		}

		if (checkSwipe) {
			checkSwipe = false;
			float deltaTime = Time.time - mSwipeStartTime;
			Vector2 swipeVector = endPosition - mStartPosition;

			float velocity = swipeVector.magnitude / deltaTime;
			if (velocity > mMinVelocity &&
				swipeVector.magnitude > mMinSwipeDist) {
				// if the swipe has enough velocity and enough distance
				swipeVector.Normalize();

				float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
				angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

				// Detect left and right swipe
				if (angleOfSwipe < mAngleRange) {
					OnSwipeRight();
				}
				else if ((180.0f - angleOfSwipe) < mAngleRange) {
					OnSwipeLeft();
				}
				else {
					// Detect top and bottom swipe
					angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
					angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
					if (angleOfSwipe < mAngleRange) {
						OnSwipeTop();
					}
					else if ((180.0f - angleOfSwipe) < mAngleRange) {
						OnSwipeBottom();
					}
				}
			}
		}

	}

	private void OnSwipeLeft() {
		if (sphereSelection.transform.localScale.x > 0 && sphereSelection.transform.localScale.y > 0 && sphereSelection.transform.localScale.z > 0) {
			sphereSelection.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
		}		
	}

	private void OnSwipeRight() {
		if (sphereSelection.transform.localScale.x < 1.2f && sphereSelection.transform.localScale.y < 1.2f && sphereSelection.transform.localScale.z < 1.2f) {
			sphereSelection.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
		}		
	}

	private void OnSwipeTop() {
		if (sphereSelection.transform.localScale.x < 1.2f && sphereSelection.transform.localScale.y < 1.2f && sphereSelection.transform.localScale.z < 1.2f) {
			sphereSelection.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
		}
	}

	private void OnSwipeBottom() {
		if (sphereSelection.transform.localScale.x > 0 && sphereSelection.transform.localScale.y > 0 && sphereSelection.transform.localScale.z > 0) {
			sphereSelection.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
		}
	}

}
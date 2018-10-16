using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Networking;

namespace VRTK {

	public class SwitchMenu : NetworkBehaviour
	{
		[SerializeField]
		SteamVR_TrackedObject trackedObj;

		private readonly Vector2 mXAxis = new Vector2(1, 0);
		private readonly Vector2 mYAxis = new Vector2(0, 1);
		private bool trackingSwipe = false;
		private bool checkSwipe = false;

		// The angle range for detecting swipe
		private const float mAngleRange = 30;

		// To recognize as swipe user should at lease swipe for this many pixels
		private const float mMinSwipeDist = 0.2f;

		// To recognize as a swipe the velocity of the swipe should be at least mMinVelocity
		// Reduce or increase to control the swipe speed
		private const float mMinVelocity = 4.0f;

		private Vector2 mStartPosition;
		private Vector2 endPosition;

		private float mSwipeStartTime;

		// Menus
		public GameObject menuRadial1;
		//public GameObject menuRadial2;
		public GameObject menuRadial3;
		public GameObject menuRadial4;
		public GameObject radialColors;
		public GameObject radialCocoon;
		public GameObject htcRightController;


		// Enable the first menu
		void Start()
		{
			menuRadial1.SetActive(true);
			//menuRadial2.SetActive(false);
			menuRadial3.SetActive(false);
			menuRadial4.SetActive(false);
		}

		void Update()
		{
			var device = SteamVR_Controller.Input((int)trackedObj.index);
			// Touch down, possible chance for a swipe
			if ((int)trackedObj.index != -1 && device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
			{
				trackingSwipe = true;
				// Record start time and position
				mStartPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
					device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
				mSwipeStartTime = Time.time;
			}
			// Touch up , possible chance for a swipe
			else if (device.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_Axis0))
			{
				trackingSwipe = false;
				trackingSwipe = true;
				checkSwipe = true;
			}
			else if (trackingSwipe)
			{
				endPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
										  device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
			}

			if (checkSwipe)
			{
				checkSwipe = false;
				float deltaTime = Time.time - mSwipeStartTime;
				Vector2 swipeVector = endPosition - mStartPosition;

				float velocity = swipeVector.magnitude / deltaTime;
				if (velocity > mMinVelocity && swipeVector.magnitude > mMinSwipeDist)
				{
					// if the swipe has enough velocity and enough distance
					swipeVector.Normalize();

					float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
					angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

					// Detect left and right swipe
					if (angleOfSwipe < mAngleRange)
					{
						OnSwipeRight();
					}
					else if ((180.0f - angleOfSwipe) < mAngleRange)
					{
						OnSwipeLeft();
					}
					else
					{
						// Detect top and bottom swipe
						angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
						angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
						if (angleOfSwipe < mAngleRange)
						{
							OnSwipeTop();
						}
						else if ((180.0f - angleOfSwipe) < mAngleRange)
						{
							OnSwipeBottom();
						}
					}
				}
			}

		}

		private void OnSwipeLeft()
		{
			// Bezier
			if (menuRadial1.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(true);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(true);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Tools
			/*else if (menuRadial2.activeInHierarchy) 
			{
				menuRadial1.SetActive(false);
				menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy) 
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}*/
			// Options
			else if (menuRadial3.activeInHierarchy)
			{
				menuRadial1.SetActive(true);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Primitives
			else
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
		}

		private void OnSwipeRight()
		{
			// Bezier
			if (menuRadial1.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(true);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy) {
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Tools
			/*else if (menuRadial2.activeInHierarchy) 
			{
				menuRadial1.SetActive(false);
				menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy) 
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}*/
			// Options
			else if (menuRadial3.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(true);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Primitives
			else {
				menuRadial1.SetActive(true);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
		}

		private void OnSwipeTop() {
			// Bezier
			if (menuRadial1.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(true);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Tools
			/*else if (menuRadial2.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy) 
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}*/
			// Options
			else if (menuRadial3.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(true);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Primitives
			else {
				menuRadial1.SetActive(true);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
		}

		private void OnSwipeBottom() {
			// Bezier
			if (menuRadial1.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(true);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(true);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Tools
			/*else if (menuRadial2.activeInHierarchy)
			{
				menuRadial1.SetActive(false);
				menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy) 
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}*/
			// Options
			else if (menuRadial3.activeInHierarchy)
			{
				menuRadial1.SetActive(true);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(false);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
			// Primitives
			else
			{
				menuRadial1.SetActive(false);
				//menuRadial2.SetActive(false);
				menuRadial3.SetActive(true);
				menuRadial4.SetActive(false);
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialColors.SetActive(false);
				radialCocoon.SetActive(false);
			}
		}

	}

}
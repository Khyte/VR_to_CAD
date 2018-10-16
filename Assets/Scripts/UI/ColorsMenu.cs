using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class ColorsMenu : MonoBehaviour
	{

		public GameObject radialColors;
		public GameObject radialTools;
		public GameObject htcRightController;

		private SteamVR_Controller.Device Controller
		{
			get
			{
				return SteamVR_Controller.Input((int)trackedObj.index);
			}
		}
		private SteamVR_TrackedObject trackedObj;


		void Start()
		{
			trackedObj = GetComponent<SteamVR_TrackedObject>();
			radialColors.SetActive(false);
		}

		// Enable color radialmenu and the pointer
		public void ChooseColorsMenu()
		{
			radialColors.SetActive(true);
			radialTools.SetActive(false);
			if (htcRightController.activeInHierarchy) {
				htcRightController.GetComponent<VRTK_SimplePointer>().enabled = true;
			}
		}

	}

}
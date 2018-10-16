using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class CocoonMenu : MonoBehaviour
	{

		public GameObject radialCocoon;
		public GameObject radialOptions;

		private SteamVR_Controller.Device controller
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
			radialCocoon.SetActive(false);
		}

		// Enable the radial cocoon menu
		public void ChooseCocoonMenu()
		{
			radialCocoon.SetActive(true);
			radialOptions.SetActive(false);
		}

	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class DeleteObject : MonoBehaviour
	{

		public GameObject htcRightController;
		public GameObject radialOptions;
		public bool deleteMode = false;


		// Check if the right HTC Controller is connected
		void Start()
		{
			if (htcRightController.GetComponent<VRTK_ControllerEvents>() == null) {
				Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
				return;
			}
			htcRightController.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
		}

		// Delete object option
		public void DeleteRadialMenu()
		{
			// Deactivate delete mode if it's active
			if (deleteMode)
			{
				deleteMode = false;
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = false;
				}
				radialOptions.transform.GetChild(1).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
			}
			// Activate delete mode if it's unactive
			else
			{
				deleteMode = true;
				if (htcRightController.activeInHierarchy)
				{
					htcRightController.GetComponent<VRTK_SimplePointer>().enabled = true;
				}
				radialOptions.transform.GetChild(1).GetComponent<UICircle>().color = new Color(0.4f, 0.4f, 0.4f);
			}
		}

		// If the button is pressed, destroy the object in front of the pointer
		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			if (deleteMode)
			{
				if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed)
				{
					GetComponent<Historique>().HistoCreate(htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed, false, true);
					htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.SetActive(false);
				}
			}
		}

	}

}
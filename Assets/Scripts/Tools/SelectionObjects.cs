using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;

namespace VRTK {

	public class SelectionObjects : MonoBehaviour
	{

		public GameObject htcRightController;

		private GameObject gameControl;

		// Save transform object
		private Vector3 firstTransform;
		private Quaternion firstRotation;
		private Vector3 firstScale;


		// Check if the right HTC Controller is connected
		void Start()
		{
			if (htcRightController.GetComponent<VRTK_ControllerEvents>() == null)
			{
				Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
				return;
			}
			htcRightController.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
			htcRightController.GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUncliked);
			gameControl = GameObject.FindGameObjectWithTag("GameController");
		}

		// If the button is pressed, grab objects
		private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
		{
			// For each object in the selection sphere
			foreach (GameObject objSelected in GameObject.FindGameObjectsWithTag("ExportableObject"))
			{
				// Start grabbing each object
				if (objSelected.GetComponent<VRTK_InteractableObject>().isSelected && !gameControl.GetComponent<CreateSphere>().isCreate)
				{
					objSelected.GetComponent<VRTK_ChildOfControllerGrabAttach>().StartGrab(gameObject, objSelected, gameObject.GetComponent<Rigidbody>());
					objSelected.GetComponent<VRTK_InteractableObject>().PrimaryControllerGrab(objSelected);
				}

				// If it's a copy mode, create a clone for each object and keep local transform
				if (objSelected.GetComponent<VRTK_InteractableObject>().isSelected && gameControl.GetComponent<CopyPaste>().copyMode)
				{
					firstTransform = objSelected.transform.position;
					firstRotation = objSelected.transform.localRotation;
					firstScale = objSelected.transform.lossyScale;
					GameObject cloneObject = Instantiate(objSelected, objSelected.transform.position, objSelected.transform.rotation);
					cloneObject.name = objSelected.name;

					// Create new data for the .obj export and scene undo
					gameControl.GetComponent<ExportOBJ>().numObject++;
					int indexOfObj = gameControl.GetComponent<Historique>().listObjects.LastIndexOf(objSelected);
					if (indexOfObj >= 0)
					{
						if (gameControl.GetComponent<Historique>().createObjects[indexOfObj]) {
							gameControl.GetComponent<Historique>().HistoCopyPaste(cloneObject, true, false, indexOfObj);
							gameControl.GetComponent<Historique>().RemoveElement(indexOfObj + 1);
						}
						else
						{
							gameControl.GetComponent<Historique>().HistoCopyPaste(cloneObject, false, false, indexOfObj);
							gameControl.GetComponent<Historique>().RemoveElement(indexOfObj + 1);
						}
					}
				}
			}
		}

		// If the button is unpressed, stop grabbing objects
		private void DoTriggerUncliked(object sender, ControllerInteractionEventArgs e)
		{
			foreach (GameObject objSelected in GameObject.FindGameObjectsWithTag("ExportableObject"))
			{
				// Unselect each object
				if (objSelected.GetComponent<VRTK_InteractableObject>().isSelected)
				{
					objSelected.transform.parent = null;
					objSelected.GetComponent<VRTK_InteractableObject>().PrimaryControllerUngrab(objSelected);
					objSelected.GetComponent<VRTK_ChildOfControllerGrabAttach>().StopGrab(false);

					// Reset variables
					if (gameControl.GetComponent<CreateSphere>().isCreate || gameControl.GetComponent<CreateCube>().isCreate || gameControl.GetComponent<CreateCylinder>().isCreate || gameControl.GetComponent<CreateCone>().isCreate)
					{
						gameControl.GetComponent<Historique>().HistoCreate(objSelected, true, false);
						gameControl.GetComponent<CreateSphere>().isCreate = false;
						gameControl.GetComponent<CreateSphere>().lastObj = null;
						gameControl.GetComponent<CreateCube>().isCreate = false;
						gameControl.GetComponent<CreateCube>().lastObj = null;
						gameControl.GetComponent<CreateCylinder>().isCreate = false;
						gameControl.GetComponent<CreateCylinder>().lastObj = null;
						gameControl.GetComponent<CreateCone>().isCreate = false;
						gameControl.GetComponent<CreateCone>().lastObj = null;
						foreach (Transform radials in gameControl.GetComponent<CreateCube>().radialPrimitives.transform)
						{
							radials.GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
						}
					}
					
					// If it's copy mode, paste and create an other scene undo
					else if (gameControl.GetComponent<CopyPaste>().copyMode)
					{
						gameControl.GetComponent<Historique>().HistoCreate(objSelected, true, false);
					}
					else
					{
						gameControl.GetComponent<Historique>().HistoCreate(objSelected, false, false);
					}
					objSelected.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}

	}

}
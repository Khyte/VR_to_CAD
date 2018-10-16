using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.SecondaryControllerGrabActions;
using VRTK.GrabAttachMechanics;

namespace VRTK {

	public class ApplyObj : MonoBehaviour
	{

		// On start, if some objects with mesh are detected, convert them to exportable objects and add scripts
		void Start()
		{
			foreach (MeshRenderer objects in FindObjectsOfType<MeshRenderer>())
			{
				if (objects.tag == "Untagged")
				{
					objects.tag = "ExportableObject";
					objects.gameObject.AddComponent<BoxCollider>();
					objects.gameObject.AddComponent<Rigidbody>();
					objects.GetComponent<Rigidbody>().useGravity = false;
					objects.GetComponent<Rigidbody>().isKinematic = true;
					objects.gameObject.AddComponent<VRTK_InteractableObject>();
					objects.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
					objects.gameObject.AddComponent<VRTK_ChildOfControllerGrabAttach>();
					objects.GetComponent<VRTK_ChildOfControllerGrabAttach>().precisionGrab = true;
					objects.gameObject.AddComponent<VRTK_AxisScaleGrabAction>();
					objects.gameObject.GetComponent<VRTK_AxisScaleGrabAction>().ungrabDistance = 10f;
				}
			}
		}

	}

}
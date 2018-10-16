using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class SelectionMode : MonoBehaviour
	{

		private GameObject gameControl;


		void Start()
		{
			gameControl = GameObject.FindGameObjectWithTag("GameController");
		}

		// If the selection is in one or more objects, select them
		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "ExportableObject" && !gameControl.GetComponent<CreateSphere>().isCreate && !gameControl.GetComponent<CreateSphere>().isCreate && !gameControl.GetComponent<CreateCube>().isCreate && !gameControl.GetComponent<CreateCylinder>().isCreate && !gameControl.GetComponent<CreateCone>().isCreate) {
				other.GetComponent<VRTK_InteractableObject>().isSelected = true;
			}
		}

		// Unselect objects if the collider is outside
		void OnTriggerExit(Collider other)
		{
			if (other.tag == "ExportableObject")
			{
				other.transform.parent = null;
				other.GetComponent<VRTK_InteractableObject>().isSelected = false;
			}
		}

	}

}
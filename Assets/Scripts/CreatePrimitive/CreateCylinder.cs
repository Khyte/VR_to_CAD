using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CreateCylinder : MonoBehaviour {

	// Vive Controller
	public GameObject viveControllerRight;

	// Poisition checker
	public GameObject ancre;
	public GameObject cylinder;
	public GameObject lastObj;

	// Color
	public GameObject radialPrimitives;
	public GameObject selectionSphere;

	// Check when the object is created
	public bool isCreate = false;


	public void CylinderRadialMenu()
	{
		if (!isCreate)
		{
			// Security : destroy all objects
			DestroyCreate(GetComponent<CreateCone>().lastObj, 1);
			DestroyCreate(GetComponent<CreateCube>().lastObj, 0);
			DestroyCreate(GetComponent<CreateSphere>().lastObj, 3);

			// Instantiate the new object and check position
			GameObject cloneCylinder = Instantiate(cylinder, selectionSphere.transform.position, viveControllerRight.transform.rotation);
			lastObj = cloneCylinder;
			cloneCylinder.transform.rotation = Quaternion.Euler(0, 0, 0);
			cloneCylinder.transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
			cloneCylinder.name = "Object" + GetComponent<ExportOBJ>().numObject + "_Cylinder";
			cloneCylinder.transform.parent = ancre.transform;
			GetComponent<ExportOBJ>().numObject++;
			cloneCylinder.GetComponent<VRTK_InteractableObject>().isSelected = true;
			isCreate = true;
			radialPrimitives.transform.GetChild(2).GetComponent<UICircle>().color = new Color(0.4f, 0.4f, 0.4f);
		}
		// Destroy the actual object
		else
		{
			if (lastObj != null)
			{
				Destroy(lastObj);
				GetComponent<ExportOBJ>().numObject--;
			}
			isCreate = false;
			radialPrimitives.transform.GetChild(2).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
		}
	}

	// Security : destroy all objects
	private void DestroyCreate(GameObject obj, int child)
	{
		if (obj != null)
		{
			Destroy(obj);
			GetComponent<ExportOBJ>().numObject--;
		}
		GetComponent<CreateCone>().isCreate = false;
		GetComponent<CreateCube>().isCreate = false;
		GetComponent<CreateSphere>().isCreate = false;
		radialPrimitives.transform.GetChild(child).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
	}

}
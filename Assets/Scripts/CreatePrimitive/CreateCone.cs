using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CreateCone : MonoBehaviour {

	// Vive Controller
	public GameObject viveControllerRight;

	// Poisition checker
	public GameObject ancre;
	public GameObject cone;
	public GameObject lastObj;

	// Color
	public GameObject radialPrimitives;
	public GameObject selectionSphere;

	// Check when the object is created
	public bool isCreate = false;


	public void ConeRadialMenu()
	{
		if (!isCreate)
		{
			// Security : destroy all objects
			DestroyCreate(GetComponent<CreateCylinder>().lastObj, 2);
			DestroyCreate(GetComponent<CreateCube>().lastObj, 0);
			DestroyCreate(GetComponent<CreateSphere>().lastObj, 3);

			// Instantiate the new object and check position
			GameObject cloneCone = Instantiate(cone, selectionSphere.transform.position, viveControllerRight.transform.rotation);
			lastObj = cloneCone;
			cloneCone.transform.rotation = Quaternion.Euler(0, 0, 0);
			cloneCone.transform.localScale = new Vector3(0.05f, 0.1f, 0.05f);
			cloneCone.name = "Object" + GetComponent<ExportOBJ>().numObject + "_Cone";
			cloneCone.transform.parent = ancre.transform;
			GetComponent<ExportOBJ>().numObject++;
			cloneCone.GetComponent<VRTK_InteractableObject>().isSelected = true;
			isCreate = true;
			radialPrimitives.transform.GetChild(1).GetComponent<UICircle>().color = new Color(0.4f, 0.4f, 0.4f);
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
			radialPrimitives.transform.GetChild(1).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
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
		GetComponent<CreateCylinder>().isCreate = false;
		GetComponent<CreateCube>().isCreate = false;
		GetComponent<CreateSphere>().isCreate = false;
		radialPrimitives.transform.GetChild(child).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CreateSphere : MonoBehaviour {

	// Vive Controller
	public GameObject viveControllerRight;

	// Poisition checker
	public GameObject ancre;
	public GameObject sphere;
	public GameObject lastObj;

	// Color
	public GameObject radialPrimitives;
	public GameObject selectionSphere;

	// Check when the object is created
	public bool isCreate = false;


	public void SphereRadialMenu()
	{
		if (!isCreate)
		{
			// Security : destroy all objects
			DestroyCreate(GetComponent<CreateCylinder>().lastObj, 2);
			DestroyCreate(GetComponent<CreateCone>().lastObj, 1);
			DestroyCreate(GetComponent<CreateCube>().lastObj, 0);

			// Instantiate the new object and check position
			GameObject cloneSphere = Instantiate(sphere, selectionSphere.transform.position, viveControllerRight.transform.rotation);
			lastObj = cloneSphere;
			cloneSphere.transform.rotation = Quaternion.Euler(0, 0, 0);
			cloneSphere.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
			cloneSphere.name = "Object" + GetComponent<ExportOBJ>().numObject + "_Sphere";
			cloneSphere.transform.parent = ancre.transform;
			GetComponent<ExportOBJ>().numObject++;
			cloneSphere.GetComponent<VRTK_InteractableObject>().isSelected = true;
			isCreate = true;
			radialPrimitives.transform.GetChild(3).GetComponent<UICircle>().color = new Color(0.4f, 0.4f, 0.4f);
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
			radialPrimitives.transform.GetChild(3).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
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
		GetComponent<CreateCone>().isCreate = false;
		GetComponent<CreateCube>().isCreate = false;
		radialPrimitives.transform.GetChild(child).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Historique : MonoBehaviour {

	// Maximum limit of scene undo
	public int limitHisto = 0;

	// List of objects and object's data in the scene
	public List<GameObject> listObjects;
	public List<Vector3> positionObjects;
	public List<Quaternion> rotationObjects;
	public List<Vector3> scaleObjects;

	public List<bool> createObjects;
	public List<bool> deleteObjects;
	public int index = 0;

	private int lastPosObj;


	// On start, create list
	void Start ()
	{
		listObjects = new List<GameObject>();
		positionObjects = new List<Vector3>();
		rotationObjects = new List<Quaternion>();
		scaleObjects = new List<Vector3>();
		createObjects = new List<bool>();
		deleteObjects = new List<bool>();
	}

	// Create a new undo if you create or destroy an object
	public void HistoCreate(GameObject obj, bool isCreate, bool isDelete)
	{
		if (index < listObjects.Count)
		{
			int i = 0;

			// If you add an index but there's some undo after this index, delete them
			foreach (bool isCreateToRemove in createObjects)
			{
				if (isCreateToRemove && i >= index && i < createObjects.Count)
				{
					Destroy(listObjects[i]);
				}
				i++;
			}
			listObjects.RemoveRange(index, listObjects.Count - index);
			positionObjects.RemoveRange(index, positionObjects.Count - index);
			rotationObjects.RemoveRange(index, rotationObjects.Count - index);
			scaleObjects.RemoveRange(index, scaleObjects.Count - index);
			createObjects.RemoveRange(index, createObjects.Count - index);
			deleteObjects.RemoveRange(index, deleteObjects.Count - index);
		}

		// Add undo data
		listObjects.Add(obj);
		positionObjects.Add(obj.transform.position);
		rotationObjects.Add(obj.transform.localRotation);
		scaleObjects.Add(obj.transform.localScale);

		if (isCreate)
		{
			createObjects.Add(true);
			deleteObjects.Add(false);
		}
		else if (isDelete)
		{
			createObjects.Add(false);
			deleteObjects.Add(true);
		}
		else
		{
			createObjects.Add(false);
			deleteObjects.Add(false);
		}

		// If the index exceed the limit, delete the oldest undo
		if (index > limitHisto)
		{
			listObjects.RemoveAt(0);
			positionObjects.RemoveAt(0);
			rotationObjects.RemoveAt(0);
			scaleObjects.RemoveAt(0);
			createObjects.RemoveAt(0);
			deleteObjects.RemoveAt(0);
			return;
		}
		index++;
	}

	// Create a new undo if you copy/paste objects
	public void HistoCopyPaste(GameObject obj, bool isCreate, bool isDelete, int indexOfObj)
	{
		if (index < listObjects.Count)
		{
			int i = 0;

			// If you add an index but there's some undo after this index, delete them
			foreach (bool isCreateToRemove in createObjects)
			{
				if (isCreateToRemove && i >= index && i < createObjects.Count)
				{
					Destroy(listObjects[i]);
				}
				i++;
			}
			listObjects.RemoveRange(index, listObjects.Count - index);
			positionObjects.RemoveRange(index, positionObjects.Count - index);
			rotationObjects.RemoveRange(index, rotationObjects.Count - index);
			scaleObjects.RemoveRange(index, scaleObjects.Count - index);
			createObjects.RemoveRange(index, createObjects.Count - index);
			deleteObjects.RemoveRange(index, deleteObjects.Count - index);
		}

		// Add undo data
		listObjects.Insert(indexOfObj, obj);
		positionObjects.Insert(indexOfObj, obj.transform.position);
		rotationObjects.Insert(indexOfObj, obj.transform.localRotation);
		scaleObjects.Insert(indexOfObj, obj.transform.localScale);

		if (isCreate)
		{
			createObjects.Insert(indexOfObj, true);
			deleteObjects.Insert(indexOfObj, false);
		}
		else if (isDelete)
		{
			createObjects.Insert(indexOfObj, false);
			deleteObjects.Insert(indexOfObj, true);
		}
		else
		{
			createObjects.Insert(indexOfObj, false);
			deleteObjects.Insert(indexOfObj, false);
		}

		// If the index exceed the limit, delete the oldest undo
		if (index > limitHisto)
		{
			listObjects.RemoveAt(0);
			positionObjects.RemoveAt(0);
			rotationObjects.RemoveAt(0);
			scaleObjects.RemoveAt(0);
			createObjects.RemoveAt(0);
			deleteObjects.RemoveAt(0);
			return;
		}
		index++;
	}

	// Remove all undo after the index number
	public void RemoveElement(int indexOfObj)
	{
		listObjects.RemoveAt(indexOfObj);
		positionObjects.RemoveAt(indexOfObj);
		rotationObjects.RemoveAt(indexOfObj);
		scaleObjects.RemoveAt(indexOfObj);
		createObjects.RemoveAt(indexOfObj);
		deleteObjects.RemoveAt(indexOfObj);
		index--;
	}

	// If you undo, make the scene like this undo index
	public void Undo()
	{
		if (listObjects.Count > 0 && index > 0)
		{
			if (createObjects[index - 1])
			{
				int j = 0;
				if (!listObjects[index - 1].activeInHierarchy)
				{
					// Seek for the object in this index number
					foreach (GameObject childList in listObjects)
					{
						if (childList.transform.position == positionObjects[index - 1])
						{
							listObjects[j].SetActive(false);
							GameObject temp = listObjects[j];
							listObjects[j] = listObjects[index - 1];
							listObjects[index - 1] = temp;
						}
						j++;
					}
				}
				listObjects[index - 1].SetActive(false);
				index--;
				return;
			}
			else if(deleteObjects[index - 1])
			{
				listObjects[index - 1].SetActive(true);
				index--;
				return;
			}
			int i = 0;
			foreach (GameObject childList in listObjects)
			{
				if (childList.name == listObjects[index - 1].name && i < index - 1)
				{
					lastPosObj = i;
				}
				i++;
			}
			listObjects[index - 1].transform.position = positionObjects[lastPosObj];
			listObjects[index - 1].transform.rotation = rotationObjects[lastPosObj];
			listObjects[index - 1].transform.localScale = scaleObjects[lastPosObj];
			index--;
		}
	}

	// If you redo, make the scene like this undo index
	public void Redo()
	{
		if (listObjects.Count > 0 && index >= 0 && index < listObjects.Count)
		{
			if (createObjects[index])
			{
				if (listObjects[index].activeInHierarchy)
				{
					int x = 0;
					int y = 0;

					// Seek for the object in this index number
					foreach (GameObject childList in listObjects) {
						if (childList.name == listObjects[index].name && x < index && !childList.activeInHierarchy)
						{
							y = x;
						}
						x++;
					}
					listObjects[y].SetActive(true);
					listObjects[y].transform.position = positionObjects[index - 1];
					listObjects[y].transform.rotation = rotationObjects[index - 1];
					listObjects[y].transform.localScale = scaleObjects[index - 1];
					GameObject temp = listObjects[y];
					listObjects[y] = listObjects[index - 1];
					listObjects[index - 1] = temp;
					listObjects[index].transform.position = positionObjects[index];
					listObjects[index].transform.rotation = rotationObjects[index];
					listObjects[index].transform.localScale = scaleObjects[index];
				}
				listObjects[index].SetActive(true);				
				index++;
				return;
			}
			else if(deleteObjects[index])
			{
				listObjects[index].SetActive(false);
				index++;
				return;
			}
			int i = 0;
			foreach (GameObject childList in listObjects)
			{
				if (childList.name == listObjects[index].name && i > index - 1 && i < 999)
				{
					lastPosObj = i;
					i = 999;
				}
				i++;
			}
			if (!listObjects[index].activeInHierarchy)
			{
				listObjects[index].SetActive(true);
				listObjects[index - 1].SetActive(false);
			}
			listObjects[index].transform.position = positionObjects[lastPosObj];
			listObjects[index].transform.rotation = rotationObjects[lastPosObj];
			listObjects[index].transform.localScale = scaleObjects[lastPosObj];
			index++;
		}
	}

}
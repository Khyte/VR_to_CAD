using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class CopyPaste : MonoBehaviour
	{

		public GameObject radialTools;
		public bool copyMode = false;


		public void CopyObject()
		{
			// Activate copy mode
			if (copyMode)
			{
				copyMode = false;
				radialTools.transform.GetChild(0).GetComponent<UICircle>().color = new Color(1f, 1f, 1f);
			}
			// Deactivate copy mode
			else
			{
				copyMode = true;
				radialTools.transform.GetChild(0).GetComponent<UICircle>().color = new Color(0.4f, 0.4f, 0.4f);
			}
		}

	}

}
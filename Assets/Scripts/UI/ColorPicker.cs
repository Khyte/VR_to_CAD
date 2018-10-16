using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK {

	public class ColorPicker : MonoBehaviour
	{

		public GameObject htcRightController;


		// On start, make a color for each texture
		void Start()
		{
			transform.GetChild(0).GetComponent<UICircle>().color = new Color(1f, 0f, 0f);
			transform.GetChild(1).GetComponent<UICircle>().color = new Color(0.5f, 0f, 1f);
			transform.GetChild(2).GetComponent<UICircle>().color = new Color(0f, 0f, 1f);
			transform.GetChild(3).GetComponent<UICircle>().color = new Color(0f, 0.5f, 1f);
			transform.GetChild(4).GetComponent<UICircle>().color = new Color(0f, 1f, 1f);
			transform.GetChild(5).GetComponent<UICircle>().color = new Color(0f, 1f, 0.5f);
			transform.GetChild(6).GetComponent<UICircle>().color = new Color(0f, 1f, 0f);
			transform.GetChild(7).GetComponent<UICircle>().color = new Color(0.5f, 1f, 0f);
			transform.GetChild(8).GetComponent<UICircle>().color = new Color(1f, 1f, 0f);
			transform.GetChild(9).GetComponent<UICircle>().color = new Color(1f, 0.5f, 0f);
		}

		public void Color0()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
			}
		}

		public void Color1()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0.5f, 0f, 1f);
			}
		}

		public void Color2()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f);
			}
		}

		public void Color3()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0f, 0.5f, 1f);
			}
		}

		public void Color4()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
			}
		}

		public void Color5()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.5f);
			}
		}

		public void Color6()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f);
			}
		}

		public void Color7()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(0.5f, 1f, 0f);
			}
		}

		public void Color8()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f);
			}
		}

		public void Color9()
		{
			if (htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed) {
				htcRightController.GetComponent<VRTK_SimplePointer>().objectPointed.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f);
			}
		}

	}

}
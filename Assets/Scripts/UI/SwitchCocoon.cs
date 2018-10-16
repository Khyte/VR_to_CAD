using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCocoon : MonoBehaviour {

	public GameObject camHTC;

	// Lightning of the scene
	public GameObject lightning;
	public GameObject lightningRoom;

	// Environnement objects
	public GameObject nightPlane;
	public GameObject sunnyPlane;
	public GameObject room;

	// Skyboxes
	public Material nightSkybox;
	public Material sunnySkybox;
	public Material snowTimeSkybox;


	// On start, make default scene
	void Start()
	{
		camHTC.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
		RenderSettings.fog = false;
		RenderSettings.skybox = sunnySkybox;
		lightning.SetActive(false);
		lightningRoom.SetActive(true);
		nightPlane.SetActive(false);
		sunnyPlane.SetActive(false);
		room.SetActive(true);
	}

	// Night sky scene
	public void NightSky()
	{
		camHTC.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
		RenderSettings.fog = false;
		RenderSettings.skybox = nightSkybox;
		lightning.SetActive(true);
		lightningRoom.SetActive(false);
		lightning.GetComponent<Light>().intensity = 0.75f;
		nightPlane.SetActive(true);
		sunnyPlane.SetActive(false);
		room.SetActive(false);
	}

	// Sunny sky scene
	public void SunnySky()
	{
		camHTC.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
		RenderSettings.fog = false;
		RenderSettings.skybox = sunnySkybox;
		lightning.SetActive(true);
		lightningRoom.SetActive(false);
		lightning.GetComponent<Light>().intensity = 1f;
		nightPlane.SetActive(false);
		sunnyPlane.SetActive(true);
		room.SetActive(false);
	}

	// Room scene
	public void Room()
	{
		camHTC.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
		RenderSettings.fog = false;
		RenderSettings.skybox = sunnySkybox;
		lightning.SetActive(false);
		lightningRoom.SetActive(true);
		nightPlane.SetActive(false);
		sunnyPlane.SetActive(false);
		room.SetActive(true);
	}

}
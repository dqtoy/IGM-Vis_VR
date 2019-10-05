using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class Movement : MonoBehaviour
{
	public bool VR = false;
	float rotationY = 0, sensitivity = 2f;
	[Range(0f, 1f)] public float speed;
	public GameObject laser;
	Vector3 target;
	GalaxyScript gs;

	[Header("VR Settings")]
	public GameObject rig;
	public GameObject SteamVR;
	Camera cam;
	public SteamVR_Input_Sources handType;
	public SteamVR_Behaviour_Pose controller;
	public SteamVR_Action_Vector2 stick;
	public SteamVR_Action_Boolean padClickAction;
	public SteamVR_Action_Boolean triggerAction;
	public SteamVR_Action_Boolean gripAction;
	// Start is called before the first frame update
	void Start()
	{
		Camera.main.transform.gameObject.SetActive(!VR);
		XRSettings.enabled = VR;
		rig.SetActive(VR);
		SteamVR.SetActive(VR);
		cam = VR ? rig.GetComponentInChildren<Camera>() : Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 axis = VR ? Trackpad() : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		bool click = VR ? GetClick() : Input.GetKey(KeyCode.LeftShift);
		bool trigger = VR ? GetTrigger() : Input.GetButton("Fire1");
		bool grip = VR ? GetGrip() : Input.GetButton("Fire2");

		if (!VR)
		{
			float rotationX = cam.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			// rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
			cam.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}

		if (axis.x != 0 || axis.y != 0)
			// (VR?rig.transform:cam.transform).Translate(cam.transform.forward * axis.y * (click?5:speed) + cam.transform.right * axis.x * (click?5:speed) / 2f);

			if (VR)
				rig.transform.Translate(cam.transform.forward * axis.y * (click ? 20 * Time.deltaTime : speed) + cam.transform.right * axis.x * (click ? 20 * Time.deltaTime : speed) / 2f);
			else
				cam.transform.Translate(cam.transform.parent.forward * axis.y * (click ? 20 * Time.deltaTime : speed) + cam.transform.parent.right * axis.x * (click ? 20 * Time.deltaTime : speed) / 2f);

		if (trigger)
		{
			Debug.Log("Trigger button pressed");
		}

		if (grip)
		{
			Debug.Log("Grip button pressed");
		}

		if (click) Debug.Log("Click");

		RaycastHit hit;
		if (VR ? Physics.Raycast(controller.transform.position, controller.transform.forward, out hit, 100) : Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
		{
			laser.transform.position = Vector3.Lerp(controller.transform.position, hit.transform.position, .5f);
			laser.transform.LookAt(hit.transform.position);
			laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
			laser.SetActive(true);
			//Debug.Log(hit.transform.gameObject.name);
			if (hit.transform.gameObject.tag == "Galaxy" && trigger)
			{
				//gs.active = false;
				gs = hit.transform.gameObject.GetComponent<GalaxyScript>();
				gs.active = true;
			}
		}
		else laser.SetActive(false);
	}

	public bool GetClick() { return padClickAction.GetState(handType); }
	public bool GetTrigger() { return triggerAction.GetStateDown(handType); }
	public bool GetGrip() { return gripAction.GetState(handType); }
	public Vector2 Trackpad() { return stick.axis; }
}

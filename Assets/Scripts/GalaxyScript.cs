using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyScript : MonoBehaviour
{
	public bool active;
	public GameObject UI;
	public Transform cam;
	public Spawn.Galaxy data;

	// Update is called once per frame
	void Update()
	{
		transform.LookAt(cam);
		if (active)
		{
			Debug.Log(name);
			UI.SetActive(true);
			UI.transform.position = transform.position;
			TextMesh[] components = UI.transform.GetComponentsInChildren<TextMesh>();
			components[0].text = "NSAID: " + data.nsaid.ToString();
			components[1].text = "Position: " + data.pos.ToString();
			components[2].text = "Coords: (Distance:" + data.rho.ToString() + ", RA:" + data.theta.ToString() + ", DEC:" + data.phi.ToString()+")";
			components[3].text = "Size: " + data.size.ToString();
			components[4].text = "Star Mass: " + data.mstars.ToString();
			components[5].text = "SFR: " + data.sfr.ToString();
			components[6].text = "logSFR: " + data.log_sfr.ToString();
			components[7].text = "SFR Error: " + data.sfr_err.ToString();
			UI.transform.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("new/" + data.nsaid);
			active = false;
		}
		UI.transform.LookAt(cam);

	}
}

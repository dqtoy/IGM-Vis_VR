using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[System.Serializable]

public class Spawn : MonoBehaviour
{
	string line;
	[SerializeField] bool spawn = true, lines = false, bigSet = false;
	[SerializeField] float scale = 1f;
	[SerializeField] GameObject GW, ps, UI;
	[SerializeField] Material red, blue;
	List<Galaxy> galaxies = new List<Galaxy>();
	public static Dictionary<int, List<Galaxy>> filaments = new Dictionary<int, List<Galaxy>>();

	public struct Galaxy
	{
		public int nsaid;
		public float rho, theta, phi;
		public float size;
		public Vector3 pos;
		public Color color;
		public string mstars, sfr, log_sfr, sfr_err;
		public int filament;

		public Galaxy(int id, float r, float t, float p, float s, Vector3 v, Color c, string m, string rate, string logRate, string rateError, int f)
		{
			nsaid = id;
			rho = r;
			theta = t;
			phi = p;
			size = s;
			pos = v;
			color = c;
			mstars = m;
			sfr = rate;
			log_sfr = logRate;
			sfr_err = rateError;
			filament = f;
		}
	}

	float parse(string s) { return float.Parse(s, CultureInfo.InvariantCulture.NumberFormat); }
	void Start()
	{
		System.IO.StreamReader file = new System.IO.StreamReader(@"./Assets/galaxies" + (bigSet ? "2" :"") + ".txt");
		while ((line = file.ReadLine()) != null)
		{
			string[] data = line.Split(' ');
			if (data[0] == "NSAID")
				continue;
			float theta = parse(data[1]) * Mathf.PI / 180.0f;
			float phi = parse(data[2]) * Mathf.PI / 180.0f;
			float rho = parse(data[3]) * 1000;
			float size = Mathf.Log(parse(data[7])) * scale;
			int fil = (int)parse(data[10]);
			Vector3 coords = new Vector3(rho * Mathf.Cos(theta) * Mathf.Sin(phi), rho * Mathf.Sin(theta) * Mathf.Cos(phi), rho * Mathf.Cos(phi));
			Galaxy g = new Galaxy((int)parse(data[0]), rho, theta, phi, size, coords,
			(data[9] == "blue" ? Color.blue : Color.red),
			data[4],
			data[5],
			data[8],
			data[6], fil);
			// Debug.Log(coords);
			galaxies.Add(g);
			bool hasKey = false;
			foreach(int k in filaments.Keys)
				if(k == fil){
					filaments[k].Add(g);
					hasKey = true;}
			if(!hasKey){
				filaments.Add(fil,new List<Galaxy>(){g});
				}
			
		}

		if (spawn)
			foreach (Galaxy g in galaxies)
			{
				// GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				GameObject obj = Instantiate(ps);
				GalaxyScript gs = obj.GetComponent<GalaxyScript>();
				gs.data = g;
				gs.UI = UI;
				// obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/" + g.nsaid);
				obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("default");
				obj.GetComponent<SpriteRenderer>().material.color = g.color;
				obj.SetActive(true);
				// ParticleSystem.MainModule m = obj.GetComponent<ParticleSystem>().main;
				// m.startColor = g.color;
				obj.transform.SetParent(GW.transform);
				obj.name = g.nsaid.ToString();
				obj.transform.position = g.pos;
				obj.transform.localScale *= g.size;
				// obj.GetComponent<ParticleSystem>().Pause();
				// StartCoroutine(AwaitAndPause(.5f, obj.GetComponent<ParticleSystem>()));
				// obj.GetComponent<MeshRenderer>().material = g.color == Color.blue ? blue : red;
			}

		if(lines)
			GetComponent<Lines>().Filaments(galaxies);
	}

	IEnumerator AwaitAndPause(float t, ParticleSystem ps)
	{
		yield return new WaitForSeconds(t);
		ps.Pause();
	}
}

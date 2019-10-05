using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[System.Serializable]
public class Quasar : MonoBehaviour
{
	string line;
	[SerializeField] GameObject ray;
	[SerializeField] Transform wrapper;
	[SerializeField] float weight = 1, startDist = 5, endDist = 25;
	List<Ray> rays = new List<Ray>();
	List<Spectrum> spectra = new List<Spectrum>();
	float parse(string s) { return float.Parse(s, CultureInfo.InvariantCulture.NumberFormat); }

	public struct Spectrum{
		public Ray ray;
		public Vector3 start, end, dir;
		public string name;
		// public Image bands;
		public Spectrum(Ray r, Vector3 d, Vector3 i, Vector3 f, string n){
			ray = r;
			dir = d;
			start = i;
			end = f;
			name = n;
		}
	}
	void Start()
	{
		System.IO.StreamReader file = new System.IO.StreamReader(@"./Assets/quasar.txt");
		while ((line = file.ReadLine()) != null)
		{
			string[] data = line.Split(' ');
			if (data[0] == "name")
				continue;
			float theta = parse(data[7]) * Mathf.PI / 180.0f;
			float phi = parse(data[8]) * Mathf.PI / 180.0f;
			Vector3 coords = new Vector3(Mathf.Cos(theta) * Mathf.Sin(phi), Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(phi));
			// Debug.Log(coords);
			Ray r = new Ray(Vector3.zero, coords);
			spectra.Add(new Spectrum(r, coords, r.GetPoint(startDist), r.GetPoint(endDist), data[0]));
		}
		foreach (Spectrum s in spectra)
		{
			GameObject rayGO = Instantiate(ray, Vector3.zero, Quaternion.identity, wrapper);
			rayGO.name = s.name;
			rayGO.SetActive(true);
			LineRenderer lr = ray.GetComponent<LineRenderer>();
			lr.widthMultiplier *= weight;
			lr.SetPosition(0, s.start);
			lr.SetPosition(1, s.end);
		}
		Destroy(wrapper.GetChild(0).gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}
}

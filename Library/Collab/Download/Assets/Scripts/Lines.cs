using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DigitalRuby.FastLineRenderer;


[System.Serializable]
public class Lines : MonoBehaviour
{
    [SerializeField] FastLineRenderer LineRenderer;
    [SerializeField] bool useUnity = true;
    [SerializeField] Transform lines;
    [SerializeField] Material white;
    [SerializeField] float radius = 1;
    // Start is called before the first frame update
    public void Filaments(List<Spawn.Galaxy> galaxies)
    
    {
        FastLineRendererProperties props = new FastLineRendererProperties();
        props.GlowIntensityMultiplier = 0.5f;
        props.Radius = .01f;
        props.Color = UnityEngine.Color.cyan;
                    
        // foreach(Spawn.Galaxy g in galaxies){
		// 	Vector3 pos = g.pos;
		// 	List<Vector3> neighbours = new List<Vector3>();
		// 	foreach(Spawn.Galaxy other in galaxies)
		// 		if(!other.nsaid.Equals(g.nsaid))
		// 			if((pos-other.pos).sqrMagnitude<radius*radius)
		// 				neighbours.Add(other.pos);
		// 	foreach(Vector3 v in neighbours){
        //         if(neighbours.Count>2)
        //         if(useUnity){
        //             GameObject lrgo = new GameObject();
        //             LineRenderer lr = lrgo.AddComponent<LineRenderer>();
        //             lr.widthMultiplier = .01f;
        //             lr.SetPosition(0, pos);
        //             lr.SetPosition(1, v);
        //             lr.material = white;
        //             lrgo.transform.SetParent(lines);
        //             }
        //         else{
        //             props.Start = v;
        //             props.End = pos;
        //             LineRenderer.AddLine(props);
        //             // LineRenderer.AppendLine(props);
        //         }
		// 	}
        //     if(!useUnity)
        //         LineRenderer.Apply();


        //     // Collider[] neighbours = Physics.OverlapSphere(g.position, radius, 1<<9);
        //     // Debug.Log(neighbours.Length > 0 ? g.name : "0");
        //     // foreach(Collider c in neighbours){
        //     //     GameObject lrgo = new GameObject();
        //     //     lrgo.transform.SetParent(g.transform);
        //     //     LineRenderer lr = lrgo.AddComponent<LineRenderer>();
        //     //     lr.widthMultiplier = .1f;
        //     //     lr.SetPosition(0, g.position);
        //     //     lr.SetPosition(1, c.transform.position);
        //     //     lr.material = white;
        //     //     lr.transform.SetParent(c.transform);
        //     // }
        // }
        foreach(KeyValuePair<int, List<Spawn.Galaxy>> kvp in Spawn.filaments){
            List<Spawn.Galaxy> draw = new List<Spawn.Galaxy>();
            draw = kvp.Value.OrderBy(f=>f.pos.sqrMagnitude).ToList();
            
            for(int i=0;i<draw.Count-1;i++){
                props.Start = draw[i].pos;
                props.End = draw[i+1].pos;
                LineRenderer.AppendLine(props);
                }
                LineRenderer.Apply();
        }
    }
}

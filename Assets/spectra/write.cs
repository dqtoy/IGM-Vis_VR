using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class write : MonoBehaviour
{
	string line;

    void Start()
    {
        System.IO.StreamReader file = new System.IO.StreamReader(@"./Assets/quasar.txt");
        System.IO.StreamWriter output = new System.IO.StreamWriter(@"./Assets/spectra/out.txt");
		while ((line = file.ReadLine()) != null)
		{
            System.IO.StreamReader data;
			string name = line.Split(' ')[0];
            string amp = "", read;
            if(name.Equals("name"))
                continue;
            try{
                data = new System.IO.StreamReader(@"C:/Users/DSCommons/Documents/azyre/GitHub/Intergalactic/data/partial/spectra_HI_partial_norm/" + name + ".dat");
            }
            catch{
                continue;
            }
            while((read = data.ReadLine()) != null){
                if(read.Contains("x"))
                    continue;
                amp += read.Split(' ')[5] + " ";
            }
            // Debug.Log(data.ReadLine());

            output.Write(name);
            // Debug.Log(name);
            output.WriteLine(" " + amp);
		}

    }

}

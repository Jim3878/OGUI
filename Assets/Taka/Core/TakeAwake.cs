using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwake : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        foreach (PlatHandler plat in transform.GetComponentsInChildren<PlatHandler>())
        {
            plat.Initialize();
        }
	}
	
}

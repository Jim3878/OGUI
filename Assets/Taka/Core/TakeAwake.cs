using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAwake : MonoBehaviour {
    public List<PlatHandler> platHandlerList;
	// Use this for initialization
	void Awake () {

        foreach (PlatHandler plat in platHandlerList)
        {
            plat.Initialize();
        }
	}
	
}

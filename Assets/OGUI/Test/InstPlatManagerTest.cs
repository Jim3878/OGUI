using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstPlatManagerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var m= PlatManager.Instance();
        m.AddPlatContent(new PlatContent(0, null));
        m.AddPlatContent(new PlatContent(1, null));
        m.AddPlatContent(new PlatContent(2, null));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pista : MonoBehaviour {

    public Transform p1, p2;

    //private int[] pistasPos = { 0, 1 };

    //
 
	void Update () {
        p1.Translate(Vector3.down * Time.deltaTime * LevelGenerator.Instance.speed);
        p2.Translate(Vector3.down * Time.deltaTime * LevelGenerator.Instance.speed);

        if(p1.position.y <= -12.8f)
        {
            p1.transform.position = new Vector3(0, 12.8f, 0);
        }
        if (p2.position.y <= -12.8f)
        {
            p2.transform.position = new Vector3(0, 12.8f, 0);
        }
    }
	
}

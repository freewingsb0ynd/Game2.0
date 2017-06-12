using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
    public GameObject obstaclePrefab;
    public Camera Camera;

    private int screenHalfHeight;
    private int jumpHeight = 170;
    private int firstObsPos = -373;
    // Use this for initialization
    void Start () {
        screenHalfHeight = (int) Camera.orthographicSize;

        for (int obsPos = firstObsPos; obsPos < screenHalfHeight-jumpHeight; obsPos += jumpHeight)
        {
            GameObject newObtacle = Instantiate(obstaclePrefab,
                                                new Vector3(Random.Range(-270f, 185f), obsPos, 0),
                                                Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

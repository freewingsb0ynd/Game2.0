using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
    public GameObject obstaclePrefab;

    private int currentPlatformIndex = 0;
    private int screenHalfHeight;
    private int screenHalfWidth;
    private int jumpHeight = 170;
    private int firstObsPos = -232;
    private int obsRangeX = 210;
    private int highestObsPos;
    private List<GameObject> obsPool = new List<GameObject>();

    // Use this for initialization
    void Start () {
        screenHalfHeight = (int) Camera.main.orthographicSize;
        screenHalfWidth = (int) (screenHalfHeight * Camera.main.aspect) ;

        for (int obsPos = firstObsPos; obsPos < screenHalfHeight; obsPos += jumpHeight)
        {
            CreateNewObsIfNeeded();
            highestObsPos = obsPos;
        }
        
    }

    private void CreateNewObsIfNeeded()
    {
        while (currentPlatformIndex * jumpHeight + firstObsPos < Camera.main.transform.position.y + Camera.main.orthographicSize)
        {
            GameObject newPlatform = GetNewObs();
            newPlatform.transform.position = new Vector3(Random.Range(-obsRangeX, obsRangeX), 
                                                        currentPlatformIndex * jumpHeight + firstObsPos,
                                                        0);
            newPlatform.SetActive(true);

            currentPlatformIndex++;
        }
    }

    private GameObject GetNewObs()
    {
        foreach (GameObject platform in obsPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }

        GameObject newPlatform = Instantiate(
            obstaclePrefab,
            Vector3.zero,
            Quaternion.identity
        );

        obsPool.Add(newPlatform);

        return newPlatform;
    }

    // Update is called once per frame
    void Update () {
        CreateNewObsIfNeeded();

        foreach (GameObject obs in obsPool)
        {
            if (obs.transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
            {
                obs.SetActive(false);
            }
        }
    }
}

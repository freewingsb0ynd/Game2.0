using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
    public GameObject obstaclePrefab;
    public int firstObsPosY;
    public int obsSpaceY;
    

    private int currentPlatformIndex = 0;
    private int screenHalfHeight;
    private int obsRangeX = 210;
    private List<GameObject> obsPool = new List<GameObject>();
    private Transform[] childrenTransformList;

    // Use this for initialization
    void Start () {
        screenHalfHeight = (int) Camera.main.orthographicSize;

        for (int obsPos = firstObsPosY; obsPos < screenHalfHeight; obsPos += obsSpaceY)
        {
            CreateNewObsIfNeeded();
        }
        
    }

    private void CreateNewObsIfNeeded()
    {
        while (currentPlatformIndex * obsSpaceY + firstObsPosY < Camera.main.transform.position.y + Camera.main.orthographicSize)
        {
            GameObject newPlatform = GetNewObs();
            newPlatform.transform.position = new Vector3(Random.Range(-obsRangeX, obsRangeX), 
                                                        currentPlatformIndex * obsSpaceY + firstObsPosY,
                                                        0);
            newPlatform.SetActive(true);

            currentPlatformIndex++;
        }
    }

    private GameObject GetNewObs()
    {
        foreach (GameObject obstacle in obsPool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }

        GameObject newObstacle = Instantiate(
            obstaclePrefab,
            Vector3.zero,
            Quaternion.identity
        );

        obsPool.Add(newObstacle);

        return newObstacle;
    }

    // Update is called once per frame
    void Update () {
        CreateNewObsIfNeeded();

        foreach (GameObject obs in obsPool)
        {
            if (obs.transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
            {
                childrenTransformList = obs.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in childrenTransformList)
                {
                    if (t.tag == "Increase Score") t.gameObject.SetActive(true);
                }
                obs.SetActive(false);
            }
        }
    }
}

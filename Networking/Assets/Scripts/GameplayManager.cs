using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {
    public static GameplayManager Instance { get; private set; }

    public List<PlanetInfo> planetSeeds;
    public GameObject planetPrefab;
    public Transform planetsContainer;

    [HideInInspector]
    public List<PlanetController> planets;

    private void Awake()
    {
        Instance = this;

        foreach(PlanetInfo info in planetSeeds)
        {
            PlanetController newPlanet = TKUtils.Instantiate<PlanetController>(planetPrefab, planetsContainer);
            newPlanet.Init(info);
            planets.Add(newPlanet);
        }
    }
}

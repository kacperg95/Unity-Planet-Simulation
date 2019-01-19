using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tworzenie i usuwanie planet
/// </summary>
public static class PlanetManager
{ 
    public static IMovingPlanet Create()
    {
        var planetPrefab = GameObject.Instantiate(Resources.Load("Prefabs/Planet")) as GameObject;
        //var planetPrefab = UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/Planet")) as GameObject;
        var planet = planetPrefab.GetComponent<Planet>();
        planet.PlanetName = PlanetNameChecker.Check("Planet");
        planet.SunDistance = 220f;
        planet.Size = 1f;
        planet.Material = "Blue";
        PlanetData.AddPlanet(planet);
        return planet;
    }
	
    public static void Destroy(IMovingPlanet planet)
    {
        PlanetData.RemovePlanet(planet);
        Object.Destroy(planet.gameObject);
        PlanetData.SelectedPlanet = null;
        Factory.GetUIController().RefreshSelectPlanetDropdown();
    }

}

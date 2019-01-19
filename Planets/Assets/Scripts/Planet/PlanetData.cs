using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlanetData
{
    public static List<IMovingPlanet> Planets = new List<IMovingPlanet>(); //Lista planet na scenie
    public static Planet SelectedPlanet; //Aktualnie zaznaczona planeta

    public static Quaternion previousPlanetRotation;

    /// <summary>
    /// Funkcja dodająca planetę na listę
    /// </summary>
    /// <param name="planet"></param>
    public static void AddPlanet(IMovingPlanet planet)
    {
        Planets.Add(planet);
    }

    /// <summary>
    /// Funkcja usuwająca planetę z listy
    /// </summary>
    /// <param name="planet"></param>
    public static void RemovePlanet(IMovingPlanet planet)
    {
        Planets.Remove(planet);
    }


    //Funkcja tymczasowa do testów
    public static void FillPlanets(IMovingPlanet[] planets)
    {
        Planets.AddRange(planets);
    }

    /// <summary>
    /// Zapisuje prędkości wszystkich planet
    /// </summary>
    public static void SaveVelocities()
    {
        foreach (IPlanet planet in PlanetData.Planets)
            planet.PreviousPlanetVelocity = planet.gameObject.GetComponent<Rigidbody>().velocity;
    }

    /// <summary>
    /// Zapisuje rotacje planety
    /// </summary>
    public static void SaveRotation(IMovingPlanet planet)
    {
        previousPlanetRotation = planet.transform.rotation;
    }

}

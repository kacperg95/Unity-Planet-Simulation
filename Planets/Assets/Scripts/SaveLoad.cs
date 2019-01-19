using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Klasa do zapisu i wczytywania układu słonecznego
/// </summary>
public static class SaveLoad {

    public static void Save(string saveName)
    {
        using (StreamWriter sw = new StreamWriter(saveName + ".txt"))
        {
            foreach(IMovingPlanet planet in PlanetData.Planets)
            {
                sw.WriteLine(planet.PlanetName);
                sw.WriteLine(planet.Size);
                sw.WriteLine(planet.Mass);
                sw.WriteLine(planet.Speed);
                sw.WriteLine(planet.SunDistance);
                sw.WriteLine(planet.Material);
            }
        }
    }


    public static void Load(string saveName)
    {
        if (!File.Exists(saveName + ".txt"))
            return;

        //Czyszczenie układu z planet
        while (PlanetData.Planets.Count != 0)
            PlanetManager.Destroy(PlanetData.Planets[0]);

        using (StreamReader sr = new StreamReader(saveName + ".txt"))
        {
            while(!sr.EndOfStream)
            {
                IMovingPlanet planet = PlanetManager.Create();
                planet.PlanetName = sr.ReadLine();
                planet.Size = float.Parse(sr.ReadLine());
                planet.Mass = float.Parse(sr.ReadLine());
                planet.Speed = float.Parse(sr.ReadLine());
                planet.SunDistance = float.Parse(sr.ReadLine());
                planet.Material = sr.ReadLine();


            }
        }

        Factory.GetUIController().RefreshSelectPlanetDropdown();

    }


}

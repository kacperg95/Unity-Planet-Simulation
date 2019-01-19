using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlanet
{
    string PlanetName { get; set; } // Nazwa
    float Mass { get; set; } // Masa
    float Speed { get; set; }
    float SunDistance { get; set; } //Odległość od słońca, w tym przypadku zawsze równa zero
    Vector3 PreviousPlanetVelocity { get; set; }

    GameObject gameObject { get; }
    Transform transform { get;}
    T GetComponent<T>();
}

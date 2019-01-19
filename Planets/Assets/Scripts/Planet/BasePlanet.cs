using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlanet : MonoBehaviour, IPlanet
{


    #region Zmienne

    //Planeta
    private string planetName; // Nazwa
    private float mass; // Masa
    private float speed; //Prędkość
    private float sunDistance; //Odległość od słońca
    private Vector3 previousPlanetVelocity = Vector3.zero; //Prędkość planety przed tym jak symulacja została zapauzowana

    // const float G = 667.4f; //Stała grawitacyjna

    #endregion

    #region Properties
    public string PlanetName
    {
        get
        {
            return planetName;
        }
        set
        {
            planetName = value;
        }
    }

    public float Mass
    {
        get
        {
            return mass;
        }
        set
        {
            mass = value;
            gameObject.GetComponent<Rigidbody>().mass = mass;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
   

    public float SunDistance
    {
        get
        {
            return sunDistance;
        }
        set
        {
            sunDistance = value;
            PlanetPositioner.SetPosition(this, value);
        }
    }

    public Vector3 PreviousPlanetVelocity
    {
        get
        {
            return previousPlanetVelocity;
        }
        set
        {
            previousPlanetVelocity = value;
        }

    }

    #endregion


    protected virtual void FixedUpdate()
    {
        //Jeżeli program jest w trybie symulacyjnym to planety oddziałują na siebie
        if (Game.State == GameState.Simulation)
        {
            PlanetAttractor.AttractPlanets(this);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingPlanet: IPlanet
{ 
    float Size {get; set; }
    string Material { get; set; }
}

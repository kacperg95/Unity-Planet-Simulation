using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LoadSun();
        PlanetColliders.Enable(false);

        Game.SetState(GameState.Creative);
        
        //Odpalanie menu na starcie
        MenuSwitcher.Switch(MenuState.Free);
    }

    private void LoadSun()
    {
        ISun sun = GameObject.FindObjectOfType<Sun>();
        sun.PlanetName = "Sun";
        sun.Mass = 10000f;
    }

}

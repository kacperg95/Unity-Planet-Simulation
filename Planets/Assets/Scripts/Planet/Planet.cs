using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : BasePlanet, IMovingPlanet {


    #region Zmienne

    //Planeta
    private float size; //rozmiar planety
    private string material; //Materiał nałożony na planetę

    #endregion

    #region Properties

    public float Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
            gameObject.transform.localScale = new Vector3(size, size, size);
        }
    }

    public string Material
    {
        get
        {
            return material;
        }
        set
        {
            material = value;
            PlanetMaterial.Set(this, value);
        }
    }

    #endregion


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Metoda wywołująca się gdy najedziemy kursorem na planetę
    /// </summary>
    void OnMouseDown()
    {
        if (Game.State != GameState.Creative)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            if(UIController.menuState == MenuState.Free)
            {
                CameraFocus.OnPlanet(this, true);
            }
        }
    }

}

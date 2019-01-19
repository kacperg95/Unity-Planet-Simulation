using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    private CameraState state = CameraState.Free; //Tryb kamery
    public CameraState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public void Update()
    {   
        if(Input.GetKey(KeyCode.Q))
        {
            CameraFocus.Unfocus();
        }

        //Tryb swobodny
        if (state == CameraState.Free)
        {
            CameraMovement.Move();
        }

        //Jeżeli kamera jest focusowana na planetę albo odfocusowywana
        if(state == CameraState.Focusing || state == CameraState.Unfocusing)
        {
           CameraFocus.Focusing();
        }
    }

    #region Helpery

    /// <summary>
    /// Funkcja sprawdzająca czy kamera dotarła do celu
    /// </summary>
    /// <param name="cameraPosition">Obecne pozycja kamery</param>
    /// <param name="target">Pozycja celu</param>
    /// <returns></returns>
    public bool CameraReachedTarget(Vector2 cameraPosition, Vector2 target, Quaternion cameraRotation, Quaternion targetRotation)
    {
        Vector2 distance = cameraPosition - target;

        if (cameraRotation != targetRotation)
            return false;

        if (distance.magnitude <= CameraData.MarginOfError)
            return true;
        else
            return false;

    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Odpowiada za poruszanie kamerą
/// </summary>
public static class CameraMovement {

    private static CameraScript camera = Factory.GetCamera();

    #region Tryb swobodny
    private static float rotationX = 0.0f;
    private static float rotationY = 0.0f;
    private static float speedMultiplier;
    #endregion


    /// <summary>
    /// Poruszanie kamerą
    /// </summary>
    public static void Move()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotationX += Input.GetAxis("Mouse X") * CameraData.Sensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * CameraData.Sensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);

            camera.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            camera.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);


        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            speedMultiplier = 2.5f;
        else
            speedMultiplier = 1.0f;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            camera.transform.position = camera.transform.position + (-camera.transform.right * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            camera.transform.position = camera.transform.position + (camera.transform.right * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            camera.transform.position = camera.transform.position + (camera.transform.forward * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            camera.transform.position = camera.transform.position + (-camera.transform.forward * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            camera.transform.position = camera.transform.position + (camera.transform.up * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            camera.transform.position = camera.transform.position + (-camera.transform.up * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            camera.transform.position = camera.transform.position + (Vector3.up * CameraData.Speed * speedMultiplier * Time.deltaTime);
        }
    }





}

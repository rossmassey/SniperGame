using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera with mouse
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [Header("Camera settings")]
    public float cameraSensitivity = 5f;
    public float cameraSmoothing = 2f;
    public float cameraFOV = 80f;

    float cameraScale;
    Transform playerTransform;
    Vector2 mouseLook;
    Vector2 smoothVector;

    void Start()
    {
        SetCameraScale(1.0f);
        playerTransform = this.transform.parent.gameObject.transform;
        Camera.main.fieldOfView = cameraFOV;
    }

    void Update()
    {
        CalculateMouseLook();
        ApplyTransforms();
    }

    /// <summary>
    /// Changes the scale of the camera sensitivity (to be used when zooming in)
    /// </summary>
    /// <param name="cameraSensitivityScale">Scale of camera sensitivty setting to use</param>
    public void SetCameraScale(float cameraSensitivityScale)
    {
        cameraScale = (cameraSensitivityScale * cameraSensitivity) * cameraSmoothing;
    }

    private void ApplyTransforms()
    {
        // rotate camera up and down
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        // rotate player left and right
        playerTransform.localRotation = Quaternion.AngleAxis(mouseLook.x, playerTransform.up);
    }

    private void CalculateMouseLook()
    {
        Vector2 mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseChange = Vector2.Scale(mouseChange, new Vector2(cameraScale, cameraScale));
        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseChange.x, 1f / cameraSmoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseChange.y, 1f / cameraSmoothing);
        mouseLook += smoothVector;
    }
}


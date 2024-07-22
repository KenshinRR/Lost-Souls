using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    private bool onLook = true;

    private void OnGameEnd() {
        Cursor.lockState = CursorLockMode.None;
        this.onLook = false;
    }

    private void Look() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_FOUND, this.OnGameEnd);
        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_TIMEOUT, this.OnGameEnd);
    }

    private void OnDestroy() {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameOver_Events.ON_FOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameOver_Events.ON_TIMEOUT);
    }

    void Update()
    {
        if(this.onLook) {
            this.Look();
        }
    }
}
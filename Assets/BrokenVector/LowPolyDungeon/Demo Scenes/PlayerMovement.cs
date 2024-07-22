using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 5f;

    float speedBoost = 1f;
    Vector3 velocity;

    private bool onMove = true;
    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity.y += gravity * Time.deltaTime;

        if (Input.GetButton("Fire3"))
            speedBoost = sprintSpeed;
        else
            speedBoost = 1f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnGameEnd() {
        this.onMove = false;
    }
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_TIMEOUT, this.OnGameEnd);
        EventBroadcaster.Instance.AddObserver(EventNames.GameOver_Events.ON_FOUND, this.OnGameEnd);
    }

    private void OnDestroy() {
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameOver_Events.ON_FOUND);
        EventBroadcaster.Instance.RemoveObserver(EventNames.GameOver_Events.ON_TIMEOUT);
    }

    void Update()
    {
        if (this.onMove) {
            this.Move();
        }
    }
}

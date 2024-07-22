using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private CharacterController PB;
    [SerializeField] private float playerSpeed = 12.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float sprintSpeed = 5.0f;

    private bool onMove = true;
    private float speedBoost = 1f;
    private Vector3 velocity;
   
    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        this.velocity.y += this.gravity * Time.deltaTime;

        if (Input.GetButton("Left Shift"))
            this.speedBoost = this.sprintSpeed;
        else
            this.speedBoost = 1f;

        if (this.PB.isGrounded && this.velocity.y < 0) {
            this.velocity.y = -2f;
        }

        Vector3 move = this.transform.right * x + this.transform.forward * z;

        this.PB.Move(move * (this.playerSpeed + this.speedBoost) * Time.deltaTime);

        this.PB.Move(this.velocity * Time.deltaTime);
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

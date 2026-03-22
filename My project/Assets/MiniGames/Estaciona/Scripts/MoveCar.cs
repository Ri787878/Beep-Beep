using System;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    [Header("Speed Settings")]
    public float forwardSpeed = 20f;
    public float backwardSpeed = 10f;

    [Header("Handling")]
    public float acceleration = 15f;
    public float deceleration = 20f;
    public float turnSpeed = 120f;

    [Header("Braking")]
    public float brakeForce = 50f;

    private float currentSpeed;
    private Rigidbody rb;

    private float verticalInput;
    private float horizontalInput;
    private bool isBraking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Helps prevent the car from tipping/sliding unrealistically
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleSteering();
        HandleBraking();
    }

    void HandleMovement()
    {
        if (isBraking) return;

        float targetSpeed = 0f;

        if (verticalInput > 0)
            targetSpeed = forwardSpeed * verticalInput;
        else if (verticalInput < 0)
            targetSpeed = -backwardSpeed * Mathf.Abs(verticalInput);
        
        float rate = (verticalInput != 0) ? acceleration : deceleration;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, rate * Time.fixedDeltaTime);
        
        Vector3 forwardVelocity = transform.forward * currentSpeed;
        
        forwardVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = forwardVelocity;
    }

    void HandleSteering()
    {
        float speed = rb.linearVelocity.magnitude;
        if (speed < 0.5f) return;

        float speedFactor = Mathf.Clamp01(speed / forwardSpeed);
        
        float direction = Vector3.Dot(rb.linearVelocity, transform.forward) >= 0 ? 1f : -1f;

        float turnAmount = horizontalInput * turnSpeed * speedFactor * direction * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void HandleBraking()
    {
        if (!isBraking) return;
        
        rb.linearVelocity = Vector3.MoveTowards(
            rb.linearVelocity,
            new Vector3(0, rb.linearVelocity.y, 0),
            brakeForce * Time.fixedDeltaTime
        );

        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakeForce * Time.fixedDeltaTime);
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 0)
        {
            var randomIndex = UnityEngine.Random.Range(0, audioSources.Length);
            
            AudioSource collisionSound = audioSources[randomIndex];
            collisionSound.Play();
            collisionSound.playOnAwake = true;

        }
    }*/
}
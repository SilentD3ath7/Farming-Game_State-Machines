using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    int moveSpeed = 0;
    public int walkSpeed = 10;
    public int sprintSpeed = 20;
    public float groundDrag = 5f, jumpForce = 10f, newJumpForce = 5f;

    [Header("Orientation")]
    public float sensX = 400f;
    public float sensY = 400f;
    float xRotation = 0, yRotation = 0, moveX, moveY;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Game Object Refrences")]
    public GameObject wateringCan;
    public TextMeshProUGUI wateringCanText, winText, bankAccountText;
    public Transform cam, playerOrientation;

    Vector3 moveDir;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        playerInput();
        SpeedControl();

        // Handle drag
        if(grounded) { rb.drag = groundDrag; }
        else { rb.drag = 0; }
    }

    public void playerInput()
{
    moveX = Input.GetAxisRaw("Horizontal");
    moveY = Input.GetAxisRaw("Vertical");

    if (Input.GetKeyDown(KeyCode.Space) && grounded)
    {
        Jump();
    }
}
    private void FixedUpdate()
    {
        // Handles all movement
        Move();
    }

    public void Move()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX, mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
        moveDir = playerOrientation.forward * moveY + playerOrientation.right * moveX;

        // Move the player based on walking or sprinting
        if (Input.GetButton("Sprint"))
        {
            moveSpeed = sprintSpeed;
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
        }
        else
        {
            moveSpeed = walkSpeed;
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
        }
        cam.transform.position = this.transform.position;

        // Mouse input changes player cam rotation
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Set the rotations
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SpeedControl()
    {
        // Speed control
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        // Reset y vel
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            wateringCan.SetActive(true);
            EventManager.Instance.DisableText(wateringCanText);
            jumpForce = newJumpForce;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Win"))
        {
            EventManager.Instance.EnableTextWithTimer(winText, false);
            EventManager.Instance.EnableTextWithTimer(bankAccountText, false);
            Destroy(other.gameObject);
        }
    }
}

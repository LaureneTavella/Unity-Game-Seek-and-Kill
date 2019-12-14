using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float airSpeedMultiplier;
    public float moveTransitionSpeed;

    private Rigidbody rb;
    private Vector3 deltaPosition;
    private bool isGrounded = true;
    private float distanceToGround;
    public Vector3 targetMoveDelta;
    public Vector3 currentMoveDelta;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        distanceToGround = this.GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Raw : directly -1/0/1 without smooth.
        float vertical = Input.GetAxisRaw("Vertical");

        float targetSpeed = isGrounded ? speed : speed * airSpeedMultiplier;
        targetMoveDelta = (transform.right * horizontal + transform.forward * vertical).normalized * targetSpeed; // so we don't go faster in diagonal !
        currentMoveDelta = Vector3.Lerp(currentMoveDelta, targetMoveDelta, Time.deltaTime * moveTransitionSpeed); 

        UpdateGrounded();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentMoveDelta * Time.fixedDeltaTime);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
               rb.AddForce(transform.up * jumpForce);
    }

    private void UpdateGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float airSpeedMultiplier;

    private Rigidbody rb;
    private Vector3 newPos;
    private bool isGrounded = true;
    private float distanceToGround;

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
        newPos = new Vector3(horizontal, 0, vertical);

        UpdateGrounded();

        // TODO : smooth
    }

    private void FixedUpdate()
    {
        float realSpeed = isGrounded ? speed : speed * airSpeedMultiplier;

        newPos = newPos.normalized * realSpeed * Time.fixedDeltaTime; // so we don't go faster in diagonal !
        rb.MovePosition(transform.position + newPos);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
               rb.AddForce(transform.up * jumpForce);
    }

    private void UpdateGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}

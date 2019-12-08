using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Raw : directly -1/0/1 without smooth.
        float vertical = Input.GetAxisRaw("Vertical");
        newPos = new Vector3(horizontal, 0, vertical);

        // TODO : smooth
    }

    private void FixedUpdate()
    {
        newPos = newPos.normalized * speed * Time.fixedDeltaTime; // so we don't go faster in diagonal !
        rb.MovePosition(transform.position + newPos);        
    }
}

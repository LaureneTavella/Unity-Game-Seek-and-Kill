using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

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

        Vector3 tempVector = new Vector3(horizontal, 0, vertical);
        tempVector = tempVector.normalized * speed * Time.deltaTime; // so we don't go faster in diagonal !
        rb.MovePosition(transform.position + tempVector);

        // TODO : smooth
    }
}

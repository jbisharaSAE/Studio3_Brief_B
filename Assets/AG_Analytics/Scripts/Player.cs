using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 500;
    [SerializeField] Rigidbody rbody;

    private void Start()
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rbComponent))
        {
            rbody = rbComponent;
        }
    }

    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            rbody.AddForce(Vector3.up * jumpPower);
        }
    }
}

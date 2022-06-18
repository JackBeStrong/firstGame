using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask playerMask;

    private bool spaceKeyPressed;
    private float horizentalInput;
    private Rigidbody rigidBodyComponent;
    private int superJumps;


    // Start is called before the first frame update
    void Start()
    {
        spaceKeyPressed = false;
        horizentalInput = 0;
        rigidBodyComponent = GetComponent<Rigidbody>();
        superJumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceKeyPressed = true;
        }

        horizentalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        float jumpPower = 7f;
        if (spaceKeyPressed && Physics.OverlapSphere(groundCheck.position, 0.1f, playerMask).Length > 0)
        {
            if (superJumps > 0)
            {
                jumpPower *= 2;
                superJumps--;
            }
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            spaceKeyPressed = false;
        }
        rigidBodyComponent.velocity = new Vector3(horizentalInput, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumps++;
        }
    }

}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float velocity = 2000;
    public float lateralVelocity = 500;

    bool movedRight;
    bool movedLeft;
    bool shouldExplode;

    void Update()
    {
        if (Input.GetKey("d")) {
            movedRight = true;
        }

        if (Input.GetKey("a")) {
            movedLeft = true;
        }

        if (Input.GetKey("space")) {
            shouldExplode = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, velocity * Time.deltaTime);

        if (movedRight) {
            rb.AddForce(lateralVelocity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            movedRight = false;
        }

        if (movedLeft) {
            rb.AddForce(-1 * lateralVelocity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            movedLeft = false;
        }

        if (shouldExplode) {
            var currPos = rb.transform.position;
            rb.AddExplosionForce(10.0f, currPos, 10.0f);
            Debug.Log("Exploding!");
            shouldExplode = false;
        }

        if (rb.transform.position.y < 0) {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}

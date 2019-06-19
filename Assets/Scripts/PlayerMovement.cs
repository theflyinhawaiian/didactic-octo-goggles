using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float difficultyFactor = 1;
    public float maxVelocity = 2000;
    public float lateralVelocity = 500;

    bool movedRight;
    bool movedLeft;
    bool shouldExplode;

    float calculatedVelocity;

    private void Start()
    {
        calculatedVelocity = difficultyFactor * maxVelocity;
    }

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
        if(rb.velocity.magnitude < maxVelocity) {
            rb.AddForce(0, 0, calculatedVelocity * Time.deltaTime);
        }

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

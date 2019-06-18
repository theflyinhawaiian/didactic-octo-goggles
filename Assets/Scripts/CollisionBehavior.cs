using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Obstacle") {
            var gm = GameObject.FindObjectOfType<GameManager>();
            gm.collided = true;
            gm.EndGame();
        }
    }
}

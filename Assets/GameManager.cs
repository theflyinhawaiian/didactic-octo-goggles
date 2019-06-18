using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float restartDelay = 2;
    public bool collided = false;

    bool gameEnded = false;
    public bool GameEnded
    {
        get { return gameEnded; }
    }

    public void EndGame()
    {
        if (!gameEnded) {
            if (collided) {
                var pos = player.transform.position;
                Destroy(player);
            }
            gameEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

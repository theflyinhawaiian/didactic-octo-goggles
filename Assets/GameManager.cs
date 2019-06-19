using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject levelCompleteUI;
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

    public void FinishLevel()
    {
        levelCompleteUI.SetActive(true);
    }
    
    public void InitNewLevel()
    {
        LevelData.IncrementLevel();
        if (LevelData.HighestLevelWon) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

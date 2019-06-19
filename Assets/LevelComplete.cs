using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

	public void LoadNextLevel()
    {
        FindObjectOfType<GameManager>().InitNewLevel();
    }
}

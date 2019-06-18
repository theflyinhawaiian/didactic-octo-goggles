using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public Transform player;
    public Text scoreText;

    private void Update()
    {
        var gameEnded = FindObjectOfType<GameManager>().GameEnded;

        if (player.position.z >= 0 && !gameEnded) {
            scoreText.text = player.position.z.ToString("0");
        }
    }
}

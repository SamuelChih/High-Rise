using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Stopwatch stopwatch;
    [SerializeField] private HighscoreTable highscoreTable;
    [SerializeField] private InputWindow inputWindow;

    private void Awake()
    {
        stopwatch.StartStopwatch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            stopwatch.StopStopwatch();

            collision.transform.GetComponent<PlayerMovement>().enabled = false;
            collision.transform.GetComponent<PlayerAttack>().enabled = false;

            highscoreTable.gameObject.SetActive(true);

            inputWindow.Show("Enter Name", "", "QWERTYUIOPASDFGHJKLZXCVBNM", 3, (string nameText) => {
                highscoreTable.AddHighScoreEntry(stopwatch.GetCurrentTime(), nameText);
                highscoreTable.ShowHighscores();
            });
        }
    }
}

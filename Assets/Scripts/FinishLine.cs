using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject highscoreTable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("Enemies").SetActive(false);
            collision.transform.GetComponent<PlayerMovement>().enabled = false;
            collision.transform.GetComponent<PlayerAttack>().enabled = false;
            highscoreTable.SetActive(true);
        }
    }
}

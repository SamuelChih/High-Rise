using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReturnToMainMenu : MonoBehaviour
{
    PlayerControl controls;
    void Awake(){
        controls = new PlayerControl();
        controls.Gameplay.MainMenu.performed += ctx => SceneManager.LoadScene("Main Menu");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

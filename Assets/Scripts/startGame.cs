using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class startGame : MonoBehaviour
{
    PlayerControl controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControl();
        controls.Gameplay.Jump.performed += ctx => SceneManager.LoadScene("Level1");
    }
    private void Update()
    {   
        controls = new PlayerControl();
        controls.Gameplay.Jump.performed += ctx => MainMenuManager.FindObjectOfType<MainMenuManager>().LoadLevel();

        if(Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.JoystickButton3)) // && playerMovement.canAttack())
        {
            MainMenuManager.FindObjectOfType<MainMenuManager>().LoadLevel();
        }
    }

    void Awake(){
        controls = new PlayerControl();
        controls.Gameplay.Jump.performed += ctx => MainMenuManager.FindObjectOfType<MainMenuManager>().LoadLevel();
    }
}
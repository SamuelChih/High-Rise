using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StrengthBar : MonoBehaviour
{
    [SerializeField] private Image StrengthBarMask;
    [SerializeField] private float maxStrength;
    [SerializeField] private float barChangeSpeed;
    [SerializeField] private float coolDown;

    [SerializeField] PlayerControl controls;

    private bool strengthBarOn;
    private bool isReleased;
    private bool ticOne;
    private bool paused;
    private float currentStrength;
    private float wait;

    // Start is called before the first frame update
    void Start()
    {
        strengthBarOn = true;
        isReleased = true;
        ticOne = false;
        paused = false;
        currentStrength = 0;
        wait = coolDown;
        StartCoroutine(UpdateStrengthBar());
        
    }

      private void Awake()
    {
        controls = new PlayerControl();
        controls.Gameplay.Charge.performed += ctx => isReleased = false;
        controls.Gameplay.Charge.canceled += ctx => isReleased = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey("j")||Input.GetKey(KeyCode.JoystickButton4))
        {
            isReleased = false;
        }
        if (Input.GetKeyUp("j")||Input.GetKeyUp(KeyCode.JoystickButton4))
        {
            if (ticOne)
            {
                paused = true;
                wait = 0;
            }
            isReleased = true;
        }
        if (wait < coolDown)
        {
            wait += Time.deltaTime;
            if (wait >= coolDown)
            {
                reset();
            }
        }
        
    }
    IEnumerator UpdateStrengthBar()
    {
        while (strengthBarOn)
        {
            if (!paused)
            {
                if (isReleased)
                {
                    reset();
                }
                else if (currentStrength < maxStrength)
                {
                    currentStrength += barChangeSpeed;
                    StrengthBarMask.fillAmount = getFill();
                    if (getFill() > 0.5f) ticOne = true;
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    private float getFill()
    {
        return currentStrength / maxStrength;
    }

    public bool getTicOne()
    {
        return ticOne;
    }

    public void reset()
    {
        currentStrength = 0;
        StrengthBarMask.fillAmount = 0;
        paused = false;
        ticOne = false;
        wait = coolDown;
    }
}

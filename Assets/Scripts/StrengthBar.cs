using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    [SerializeField] private Image StrengthBarMask;
    [SerializeField] private float maxStrength;
    [SerializeField] private float barChangeSpeed;
    [SerializeField] private float coolDown;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("j"))
        {
            isReleased = false;
        }
        if (Input.GetKeyUp("j"))
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

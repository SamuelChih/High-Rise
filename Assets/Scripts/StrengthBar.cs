using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    public Image StrengthBarMask;
    public float maxStrength = 10;
    public float barChangeSpeed = 1;
    float currentStrength;
    bool strengthBarOn;
    bool isReleased;

    // Start is called before the first frame update
    void Start()
    {
        currentStrength = 0;
        strengthBarOn = true;
        isReleased = true;
        StartCoroutine(UpdateStrengthBar());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f"))
        {
            isReleased = false;
        }
        if (Input.GetKeyUp("f"))
        {
            isReleased = true;
        }
    }

    IEnumerator UpdateStrengthBar()
    {
        while (strengthBarOn)
        {
            if (isReleased)
            {
                currentStrength = 0;
                StrengthBarMask.fillAmount = 0;
            }
            else if (currentStrength < maxStrength)
            {
                currentStrength += barChangeSpeed;
                float fill = currentStrength / maxStrength;
                StrengthBarMask.fillAmount = fill;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}

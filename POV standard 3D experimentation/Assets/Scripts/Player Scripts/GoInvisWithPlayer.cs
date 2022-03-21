using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoInvisWithPlayer : MonoBehaviour
{
    void LateUpdate()
    {
        if (GetComponentInChildren<Image>())
        {
            GetComponentInChildren<Image>().enabled = UpdateController.switcher.playerOnScreen;
        }

        if (GetComponentInChildren<Text>())
        {
            GetComponentInChildren<Text>().enabled = UpdateController.switcher.playerOnScreen;
        }
    }
}

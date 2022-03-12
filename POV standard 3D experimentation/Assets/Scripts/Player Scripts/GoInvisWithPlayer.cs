using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoInvisWithPlayer : MonoBehaviour
{
    void LateUpdate()
    {
        GetComponentInChildren<Image>().enabled = UpdateController.switcher.playerOnScreen;
    }
}

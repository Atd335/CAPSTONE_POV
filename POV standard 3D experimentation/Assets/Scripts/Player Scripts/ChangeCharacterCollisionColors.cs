using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacterCollisionColors : MonoBehaviour
{


    void Awake()
    {
        UpdateController.col = this;
    }

    public void ChangeColor(string property, string col)
    {
        switch (property)
        {
            case "object":
                UpdateController.cc2D.objectColor = selectColor(col);
                break;
            case "platform":
                UpdateController.cc2D.platformColor = selectColor(col);
                //print(col);
                break;
            case "cutout":
                UpdateController.cc2D.cutOutColor = selectColor(col);
                break;
            case "ouch":
                UpdateController.cc2D.ouchColor = selectColor(col);
                break;
            default:
                break;
        }
    }

    Color selectColor(string id)
    {

        switch (id)
        {
            case "black":
                return ColorContainer.black;
                
            case "white":
                return ColorContainer.white;

            case "green":
                return ColorContainer.green;
               
            case "blue":
                return ColorContainer.blue;
             
            case "red":
                return ColorContainer.red;
          
            case "purple":
                return ColorContainer.purple;
               
            case "yellow":
                return ColorContainer.yellow;
               
            case "orange":
                return ColorContainer.orange;
              
            default:
                return ColorContainer.black;
        }
    }
}

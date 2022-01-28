using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUpLevel : MonoBehaviour
{

    public bool spawnInPlayer = true;
    public bool fpsCharacterEnabled = true;
    public bool platformerCharacterEnabled = true;

    public Vector3 spawnPosition;

    public string nextLevelName = "";

    private void Awake()
    {
        UpdateController.SUL = this;
    }

    // Start is called before the first frame update
    public void _Start()
    {
        if (spawnInPlayer)
        {
            UpdateController.cc2D.respawnPosition = spawnPosition;
            UpdateController.qol.Toggle2DCharacter(true, spawnPosition.x, spawnPosition.y, spawnPosition.z);
        }
    }

    // Update is called once per frame
    public void manualUpdate()
    {
        
    }

    public void switchLevel()
    {
        if (nextLevelName == "") { return; }
        SceneManager.LoadScene(nextLevelName);
    }
}

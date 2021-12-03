using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderController : MonoBehaviour
{
    public bool folderOpen;

    public Image[] folderPieces;

    void Start()
    {
        folderPieces = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        folderPieces[0].enabled = folderOpen;
        folderPieces[2].enabled = folderOpen;
        folderPieces[3].enabled = !folderOpen;

        folderPieces[1].transform.localScale = Vector3.Lerp(folderPieces[1].transform.localScale, Vector3.one, Time.deltaTime * 10);
        folderPieces[1].transform.localPosition = Vector3.Lerp(folderPieces[1].transform.localPosition, paperPos, Time.deltaTime * 6);
        folderPieces[1].transform.localRotation = Quaternion.Lerp(folderPieces[1].transform.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * 4);
    }

    Vector3 inFolderPos = new Vector3(0,-15,0);
    Vector3 paperPos;
    public void ChangeLocalPaperPos(Vector3 v)
    {
        paperPos = v;
    }

    public void PaperPosSet(Vector3 v)
    {
        paperPos = v;
        folderPieces[1].transform.localPosition = paperPos;
    }

    public void ChangeSize(Vector3 v)
    {
        folderPieces[1].transform.localScale = v;
    }

    public void ChangeRot(Vector3 v)
    {
        folderPieces[1].transform.localRotation = Quaternion.Euler(v);
    }

    public void resetPosition()
    {
        ChangeLocalPaperPos(inFolderPos);
        folderPieces[1].transform.localPosition = paperPos;
    }

    public void open()
    {
        folderOpen = true;
    }
    public void close()
    {
        folderOpen = false;
    }

    public void setPaperVisible(bool b)
    {
        folderPieces[1].color = new Color(1,1,1,0);
        if (b)
        {
            folderPieces[1].color = new Color(1, 1, 1, 1);
        }
    }
}

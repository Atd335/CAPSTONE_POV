using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_textureScaler : MonoBehaviour
{

    public Texture2D initialImage;
    public Texture2D rescaledImage;

    // Start is called before the first frame update
    void Start()
    {
        rescaledImage = TextureScaler.scaled(initialImage,100,100,FilterMode.Point);
        rescaledImage.Apply();

        print(initialImage.width);
        print(rescaledImage.width);
    }
}


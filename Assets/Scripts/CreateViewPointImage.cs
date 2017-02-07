using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateViewPointImage : MonoBehaviour
{
    public Camera[] camera = new Camera[15];
    public RenderTexture[] renderTex = new RenderTexture[15];
    public float viewRatio = 0.02f;
    private float leftViewPoint;

    // Use this for initialization
    void Start()
    {
        leftViewPoint = viewRatio * -7.0f;
        Vector3 pos = transform.position;
        for (int i = 0; i < 15; i ++) {
            camera[i].targetTexture = renderTex[i];
            camera[i].transform.position = new Vector3(leftViewPoint + i * viewRatio, pos.y, pos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        leftViewPoint = viewRatio * -7.0f;
        Vector3 pos = transform.position;
        for (int i = 0; i < 15; i ++) {
            camera[i].transform.position = new Vector3(leftViewPoint + i * viewRatio, pos.y, pos.z);
        }

        if (Input.GetKey(KeyCode.Return))
        {
            for (int i = 0; i < 15; i++)
            {
                saveImage(i);
            }
            Debug.Log("SaveImage");
        }
    }
    void saveImage(int num)
    {
        Texture2D tex = new Texture2D(renderTex[num].width, renderTex[num].height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTex[num];
        tex.ReadPixels(new Rect(0, 0, renderTex[num].width, renderTex[num].height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        //Write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../SaveImage/image" + num.ToString() + ".png", bytes);

    }
}

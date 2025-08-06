using System.Collections;
using UnityEngine;

public class SelectMoreTexture : MonoBehaviour
{
    public Texture2D[] face;
    public Texture2D[] eye;
    public Texture2D[] eyebrow;
    public Texture2D[] mouth;
    public Texture2D[] nose;

    void OnGUI()
    {
        for (int i = 0; i < face.Length; i++)
            if (GUI.Button(new Rect(0, i * 64, 128, 64), face[i]))
                ChangeMaterial("face", i);
        for (int j = 0; j < eye.Length; j++)
            if (GUI.Button(new Rect(128, j * 64, 128, 64), eye[j]))
                ChangeMaterial("eye", j);
        for (int j = 0; j < eyebrow.Length; j++)
            if (GUI.Button(new Rect(256, j * 64, 128, 64), eyebrow[j]))
                ChangeMaterial("eyebrow", j);
        for (int j = 0; j < mouth.Length; j++)
            if (GUI.Button(new Rect(384, j * 64, 128, 64), mouth[j]))
                ChangeMaterial("mouth", j);
        for (int j = 0; j < nose.Length; j++)
            if (GUI.Button(new Rect(512, j * 64, 128, 64), nose[j]))
                ChangeMaterial("nose", j);

    }
    void ChangeMaterial(string category,int index)
    {
        if (category == "face")
            GetComponent<Renderer>().material.SetTexture("_FaceTex",face[index]);
        if (category == "eye")
            GetComponent<Renderer>().material.SetTexture("_EyeTex", eye[index]);
        if (category == "eyebrow")
            GetComponent<Renderer>().material.SetTexture("_EyebrowTex", eyebrow[index]);
        if (category == "mouth")
            GetComponent<Renderer>().material.SetTexture("_MouthTex", mouth[index]);
        if (category == "nose")
            GetComponent<Renderer>().material.SetTexture("_NoseTex", nose[index]);
    }
}

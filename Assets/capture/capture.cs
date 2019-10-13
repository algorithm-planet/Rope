using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class capture : MonoBehaviour
{
    /*
    public string title = "";
    public bool take = false;

    private void Update()
    {
        if (take)
        {
            take = false;
            ScreenCapture.CaptureScreenshot("Assets/capture/" + title + ".png", 1);
        }
    }
    */
    //
    /*
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            capture.TAKE(width, height);
    }


    Camera cam; 
    bool Nextfameshot;

    public bool start_ = false;

    private void OnPostRender()
    {
        if (start_)
        {
            start_ = false;
            cam = gameObject.GetComponent<Camera>();
            instance = this;
        }

        if (!Nextfameshot)
            return; 

        Nextfameshot = false;
        RenderTexture rt = cam.targetTexture;

        int w = rt.width,
            h = rt.height;

        Texture2D texture2D = new Texture2D(w, h, TextureFormat.ARGB32, false);
        Rect rect = corner(0, 0, Screen.width, Screen.height);
        texture2D.ReadPixels(rect, 0, 0);

        #region save
        byte[] byteArray = texture2D.EncodeToPNG();
        string path = Application.dataPath + "/";
        System.IO.File.WriteAllBytes(path + "shot.png", byteArray);
        Debug.Log(" - saved");
        #endregion
    }

    static capture instance;
    void TakeShot(int w, int h)
    {
        cam.targetTexture = RenderTexture.GetTemporary(w, h, 16);
        Nextfameshot = true;
    }
     
    public static void TAKE(int w , int h)
    {
        instance.TakeShot(w, h);
    }
    */


    /*
    public string title = "";
    public bool shot = false;
    [Space]
    public Camera cam;

    int w, h;
    private void Update()
    {
        if (shot)
        {
            w = Screen.width;
            h = Screen.height;

            cam.targetTexture = RenderTexture.GetTemporary(w, h, 16);
        }
    }

    private void OnPostRender()
    {
        if (!shot)
            return;
        shot = false;

        Texture2D texture2D = new Texture2D(w, h, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, w, h);
        texture2D.ReadPixels(rect, 0, 0);

        byte[] save = texture2D.EncodeToPNG();
        string path = Application.dataPath + "/capture/";

        System.IO.File.WriteAllBytes(path + title + ".png", save);

        shot = false;
    }
    */
}

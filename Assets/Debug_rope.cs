using UnityEngine;
using System.Collections;

public class Debug_rope : MonoBehaviour
{
    [Range(1, 1000)]
    public int N = 10;

    public float k;
    public float g = 0.01f;

    public bool start = true;

    Rope rope;
    public float frameRate = 100;

    public Color c = Color.cyan;
    private void Update()
    {
        if (start)
        {
            rope = new Rope(transform.position ,N);
            start = false;
        }

        rope.Move(pos_2D, k, -g * Vector2.up);
        rope.Gizmos_(c);
        frameRate = 1f / Time.deltaTime;
    }

    public Camera cam;
    Vector2 pos_2D
    {
        get
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            Vector3 o = ray.origin,
                  dir = ray.direction;

            Vector3 up = Vector3.forward;

            float num = Z.dot(-o, up),
                 deno = Z.dot(dir, up);

            return o + (num / deno) * dir;
        }
    }
}

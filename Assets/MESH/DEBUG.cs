using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    [Range(0f, 1f)]
    public float u, v;

    [Range(3, 64)]
    public int N = 10;
    public Color c = Color.red;
    public Vector3 pos;

    private void Update()
    {
        /*
        transform.localScale = Vector3.one * 0.1f;
        transform.position = Ball.pos(u, v);
        */

        /*
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        mf.sharedMesh = Ball.mesh(N);

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = C;
        mat.mainTexture = TEXTURE_2D.checker_2D(Color.red, Color.gray);
        mat.mainTextureScale = Vector2.one * 10;

        mr.sharedMaterial = mat;
        */

        /*
        Ball ball = new Ball(0);
        ball.Move(pos, 0.1f, c);
        */

        Line line = new Line(0);
        line.move(Vector3.zero, pos, 0.1f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    Transform transform;
    Material mat;

    public Ball(int i = 0)
    {
        string name = "ball_" + i.ToString();
        GameObject G = GameObject.Find(name);
        if (!(bool)G)
        {
            G = new GameObject(name);
            //G.layer = LayerMask.NameToLayer("mesh");
            MeshFilter mf = G.AddComponent<MeshFilter>();
            MeshRenderer mr = G.AddComponent<MeshRenderer>();

            mat = new Material(Shader.Find("unlit"));

            mf.sharedMesh = mesh();
            mr.sharedMaterial = mat;
        }

        mat = G.GetComponent<MeshRenderer>().material;
        transform = G.transform;
    }

    public void Move(Vector3 pos , float r , Color c)
    {
        transform.position = pos;
        transform.localScale = Vector3.one * r;
        mat.mainTexture = TEXTURE_2D.checker_1D(Color.black, c);
    }

    public static Mesh mesh(int N = 32)
    {
        #region Ball
        /*
        Vector3 a = Vector3.right,
        b = Vector3.up,
        K = -Vector3.forward;

        Node[,] nodes_2D = new Node[N + 1, N + 1];
        float A_s = 180f / N,
              a_s = 2f * A_s;

        for (int y = 0; y <= N; y += 1)
        {
            Vector3 V = Z.Rotate(-b, K, y * A_s);
            float _x = Z.dot(V, a),
                  _y = Z.dot(V, b);

            for (int x = 0; x <= N; x += 1)
            {
                Vector3 pos = Z.Rotate(a, b, x * a_s);

                nodes_2D[x, y] = new Node()
                {
                    pos = pos * _x + b * _y,
                    wrap = new Vector2(x, y) / N
                };
            }
        } 
        */
        #endregion


        float[] R = new float[3]
        {
            0f , 0.7f , 1f
        };
        int l = R.Length - 1;

        Node[,] nodes_2D = new Node[l + 1, N + 1];

        Vector3 k = Vector3.forward,
            start = Vector3.up;
        float a_s = 360f / N;

        for (int x = 0; x <= l; x += 1) 
        {
            for(int y = 0; y <= N; y += 1)
            {
                Vector3 pos = Z.Rotate(start, -k, a_s * y);

                nodes_2D[x, y] = new Node()
                {
                    pos = pos * R[x],
                    wrap = new Vector2(x, 0) / l
                };
            }
        }

        return MESH.mesh(nodes_2D);
    }

    public static Vector3 pos(float u , float v)
    {
        Vector3 a = Vector3.right,
                b = Vector3.up,
                K = -Vector3.forward;

        Vector3 y = Z.Rotate(-b, K, v * 180f);


        float _x = Z.dot(y, a),
              _y = Z.dot(y, b);

        Vector3 x = Z.Rotate(a, b, u * 360f);

        return b * _y + x * _x;
    }
}

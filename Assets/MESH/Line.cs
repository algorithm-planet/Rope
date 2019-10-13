using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    Transform transform;
    Material mat;

    public Line(int i = 0)
    {
        string name = "line_" + i.ToString();
        GameObject G = GameObject.Find(name);
        if (!(bool)G)
        {
            G = new GameObject(name);

            MeshFilter mf = G.AddComponent<MeshFilter>();
            MeshRenderer mr = G.AddComponent<MeshRenderer>();

            mat = new Material(Shader.Find("unlit"));
            mat.mainTexture = TEXTURE_2D.checker_1D(Color.white, Color.clear);

            mf.sharedMesh = mesh();
            mr.sharedMaterial = mat;
        }

        transform = G.transform;
        mat = G.GetComponent<MeshRenderer>().material;
    }

    public void move(Vector3 o, Vector3 n, float r)
    {
        float l = n.magnitude;

        mat.mainTextureScale = Vector3.one * l * 20;

        transform.position = o;
        transform.up = n;
        transform.localScale = new Vector3(r, l, r);
    }

    static Mesh mesh(int N = 4)
    {
        Node[,] nodes_2D = new Node[2, N + 1];
        Vector3 dir = Vector3.up,
               start = Vector3.right;

        float a_s = 360f / N;

        for(int x = 0; x <= 1; x += 1)
        {
            Vector3 o = dir * x;

            for (int y = 0; y <= N; y += 1)
            {
                Vector3 v = Z.Rotate(start, -dir, a_s * y);
                nodes_2D[x, y] = new Node()
                {
                    pos = o + v,
                    wrap = new Vector2(x, 0)
                };
            }
        }

        return MESH.mesh(nodes_2D);
    }

}

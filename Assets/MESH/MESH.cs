using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MESH
{
    public static  Mesh mesh(Node[,] nodes_2D)
    {
        int x_l = nodes_2D.GetLength(0) - 1,
            y_l = nodes_2D.GetLength(1) - 1;

        List<Vector3> verts = new List<Vector3>();
        List<int> Tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int y = 0; y <= y_l - 1; y += 1)
        {
            for(int x = 0;x <= x_l - 1; x += 1)
            {
                Node[] nodes = new Node[4]
                {
                    nodes_2D[x , y] , nodes_2D[x , y + 1],
                    nodes_2D[x + 1 , y + 1] , nodes_2D[x + 1 , y],
                };

                foreach(Node node in nodes)
                {
                    if(node.index == -1)
                    {
                        node.index = verts.Count;
                        verts.Add(node.pos);
                        uvs.Add(node.wrap);
                    }
                }

                for(int i = 0; i <= 1; i += 1)
                {
                    Tris.Add(nodes[0].index);
                    Tris.Add(nodes[i + 1].index);
                    Tris.Add(nodes[i + 2].index);
                }
            }
        }

        Mesh mesh = new Mesh()
        {
            indexFormat = IndexFormat.UInt32,
            vertices = verts.ToArray(),
            triangles = Tris.ToArray(),
            uv = uvs.ToArray()
        };
        mesh.RecalculateNormals();

        return mesh;
    }

}

public class Node
{
    public Vector3 pos;
    public int index = -1;
    public Vector2 wrap;
}

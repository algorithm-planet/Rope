using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip
{
    #region #2D
    public List<int> Tris;

    public Clip(List<Vector2> clock)
    {
        #region nodes
        List<Node> nodes = new List<Node>();
        for (int i = 0; i < clock.Count; i += 1)
        {
            Node node = new Node()
            {
                pos = clock[i],
                index = i
            };
            nodes.Add(node);
        }

        int max = nodes.Count;
        foreach (Node node in nodes)
        {
            int i = node.index;
            node.prev = nodes[(i + max - 1) % max];
            node.next = nodes[(i + 1) % max];
        }
        #endregion

        List<Polygon> polys = new List<Polygon>();
        while (true)
        {
            max = nodes.Count;
            Node node = nodes[0];

            bool convex = true;
            for (int y = 0; y < max; y += 1)
            {
                node = nodes[y];

                Vector2 o = node.pos,
                       n1 = node.prev.pos - o,
                       n2 = node.next.pos - o;

                #region convex
                Vector3 area = Z.cross(n1, n2),
                  up = -Vector3.forward;

                convex = Z.dot(area, up) > 0f;
                if (!convex)
                    continue;
                #endregion

                #region ear
                bool ear = true;
                for (int x = 0; x < max; x += 1)
                {
                    if (x == y ||
                        x == (y + max - 1) % max ||
                        x == (y + 1) % max)
                        continue;

                    ear = Z.ear(o, n1, n2, nodes[x].pos);
                }
                #endregion

                #region break
                if (ear)
                {
                    nodes.RemoveAt(y);
                    break;
                }
                #endregion
            }

            if (!convex)
                break;

            Node prev = node.prev,
                 next = node.next;

            Polygon polygon = new Polygon(prev.index, node.index, next.index);
            polys.Add(polygon);

            prev.next = next;
            next.prev = prev;
        }

        Tris = new List<int>();
        foreach (Polygon poly in polys)
        {
            Tris.AddRange(poly.Index);
        }
    }


    #region Node
    class Node
    {
        public Vector2 pos;
        public int index;
        public Node prev, next;
    }
    #endregion

    #region Polygon
    class Polygon
    {
        public int[] Index;
        public Polygon(int a, int b, int c)
        {
            Index = new int[3]
            {
                a , b , c
            };
        }
    }
    #endregion
    #endregion

    #region #3D
    public Clip(List<Vector3> clock)
    {
        #region normal
        int max = clock.Count;

        Vector3 sum = Vector3.zero;
        for (int i = 0; i < max; i += 1)
        {
            sum += clock[i];
        }
        sum *= 1 / max;
        //

        Vector3 normal = Vector3.zero;
        for (int i = 0; i < max; i += 1)
        {
            Vector3 n1 = clock[(i + 1) % max] - sum,
                    n2 = clock[i] - sum;

            normal += Z.cross(n1, n2);
        }

        Vector3 a = Z.axis(normal, 0),
                b = Z.axis(normal, 1);
        // o = v3.zero /**/
        #endregion

        #region clock_2D
        List<Vector2> clock_2D = new List<Vector2>();
        for (int i = 0; i < max; i += 1)
        {
            Vector3 pos = clock[i];
            Vector3 foot_pos = foot(normal, pos);

            Vector2 pos_2D = new Vector2()
            {
                x = Z.dot(foot_pos, a),
                y = Z.dot(foot_pos, b)
            };
            clock_2D.Add(pos_2D);
        } 
        #endregion

        Clip clip = new Clip(clock_2D);
        Tris = clip.Tris;
    }

    #region foot
    static Vector3 foot(Vector3 up, Vector3 pos)
    {
        Vector3 dir = up;

        float num = Z.dot(-pos, up),
             deno = Z.dot(dir, up);

        return pos + (num / deno) * dir;
    } 
    #endregion

    #endregion
}

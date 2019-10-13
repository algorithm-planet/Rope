using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye : MonoBehaviour
{
    public MeshFilter mf;

    private void Update()
    {
        Generate_points();
        Create_Bezier();
    }

    #region points
    List<Vector2> points;
    void Generate_points()
    {
        points = new List<Vector2>();
        foreach(Transform transform_ in transform)
        {
            points.Add(transform_.position);
        }
    }
    #endregion

    public u[] U = new u[4];
    [Range(0f , 0.2f)]
    public float r = 0.2f;
    //
    void Create_Bezier()
    {
        List<Vector2> way = new List<Vector2>();

        #region split
        int max = 4;
        for (int i = 0; i < max; i += 1)
        {
            Vector2 o = points[i];
            Vector2 n1 = points[(i - 1 + max) % max] - o,
                    n2 = points[(i + 1) % max] - o;

            float l1 = n1.magnitude,
                  l2 = n2.magnitude;

            Vector2 dir = n1 / l1 - n2 / l2;
            Vector2 a = o + dir * l1 * U[i].t,
                    b = o - dir * l2 * U[i].t;
            way.Add(a);
            way.Add(o);
            way.Add(b);

            Debug.DrawLine(a, o, Color.red, Time.deltaTime);
            Debug.DrawLine(b, o, Color.red, Time.deltaTime);
        }
        #endregion


        way.Add(way[0]);
        way.RemoveAt(0);
        max = way.Count;

        #region beziers
        List<Bezier> beziers = new List<Bezier>();
        for (int i = 0; i < max; i += 3)
        {
            Bezier bezier = new Bezier(way[i], way[i + 1], way[i + 2], way[(i + 3) % max]);
            beziers.Add(bezier);
        }
        #endregion

        int N = 20;
        float t_s = 1f / N;
        max = beziers.Count;

        for (int y = 0; y < max; y += 1)
        {
            Bezier bezier = beziers[y];
            for (int x = 0; x < N; x += 1)
            {
                Vector2 a = bezier.pos(x * t_s),
                        b = bezier.pos((x + 1) * t_s);

                //Debug.DrawRay(a, b - a, Color.green, Time.deltaTime);
                //Line line = new Line(y + x * max);
                //line.move(a, b - a, 0.005f);
            }
        }

        /*
        int I = (int)(index * 3);
        //Vector3 pos = beziers[I].pos(time) - Vector3.forward * 0.003f;
        Vector3 pos = lerp(t, beziers);

        Ball ball = new Ball(0);
        ball.Move(pos, 0.05f, Color.red);
        */

        List<Vector2> edge_Points = new List<Vector2>();
        N *= beziers.Count;
        t_s = 1f / N;

        Vector2 sum = Vector2.zero;
        for (int i = 0; i <= N; i += 1)
        {
            Vector2 point = lerp(t_s * i, beziers);
            //Ball ball = new Ball(i);
            //ball.Move(point, 0.02f, Color.yellow);

            sum += point;
            edge_Points.Add(point);
        }

        sum *= 1f / (N + 1);
        collider.points = edge_Points.ToArray();

        RaycastHit2D hit = Physics2D.Raycast(sum, pos_2D - sum);
        if (hit)
        {
            Vector2 pos = hit.point + hit.normal * r;
            eyeBall.position = pos;
            eyeBall.localScale = Vector3.one * r;
            /*
            Ball ball = new Ball(0);
            ball.Move(pos, r, Color.black);
            */
        }

        Ball pointer = new Ball(1);
        pointer.Move(pos_2D, r / 5f, Color.red);
        Line l = new Line(100);
        l.move(sum, pos_2D - sum, r / 20f);

        #region mesh
        Clip clip = new Clip(edge_Points);

        List<Vector3> points_3D = new List<Vector3>();
        foreach (Vector2 p in edge_Points)
        {
            points_3D.Add(new Vector3(p.x, p.y, 0.1f));
        }
        points_3D.RemoveAt(points_3D.Count - 1);

        Mesh mesh = new Mesh()
        {
            vertices = points_3D.ToArray(),
            triangles = clip.Tris.ToArray()
        };
        mesh.RecalculateNormals();
        mf.sharedMesh = mesh; 
        #endregion
    }

    public Transform eyeBall;
    public EdgeCollider2D collider;
    //
    [Range(0f, 1f)]
    public float t;
    [Range(0 , 1f)]
    public float index;

    #region lerp
    Vector3 lerp(float t, List<Bezier> beziers)
    {
        int N = beziers.Count;
        if (t == 1f)
            return beziers[N - 1].pos(1f);

        float t_s = 1f / N;
        int index = (int)(t / t_s);

        float time = (t - t_s * index) * N;

        Bezier bezier = beziers[index];
        return bezier.pos(time);
    }
    #endregion

    public Camera cam;
    Vector2 pos_2D
    {
        get
        {
            Ray ray =  cam.ScreenPointToRay(Input.mousePosition);
            Vector3 o = ray.origin,
                  dir = ray.direction;

            Vector3 up = Vector3.forward;
            float num = Z.dot(-o , up),
                 deno = Z.dot(dir, up);

            return o + dir * (num / deno);
        }
    }
}


#region u
[System.Serializable]
public class u
{
    [Range(0f, 1f)]
    public float t = 0.3f;
} 
#endregion


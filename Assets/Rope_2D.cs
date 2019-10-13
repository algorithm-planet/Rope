using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope
{
    List<Vector2> pos;
    float dt , dm;

    public Rope(Vector2 b  ,int N , float step = 0.01f)
    {
        pos = new List<Vector2>();

        for(int i = 0; i <= N; i += 1)
        {
            Vector2 v = b * (float)i / N;
            pos.Add(v);
        }

        Time.fixedDeltaTime = step;
        dt = step;
        dm = dt / 10;
    }

    public void Move(Vector2 end , float k, Vector2 grav)
    {

        for (int i = 1; i <= pos.Count - 2; i += 1)
        {
            Vector2 stretch = k * (pos[i - 1] - pos[i] +
                                   pos[i + 1] - pos[i]);
            stretch *= 1f / dm;

            Vector2 dv = (stretch + grav) * dt;
            Vector2 vel = dv;

            pos[i] += vel * dt;
        }

        pos[pos.Count - 1] = end;
    }

    public void Gizmos_(Color c)
    {
        for (int i = 0; i < pos.Count; i += 1)
        {
            float r = 0.1f;
            Ball ball = new Ball(i);
            Vector3 o = new Vector3(pos[i].x, pos[i].y, -r / 10);
            ball.Move(o, r, c);

            if (i == pos.Count - 1)
                break;

            Line line = new Line(i);
            line.move(pos[i], pos[i + 1] - pos[i], r / 10f);

            //Gizmos.DrawSphere(pos[i], 0.001f);
            //Debug.DrawLine(pos[i], pos[i + 1], Color.red, dt);
        }
    }
}

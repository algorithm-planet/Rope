# Rope
smooth rope flow

## Initialize
```c#
List<Vector2> pos;
dm , dt
```
 
## make
```c#  
  pos[0] = Vector2.zero;
  pos[pos.Count - 1] = end;
  
  for (int i = 1; i <= pos.Count - 2; i += 1)
  {
      Vector2 stretch = k * (pos[i - 1] - pos[i] +
                             pos[i + 1] - pos[i]);
      stretch *= 1f / dm;
  
      Vector2 dv = (stretch + grav) * dt;
      Vector2 vel = dv;
  
      pos[i] += vel * dt;
  }
```

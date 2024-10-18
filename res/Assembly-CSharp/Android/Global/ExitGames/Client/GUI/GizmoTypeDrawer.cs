// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.GUI.GizmoTypeDrawer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.Client.GUI
{
  public class GizmoTypeDrawer
  {
    public static void Draw(Vector3 center, GizmoType type, Color color, float size)
    {
      Gizmos.color = color;
      switch (type)
      {
        case GizmoType.WireSphere:
          Gizmos.DrawWireSphere(center, size * 0.5f);
          break;
        case GizmoType.Sphere:
          Gizmos.DrawSphere(center, size * 0.5f);
          break;
        case GizmoType.WireCube:
          Gizmos.DrawWireCube(center, Vector3.one * size);
          break;
        case GizmoType.Cube:
          Gizmos.DrawCube(center, Vector3.one * size);
          break;
      }
    }
  }
}

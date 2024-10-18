// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.GUI.GizmoTypeDrawer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
          Gizmos.DrawWireCube(center, Vector3.op_Multiply(Vector3.one, size));
          break;
        case GizmoType.Cube:
          Gizmos.DrawCube(center, Vector3.op_Multiply(Vector3.one, size));
          break;
      }
    }
  }
}

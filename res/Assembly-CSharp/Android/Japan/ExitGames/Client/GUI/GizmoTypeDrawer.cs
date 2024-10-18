// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.GUI.GizmoTypeDrawer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

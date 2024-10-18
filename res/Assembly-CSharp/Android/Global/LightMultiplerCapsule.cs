// Decompiled with JetBrains decompiler
// Type: LightMultiplerCapsule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Rendering/Light Multipler Capsule")]
public class LightMultiplerCapsule : LightMultipler
{
  [SerializeField]
  public List<Vector3> mPoints = new List<Vector3>();
}

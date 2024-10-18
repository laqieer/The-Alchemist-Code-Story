// Decompiled with JetBrains decompiler
// Type: LightMultiplerCapsule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("Rendering/Light Multipler Capsule")]
public class LightMultiplerCapsule : LightMultipler
{
  [SerializeField]
  public List<Vector3> mPoints = new List<Vector3>();
}

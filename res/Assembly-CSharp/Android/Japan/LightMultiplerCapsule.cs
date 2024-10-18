// Decompiled with JetBrains decompiler
// Type: LightMultiplerCapsule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Rendering/Light Multipler Capsule")]
public class LightMultiplerCapsule : LightMultipler
{
  [SerializeField]
  public List<Vector3> mPoints = new List<Vector3>();
}

// Decompiled with JetBrains decompiler
// Type: MapPickup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class MapPickup : MonoBehaviour
{
  public MapPickup.PickupEvent OnPickup;
  public Transform Shadow;
  public Vector3 DropEffectOffset;

  public delegate void PickupEvent();
}

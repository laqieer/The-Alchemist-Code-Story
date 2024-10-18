// Decompiled with JetBrains decompiler
// Type: SortingOrder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Rendering/SortingOrder")]
[RequireComponent(typeof (Renderer))]
public class SortingOrder : MonoBehaviour
{
  [SerializeField]
  private int mSortingOrder;

  private void Awake()
  {
    this.enabled = false;
  }

  private void OnValidate()
  {
    this.GetComponent<Renderer>().sortingOrder = this.mSortingOrder;
  }
}

// Decompiled with JetBrains decompiler
// Type: SortingOrder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

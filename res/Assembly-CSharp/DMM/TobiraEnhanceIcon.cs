// Decompiled with JetBrains decompiler
// Type: TobiraEnhanceIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class TobiraEnhanceIcon : MonoBehaviour
{
  [SerializeField]
  private GameObject mUseSubPieceIcon;

  public void ShowUseSubPieceIcon()
  {
    if (!Object.op_Inequality((Object) this.mUseSubPieceIcon, (Object) null))
      return;
    this.mUseSubPieceIcon.SetActive(UnitTobiraEnhanceWindow.IsUseSubPiese);
  }
}

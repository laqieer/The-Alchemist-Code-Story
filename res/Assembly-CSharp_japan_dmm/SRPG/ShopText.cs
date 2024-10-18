// Decompiled with JetBrains decompiler
// Type: SRPG.ShopText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ShopText : MonoBehaviour
  {
    public string Normal;
    public string Tabi;
    public string Kimagure;
    public string Monozuki;
    public string Tour;
    public string Arena;
    public string Multi;
    public string AwakePiece;
    public string Artifact;
    public string Limited;
    public string Port;
    private string mTextID;

    private void LateUpdate()
    {
      if (this.mTextID != null)
        return;
      Text component = ((Component) this).GetComponent<Text>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
          this.mTextID = this.Normal;
          break;
        case EShopType.Tabi:
          this.mTextID = this.Tabi;
          break;
        case EShopType.Kimagure:
          this.mTextID = this.Kimagure;
          break;
        case EShopType.Monozuki:
          this.mTextID = this.Monozuki;
          break;
        case EShopType.Tour:
          this.mTextID = this.Tour;
          break;
        case EShopType.Arena:
          this.mTextID = this.Arena;
          break;
        case EShopType.Multi:
          this.mTextID = this.Multi;
          break;
        case EShopType.AwakePiece:
          this.mTextID = this.AwakePiece;
          break;
        case EShopType.Artifact:
          this.mTextID = this.Artifact;
          break;
        case EShopType.Limited:
          this.mTextID = this.Limited;
          break;
        case EShopType.Port:
          this.mTextID = this.Port;
          break;
      }
      if (string.IsNullOrEmpty(this.mTextID))
        this.mTextID = string.Empty;
      else
        component.text = LocalizedText.Get(this.mTextID);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawListIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawListIcon : MonoBehaviour
  {
    [SerializeField]
    private GameObject RuneItemGO;
    [SerializeField]
    private GameObject RuneFrameGO;
    [SerializeField]
    private GameObject EvoImageGO;
    [SerializeField]
    private ImageArray EnhImageArray;
    [SerializeField]
    private GameObject SetTypeGO;
    [SerializeField]
    private Image mOwnerIcon;
    private BindRuneData mRuneData;

    public void SetDrawParam(BindRuneData rundata)
    {
      this.mRuneData = rundata;
      if (Object.op_Inequality((Object) this.RuneItemGO, (Object) null))
        DataSource.Bind<BindRuneData>(this.RuneItemGO.gameObject, rundata);
      if (Object.op_Inequality((Object) this.RuneFrameGO, (Object) null))
        DataSource.Bind<BindRuneData>(this.RuneFrameGO.gameObject, rundata);
      if (Object.op_Inequality((Object) this.EvoImageGO, (Object) null))
        DataSource.Bind<BindRuneData>(this.EvoImageGO.gameObject, rundata);
      if (Object.op_Inequality((Object) this.EnhImageArray, (Object) null))
        DataSource.Bind<BindRuneData>(((Component) this.EnhImageArray).gameObject, rundata);
      if (Object.op_Inequality((Object) this.SetTypeGO, (Object) null))
        DataSource.Bind<BindRuneData>(this.SetTypeGO.gameObject, rundata);
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Inequality((Object) this.RuneItemGO, (Object) null))
        GameParameter.UpdateAll(this.RuneItemGO.gameObject);
      if (Object.op_Inequality((Object) this.RuneFrameGO, (Object) null))
        GameParameter.UpdateAll(this.RuneFrameGO.gameObject);
      if (Object.op_Inequality((Object) this.EvoImageGO, (Object) null))
        GameParameter.UpdateAll(this.EvoImageGO.gameObject);
      if (Object.op_Inequality((Object) this.EnhImageArray, (Object) null))
        GameParameter.UpdateAll(((Component) this.EnhImageArray).gameObject);
      if (Object.op_Inequality((Object) this.SetTypeGO, (Object) null))
        GameParameter.UpdateAll(this.SetTypeGO.gameObject);
      this.RefreshOwnerIcon();
    }

    private void RefreshOwnerIcon()
    {
      if (Object.op_Equality((Object) this.mOwnerIcon, (Object) null) || this.mRuneData == null)
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      UnitData owner = rune.GetOwner();
      if (owner != null)
        this.mOwnerIcon.sprite = AssetManager.Load<SpriteSheet>("ItemIcon/small").GetSprite(MonoSingleton<GameManager>.Instance.GetItemParam(owner.UnitParam.piece).icon);
      else
        this.mOwnerIcon.sprite = (Sprite) null;
    }
  }
}

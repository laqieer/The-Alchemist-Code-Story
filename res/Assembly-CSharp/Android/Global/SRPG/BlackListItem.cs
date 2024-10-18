// Decompiled with JetBrains decompiler
// Type: SRPG.BlackListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BlackListItem : MonoBehaviour
  {
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Lv;
    [SerializeField]
    private Text LastLogin;
    [SerializeField]
    private RawImage Icon;

    public void Refresh(ChatBlackListParam param)
    {
      if (param == null)
        return;
      if ((UnityEngine.Object) this.Name != (UnityEngine.Object) null)
        this.Name.text = param.name;
      if ((UnityEngine.Object) this.Lv != (UnityEngine.Object) null)
        this.Lv.text = PlayerData.CalcLevelFromExp(param.exp).ToString();
      if ((UnityEngine.Object) this.LastLogin != (UnityEngine.Object) null)
        this.LastLogin.text = ChatLogItem.GetPostAt(param.lastlogin);
      if (!((UnityEngine.Object) this.Icon != (UnityEngine.Object) null))
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.icon);
      if (unitParam == null)
        return;
      if (!string.IsNullOrEmpty(param.skin_iname))
      {
        ArtifactParam skin = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == param.skin_iname));
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.UnitSkinIconSmall(unitParam, skin, param.job_iname));
      }
      else
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.UnitIconSmall(unitParam, param.job_iname));
    }
  }
}

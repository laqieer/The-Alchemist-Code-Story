// Decompiled with JetBrains decompiler
// Type: SRPG.GvGCoolTimeRankingContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGCoolTimeRankingContent : MonoBehaviour
  {
    [SerializeField]
    private Text mName;
    [SerializeField]
    private Text mPoint;
    [SerializeField]
    private Image mColor;
    [SerializeField]
    private Image mIcon;

    private void Start() => this.Refresh();

    public void Refresh()
    {
      GvGHalfTime data = DataSource.FindDataOfClass<GvGHalfTime>(((Component) this).gameObject, (GvGHalfTime) null);
      if (data == null)
        return;
      GvGLeagueViewGuild data1 = data.Gid != GvGManager.Instance.MyGuild.id ? GvGManager.Instance.OtherGuildList.Find((Predicate<GvGLeagueViewGuild>) (g => g.id == data.Gid)) : GvGManager.Instance.MyGuild;
      if (data1 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mIcon, (UnityEngine.Object) null))
        DataSource.Bind<ViewGuildData>(((Component) this.mIcon).gameObject, (ViewGuildData) data1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mName, (UnityEngine.Object) null))
        this.mName.text = data.Name;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPoint, (UnityEngine.Object) null))
        this.mPoint.text = data.Point.ToString();
      GvGManager.Instance.SetNodeColorIndex(GvGManager.Instance.GetMatchingOrderIndex(data.Gid), this.mColor);
    }
  }
}

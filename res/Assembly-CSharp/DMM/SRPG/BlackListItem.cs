// Decompiled with JetBrains decompiler
// Type: SRPG.BlackListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (Object.op_Inequality((Object) this.Name, (Object) null))
        this.Name.text = param.name;
      if (Object.op_Inequality((Object) this.Lv, (Object) null))
        this.Lv.text = PlayerData.CalcLevelFromExp(param.exp).ToString();
      if (Object.op_Inequality((Object) this.LastLogin, (Object) null))
        this.LastLogin.text = ChatLogItem.GetPostAt(param.lastlogin);
      if (!Object.op_Inequality((Object) this.Icon, (Object) null) || param.unit == null)
        return;
      UnitData data = new UnitData();
      data.Deserialize(param.unit);
      DataSource.Bind<UnitData>(((Component) this).gameObject, data);
    }
  }
}

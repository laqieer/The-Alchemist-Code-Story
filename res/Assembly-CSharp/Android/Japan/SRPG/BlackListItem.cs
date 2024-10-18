// Decompiled with JetBrains decompiler
// Type: SRPG.BlackListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      if (!((UnityEngine.Object) this.Icon != (UnityEngine.Object) null) || param.unit == null)
        return;
      UnitData data = new UnitData();
      data.Deserialize(param.unit);
      DataSource.Bind<UnitData>(this.gameObject, data, false);
    }
  }
}

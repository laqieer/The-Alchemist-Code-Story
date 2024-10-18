// Decompiled with JetBrains decompiler
// Type: SRPG.QuestHaveUnitOpenPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestHaveUnitOpenPopup : MonoBehaviour
  {
    [SerializeField]
    private string MessageLText;
    private GameObject mMsg;

    public void OnClick(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null) || string.IsNullOrEmpty(this.MessageLText) || Object.op_Inequality((Object) this.mMsg, (Object) null))
        return;
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(dataOfClass.OpenUnit);
      if (unitParam == null)
        return;
      this.mMsg = UIUtility.SystemMessage(LocalizedText.Get(this.MessageLText, (object) unitParam.name), new UIUtility.DialogResultEvent(this.OnConfirm));
    }

    public void OnConfirm(GameObject go) => this.mMsg = (GameObject) null;
  }
}

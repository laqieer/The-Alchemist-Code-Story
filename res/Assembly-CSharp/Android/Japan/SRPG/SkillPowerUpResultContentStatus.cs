// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResultContentStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SkillPowerUpResultContentStatus : MonoBehaviour
  {
    [SerializeField]
    private Text paramNameText;
    [SerializeField]
    private Text prevParamText;
    [SerializeField]
    private Text resultParamText;
    [SerializeField]
    private Text resultAddedParamText;

    public void SetData(SkillPowerUpResultContent.Param param, ParamTypes type, bool isScale)
    {
      string str = !isScale ? string.Empty : "%";
      int num1 = (int) (!isScale ? param.currentParam[type] : param.currentParamMul[type]);
      int num2 = (int) (!isScale ? param.prevParam[type] : param.prevParamMul[type]);
      int num3 = (int) (!isScale ? param.currentParamBonus[type] : param.currentParamBonusMul[type]);
      int num4 = (int) (!isScale ? param.prevParamBonus[type] : param.prevParamBonusMul[type]);
      int num5 = num2 + num4;
      int num6 = num1 + num3;
      this.paramNameText.text = LocalizedText.Get("sys." + (object) type);
      this.prevParamText.text = num5.ToString() + str;
      this.resultParamText.text = num6.ToString() + str;
      int num7 = num3;
      if (num7 >= 0)
        this.resultAddedParamText.text = "(+" + (object) num7 + str + ")";
      else
        this.resultAddedParamText.text = "(" + (object) num7 + str + ")";
    }
  }
}

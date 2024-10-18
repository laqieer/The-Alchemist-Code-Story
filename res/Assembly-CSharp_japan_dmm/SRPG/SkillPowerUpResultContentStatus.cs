// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResultContentStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      switch (type)
      {
        case ParamTypes.Tokkou:
          this.paramNameText.text = string.Empty;
          this.prevParamText.text = string.Empty;
          this.resultParamText.text = string.Empty;
          this.resultAddedParamText.text = string.Empty;
          int count1 = param.currentParam.tokkou.Count;
          if (isScale)
            count1 = param.currentParamMul.tokkou.Count;
          for (int index = 0; index < count1; ++index)
          {
            if (isScale)
            {
              if (param.currentParamMul.tokkou.Count != 0)
                num1 = (int) param.currentParamMul.tokkou[index].value;
              if (param.prevParamMul.tokkou.Count != 0)
                num2 = (int) param.prevParamMul.tokkou[index].value;
              if (param.currentParamBonusMul.tokkou.Count != 0)
                num3 = (int) param.currentParamBonusMul.tokkou[index].value;
              if (param.prevParamBonusMul.tokkou.Count != 0)
                num4 = (int) param.prevParamBonusMul.tokkou[index].value;
            }
            else
            {
              if (param.currentParam.tokkou.Count != 0)
                num1 = (int) param.currentParam.tokkou[index].value;
              if (param.prevParam.tokkou.Count != 0)
                num2 = (int) param.prevParam.tokkou[index].value;
              if (param.currentParamBonus.tokkou.Count != 0)
                num3 = (int) param.currentParamBonus.tokkou[index].value;
              if (param.prevParamBonus.tokkou.Count != 0)
                num4 = (int) param.prevParamBonus.tokkou[index].value;
            }
            int num5 = num2 + num4;
            int num6 = num1 + num3;
            if (index != 0)
            {
              this.paramNameText.text += "\n";
              this.prevParamText.text += "\n";
              this.resultParamText.text += "\n";
              this.resultAddedParamText.text += "\n";
            }
            this.paramNameText.text += string.Format(LocalizedText.Get("sys." + (object) type), (object) param.currentParam.tokkou[index].tag);
            Text prevParamText = this.prevParamText;
            prevParamText.text = prevParamText.text + num5.ToString() + str;
            Text resultParamText = this.resultParamText;
            resultParamText.text = resultParamText.text + num6.ToString() + str;
            int num7 = num3;
            if (num7 >= 0)
            {
              Text resultAddedParamText = this.resultAddedParamText;
              resultAddedParamText.text = resultAddedParamText.text + "(+" + (object) num7 + str + ")";
            }
            else
            {
              Text resultAddedParamText = this.resultAddedParamText;
              resultAddedParamText.text = resultAddedParamText.text + "(" + (object) num7 + str + ")";
            }
          }
          break;
        case ParamTypes.Tokubou:
          this.paramNameText.text = string.Empty;
          this.prevParamText.text = string.Empty;
          this.resultParamText.text = string.Empty;
          this.resultAddedParamText.text = string.Empty;
          int count2 = param.currentParam.tokubou.Count;
          if (isScale)
            count2 = param.currentParamMul.tokubou.Count;
          for (int index = 0; index < count2; ++index)
          {
            if (isScale)
            {
              if (param.currentParamMul.tokubou.Count != 0)
                num1 = (int) param.currentParamMul.tokubou[index].value;
              if (param.prevParamMul.tokubou.Count != 0)
                num2 = (int) param.prevParamMul.tokubou[index].value;
              if (param.currentParamBonusMul.tokubou.Count != 0)
                num3 = (int) param.currentParamBonusMul.tokubou[index].value;
              if (param.prevParamBonusMul.tokubou.Count != 0)
                num4 = (int) param.prevParamBonusMul.tokubou[index].value;
            }
            else
            {
              if (param.currentParam.tokubou.Count != 0)
                num1 = (int) param.currentParam.tokubou[index].value;
              if (param.prevParam.tokubou.Count != 0)
                num2 = (int) param.prevParam.tokubou[index].value;
              if (param.currentParamBonus.tokubou.Count != 0)
                num3 = (int) param.currentParamBonus.tokubou[index].value;
              if (param.prevParamBonus.tokubou.Count != 0)
                num4 = (int) param.prevParamBonus.tokubou[index].value;
            }
            int num8 = num2 + num4;
            int num9 = num1 + num3;
            if (index != 0)
            {
              this.paramNameText.text += "\n";
              this.prevParamText.text += "\n";
              this.resultParamText.text += "\n";
              this.resultAddedParamText.text += "\n";
            }
            this.paramNameText.text += string.Format(LocalizedText.Get("sys." + (object) type), (object) param.currentParam.tokubou[index].tag);
            Text prevParamText = this.prevParamText;
            prevParamText.text = prevParamText.text + num8.ToString() + str;
            Text resultParamText = this.resultParamText;
            resultParamText.text = resultParamText.text + num9.ToString() + str;
            int num10 = num3;
            if (num10 >= 0)
            {
              Text resultAddedParamText = this.resultAddedParamText;
              resultAddedParamText.text = resultAddedParamText.text + "(+" + (object) num10 + str + ")";
            }
            else
            {
              Text resultAddedParamText = this.resultAddedParamText;
              resultAddedParamText.text = resultAddedParamText.text + "(" + (object) num10 + str + ")";
            }
          }
          break;
        default:
          int num11 = !isScale ? param.currentParam[type] : param.currentParamMul[type];
          int num12 = !isScale ? param.prevParam[type] : param.prevParamMul[type];
          int num13 = !isScale ? param.currentParamBonus[type] : param.currentParamBonusMul[type];
          int num14 = !isScale ? param.prevParamBonus[type] : param.prevParamBonusMul[type];
          int num15 = num12 + num14;
          int num16 = num11 + num13;
          this.paramNameText.text = LocalizedText.Get("sys." + (object) type);
          this.prevParamText.text = num15.ToString() + str;
          this.resultParamText.text = num16.ToString() + str;
          int num17 = num13;
          if (num17 >= 0)
          {
            this.resultAddedParamText.text = "(+" + (object) num17 + str + ")";
            break;
          }
          this.resultAddedParamText.text = "(" + (object) num17 + str + ")";
          break;
      }
    }
  }
}

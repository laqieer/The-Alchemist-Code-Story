// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffectText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class BuffEffectText : MonoBehaviour
  {
    public RichBitmapText Text;

    public void SetText(ParamTypes type, bool down)
    {
      if (!Object.op_Inequality((Object) this.Text, (Object) null))
        return;
      string str1 = type.ToString();
      if (string.IsNullOrEmpty(str1))
        return;
      StringBuilder stringBuilder1 = GameUtility.GetStringBuilder();
      stringBuilder1.Append("quest.BUFF_");
      stringBuilder1.Append(str1);
      string str2 = LocalizedText.Get(stringBuilder1.ToString());
      StringBuilder stringBuilder2 = GameUtility.GetStringBuilder();
      stringBuilder2.Append(str2);
      stringBuilder2.Append(' ');
      if (down)
      {
        if (BuffEffectParam.IsNegativeValueIsBuff(type))
          stringBuilder2.Append(LocalizedText.Get("quest.EFF_UP"));
        else
          stringBuilder2.Append(LocalizedText.Get("quest.EFF_DOWN"));
        this.Text.BottomColor = GameSettings.Instance.Debuff_TextBottomColor;
        this.Text.TopColor = GameSettings.Instance.Debuff_TextTopColor;
      }
      else
      {
        if (BuffEffectParam.IsNegativeValueIsBuff(type))
          stringBuilder2.Append(LocalizedText.Get("quest.EFF_DOWN"));
        else
          stringBuilder2.Append(LocalizedText.Get("quest.EFF_UP"));
        this.Text.BottomColor = GameSettings.Instance.Buff_TextBottomColor;
        this.Text.TopColor = GameSettings.Instance.Buff_TextTopColor;
      }
      ((UnityEngine.UI.Text) this.Text).text = stringBuilder2.ToString();
    }
  }
}

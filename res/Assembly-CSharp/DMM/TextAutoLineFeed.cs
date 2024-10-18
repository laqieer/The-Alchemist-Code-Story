// Decompiled with JetBrains decompiler
// Type: TextAutoLineFeed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class TextAutoLineFeed : MonoBehaviour
{
  [SerializeField]
  private int LineMaxLength = 10;
  [SerializeField]
  private string[] BeforeInsert = new string[1]{ "【" };
  [SerializeField]
  private string[] LaterInsert = new string[2]{ "・", "---" };
  [SerializeField]
  private bool IgnoreEmptyLine = true;
  private Text mTargetText;
  private string mPreText = string.Empty;

  private void Start() => this.mTargetText = ((Component) this).GetComponent<Text>();

  private void Update()
  {
    if (Object.op_Equality((Object) this.mTargetText, (Object) null) || string.IsNullOrEmpty(this.mTargetText.text) || this.mPreText == this.mTargetText.text || this.mTargetText.text.Length <= this.LineMaxLength)
      return;
    this.mTargetText.text = this.InsertLineFeed(this.mTargetText.text);
    if (this.IgnoreEmptyLine)
      this.mTargetText.text = this.DeleteEmptyLine(this.mTargetText.text);
    this.mPreText = this.mTargetText.text;
  }

  private string InsertLineFeed(string text)
  {
    foreach (string str in this.BeforeInsert)
    {
      if (!string.IsNullOrEmpty(str))
      {
        int num1 = text.IndexOf(str, 1);
        if (num1 != -1)
        {
          int num2 = num1;
          string text1 = text.Substring(0, num2);
          string text2 = text.Substring(num2);
          return this.InsertLineFeed(text1) + "\n" + this.InsertLineFeed(text2);
        }
      }
    }
    foreach (string str in this.LaterInsert)
    {
      if (!string.IsNullOrEmpty(str))
      {
        int num3 = text.IndexOf(str, 0, text.Length - 1);
        if (num3 != -1)
        {
          int num4 = num3 + str.Length;
          string text3 = text.Substring(0, num4);
          string text4 = text.Substring(num4);
          return this.InsertLineFeed(text3) + "\n" + this.InsertLineFeed(text4);
        }
      }
    }
    return text;
  }

  private string DeleteEmptyLine(string text)
  {
    text = text.Replace("\n\n", "\n");
    return text;
  }
}

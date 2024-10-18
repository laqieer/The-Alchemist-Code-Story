// Decompiled with JetBrains decompiler
// Type: TextAutoLineFeed
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
  private string mPreText = string.Empty;
  private Text mTargetText;

  private void Start()
  {
    this.mTargetText = this.GetComponent<Text>();
  }

  private void Update()
  {
    if ((Object) this.mTargetText == (Object) null || string.IsNullOrEmpty(this.mTargetText.text) || (this.mPreText == this.mTargetText.text || this.mTargetText.text.Length <= this.LineMaxLength))
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
          return this.InsertLineFeed(text.Substring(0, num2)) + "\n" + this.InsertLineFeed(text.Substring(num2));
        }
      }
    }
    foreach (string str in this.LaterInsert)
    {
      if (!string.IsNullOrEmpty(str))
      {
        int num1 = text.IndexOf(str, 0, text.Length - 1);
        if (num1 != -1)
        {
          int num2 = num1 + str.Length;
          return this.InsertLineFeed(text.Substring(0, num2)) + "\n" + this.InsertLineFeed(text.Substring(num2));
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

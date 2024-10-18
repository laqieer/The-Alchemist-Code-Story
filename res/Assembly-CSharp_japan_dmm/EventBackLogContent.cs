// Decompiled with JetBrains decompiler
// Type: EventBackLogContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
public class EventBackLogContent : MonoBehaviour
{
  [SerializeField]
  private UnityEngine.UI.Text m_LogName;
  [SerializeField]
  private UnityEngine.UI.Text m_LogText;
  private static Regex pattern = new Regex("<.*?>", RegexOptions.Singleline);

  public void SetBackLogText(string name, string text)
  {
    this.m_LogName.text = this.ReplaceTag(name);
    this.m_LogText.text = this.ReplaceTag(text);
  }

  private string ReplaceTag(string text)
  {
    if (string.IsNullOrEmpty(text))
      return string.Empty;
    MatchCollection matchCollection = EventBackLogContent.pattern.Matches(text);
    if (matchCollection != null)
    {
      foreach (Match match in matchCollection)
        text = text.Replace(match.Value, string.Empty);
    }
    return text;
  }
}

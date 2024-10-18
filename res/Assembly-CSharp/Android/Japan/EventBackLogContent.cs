// Decompiled with JetBrains decompiler
// Type: EventBackLogContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class EventBackLogContent : MonoBehaviour
{
  private static Regex pattern = new Regex("<.*?>", RegexOptions.Singleline);
  [SerializeField]
  private UnityEngine.UI.Text m_LogName;
  [SerializeField]
  private UnityEngine.UI.Text m_LogText;

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
      IEnumerator enumerator = matchCollection.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Match current = (Match) enumerator.Current;
          text = text.Replace(current.Value, string.Empty);
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }
    return text;
  }
}

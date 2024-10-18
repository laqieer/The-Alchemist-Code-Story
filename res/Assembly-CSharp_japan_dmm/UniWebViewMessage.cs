// Decompiled with JetBrains decompiler
// Type: UniWebViewMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine.Networking;

#nullable disable
public struct UniWebViewMessage
{
  public UniWebViewMessage(string rawMessage)
    : this()
  {
    UniWebViewLogger.Instance.Debug("Try to parse raw message: " + rawMessage);
    this.RawMessage = rawMessage;
    string[] strArray1 = rawMessage.Split(new string[1]
    {
      "://"
    }, StringSplitOptions.None);
    if (strArray1.Length >= 2)
    {
      this.Scheme = strArray1[0];
      UniWebViewLogger.Instance.Debug("Get scheme: " + this.Scheme);
      string empty = string.Empty;
      for (int index = 1; index < strArray1.Length; ++index)
        empty += strArray1[index];
      UniWebViewLogger.Instance.Verbose("Build path and args string: " + empty);
      string[] strArray2 = empty.Split("?"[0]);
      this.Path = UnityWebRequest.UnEscapeURL(strArray2[0].TrimEnd('/'));
      this.Args = new Dictionary<string, string>();
      if (strArray2.Length <= 1)
        return;
      string str1 = strArray2[1];
      char[] chArray1 = new char[1]{ "&"[0] };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ "="[0] };
        string[] strArray3 = str2.Split(chArray2);
        if (strArray3.Length > 1)
        {
          string key = UnityWebRequest.UnEscapeURL(strArray3[0]);
          if (this.Args.ContainsKey(key))
          {
            string str3 = this.Args[key];
            this.Args[key] = str3 + "," + UnityWebRequest.UnEscapeURL(strArray3[1]);
          }
          else
            this.Args[key] = UnityWebRequest.UnEscapeURL(strArray3[1]);
          UniWebViewLogger.Instance.Debug("Get arg, key: " + key + " value: " + this.Args[key]);
        }
      }
    }
    else
      UniWebViewLogger.Instance.Critical("Bad url scheme. Can not be parsed to UniWebViewMessage: " + rawMessage);
  }

  public string RawMessage { get; private set; }

  public string Scheme { get; private set; }

  public string Path { get; private set; }

  public Dictionary<string, string> Args { get; private set; }
}

﻿// Decompiled with JetBrains decompiler
// Type: UniWebViewMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct UniWebViewMessage
{
  public UniWebViewMessage(string rawMessage)
  {
    this = new UniWebViewMessage();
    this.rawMessage = rawMessage;
    string[] strArray1 = rawMessage.Split(new string[1]{ "://" }, StringSplitOptions.None);
    if (strArray1.Length >= 2)
    {
      this.scheme = strArray1[0];
      string empty = string.Empty;
      for (int index = 1; index < strArray1.Length; ++index)
        empty += strArray1[index];
      string[] strArray2 = empty.Split("?"[0]);
      this.path = strArray2[0].TrimEnd('/');
      this.args = new Dictionary<string, string>();
      if (strArray2.Length <= 1)
        return;
      string str1 = strArray2[1];
      char[] chArray1 = new char[1]{ "&"[0] };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ "="[0] };
        string[] strArray3 = str2.Split(chArray2);
        if (strArray3.Length > 1)
          this.args[strArray3[0]] = WWW.UnEscapeURL(strArray3[1]);
      }
    }
    else
      Debug.LogError((object) ("Bad url scheme. Can not be parsed to UniWebViewMessage: " + rawMessage));
  }

  public string rawMessage { get; private set; }

  public string scheme { get; private set; }

  public string path { get; private set; }

  public Dictionary<string, string> args { get; private set; }
}

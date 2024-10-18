// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_ChapterParam
  {
    public string iname;
    public string name;
    public string expr;
    public string world;
    public long start;
    public long end;
    public string parent;
    public int hide;
    public string chap;
    public string banr;
    public string item;
    public string keyitem1;
    public int keynum1;
    public string keyitem2;
    public int keynum2;
    public string keyitem3;
    public int keynum3;
    public long keytime;
    public string hurl;
    public int limit;
  }
}

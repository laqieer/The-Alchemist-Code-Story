// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_JobSetParam
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
  public class JSON_JobSetParam
  {
    public string iname;
    public string job;
    public int lrare;
    public int lplus;
    public string ljob1;
    public int llv1;
    public string ljob2;
    public int llv2;
    public string ljob3;
    public int llv3;
    public string cjob;
    public string target_unit;
    public int joblv_opened;
  }
}

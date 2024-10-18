// Decompiled with JetBrains decompiler
// Type: SRPG.JobRankParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JobRankParam
  {
    public static readonly int MAX_RANKUP_EQUIPS = 6;
    public int JobChangeCost;
    public string[] JobChangeItems = new string[3];
    public int[] JobChangeItemNums = new int[3];
    public int cost;
    public string[] equips = new string[JobRankParam.MAX_RANKUP_EQUIPS];
    public BuffEffect.BuffValues[] buff_list;
    public OString[] learnings;

    public bool Deserialize(JSON_JobRankParam json)
    {
      if (json == null)
        return false;
      this.JobChangeCost = json.chcost;
      this.JobChangeItems[0] = json.chitm1;
      this.JobChangeItems[1] = json.chitm2;
      this.JobChangeItems[2] = json.chitm3;
      this.JobChangeItemNums[0] = json.chnum1;
      this.JobChangeItemNums[1] = json.chnum2;
      this.JobChangeItemNums[2] = json.chnum3;
      this.cost = json.cost;
      this.equips[0] = json.eqid1;
      this.equips[1] = json.eqid2;
      this.equips[2] = json.eqid3;
      this.equips[3] = json.eqid4;
      this.equips[4] = json.eqid5;
      this.equips[5] = json.eqid6;
      this.learnings = (OString[]) null;
      int length = 0;
      if (!string.IsNullOrEmpty(json.learn1))
        ++length;
      if (!string.IsNullOrEmpty(json.learn2))
        ++length;
      if (!string.IsNullOrEmpty(json.learn3))
        ++length;
      if (length > 0)
      {
        this.learnings = new OString[length];
        int num1 = 0;
        if (!string.IsNullOrEmpty(json.learn1))
          this.learnings[num1++] = (OString) json.learn1;
        if (!string.IsNullOrEmpty(json.learn2))
          this.learnings[num1++] = (OString) json.learn2;
        if (!string.IsNullOrEmpty(json.learn3))
        {
          OString[] learnings = this.learnings;
          int index = num1;
          int num2 = index + 1;
          learnings[index] = (OString) json.learn3;
        }
      }
      return true;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.MultiSendLogBuffer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class MultiSendLogBuffer
  {
    private static readonly int BUFFER_DIGIT = 8;
    private readonly int CAPACITY;
    private readonly int MASK;
    private SceneBattle.MultiPlayRecvData[] mData;
    private int mTop;
    private int mBottom;
    private int mCount;

    public MultiSendLogBuffer()
    {
      this.CAPACITY = 1 << MultiSendLogBuffer.BUFFER_DIGIT;
      this.MASK = this.CAPACITY - 1;
      this.Clear();
    }

    public bool isEmpty => this.mCount == 0;

    public int Count => this.mCount;

    public int Capacity => this.CAPACITY;

    public void Add(SceneBattle.MultiPlayRecvData recv)
    {
      this.mData[this.mBottom] = recv;
      this.mBottom = this.mBottom + 1 & this.MASK;
      if (this.mCount < this.CAPACITY)
        ++this.mCount;
      else
        this.mTop = this.mTop + 1 & this.MASK;
    }

    public SceneBattle.MultiPlayRecvData Get()
    {
      if (this.mCount == 0)
        return (SceneBattle.MultiPlayRecvData) null;
      SceneBattle.MultiPlayRecvData multiPlayRecvData = this.mData[this.mTop];
      this.mTop = this.mTop + 1 & this.MASK;
      --this.mCount;
      return multiPlayRecvData;
    }

    public void Clear()
    {
      this.mData = new SceneBattle.MultiPlayRecvData[this.CAPACITY];
      this.mCount = this.mTop = this.mBottom = 0;
    }
  }
}

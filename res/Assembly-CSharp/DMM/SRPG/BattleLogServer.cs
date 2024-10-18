﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BattleLogServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class BattleLogServer
  {
    public static readonly int MAX_REGISTER_BATTLE_LOG = 512;
    private static readonly int BATTLE_LOG_REPORT_SIZE = 2048;
    private BattleLog[] mLogs;
    private int mLogNum;
    private int mLogTop;
    private StringBuilder mReport = new StringBuilder(BattleLogServer.BATTLE_LOG_REPORT_SIZE);

    public BattleLogServer()
    {
      this.mLogs = new BattleLog[BattleLogServer.MAX_REGISTER_BATTLE_LOG];
      this.Reset();
    }

    public BattleLog this[int offset] => this.mLogs[(this.mLogTop + offset) % this.mLogs.Length];

    public int Num => this.mLogNum;

    public int Top => this.mLogTop;

    public BattleLog Peek => this.mLogs[this.mLogTop];

    public BattleLog Last
    {
      get
      {
        int index = (this.mLogTop + (this.mLogNum - 1)) % this.mLogs.Length;
        if (index < 0)
          index = 0;
        return this.mLogs[index];
      }
    }

    public StringBuilder Report => this.mReport;

    public void Release()
    {
      if (this.mLogs != null)
      {
        for (int index = 0; index < this.mLogs.Length; ++index)
          this.mLogs[index] = (BattleLog) null;
        this.mLogs = (BattleLog[]) null;
      }
      this.mReport = (StringBuilder) null;
    }

    public void Reset()
    {
      for (int index = 0; index < this.mLogs.Length; ++index)
        this.mLogs[index] = (BattleLog) null;
      this.mLogNum = 0;
      this.mLogTop = 0;
    }

    public LogType Log<LogType>() where LogType : BattleLog, new()
    {
      if (this.mLogNum > this.mLogs.Length)
      {
        DebugUtility.LogError("BattleLog: failed many log.");
        return (LogType) null;
      }
      int index = (this.mLogTop + this.mLogNum) % this.mLogs.Length;
      LogType logType = new LogType();
      this.mLogs[index] = (BattleLog) logType;
      ++this.mLogNum;
      return logType;
    }

    public void RemoveLog()
    {
      if (this.mLogs[this.mLogTop] != null)
        this.mLogs[this.mLogTop].Serialize(this.mReport);
      this.mLogs[this.mLogTop] = (BattleLog) null;
      this.mLogTop = (this.mLogTop + 1) % this.mLogs.Length;
      --this.mLogNum;
    }

    public void RemoveLogLast()
    {
      if (this.mLogNum <= 0)
        return;
      int index = (this.mLogTop + (this.mLogNum - 1)) % this.mLogs.Length;
      if (index < 0)
        index = 0;
      this.mLogs[index] = (BattleLog) null;
      --this.mLogNum;
    }

    public void RemoveLogOffs(int offs)
    {
      if (offs < 0 || offs >= this.mLogNum)
        return;
      if (offs == 0)
      {
        this.RemoveLog();
      }
      else
      {
        this.mLogs[(this.mLogTop + offs) % this.mLogs.Length] = (BattleLog) null;
        for (int index1 = 0; index1 < this.mLogNum - offs; ++index1)
        {
          int index2 = (this.mLogTop + offs + index1 + 1) % this.mLogs.Length;
          this.mLogs[(this.mLogTop + offs + index1) % this.mLogs.Length] = this.mLogs[index2];
        }
        if (this.mLogNum == BattleLogServer.MAX_REGISTER_BATTLE_LOG)
          this.mLogs[(this.mLogTop + BattleLogServer.MAX_REGISTER_BATTLE_LOG - 1) % this.mLogs.Length] = (BattleLog) null;
        --this.mLogNum;
      }
    }
  }
}

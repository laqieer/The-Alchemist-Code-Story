// Decompiled with JetBrains decompiler
// Type: SRPG.VersusAudienceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusAudienceManager
  {
    public static readonly float CONNECTTIME_MAX = 30f;
    public readonly int RETRY_MAX = 30;
    private Dictionary<int, List<SceneBattle.MultiPlayRecvData>> mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
    private Dictionary<int, List<VersusDraftList.VersusDraftMessageData>> mDraftTurnLog = new Dictionary<int, List<VersusDraftList.VersusDraftMessageData>>();
    private AudienceStartParam mStartedParam;
    private JSON_MyPhotonRoomParam mRoomParam;
    private DownloadLogger mDownloadLogger;
    private int mReadCnt;
    private int mSkipReadMax;
    private int mDraftReadCnt;
    private int mRetryStartQuestCnt;
    private bool mSkipLog;
    private float mNoneConnectedTime;
    private string mNonAnalyzeLog = string.Empty;
    private VersusAudienceManager.CONNECT_STATE mState;

    public bool IsConnected => this.mState == VersusAudienceManager.CONNECT_STATE.CONNECTED;

    public bool IsDisconnected => this.mState == VersusAudienceManager.CONNECT_STATE.NONE;

    public DownloadLogger Logger => this.mDownloadLogger;

    public bool IsRetryError => this.mRetryStartQuestCnt >= this.RETRY_MAX;

    private int LogLength
    {
      get
      {
        int logLength = 0;
        foreach (int key in this.mTurnLog.Keys)
          logLength += this.mTurnLog[key].Count;
        return logLength;
      }
    }

    private int DraftLogLength
    {
      get
      {
        int draftLogLength = 0;
        foreach (int key in this.mDraftTurnLog.Keys)
          draftLogLength += this.mDraftTurnLog[key].Count;
        return draftLogLength;
      }
    }

    public void Analyze(string log)
    {
      string json = string.Empty;
      string src = string.Empty;
      if (!string.IsNullOrEmpty(this.mNonAnalyzeLog))
      {
        log = this.mNonAnalyzeLog + log;
        this.mNonAnalyzeLog = string.Empty;
      }
      if (log.IndexOf("creatorName") != -1)
        json = log;
      else if (log.IndexOf("players") != -1)
        src = log;
      else if (log.IndexOf("bm") != -1)
      {
        try
        {
          byte[] data = MyEncrypt.Decrypt(JsonUtility.FromJson<AudienceLog>(log).bm);
          VersusDraftList.VersusDraftMessageData buffer1;
          if (GameUtility.Binary2Object<VersusDraftList.VersusDraftMessageData>(out buffer1, data) && buffer1 != null && buffer1.qst == "draft")
          {
            if (!this.mDraftTurnLog.ContainsKey(buffer1.b))
              this.mDraftTurnLog[buffer1.b] = new List<VersusDraftList.VersusDraftMessageData>();
            this.mDraftTurnLog[buffer1.b].Add(buffer1);
          }
          else
          {
            SceneBattle.MultiPlayRecvData buffer2;
            if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out buffer2, data))
            {
              if (!this.mTurnLog.ContainsKey(buffer2.b))
                this.mTurnLog[buffer2.b] = new List<SceneBattle.MultiPlayRecvData>();
              this.mTurnLog[buffer2.b].Add(buffer2);
            }
          }
        }
        catch
        {
          this.mNonAnalyzeLog = log;
        }
      }
      else if (log.IndexOf("bin") != -1)
      {
        try
        {
          SceneBattle.MultiPlayRecvBinData multiPlayRecvBinData = JsonUtility.FromJson<SceneBattle.MultiPlayRecvBinData>(log);
          string buffer;
          GameUtility.Binary2Object<string>(out buffer, multiPlayRecvBinData.bin);
          this.Analyze(buffer);
          return;
        }
        catch
        {
          this.mNonAnalyzeLog = log;
        }
      }
      else
        this.mNonAnalyzeLog = log;
      if (!string.IsNullOrEmpty(json))
      {
        try
        {
          this.mRoomParam = JSON_MyPhotonRoomParam.Parse(json);
        }
        catch
        {
          Debug.LogWarning((object) json);
          this.mNonAnalyzeLog = json;
        }
      }
      if (string.IsNullOrEmpty(src))
        return;
      try
      {
        this.mStartedParam = JSONParser.parseJSONObject<AudienceStartParam>(src);
      }
      catch
      {
        Debug.LogWarning((object) src);
        this.mNonAnalyzeLog = src;
      }
    }

    public void AddStartQuest() => ++this.mRetryStartQuestCnt;

    public AudienceStartParam GetStartedParam() => this.mStartedParam;

    public JSON_MyPhotonRoomParam GetRoomParam() => this.mRoomParam;

    public SceneBattle.MultiPlayRecvData GetData()
    {
      if (this.mReadCnt >= this.LogLength)
        return (SceneBattle.MultiPlayRecvData) null;
      List<SceneBattle.MultiPlayRecvData> multiPlayRecvDataList = new List<SceneBattle.MultiPlayRecvData>();
      foreach (int key in this.mTurnLog.Keys)
        multiPlayRecvDataList.AddRange((IEnumerable<SceneBattle.MultiPlayRecvData>) this.mTurnLog[key]);
      return multiPlayRecvDataList[this.mReadCnt++];
    }

    public VersusDraftList.VersusDraftMessageData GetDraftData()
    {
      if (this.mDraftReadCnt >= this.DraftLogLength)
        return (VersusDraftList.VersusDraftMessageData) null;
      List<VersusDraftList.VersusDraftMessageData> draftMessageDataList = new List<VersusDraftList.VersusDraftMessageData>();
      foreach (int key in this.mDraftTurnLog.Keys)
        draftMessageDataList.AddRange((IEnumerable<VersusDraftList.VersusDraftMessageData>) this.mDraftTurnLog[key]);
      return draftMessageDataList[this.mDraftReadCnt++];
    }

    public void Restore()
    {
      if (this.mReadCnt <= 0)
        return;
      --this.mReadCnt;
    }

    public void Reset()
    {
      if (this.mTurnLog == null)
        this.mTurnLog = new Dictionary<int, List<SceneBattle.MultiPlayRecvData>>();
      this.mTurnLog.Clear();
      if (this.mDraftTurnLog == null)
        this.mDraftTurnLog = new Dictionary<int, List<VersusDraftList.VersusDraftMessageData>>();
      this.mDraftTurnLog.Clear();
      this.mStartedParam = (AudienceStartParam) null;
      this.mRoomParam = (JSON_MyPhotonRoomParam) null;
      this.mNonAnalyzeLog = (string) null;
      this.mReadCnt = 0;
      this.mDraftReadCnt = 0;
      this.mRetryStartQuestCnt = 0;
      this.mDownloadLogger = (DownloadLogger) null;
      this.mDownloadLogger = new DownloadLogger();
      this.mDownloadLogger.Manager = this;
      this.mState = VersusAudienceManager.CONNECT_STATE.REQ;
    }

    public void Add(string data)
    {
      this.Analyze(data);
      this.mState = VersusAudienceManager.CONNECT_STATE.CONNECTED;
      this.mNoneConnectedTime = 0.0f;
    }

    public void Disconnect() => this.mState = VersusAudienceManager.CONNECT_STATE.NONE;

    public void FinishLoad() => this.mSkipReadMax = this.LogLength;

    public void Update()
    {
      if (this.mState != VersusAudienceManager.CONNECT_STATE.CONNECTED)
        return;
      this.mNoneConnectedTime += Time.deltaTime;
      if ((double) this.mNoneConnectedTime < (double) VersusAudienceManager.CONNECTTIME_MAX)
        return;
      this.mState = VersusAudienceManager.CONNECT_STATE.NONE;
    }

    public void ResetTime()
    {
      this.mNoneConnectedTime = 0.0f;
      this.mState = VersusAudienceManager.CONNECT_STATE.REQ;
    }

    public bool SkipMode
    {
      set => this.mSkipLog = value;
    }

    public bool IsSkipEnd => !this.mSkipLog || this.mSkipReadMax == this.mReadCnt;

    public bool IsEnd
    {
      get => !Network.IsStreamConnecting && this.mReadCnt == this.LogLength || Network.IsError;
    }

    private enum CONNECT_STATE
    {
      NONE,
      REQ,
      CONNECTED,
    }
  }
}

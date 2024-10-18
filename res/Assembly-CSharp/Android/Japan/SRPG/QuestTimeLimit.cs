// Decompiled with JetBrains decompiler
// Type: SRPG.QuestTimeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestTimeLimit : MonoBehaviour, IGameParameter
  {
    private float mRefreshInterval = 1f;
    public GameObject Body;
    public Text Timer;
    public bool IsTTMMSS;
    private long mEndTime;

    private void Start()
    {
      this.UpdateValue();
      this.Refresh();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      if (this.mEndTime <= 0L)
      {
        if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.Body != (UnityEngine.Object) null)
          this.Body.SetActive(true);
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        string str1 = (string) null;
        string str2;
        if (this.IsTTMMSS)
        {
          int num1 = Math.Max(Math.Min(timeSpan.Days * 24 + timeSpan.Hours, 99), 0);
          int num2 = Math.Max(Math.Min(timeSpan.Minutes, 59), 0);
          int num3 = Math.Max(Math.Min(timeSpan.Seconds, 59), 0);
          str2 = str1 + string.Format("{0:D2}", (object) num1).ToString() + ":" + string.Format("{0:D2}", (object) num2).ToString() + ":" + string.Format("{0:D2}", (object) num3).ToString();
        }
        else if (timeSpan.TotalDays >= 1.0)
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
          {
            (object) timeSpan.Days
          });
        else if (timeSpan.TotalHours >= 1.0)
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
          {
            (object) timeSpan.Hours
          });
        else
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
          {
            (object) Mathf.Max(timeSpan.Minutes, 0)
          });
        if (!((UnityEngine.Object) this.Timer != (UnityEngine.Object) null) || !(this.Timer.text != str2))
          return;
        this.Timer.text = str2;
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      OpenedQuestArchive dataOfClass1 = DataSource.FindDataOfClass<OpenedQuestArchive>(this.gameObject, (OpenedQuestArchive) null);
      if (dataOfClass1 != null)
      {
        this.mEndTime = TimeManager.FromDateTime(dataOfClass1.end_at);
        this.Refresh();
      }
      else
      {
        QuestParam dataOfClass2 = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
        if (dataOfClass2 != null && dataOfClass2.Chapter != null)
        {
          switch (dataOfClass2.Chapter.GetKeyQuestType())
          {
            case KeyQuestTypes.Timer:
              this.mEndTime = dataOfClass2.Chapter.key_end;
              break;
            case KeyQuestTypes.Count:
              this.mEndTime = 0L;
              break;
            default:
              this.mEndTime = dataOfClass2.Chapter.end;
              break;
          }
          this.Refresh();
        }
        else
        {
          ChapterParam dataOfClass3 = DataSource.FindDataOfClass<ChapterParam>(this.gameObject, (ChapterParam) null);
          if (dataOfClass3 != null)
          {
            switch (dataOfClass3.GetKeyQuestType())
            {
              case KeyQuestTypes.Timer:
                this.mEndTime = dataOfClass3.key_end;
                break;
              case KeyQuestTypes.Count:
                this.mEndTime = 0L;
                break;
              default:
                this.mEndTime = dataOfClass3.end;
                break;
            }
            this.Refresh();
          }
          else
          {
            if (dataOfClass2 == null || dataOfClass2.type != QuestTypes.Tower)
              return;
            this.mEndTime = dataOfClass2.end;
            this.Refresh();
          }
        }
      }
    }
  }
}

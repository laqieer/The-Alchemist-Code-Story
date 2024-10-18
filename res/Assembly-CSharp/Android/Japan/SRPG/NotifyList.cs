// Decompiled with JetBrains decompiler
// Type: SRPG.NotifyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NotifyList : MonoBehaviour
  {
    public string FadeTrigger = "KILL";
    public float FadeTime = 1f;
    private List<NotifyListItem> mItems = new List<NotifyListItem>();
    private List<NotifyListItem> mQueue = new List<NotifyListItem>();
    public float Lifetime = 2f;
    public float Spacing = 10f;
    public float MaxHeight = 400f;
    public float Interval = 0.1f;
    public float FadeInterval = 0.1f;
    public float GroupSpan = 0.8f;
    private static NotifyList mInstance;
    public RectTransform ListParent;
    public NotifyListItem Item_Generic;
    public NotifyListItem Item_LoginBonus;
    public NotifyListItem Item_Mission;
    public NotifyListItem Item_DailyMission;
    public NotifyListItem Item_ContentsUnlock;
    public NotifyListItem Item_QuestSupport;
    public NotifyListItem Item_Award;
    public NotifyListItem Item_MultiInvitation;
    private float mStackHeight;
    private float mGroupTime;
    public string[] DebugItems;

    public static bool hasInstance
    {
      get
      {
        return (UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null;
      }
    }

    public static bool mNotifyEnable { set; get; }

    public static void Push(string msg)
    {
      if (!((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_Generic != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Generic);
      notifyListItem.Message.text = msg;
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushQuestSupport(int count, int gold)
    {
      if (!((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_QuestSupport != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_QuestSupport);
      notifyListItem.Message.text = LocalizedText.Get("sys.NOTIFY_SUPPORT", (object) count, (object) gold);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushContentsUnlock(UnlockParam unlock)
    {
      if (unlock.UnlockTarget == UnlockTargets.Tower || !((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || (!((UnityEngine.Object) NotifyList.mInstance.Item_ContentsUnlock != (UnityEngine.Object) null) || unlock == null))
        return;
      string str = LocalizedText.Get("sys.UNLOCK_" + unlock.iname.ToUpper());
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_ContentsUnlock);
      notifyListItem.Message.text = LocalizedText.Get("sys.NOTIFY_CONTENTSUNLOCK", new object[1]
      {
        (object) str
      });
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushLoginBonus(ItemData data)
    {
      if (!((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_LoginBonus != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_LoginBonus);
      notifyListItem.Message.text = LocalizedText.Get("sys.LOGBO_TODAY", new object[1]
      {
        (object) data.Param.name
      });
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushDailyTrophy(TrophyParam trophy)
    {
      if (!((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_DailyMission != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_DailyMission);
      notifyListItem.Message.text = LocalizedText.Get("sys.TRPYCOMP", new object[1]
      {
        (object) trophy.Name
      });
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushTrophy(TrophyParam trophy)
    {
      if (trophy == null || !((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_Mission != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Mission);
      notifyListItem.Message.text = LocalizedText.Get("sys.TRPYCOMP", new object[1]
      {
        (object) trophy.Name
      });
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushAward(TrophyParam trophy)
    {
      if (trophy == null || !((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_Award != (UnityEngine.Object) null))
        return;
      for (int index = 0; index < trophy.Items.Length; ++index)
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(trophy.Items[index].iname);
        if (awardParam != null)
        {
          NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Award);
          notifyListItem.Message.text = LocalizedText.Get("sys.AWARD_GET", new object[1]
          {
            (object) awardParam.name
          });
          NotifyList.mInstance.Push(notifyListItem);
        }
        else
          DebugUtility.LogError("Not found trophy award. iname is [ " + trophy.Items[index].iname + " ]");
      }
    }

    public static void PushMultiInvitation()
    {
      if (!((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null) || !((UnityEngine.Object) NotifyList.mInstance.Item_MultiInvitation != (UnityEngine.Object) null))
        return;
      NotifyListItem notifyListItem = UnityEngine.Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_MultiInvitation);
      notifyListItem.Message.text = LocalizedText.Get("sys.MULTIINVITATION_NOTIFY");
      NotifyList.mInstance.Push(notifyListItem);
    }

    private bool Push(NotifyListItem item)
    {
      if ((UnityEngine.Object) item == (UnityEngine.Object) null)
        return false;
      RectTransform transform = item.transform as RectTransform;
      item.gameObject.SetActive(true);
      float preferredHeight = LayoutUtility.GetPreferredHeight(transform);
      item.gameObject.SetActive(false);
      item.Lifetime = this.Interval;
      item.Height = preferredHeight;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) item.gameObject);
      this.mQueue.Add(item);
      return true;
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new NotifyList.\u003CStart\u003Ec__Iterator0() { \u0024this = this };
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) NotifyList.mInstance == (UnityEngine.Object) this))
        return;
      NotifyList.mInstance = (NotifyList) null;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) NotifyList.mInstance != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
      else
      {
        NotifyList.mInstance = this;
        if ((UnityEngine.Object) this.Item_Generic != (UnityEngine.Object) null && this.Item_Generic.gameObject.activeInHierarchy)
          this.Item_Generic.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.Item_LoginBonus != (UnityEngine.Object) null && this.Item_LoginBonus.gameObject.activeInHierarchy)
          this.Item_LoginBonus.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.Item_Mission != (UnityEngine.Object) null && this.Item_Mission.gameObject.activeInHierarchy)
          this.Item_Mission.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.Item_DailyMission != (UnityEngine.Object) null && this.Item_DailyMission.gameObject.activeInHierarchy)
          this.Item_DailyMission.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.Item_ContentsUnlock != (UnityEngine.Object) null && this.Item_ContentsUnlock.gameObject.activeInHierarchy)
          this.Item_ContentsUnlock.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.Item_QuestSupport != (UnityEngine.Object) null && this.Item_QuestSupport.gameObject.activeInHierarchy)
          this.Item_QuestSupport.gameObject.SetActive(false);
        NotifyList.mNotifyEnable = true;
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      }
    }

    private void Update()
    {
      float unscaledDeltaTime = Time.unscaledDeltaTime;
      if (this.mItems.Count > 0 || this.mQueue.Count > 0)
        this.mGroupTime += unscaledDeltaTime;
      if (this.mItems.Count > 0)
      {
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          this.mItems[index].Lifetime -= unscaledDeltaTime;
          if ((double) this.mItems[index].Lifetime <= 0.0)
          {
            if (!string.IsNullOrEmpty(this.FadeTrigger))
            {
              Animator component = this.mItems[index].GetComponent<Animator>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                component.SetTrigger(this.FadeTrigger);
            }
            UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].gameObject, this.FadeTime);
            this.mItems.RemoveAt(index);
            --index;
          }
        }
        if (this.mItems.Count <= 0)
        {
          this.mGroupTime = 0.0f;
          this.mStackHeight = 0.0f;
        }
      }
      if (this.mQueue.Count <= 0)
        return;
      if (this.mItems.Count == 0)
        this.mGroupTime = 0.0f;
      NotifyListItem m = this.mQueue[0];
      if ((double) this.mStackHeight + (double) this.mQueue[0].Height + (double) this.Spacing > (double) this.MaxHeight || (double) this.mGroupTime >= (double) this.GroupSpan)
        return;
      if ((double) this.mQueue[0].Lifetime > 0.0)
      {
        this.mQueue[0].Lifetime -= unscaledDeltaTime;
      }
      else
      {
        if (!NotifyList.mNotifyEnable)
          return;
        RectTransform transform = m.transform as RectTransform;
        transform.SetParent((Transform) this.ListParent, false);
        transform.anchoredPosition = new Vector2(0.0f, -this.mStackHeight);
        this.mStackHeight += m.Height + this.Spacing;
        m.gameObject.SetActive(true);
        this.mItems.Add(m);
        this.mQueue.RemoveAt(0);
        for (int index = 0; index < this.mItems.Count; ++index)
          this.mItems[index].Lifetime = this.Lifetime + (float) index * this.FadeInterval;
      }
    }
  }
}

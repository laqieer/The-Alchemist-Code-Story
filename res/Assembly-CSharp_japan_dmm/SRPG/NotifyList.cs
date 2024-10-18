// Decompiled with JetBrains decompiler
// Type: SRPG.NotifyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class NotifyList : MonoBehaviour
  {
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
    private float mStackHeight;
    private float mGroupTime;
    public string[] DebugItems;

    public static bool hasInstance
    {
      get => Object.op_Inequality((Object) NotifyList.mInstance, (Object) null);
    }

    public static bool mNotifyEnable { set; get; }

    public static void Push(string msg)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Generic, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Generic);
      notifyListItem.Message.text = msg;
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushQuestSupport(int count, int gold)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_QuestSupport, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_QuestSupport);
      string formatedText = CurrencyBitmapText.CreateFormatedText(gold.ToString());
      notifyListItem.Message.text = LocalizedText.Get("sys.NOTIFY_SUPPORT", (object) count, (object) formatedText);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushContentsUnlock(UnlockParam unlock)
    {
      if (unlock.UnlockTarget == UnlockTargets.Tower || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_ContentsUnlock, (Object) null) || unlock == null)
        return;
      string str = LocalizedText.Get("sys.UNLOCK_" + unlock.iname.ToUpper());
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_ContentsUnlock);
      notifyListItem.Message.text = LocalizedText.Get("sys.NOTIFY_CONTENTSUNLOCK", (object) str);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushLoginBonus(ItemData data)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_LoginBonus, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_LoginBonus);
      notifyListItem.Message.text = LocalizedText.Get("sys.LOGBO_TODAY", (object) data.Param.name);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushDailyTrophy(TrophyParam trophy)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_DailyMission, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_DailyMission);
      notifyListItem.Message.text = LocalizedText.Get("sys.TRPYCOMP", (object) trophy.Name);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushTrophy(TrophyParam trophy)
    {
      if (trophy == null || trophy.ContainsCondition(TrophyConditionTypes.logincount) || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Mission, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Mission);
      notifyListItem.Message.text = LocalizedText.Get("sys.TRPYCOMP", (object) trophy.Name);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushAward(TrophyParam trophy)
    {
      if (trophy == null || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Award, (Object) null))
        return;
      for (int index = 0; index < trophy.Items.Length; ++index)
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(trophy.Items[index].iname);
        if (awardParam != null)
        {
          NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_Award);
          notifyListItem.Message.text = LocalizedText.Get("sys.AWARD_GET", (object) awardParam.name);
          NotifyList.mInstance.Push(notifyListItem);
        }
        else
          DebugUtility.LogError("Not found trophy award. iname is [ " + trophy.Items[index].iname + " ]");
      }
    }

    public static void PushMultiInvitation()
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_MultiInvitation, (Object) null))
        return;
      NotifyListItem notifyListItem = Object.Instantiate<NotifyListItem>(NotifyList.mInstance.Item_MultiInvitation);
      notifyListItem.Message.text = LocalizedText.Get("sys.MULTIINVITATION_NOTIFY");
      NotifyList.mInstance.Push(notifyListItem);
    }

    private bool Push(NotifyListItem item)
    {
      if (Object.op_Equality((Object) item, (Object) null))
        return false;
      RectTransform transform = ((Component) item).transform as RectTransform;
      ((Component) item).gameObject.SetActive(true);
      float preferredHeight = LayoutUtility.GetPreferredHeight(transform);
      ((Component) item).gameObject.SetActive(false);
      item.Lifetime = this.Interval;
      item.Height = preferredHeight;
      Object.DontDestroyOnLoad((Object) ((Component) item).gameObject);
      this.mQueue.Add(item);
      return true;
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new NotifyList.\u003CStart\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) NotifyList.mInstance, (Object) this))
        return;
      NotifyList.mInstance = (NotifyList) null;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) NotifyList.mInstance, (Object) null))
      {
        Object.Destroy((Object) ((Component) this).gameObject);
      }
      else
      {
        NotifyList.mInstance = this;
        if (Object.op_Inequality((Object) this.Item_Generic, (Object) null) && ((Component) this.Item_Generic).gameObject.activeInHierarchy)
          ((Component) this.Item_Generic).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.Item_LoginBonus, (Object) null) && ((Component) this.Item_LoginBonus).gameObject.activeInHierarchy)
          ((Component) this.Item_LoginBonus).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.Item_Mission, (Object) null) && ((Component) this.Item_Mission).gameObject.activeInHierarchy)
          ((Component) this.Item_Mission).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.Item_DailyMission, (Object) null) && ((Component) this.Item_DailyMission).gameObject.activeInHierarchy)
          ((Component) this.Item_DailyMission).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.Item_ContentsUnlock, (Object) null) && ((Component) this.Item_ContentsUnlock).gameObject.activeInHierarchy)
          ((Component) this.Item_ContentsUnlock).gameObject.SetActive(false);
        if (Object.op_Inequality((Object) this.Item_QuestSupport, (Object) null) && ((Component) this.Item_QuestSupport).gameObject.activeInHierarchy)
          ((Component) this.Item_QuestSupport).gameObject.SetActive(false);
        NotifyList.mNotifyEnable = true;
        Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);
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
              Animator component = ((Component) this.mItems[index]).GetComponent<Animator>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.SetTrigger(this.FadeTrigger);
            }
            Object.Destroy((Object) ((Component) this.mItems[index]).gameObject, this.FadeTime);
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
        RectTransform transform = ((Component) m).transform as RectTransform;
        ((Transform) transform).SetParent((Transform) this.ListParent, false);
        transform.anchoredPosition = new Vector2(0.0f, -this.mStackHeight);
        this.mStackHeight += m.Height + this.Spacing;
        ((Component) m).gameObject.SetActive(true);
        this.mItems.Add(m);
        this.mQueue.RemoveAt(0);
        for (int index = 0; index < this.mItems.Count; ++index)
          this.mItems[index].Lifetime = this.Lifetime + (float) index * this.FadeInterval;
      }
    }
  }
}

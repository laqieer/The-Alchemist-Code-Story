// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExpireItemNotify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/ExpirItemNotify", 32741)]
  [FlowNode.Pin(101, "Open", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(1000, "Close", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ExpireItemNotify : FlowNode
  {
    [SerializeField]
    private int ExpireItemWarningDay = 4;
    private const int PIN_INPUT_OPEN = 101;
    private const int PIN_OUTPUT_CLOSE = 1000;
    [StringIsResourcePath(typeof (GameObject))]
    public string mNotifyPrefabPath;
    [SerializeField]
    private FlowNode_ExpireItemNotify.eNotifyType mNotifyType;
    private static bool IsWarningNotified;
    private int mTargetWarningDay;

    public static void Deserialize(Json_ExpireItem[] json_items)
    {
      if (json_items == null || json_items.Length <= 0)
        return;
      ExpireItemParamList expireItemParamList1 = FlowNode_ExpireItemNotify.GetExpireItem() ?? new ExpireItemParamList();
      List<ExpireItemParam> expireItemParamList2 = new List<ExpireItemParam>();
      if (expireItemParamList1.items != null)
      {
        for (int index = 0; index < expireItemParamList1.items.Length; ++index)
          expireItemParamList2.Add(expireItemParamList1.items[index]);
      }
      for (int index = 0; index < json_items.Length; ++index)
      {
        ExpireItemParam expireItemParam = new ExpireItemParam();
        expireItemParam.Deserialize(json_items[index]);
        expireItemParamList2.Add(expireItemParam);
      }
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.PREFS_KEY_EXPIRE_ITEM, JsonUtility.ToJson((object) new ExpireItemParamList()
      {
        items = expireItemParamList2.ToArray()
      }), true);
    }

    public static void ResetParam() => FlowNode_ExpireItemNotify.IsWarningNotified = false;

    private static void ClearPrefs()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_EXPIRE_ITEM))
        return;
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.PREFS_KEY_EXPIRE_ITEM, string.Empty);
    }

    private static ExpireItemParamList GetExpireItem()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_EXPIRE_ITEM))
        return (ExpireItemParamList) null;
      string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_EXPIRE_ITEM, string.Empty);
      if (string.IsNullOrEmpty(str))
        return (ExpireItemParamList) null;
      try
      {
        ExpireItemParamList expireItemParamList = JsonUtility.FromJson<ExpireItemParamList>(str);
        if (expireItemParamList != null)
        {
          if (expireItemParamList.items != null)
          {
            if (expireItemParamList.items.Length > 0)
              return expireItemParamList.items == null ? (ExpireItemParamList) null : expireItemParamList;
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
      }
      return (ExpireItemParamList) null;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 101)
        return;
      this.Open();
    }

    private void Open()
    {
      switch (this.mNotifyType)
      {
        case FlowNode_ExpireItemNotify.eNotifyType.Warning:
          this.Open_ExpireWarning();
          break;
        case FlowNode_ExpireItemNotify.eNotifyType.Expired:
          this.Open_ExpiredNotify();
          break;
      }
    }

    private void Open_ExpireWarning()
    {
      if (FlowNode_ExpireItemNotify.IsWarningNotified)
      {
        this.ActivateOutputLinks(1000);
      }
      else
      {
        FlowNode_ExpireItemNotify.IsWarningNotified = true;
        this.mTargetWarningDay = this.ExpireItemWarningDay;
        this.Open_ExpireWarning(this.mTargetWarningDay);
      }
    }

    private void Open_ExpireWarning(int target_day)
    {
      List<ItemData>[] expireWarningItems = this.GetExpireWarningItems(target_day);
      if (expireWarningItems == null)
      {
        this.NextWarning();
      }
      else
      {
        int rest_day = target_day - 1;
        if (expireWarningItems.Length <= rest_day || expireWarningItems[rest_day] == null || expireWarningItems[rest_day].Count <= 0)
        {
          this.NextWarning();
        }
        else
        {
          GameObject gameObject1 = AssetManager.Load<GameObject>(this.mNotifyPrefabPath);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          {
            this.NextWarning();
          }
          else
          {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
            Canvas canvas = UIUtility.PushCanvas();
            gameObject2.transform.SetParent(((Component) canvas).transform, false);
            ExpireItemWindow component = gameObject2.GetComponent<ExpireItemWindow>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.Setup_ExpireWarning(expireWarningItems[rest_day], rest_day);
            gameObject2.AddComponent<DestroyEventListener>().Listeners = (DestroyEventListener.DestroyEvent) (go =>
            {
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) canvas).gameObject);
              this.NextWarning();
            });
          }
        }
      }
    }

    private void NextWarning()
    {
      --this.mTargetWarningDay;
      if (this.mTargetWarningDay <= 0)
        this.ActivateOutputLinks(1000);
      else
        this.Open_ExpireWarning(this.mTargetWarningDay);
    }

    private void Open_ExpiredNotify()
    {
      ExpireItemParamList expireItem = FlowNode_ExpireItemNotify.GetExpireItem();
      if (expireItem == null)
      {
        this.ActivateOutputLinks(1000);
      }
      else
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(this.mNotifyPrefabPath);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          this.ActivateOutputLinks(1000);
        }
        else
        {
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
          Canvas canvas = UIUtility.PushCanvas();
          gameObject2.transform.SetParent(((Component) canvas).transform, false);
          ExpireItemWindow component = gameObject2.GetComponent<ExpireItemWindow>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.Setup_ExpiredNotify(expireItem);
          gameObject2.AddComponent<DestroyEventListener>().Listeners = (DestroyEventListener.DestroyEvent) (go =>
          {
            FlowNode_ExpireItemNotify.ClearPrefs();
            this.ActivateOutputLinks(1000);
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) canvas).gameObject);
          });
        }
      }
    }

    private List<ItemData>[] GetExpireWarningItems(int target_day)
    {
      List<ItemData> all = MonoSingleton<GameManager>.Instance.Player.Items.FindAll((Predicate<ItemData>) (item => item.Param.IsLimited && item.Num > 0));
      if (all == null || all.Count <= 0)
        return (List<ItemData>[]) null;
      List<ItemData>[] array = new List<ItemData>[target_day];
      for (int index = 0; index < array.Length; ++index)
        array[index] = new List<ItemData>();
      bool flag = false;
      for (int index1 = 0; index1 < target_day; ++index1)
      {
        DateTime dateTime = TimeManager.ServerTime.AddDays((double) index1);
        for (int index2 = 0; index2 < all.Count; ++index2)
        {
          if (!all[index2].Param.IsExpire && all[index2].Param.end_at.Date <= dateTime.Date && !this.MyContains<ItemData>(array, all[index2]))
          {
            array[index1].Add(all[index2]);
            flag = true;
          }
        }
      }
      return !flag ? (List<ItemData>[]) null : array;
    }

    private bool MyContains<T>(List<T>[] array, T target)
    {
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index].Contains(target))
          return true;
      }
      return false;
    }

    public enum eNotifyType
    {
      None,
      Warning,
      Expired,
    }
  }
}

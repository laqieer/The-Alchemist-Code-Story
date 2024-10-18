// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RaidResultElement : MonoBehaviour, IFlowInterface
  {
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger = "on";
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval = 1f;
    private float mTimeScale = 1f;
    public Text TxtTitle;
    public Text TxtExp;
    public Text TxtGold;
    public Transform ItemParent;
    public GameObject ItemTemplate;
    private List<GameObject> mItems;
    private bool mRequest;
    private bool mFinished;

    public float TimeScale
    {
      get
      {
        return this.mTimeScale;
      }
      set
      {
        this.mTimeScale = Mathf.Clamp(value, 0.1f, 10f);
      }
    }

    public void Start()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null))
        return;
      this.ItemTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
    }

    public bool IsRequest()
    {
      return this.mRequest;
    }

    public bool IsFinished()
    {
      return this.mFinished;
    }

    public void RequestAnimation()
    {
      RaidQuestResult dataOfClass = DataSource.FindDataOfClass<RaidQuestResult>(this.gameObject, (RaidQuestResult) null);
      if (dataOfClass == null)
      {
        this.mFinished = true;
      }
      else
      {
        if (this.IsRequest() || this.IsFinished())
          return;
        this.mRequest = true;
        if ((UnityEngine.Object) this.TxtTitle != (UnityEngine.Object) null)
          this.TxtTitle.text = string.Format(LocalizedText.Get("sys.RAID_RESULT_INDEX"), (object) (dataOfClass.index + 1));
        if ((UnityEngine.Object) this.TxtExp != (UnityEngine.Object) null)
          this.TxtExp.text = dataOfClass.uexp.ToString();
        if ((UnityEngine.Object) this.TxtGold != (UnityEngine.Object) null)
          this.TxtGold.text = dataOfClass.gold.ToString();
        if (dataOfClass.drops != null)
        {
          this.mItems = new List<GameObject>(dataOfClass.drops.Length);
          for (int index = 0; index < dataOfClass.drops.Length; ++index)
          {
            if (dataOfClass.drops[index] != null)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
              gameObject.transform.SetParent(this.ItemParent, false);
              DataSource.Bind<ItemData>(gameObject, dataOfClass.drops[index]);
              this.mItems.Add(gameObject);
            }
          }
        }
        this.gameObject.SetActive(true);
        GameParameter.UpdateAll(this.gameObject);
        this.StartCoroutine(this.TreasureAnimation());
      }
    }

    [DebuggerHidden]
    private IEnumerator TreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultElement.\u003CTreasureAnimation\u003Ec__Iterator11D() { \u003C\u003Ef__this = this };
    }
  }
}

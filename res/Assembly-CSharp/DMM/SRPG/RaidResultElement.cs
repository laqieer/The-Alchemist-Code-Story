// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RaidResultElement : MonoBehaviour, IFlowInterface
  {
    public Text TxtTitle;
    public Text TxtExp;
    public Text TxtGold;
    public Transform ItemParent;
    public GameObject ItemTemplate;
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger = "on";
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval = 1f;
    private List<GameObject> mItems;
    private bool mRequest;
    private bool mFinished;
    private float mTimeScale = 1f;

    public float TimeScale
    {
      get => this.mTimeScale;
      set => this.mTimeScale = Mathf.Clamp(value, 0.1f, 10f);
    }

    public void Start()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      this.ItemTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
    }

    public bool IsRequest() => this.mRequest;

    public bool IsFinished() => this.mFinished;

    public void RequestAnimation()
    {
      RaidQuestResult dataOfClass = DataSource.FindDataOfClass<RaidQuestResult>(((Component) this).gameObject, (RaidQuestResult) null);
      if (dataOfClass == null)
      {
        this.mFinished = true;
      }
      else
      {
        if (this.IsRequest() || this.IsFinished())
          return;
        this.mRequest = true;
        if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
          this.TxtTitle.text = string.Format(LocalizedText.Get("sys.RAID_RESULT_INDEX"), (object) (dataOfClass.index + 1));
        if (Object.op_Inequality((Object) this.TxtExp, (Object) null))
          this.TxtExp.text = dataOfClass.uexp.ToString();
        if (Object.op_Inequality((Object) this.TxtGold, (Object) null))
          this.TxtGold.text = dataOfClass.gold.ToString();
        if (dataOfClass.drops != null)
        {
          this.mItems = new List<GameObject>(dataOfClass.drops.Length);
          for (int index = 0; index < dataOfClass.drops.Length; ++index)
          {
            if (dataOfClass.drops[index] != null)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
              gameObject.transform.SetParent(this.ItemParent, false);
              DataSource.Bind<QuestResult.DropItemData>(gameObject, dataOfClass.drops[index]);
              this.mItems.Add(gameObject);
            }
          }
        }
        ((Component) this).gameObject.SetActive(true);
        GameParameter.UpdateAll(((Component) this).gameObject);
        this.StartCoroutine(this.TreasureAnimation());
      }
    }

    [DebuggerHidden]
    private IEnumerator TreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultElement.\u003CTreasureAnimation\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusSeasonReward : MonoBehaviour
  {
    private readonly float WAIT_TIME = 0.5f;
    private readonly float WAIT_OPEN = 3f;
    public GameObject item;
    public GameObject root;
    public GameObject template;
    public GameObject parent;
    public GameObject arrival;
    public Text floorTxt;
    public Text floorEffTxt;
    public Text rewardTxt;
    public Text okTxt;
    public Text getTxt;
    public string openAnim;
    public string nextAnim;
    public string resultAnim;
    private int mNow;
    private int mMax;
    private float mWaitTime;
    private VersusSeasonReward.MODE mMode = VersusSeasonReward.MODE.REQ;

    private void Start() => this.Refresh();

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      if (Object.op_Inequality((Object) this.floorTxt, (Object) null))
        this.floorTxt.text = player.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      if (Object.op_Inequality((Object) this.floorEffTxt, (Object) null))
        this.floorEffTxt.text = player.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      int index = player.VersusTowerFloor - 1;
      if (versusTowerParam == null || index < 0 || index >= versusTowerParam.Length)
        return;
      this.mNow = 0;
      if (versusTowerParam[index].SeasonIteminame != null)
        this.mMax = versusTowerParam[index].SeasonIteminame.Length;
      DataSource.Bind<VersusTowerParam>(this.item, versusTowerParam[index]);
      if (this.mNow + 1 < this.mMax)
        this.SetButtonText(true);
      this.mWaitTime = this.WAIT_OPEN;
      this.mMode = VersusSeasonReward.MODE.NEXT;
    }

    private void SetButtonText(bool bNext)
    {
      if (Object.op_Inequality((Object) this.okTxt, (Object) null))
        this.okTxt.text = LocalizedText.Get(!bNext ? "sys.CMD_OK" : "sys.BTN_NEXT");
      if (bNext || !Object.op_Inequality((Object) this.rewardTxt, (Object) null))
        return;
      this.rewardTxt.text = LocalizedText.Get("sys.MULTI_VERSUS_SEND_GIFT");
    }

    private void Update()
    {
      switch (this.mMode)
      {
        case VersusSeasonReward.MODE.REQ:
          while (!this.SetData(this.mNow, true))
            ++this.mNow;
          this.mMode = VersusSeasonReward.MODE.COUNTDOWN;
          this.mWaitTime = this.WAIT_TIME;
          break;
        case VersusSeasonReward.MODE.COUNTDOWN:
          this.mWaitTime -= Time.deltaTime;
          if ((double) this.mWaitTime > 0.0)
            break;
          this.mMode = VersusSeasonReward.MODE.WAIT;
          break;
        case VersusSeasonReward.MODE.NEXT:
        case VersusSeasonReward.MODE.FINISH:
          this.mWaitTime -= Time.deltaTime;
          if ((double) this.mWaitTime > 0.0)
            break;
          if (this.mMode == VersusSeasonReward.MODE.FINISH)
          {
            this.mMode = VersusSeasonReward.MODE.END;
            break;
          }
          this.mMode = VersusSeasonReward.MODE.REQ;
          break;
      }
    }

    private bool SetData(int idx, bool bPlay = false, GameObject obj = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = !Object.op_Inequality((Object) obj, (Object) null) ? this.item : obj;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam();
      if (versusTowerParam != null && versusTowerParam.SeasonIteminame != null && !string.IsNullOrEmpty((string) versusTowerParam.SeasonIteminame[idx]))
      {
        if (versusTowerParam.SeasonItemType[idx] == VERSUS_ITEM_TYPE.award && instance.Player.IsHaveAward((string) versusTowerParam.SeasonIteminame[idx]))
          return false;
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          VersusTowerRewardItem component = gameObject.GetComponent<VersusTowerRewardItem>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetData(VersusTowerRewardItem.REWARD_TYPE.Season, idx);
          if (bPlay)
            this.ReqAnim(this.openAnim);
          this.SetRewardName(idx, versusTowerParam);
        }
      }
      if (Object.op_Inequality((Object) this.arrival, (Object) null))
        this.arrival.SetActive(false);
      return true;
    }

    private void SetRewardName(int idx, VersusTowerParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = (int) param.SeasonItemnum[idx];
      string key = (string) param.SeasonIteminame[idx];
      VERSUS_ITEM_TYPE versusItemType = param.SeasonItemType[idx];
      string str1 = LocalizedText.Get("sys.MULTI_VERSUS_REWARD_GET_MSG");
      if (!Object.op_Inequality((Object) this.rewardTxt, (Object) null))
        return;
      string str2 = string.Empty;
      switch (versusItemType)
      {
        case VERSUS_ITEM_TYPE.item:
          ItemParam itemParam = instance.GetItemParam(key);
          if (itemParam != null)
          {
            str2 = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.gold:
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
        case VERSUS_ITEM_TYPE.coin:
          str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case VERSUS_ITEM_TYPE.unit:
          UnitParam unitParam = instance.GetUnitParam(key);
          if (unitParam != null)
          {
            str2 = unitParam.name;
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(key);
          if (artifactParam != null)
          {
            str2 = artifactParam.name;
            break;
          }
          break;
        case VERSUS_ITEM_TYPE.award:
          AwardParam awardParam = instance.GetAwardParam(key);
          if (awardParam != null)
            str2 = awardParam.name;
          str1 = LocalizedText.Get("sys.MULTI_VERSUS_REWARD_GET_AWARD");
          break;
      }
      this.rewardTxt.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_NAME"), (object) str2);
      if (!Object.op_Inequality((Object) this.getTxt, (Object) null))
        return;
      this.getTxt.text = str1;
    }

    private void ReqAnim(string anim)
    {
      if (!Object.op_Inequality((Object) this.root, (Object) null))
        return;
      Animator component = this.root.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Play(anim);
    }

    private void CreateResult()
    {
      VersusTowerParam versusTowerParam = MonoSingleton<GameManager>.Instance.GetCurrentVersusTowerParam();
      if (versusTowerParam == null || !Object.op_Inequality((Object) this.template, (Object) null))
        return;
      for (int idx = 0; idx < versusTowerParam.SeasonIteminame.Length; ++idx)
      {
        GameObject obj = Object.Instantiate<GameObject>(this.template);
        if (Object.op_Inequality((Object) obj, (Object) null))
        {
          DataSource.Bind<VersusTowerParam>(obj, versusTowerParam);
          obj.SetActive(true);
          if (this.SetData(idx, obj: obj))
          {
            if (Object.op_Inequality((Object) this.parent, (Object) null))
              obj.transform.SetParent(this.parent.transform, false);
          }
          else
            obj.SetActive(false);
        }
      }
      this.template.SetActive(false);
      this.item.SetActive(false);
    }

    public void OnClickNext()
    {
      if (this.mMode == VersusSeasonReward.MODE.WAIT)
      {
        this.mWaitTime = this.WAIT_TIME;
        if (++this.mNow < this.mMax)
        {
          this.mMode = VersusSeasonReward.MODE.NEXT;
          this.ReqAnim(this.nextAnim);
        }
        else
        {
          this.CreateResult();
          this.ReqAnim(this.resultAnim);
          this.SetButtonText(false);
          this.mMode = VersusSeasonReward.MODE.FINISH;
        }
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap);
      }
      else
      {
        if (this.mMode != VersusSeasonReward.MODE.END)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap);
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "Finish");
        this.mMode = VersusSeasonReward.MODE.NONE;
      }
    }

    private enum MODE
    {
      NONE,
      REQ,
      COUNTDOWN,
      WAIT,
      NEXT,
      FINISH,
      END,
    }
  }
}

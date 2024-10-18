// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusSeasonReward : MonoBehaviour
  {
    private readonly float WAIT_TIME = 0.5f;
    private readonly float WAIT_OPEN = 3f;
    private VersusSeasonReward.MODE mMode = VersusSeasonReward.MODE.REQ;
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

    private void Start()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      if ((UnityEngine.Object) this.floorTxt != (UnityEngine.Object) null)
        this.floorTxt.text = player.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
      if ((UnityEngine.Object) this.floorEffTxt != (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.okTxt != (UnityEngine.Object) null)
        this.okTxt.text = LocalizedText.Get(!bNext ? "sys.CMD_OK" : "sys.BTN_NEXT");
      if (bNext || !((UnityEngine.Object) this.rewardTxt != (UnityEngine.Object) null))
        return;
      this.rewardTxt.text = LocalizedText.Get("sys.MULTI_VERSUS_SEND_GIFT");
    }

    private void Update()
    {
      switch (this.mMode)
      {
        case VersusSeasonReward.MODE.REQ:
          while (!this.SetData(this.mNow, true, (GameObject) null))
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
      GameObject gameObject = !((UnityEngine.Object) obj != (UnityEngine.Object) null) ? this.item : obj;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam != null && versusTowerParam.SeasonIteminame != null && !string.IsNullOrEmpty((string) versusTowerParam.SeasonIteminame[idx]))
      {
        if (versusTowerParam.SeasonItemType[idx] == VERSUS_ITEM_TYPE.award && instance.Player.IsHaveAward((string) versusTowerParam.SeasonIteminame[idx]))
          return false;
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        {
          gameObject.SetActive(true);
          VersusTowerRewardItem component = gameObject.GetComponent<VersusTowerRewardItem>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.SetData(VersusTowerRewardItem.REWARD_TYPE.Season, idx);
          if (bPlay)
            this.ReqAnim(this.openAnim);
          this.SetRewardName(idx, versusTowerParam);
        }
      }
      if ((UnityEngine.Object) this.arrival != (UnityEngine.Object) null)
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
      if (!((UnityEngine.Object) this.rewardTxt != (UnityEngine.Object) null))
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
      if (!((UnityEngine.Object) this.getTxt != (UnityEngine.Object) null))
        return;
      this.getTxt.text = str1;
    }

    private void ReqAnim(string anim)
    {
      if (!((UnityEngine.Object) this.root != (UnityEngine.Object) null))
        return;
      Animator component = this.root.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Play(anim);
    }

    private void CreateResult()
    {
      VersusTowerParam versusTowerParam = MonoSingleton<GameManager>.Instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam == null || !((UnityEngine.Object) this.template != (UnityEngine.Object) null))
        return;
      for (int idx = 0; idx < versusTowerParam.SeasonIteminame.Length; ++idx)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.template);
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        {
          DataSource.Bind<VersusTowerParam>(gameObject, versusTowerParam);
          gameObject.SetActive(true);
          if (this.SetData(idx, false, gameObject))
          {
            if ((UnityEngine.Object) this.parent != (UnityEngine.Object) null)
              gameObject.transform.SetParent(this.parent.transform, false);
          }
          else
            gameObject.SetActive(false);
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
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
      }
      else
      {
        if (this.mMode != VersusSeasonReward.MODE.END)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
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

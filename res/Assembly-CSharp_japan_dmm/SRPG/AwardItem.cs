// Decompiled with JetBrains decompiler
// Type: SRPG.AwardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "UpdateConfigPlayerInfo", FlowNode.PinTypes.Input, 0)]
  public class AwardItem : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject AwardBG;
    [SerializeField]
    private Text AwardTxt;
    public AwardItem.PlayerType Type;
    private ImageArray mImageArray;
    private bool IsDone;
    private string mSelectedAward;
    private bool IsRefresh;
    private AwardParam mAwardParam;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.IsRefresh = false;
      this.SetUp();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.AwardBG, (Object) null))
      {
        this.AwardBG.SetActive(false);
        ImageArray component = this.AwardBG.GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.mImageArray = component;
      }
      if (!Object.op_Inequality((Object) this.AwardTxt, (Object) null))
        return;
      this.AwardTxt.text = LocalizedText.Get("sys.TEXT_NOT_SELECT");
      ((Component) this.AwardTxt).gameObject.SetActive(false);
    }

    private void Start() => this.Initialize();

    private void OnEnable() => this.Initialize();

    private void Initialize()
    {
      this.SetUp();
      this.IsRefresh = false;
    }

    private void Update()
    {
      if (!this.IsDone || this.IsRefresh)
        return;
      this.IsRefresh = true;
      this.Refresh();
    }

    private void SetUp()
    {
      string str = string.Empty;
      if (this.Type == AwardItem.PlayerType.Player)
      {
        PlayerData dataOfClass = DataSource.FindDataOfClass<PlayerData>(((Component) this).gameObject, (PlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectedAward;
      }
      else if (this.Type == AwardItem.PlayerType.Friend)
      {
        FriendData dataOfClass = DataSource.FindDataOfClass<FriendData>(((Component) this).gameObject, (FriendData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.ArenaPlayer)
      {
        ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).gameObject, (ArenaPlayer) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.MultiPlayer)
      {
        JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).gameObject, (JSON_MyPhotonPlayerParam) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.ChatPlayer)
      {
        ChatPlayerData dataOfClass = DataSource.FindDataOfClass<ChatPlayerData>(((Component) this).gameObject, (ChatPlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.TowerPlayer)
      {
        TowerResuponse.TowerRankParam dataOfClass = DataSource.FindDataOfClass<TowerResuponse.TowerRankParam>(((Component) this).gameObject, (TowerResuponse.TowerRankParam) null);
        if (dataOfClass != null)
          str = dataOfClass.selected_award;
      }
      else if (this.Type == AwardItem.PlayerType.GuildMember)
      {
        GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(((Component) this).gameObject, (GuildMemberData) null);
        if (dataOfClass != null)
          str = dataOfClass.Award;
      }
      else if (this.Type == AwardItem.PlayerType.AwardParam)
      {
        AwardParam dataOfClass = DataSource.FindDataOfClass<AwardParam>(((Component) this).gameObject, (AwardParam) null);
        if (dataOfClass != null)
          str = dataOfClass.iname;
      }
      this.mSelectedAward = str;
      if (!string.IsNullOrEmpty(this.mSelectedAward))
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAwardParam(this.mSelectedAward);
        if (awardParam != null)
          this.mAwardParam = awardParam;
      }
      else
        this.mAwardParam = (AwardParam) null;
      this.IsDone = true;
    }

    public void Refresh()
    {
      this.SetUp();
      if (this.mAwardParam != null)
      {
        if (Object.op_Inequality((Object) this.mImageArray, (Object) null))
        {
          if (this.mImageArray.Images.Length <= this.mAwardParam.grade)
          {
            this.SetExtraAwardImage();
            this.AwardTxt.text = string.Empty;
          }
          else
          {
            this.mImageArray.ImageIndex = this.mAwardParam.grade;
            this.AwardTxt.text = string.IsNullOrEmpty(this.mAwardParam.name) ? LocalizedText.Get("sys.TEXT_NOT_SELECT") : this.mAwardParam.name;
          }
        }
      }
      else
      {
        if (Object.op_Inequality((Object) this.mImageArray, (Object) null))
          this.mImageArray.ImageIndex = 0;
        if (Object.op_Inequality((Object) this.AwardTxt, (Object) null))
          this.AwardTxt.text = LocalizedText.Get("sys.TEXT_NOT_SELECT");
      }
      if (Object.op_Inequality((Object) this.AwardBG, (Object) null))
        this.AwardBG.SetActive(true);
      if (!Object.op_Inequality((Object) this.AwardTxt, (Object) null))
        return;
      ((Component) this.AwardTxt).gameObject.SetActive(true);
    }

    private bool SetExtraAwardImage()
    {
      if (this.mAwardParam == null)
        return false;
      string bg = this.mAwardParam.bg;
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (Object.op_Inequality((Object) spriteSheet, (Object) null))
        this.mImageArray.sprite = spriteSheet.GetSprite(bg);
      return true;
    }

    public enum PlayerType : byte
    {
      Player = 0,
      Friend = 1,
      ArenaPlayer = 2,
      MultiPlayer = 3,
      ChatPlayer = 4,
      TowerPlayer = 5,
      GuildMember = 6,
      AwardParam = 100, // 0x64
    }
  }
}

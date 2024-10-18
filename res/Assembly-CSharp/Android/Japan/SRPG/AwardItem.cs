// Decompiled with JetBrains decompiler
// Type: SRPG.AwardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) this.AwardBG != (UnityEngine.Object) null)
      {
        this.AwardBG.SetActive(false);
        ImageArray component = this.AwardBG.GetComponent<ImageArray>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          this.mImageArray = component;
      }
      if (!((UnityEngine.Object) this.AwardTxt != (UnityEngine.Object) null))
        return;
      this.AwardTxt.text = LocalizedText.Get("sys.TEXT_NOT_SELECT");
      this.AwardTxt.gameObject.SetActive(false);
    }

    private void Start()
    {
      this.Initialize();
    }

    private void OnEnable()
    {
      this.Initialize();
    }

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
        PlayerData dataOfClass = DataSource.FindDataOfClass<PlayerData>(this.gameObject, (PlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectedAward;
      }
      else if (this.Type == AwardItem.PlayerType.Friend)
      {
        FriendData dataOfClass = DataSource.FindDataOfClass<FriendData>(this.gameObject, (FriendData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.ArenaPlayer)
      {
        ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(this.gameObject, (ArenaPlayer) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.MultiPlayer)
      {
        JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(this.gameObject, (JSON_MyPhotonPlayerParam) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.ChatPlayer)
      {
        ChatPlayerData dataOfClass = DataSource.FindDataOfClass<ChatPlayerData>(this.gameObject, (ChatPlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.TowerPlayer)
      {
        TowerResuponse.TowerRankParam dataOfClass = DataSource.FindDataOfClass<TowerResuponse.TowerRankParam>(this.gameObject, (TowerResuponse.TowerRankParam) null);
        if (dataOfClass != null)
          str = dataOfClass.selected_award;
      }
      else if (this.Type == AwardItem.PlayerType.GuildMember)
      {
        GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(this.gameObject, (GuildMemberData) null);
        if (dataOfClass != null)
          str = dataOfClass.Award;
      }
      else if (this.Type == AwardItem.PlayerType.AwardParam)
      {
        AwardParam dataOfClass = DataSource.FindDataOfClass<AwardParam>(this.gameObject, (AwardParam) null);
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

    private void Refresh()
    {
      this.SetUp();
      if (this.mAwardParam != null)
      {
        if ((UnityEngine.Object) this.mImageArray != (UnityEngine.Object) null)
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
        this.mImageArray.ImageIndex = 0;
        this.AwardTxt.text = LocalizedText.Get("sys.TEXT_NOT_SELECT");
      }
      if ((UnityEngine.Object) this.AwardBG != (UnityEngine.Object) null)
        this.AwardBG.SetActive(true);
      if (!((UnityEngine.Object) this.AwardTxt != (UnityEngine.Object) null))
        return;
      this.AwardTxt.gameObject.SetActive(true);
    }

    private bool SetExtraAwardImage()
    {
      if (this.mAwardParam == null)
        return false;
      string bg = this.mAwardParam.bg;
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null)
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

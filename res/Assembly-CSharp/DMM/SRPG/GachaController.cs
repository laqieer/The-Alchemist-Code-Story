// Decompiled with JetBrains decompiler
// Type: SRPG.GachaController
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
  [FlowNode.Pin(1, "AllSkip", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "InputNextBtn", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "NextBtnEnable", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "NextBtnDisable", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(200, "Setup CardUnitRarity", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Finish CardUnitAnim", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(202, "Next Gacha Phase", FlowNode.PinTypes.Input, 202)]
  [FlowNode.Pin(250, "Setuped CardUnitAnim", FlowNode.PinTypes.Output, 250)]
  [FlowNode.Pin(251, "Started CardUnitAnim", FlowNode.PinTypes.Output, 251)]
  [FlowNode.Pin(252, "Finished CardUnitAnim", FlowNode.PinTypes.Output, 252)]
  [FlowNode.Pin(50, "Setupd", FlowNode.PinTypes.Output, 50)]
  public class GachaController : MonoSingleton<GachaController>, IFlowInterface
  {
    private const int PIN_OT_SETUPED = 50;
    public GameObject DropStone;
    public GameObject DropStone_CCard;
    public GameObject DropStone_Item;
    public GameObject DropStone_Artifact;
    public GameObject DropMaterial;
    public RawImage DropMaterialImage;
    public RawImage DropMaterialBlurImage;
    public RawImage DropMaterialIconImage;
    public Image DropMaterialIconFrameImage;
    public GameObject ItemThumbnailPrefab;
    public GameObject UnitThumbnailPrefab;
    public GameObject ArtifactThumbnailPrefab;
    public Transform ThumbnailPool;
    public GameObject StartArrowB;
    public GameObject StartArrowTop;
    public GameObject[] StartArrowTopMasks;
    public GameObject StartStone;
    public GameObject StartStoneMask;
    public GameObject StartStoneEff01;
    public GameObject StartStoneEff02;
    public GameObject StartStoneEff03;
    public Text MaterialName;
    public Text MaterialComment;
    public Text MaterialCount;
    public ImageSpriteSheet MaterialRuneType;
    public GameObject[] ResetMaterials;
    public Sprite[] StartArrowSprite;
    public Sprite[] StartArrowTopSprite;
    public Sprite[] StartStoneSprite;
    private TouchController mTouchController;
    public GameObject GaugeObject;
    public bool OnShardEffect = true;
    private StateMachine<GachaController> mState;
    private bool isSkipping;
    private List<GameObject> mDropStones = new List<GameObject>();
    private GameObject mDropMaterial;
    public GameObject OpenMaterial;
    public GameObject OpenItem;
    private bool mIgnoreDragVelocity;
    private bool mDraged;
    private bool mDraging;
    private bool mClicked;
    private float mDragY;
    private float mDragX;
    private float mDragEndX;
    private float mDragEndY;
    public float MIN_SWIPE_DIST_X = 400f;
    public float MIN_SWIPE_DIST_Y = 400f;
    public float StoneRadius;
    public float StoneAppear;
    public Texture[] StoneBases;
    public Texture[] StoneHand01s;
    public Texture[] StoneHand02s;
    public Texture[] StoneEye01s;
    public Texture[] StoneEye02s;
    public Texture stoneBaseN;
    public Sprite[] LithographBases;
    public float StoneRotateTime;
    public Text ConceptCardNameText;
    public ImageArray ConceptCardFrame;
    public Texture[] ConceptCardStoneBases;
    public Texture[] ItemStoneBases;
    public Texture[] ArtifactStoneBases;
    public GameObject ConceptCardRendererPrefab;
    private ConceptCardCompositeRenderer mConceptCardRenderer;
    private List<GameObject> mUseThumbnailList = new List<GameObject>(10);
    private const float MinWaitBeforMoveDropStone = 2f;
    private MySound.VolumeHandle mBGMVolume;
    private MySound.PlayHandle mJingleHandle;
    private bool AllAnimSkip;
    private bool mLithograph = true;
    public static readonly int MAX_VIEW_STONE = 10;
    private int mViewStoneCount;
    private int mMaxPage;
    private int mCurrentPage;
    private bool IsOverMaxView;
    private bool IsNextDropSet;
    private GachaController.GachaFlowType mFlowType;
    private GameObject item_root;
    private int thumbnail_count;
    private List<GameObject> mUnitTempList = new List<GameObject>(10);
    private List<GameObject> mItemTempList = new List<GameObject>(10);
    private List<GameObject> mArtifactTempList = new List<GameObject>(10);
    public float StoneCenterHeight;
    private List<GameObject> m_TempList;
    [SerializeField]
    private GameObject ThumbnailTemlate;
    [SerializeField]
    private Animator GetUnitAnimator;
    [SerializeField]
    private RawImage GetUnitImage;
    [SerializeField]
    private RawImage GetUnitBlurImage;
    [SerializeField]
    private Text GetUnitDescriptionText;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT1_PREFAB = "UI/Gacha/cut1";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT2_1_PREFAB = "UI/Gacha/cut2_1";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT2_2_PREFAB = "UI/Gacha/cut2_2";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT2_3_PREFAB = "UI/Gacha/cut2_3";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT3_PREFAB = "UI/Gacha/cut3";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT4_1_PREFAB = "UI/Gacha/cut4_1";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT4_2_PREFAB = "UI/Gacha/cut4_2";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT5_1_PREFAB = "UI/Gacha/cut5_1";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_CUT5_2_PREFAB = "UI/Gacha/cut5_2";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_PICKUP_PREFAB = "UI/Gacha/pickup";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_BACKGROUND = "UI/Gacha/bg_gacha";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_BACKGROUND_RESULT = "UI/Gacha/bg_gacha_black";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_DROP_UNITOBJ_PREFAB = "UI/Gacha/drop_stone";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_DROP_ARTIFACTOBJ_PREFAB = "UI/Gacha/drop_artifact";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_DROP_CCARDOBJ_PREFAB = "UI/Gacha/drop_ccard";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_DROP_ITEMOBJ_PREFAB = "UI/Gacha/drop_item";
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string PATH_GACHA_DROP_NORMALOBJ_PREFAB = "UI/Gacha/drop_stone_normal";
    [Header("最終カットを表示するカメラ")]
    [SerializeField]
    private Camera m_CharaCamera;
    [SerializeField]
    private Camera m_BackCamera;
    [Header("星5ピックアップ時にリル＝ウロボロスが出現する確率 100分率")]
    [SerializeField]
    private int m_OuroborosRate = 50;
    [Header("ユニット排出時のボイス")]
    [SerializeField]
    private string UNIT_GET_VOICE = "chara_0001";
    [Header("ユニット排出時のボイス再生タイミング")]
    [SerializeField]
    private float UNIT_GET_VOICE_DELAY = 1.4f;
    private GameObject m_Cut1;
    private GameObject m_Cut2;
    private GameObject m_Cut3;
    private GameObject m_Cut4;
    private GameObject m_Cut5;
    private GameObject m_Cut5_pu;
    private GameObject m_Pickup;
    private GameObject m_BackGround;
    private GameObject m_BackGround_Result;
    private Animation m_Cut1_Anim;
    private Animation m_Cut2_Anim;
    private Animation m_Cut3_Anim;
    private Animation m_Cut4_Anim;
    private Animation m_Cut5_Anim;
    private Animation m_Cut5_pu_Anim;
    private Animator m_Pickup_Animator;
    private Animation m_CurrentAnim;
    private GameObject[] m_DropObjects;
    private GameObject m_PrevDropObject;
    private GameObject m_CutAnimStone;
    private GachaPickupParts m_PickupParts;
    private bool m_pickup_flag;
    private bool m_pickup_anim_use_flag;
    private List<GachaController.PickupAnimType> m_pickup_anim_type_list = new List<GachaController.PickupAnimType>();
    private MySound.Voice m_UnitVoice;

    public int GachaSequence => GachaResultData.drops.Length;

    private Canvas OverlayCanvas
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null) ? ((Component) this.mTouchController).GetComponent<Canvas>() : (Canvas) null;
      }
    }

    public bool IsAssetDownloadDone() => !GlobalVars.IsTutorialEnd || AssetDownloader.isDone;

    private GachaController.DropInfo DropCurrent { get; set; }

    public GachaDropData CurrentDropData
    {
      get
      {
        return this.DropCurrent.Index >= GachaResultData.drops.Length ? (GachaDropData) null : GachaResultData.drops[this.DropCurrent.Index];
      }
    }

    public int Rarity => this.DropCurrent.Rarity + 1;

    public bool Shard => this.DropCurrent.IsShard;

    public bool Item => this.DropCurrent.IsItem;

    public bool IsConceptCard => this.DropCurrent.IsConceptCard;

    public bool Pickup => this.DropCurrent.IsPickup;

    private int mFirstSeq => GachaResultData.excites[0];

    private int mSecondSeq => GachaResultData.excites[1];

    private int mThirdSeq => GachaResultData.excites[2];

    private int mFourthSeq => GachaResultData.excites[3];

    private int mFifthSeq => GachaResultData.excites[4];

    [DebuggerHidden]
    private IEnumerator CreateDropInfo()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CCreateDropInfo\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void PlayJINGLE0010()
    {
      this.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0010", "JIN_0010", MySound.EType.JINGLE);
    }

    public bool IsLithograph
    {
      get => this.mLithograph;
      set => this.mLithograph = value;
    }

    public int DropIndex => this.thumbnail_count + this.mCurrentPage * this.mViewStoneCount;

    public bool FinishedCardUnitAnimation { get; set; }

    public bool NextGachaPhase { get; set; }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.AllAnimSkip = true;
          break;
        case 100:
          this.IsNextDropSet = true;
          break;
        case 200:
          this.StartGetCardUnitAnim();
          break;
        case 201:
          this.FinishedCardUnitAnimation = true;
          break;
        case 202:
          this.NextGachaPhase = true;
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CStart\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private Color ConvertColor(Color color)
    {
      return new Color(color.r / (float) byte.MaxValue, color.g / (float) byte.MaxValue, color.b / (float) byte.MaxValue);
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      if (this.AllAnimSkip)
      {
        Animator component = ((Component) this).gameObject.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetTrigger("all_anime_skip");
        this.mState.GotoState<GachaController.State_EndSetting>();
      }
      else
        this.mState.Update();
    }

    [DebuggerHidden]
    private IEnumerator InitTempList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CInitTempList\u003Ec__Iterator2()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator CreateThumbnailObject(GachaDropData.Type type)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CCreateThumbnailObject\u003Ec__Iterator3()
      {
        type = type,
        \u0024this = this
      };
    }

    public void RefreshThumbnailList()
    {
      if (this.mUseThumbnailList == null)
        return;
      foreach (GameObject mUseThumbnail in this.mUseThumbnailList)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mUseThumbnail, (UnityEngine.Object) null))
        {
          DataSource.Bind<UnitData>(mUseThumbnail, (UnitData) null);
          DataSource.Bind<ItemData>(mUseThumbnail, (ItemData) null);
          DataSource.Bind<ArtifactData>(mUseThumbnail, (ArtifactData) null);
          GameParameter.UpdateAll(mUseThumbnail);
          mUseThumbnail.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ThumbnailPool, (UnityEngine.Object) null))
            mUseThumbnail.transform.SetParent(this.ThumbnailPool, false);
        }
      }
      this.mUseThumbnailList.Clear();
    }

    private UnitData CreateUnitData(UnitParam uparam)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = uparam.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    public static ArtifactData CreateTempArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData tempArtifactData = new ArtifactData();
      tempArtifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        iname = param.iname,
        exp = 0,
        fav = 0,
        rare = rarity
      });
      return tempArtifactData;
    }

    private void OnDestroy()
    {
      this.DestroyTouchArea();
      if (this.mBGMVolume != null)
      {
        this.mBGMVolume.Discard();
        this.mBGMVolume = (MySound.VolumeHandle) null;
      }
      this.DropMaterialImage = (RawImage) null;
      this.DropMaterialBlurImage = (RawImage) null;
      this.DropMaterialIconImage = (RawImage) null;
      this.DropMaterialIconFrameImage = (Image) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConceptCardRenderer, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mConceptCardRenderer).gameObject);
      this.ResetUnitVoice();
    }

    private void CreateTouchArea()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) null, (UnityEngine.Object) this.mTouchController))
        return;
      GameObject gameObject = new GameObject("TouchArea", new System.Type[6]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (CanvasStack),
        typeof (NullGraphic),
        typeof (TouchController),
        typeof (SRPG_CanvasScaler)
      });
      this.mTouchController = gameObject.GetComponent<TouchController>();
      this.mTouchController.OnClick = new TouchController.ClickEvent(this.OnClick);
      this.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
      this.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
      gameObject.GetComponent<Canvas>().renderMode = (RenderMode) 0;
      gameObject.GetComponent<CanvasStack>().Priority = 0;
      ((Behaviour) gameObject.GetComponent<GraphicRaycaster>()).enabled = true;
    }

    private void DestroyTouchArea()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) null, (UnityEngine.Object) this.mTouchController))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mTouchController).gameObject);
      this.mTouchController = (TouchController) null;
    }

    private void OnClick(Vector2 screenPosition)
    {
      if (!this.mState.IsInState<GachaController.State_WaitDropMaterial>() && !this.mState.IsInState<GachaController.State_WaitDropmaterialT>() && !this.mState.IsInState<GachaController.State_WaitDropmaterialShard>() && !this.mState.IsInState<GachaController.State_WaitEndInput>() && !this.mState.IsInState<GachaController.State_WaitGaugeAnimation>() && !this.mState.IsInState<GachaController.State_WaitBeforeSummons>() && !this.mState.IsInState<GachaController.State_WaitCardAnim>() && !this.mState.IsInState<GachaController.State_WaitStartupAnimation>() && !this.mState.IsInState<GachaController.State_NextDropAnimation>())
        return;
      this.mClicked = !this.AllAnimSkip;
    }

    private void OnDrag()
    {
      if (this.mIgnoreDragVelocity)
        return;
      this.mDraged = false;
      this.mDraging = true;
      this.mDragX += this.mTouchController.DragDelta.x;
      this.mDragY += this.mTouchController.DragDelta.y;
    }

    private void OnDragEnd()
    {
      this.mDragEndX = this.mTouchController.DragStart.x + this.mDragX;
      this.mDragEndY = this.mTouchController.DragStart.y + this.mDragY;
      this.mDraged = true;
      this.mDraging = false;
      this.mIgnoreDragVelocity = false;
    }

    private bool CheckSkip() => !this.AllAnimSkip && this.mTouchController.IsTouching;

    private int GetRarityTextureIndex(int rarity)
    {
      if (rarity >= 5)
        return 2;
      return rarity >= 4 ? 1 : 0;
    }

    public void RefreshGachaImageSize(RectTransform _rect_tf, GachaDropData.Type _type)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) _rect_tf, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("演出表示に必要なオブジェクトが設定されていません");
      }
      else
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(1024f, 1024f);
        if (_type == GachaDropData.Type.ConceptCard)
        {
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2).\u002Ector(1024f, 612f);
        }
        _rect_tf.sizeDelta = vector2;
      }
    }

    public GameObject GetDropStone(GachaDropData _drop)
    {
      if (_drop == null)
        return (GameObject) null;
      GameObject dropStone = this.DropStone;
      if (this.mFlowType != GachaController.GachaFlowType.Normal)
      {
        switch (_drop.type)
        {
          case GachaDropData.Type.Item:
            if (_drop.unitOrigin == null)
            {
              dropStone = this.DropStone_Item;
              break;
            }
            break;
          case GachaDropData.Type.Artifact:
            dropStone = this.DropStone_Artifact;
            break;
          case GachaDropData.Type.ConceptCard:
            dropStone = this.DropStone_CCard;
            break;
        }
      }
      return dropStone;
    }

    private bool InitThumbnailTemplateList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ThumbnailTemlate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("サムネイルのテンプレートが指定されていません");
        return false;
      }
      this.m_TempList = new List<GameObject>(GachaController.MAX_VIEW_STONE);
      for (int index = 0; index < GachaController.MAX_VIEW_STONE; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ThumbnailTemlate);
        gameObject.transform.SetParent(this.ThumbnailPool, false);
        this.m_TempList.Add(gameObject);
      }
      return true;
    }

    private bool ResetThumbnailList()
    {
      if (this.m_TempList == null)
        return false;
      foreach (GameObject temp in this.m_TempList)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) temp, (UnityEngine.Object) null))
        {
          DataSource.Bind<ItemData>(temp, (ItemData) null);
          DataSource.Bind<UnitData>(temp, (UnitData) null);
          DataSource.Bind<ArtifactData>(temp, (ArtifactData) null);
          SerializeValueBehaviour component = temp.GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            this.ResetThumbnailIcon(component, "item");
            this.ResetThumbnailIcon(component, "unit");
            this.ResetThumbnailIcon(component, "artifact");
            this.ResetThumbnailIcon(component, "concept_card");
          }
          temp.SetActive(false);
          temp.transform.SetParent(this.ThumbnailPool, false);
        }
      }
      return true;
    }

    private void ResetThumbnailIcon(SerializeValueBehaviour svb, string name)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) svb, (UnityEngine.Object) null) || string.IsNullOrEmpty(name))
        return;
      GameObject gameObject1 = this.SetThumbnailValid(svb, name, false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      ConceptCardIcon component1 = gameObject1.GetComponent<ConceptCardIcon>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.ResetIcon();
      SerializeValueBehaviour component2 = gameObject1.GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = component2.list.GetGameObject("new");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      gameObject2.SetActive(false);
    }

    private GameObject SetThumbnailValid(
      SerializeValueBehaviour _valuelist,
      string _name,
      bool _active)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) _valuelist, (UnityEngine.Object) null))
      {
        GameObject gameObject = _valuelist.list.GetGameObject(_name);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          gameObject.SetActive(_active);
          return gameObject;
        }
      }
      return (GameObject) null;
    }

    private bool SetupThumbnail(GameObject _thumbnail, GachaDropData _data)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) _thumbnail, (UnityEngine.Object) null) && _data != null)
      {
        SerializeValueBehaviour component1 = _thumbnail.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        {
          GameObject gameObject1 = (GameObject) null;
          if (_data.type == GachaDropData.Type.Item)
          {
            gameObject1 = this.SetThumbnailValid(component1, "item", true);
            ItemData data = new ItemData();
            data.Setup(0L, _data.item, _data.num);
            data.IsNew = _data.isNew;
            DataSource.Bind<ItemData>(_thumbnail, data);
          }
          else if (_data.type == GachaDropData.Type.Unit)
          {
            gameObject1 = this.SetThumbnailValid(component1, "unit", true);
            UnitData unitData = this.CreateUnitData(_data.unit);
            DataSource.Bind<UnitData>(_thumbnail, unitData);
          }
          else if (_data.type == GachaDropData.Type.Artifact)
          {
            gameObject1 = this.SetThumbnailValid(component1, "artifact", true);
            ArtifactData tempArtifactData = GachaController.CreateTempArtifactData(_data.artifact, _data.Rare);
            DataSource.Bind<ArtifactData>(_thumbnail, tempArtifactData);
          }
          else if (_data.type == GachaDropData.Type.ConceptCard)
          {
            gameObject1 = this.SetThumbnailValid(component1, "concept_card", true);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            {
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(_data.conceptcard.iname);
              if (cardDataForDisplay != null)
              {
                ConceptCardIcon component2 = gameObject1.GetComponent<ConceptCardIcon>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                  component2.Setup(cardDataForDisplay);
              }
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          {
            SerializeValueBehaviour component3 = gameObject1.GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
            {
              GameObject gameObject2 = component3.list.GetGameObject("new");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
                gameObject2.SetActive(_data.isNew);
            }
          }
          return true;
        }
      }
      return false;
    }

    private GameObject GetThumbnailObject(int _index)
    {
      return this.m_TempList != null && this.m_TempList.Count > _index ? this.m_TempList[_index] : (GameObject) null;
    }

    private GameObject GetThumbnailTypeObject(GameObject _thumbnail, GachaDropData.Type _type)
    {
      GameObject thumbnailTypeObject = (GameObject) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) _thumbnail, (UnityEngine.Object) null))
      {
        SerializeValueBehaviour component = _thumbnail.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          switch (_type)
          {
            case GachaDropData.Type.Item:
              thumbnailTypeObject = component.list.GetGameObject("item");
              break;
            case GachaDropData.Type.Unit:
              thumbnailTypeObject = component.list.GetGameObject("unit");
              break;
            case GachaDropData.Type.Artifact:
              thumbnailTypeObject = component.list.GetGameObject("artifact");
              break;
            case GachaDropData.Type.ConceptCard:
              thumbnailTypeObject = component.list.GetGameObject("concept_card");
              break;
          }
        }
      }
      return thumbnailTypeObject;
    }

    private void SetGetUnitParameter(UnitParam _unit_param, string _ccard_name)
    {
      if (_unit_param == null)
      {
        DebugUtility.LogError("[SetGetUnitParameter]UnitParamがありません.");
      }
      else
      {
        string path = AssetPath.UnitImage(_unit_param, _unit_param.GetJobId(0));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitImage, (UnityEngine.Object) null))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitImage, path);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitBlurImage, (UnityEngine.Object) null))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitBlurImage, path);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitDescriptionText, (UnityEngine.Object) null))
          return;
        this.GetUnitDescriptionText.text = LocalizedText.Get("sys.CONCEPT_CARD_UNIT_GET_DESCRIPTION", (object) _ccard_name, (object) _unit_param.name);
      }
    }

    private void ResetGetUnitAnim()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitImage, (UnityEngine.Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitImage, string.Empty);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitBlurImage, (UnityEngine.Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.GetUnitBlurImage, string.Empty);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitDescriptionText, (UnityEngine.Object) null))
        return;
      this.GetUnitDescriptionText.text = string.Empty;
    }

    private void SetupGetCardUnitAnim()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 250);
    }

    private void StartGetCardUnitAnim()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GetUnitAnimator, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("GetUnitAnimatorが指定されていません.");
      }
      else
      {
        this.GetUnitAnimator.SetInteger("rariry", this.DropCurrent.CardUnitRarity + 1);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 251);
      }
    }

    private void ResetGetCardUnitAnim()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetUnitAnimator, (UnityEngine.Object) null))
        return;
      this.GetUnitAnimator.SetInteger("rariry", 0);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 252);
    }

    private bool IsNormalGacha() => this.mFlowType == GachaController.GachaFlowType.Normal;

    private bool IsSingleGacha() => GachaResultData.drops.Length == 1;

    private bool IsSingleAnimation()
    {
      return this.IsSingleGacha() || this.mFlowType != GachaController.GachaFlowType.Rare;
    }

    public bool IsSetupedCut1
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut1, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut1_Anim, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedCut2
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut2_Anim, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedCut3
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut3, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut3_Anim, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedCut4
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut4, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut4_Anim, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedCut5
    {
      get
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_Anim, (UnityEngine.Object) null))
          return false;
        if (!this.m_pickup_anim_use_flag)
          return true;
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_pu, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_pu_Anim, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedPickup
    {
      get
      {
        if (!this.m_pickup_flag)
          return true;
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Pickup, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Pickup_Animator, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PickupParts, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedBG
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround_Result, (UnityEngine.Object) null);
      }
    }

    public bool IsSetupedCut
    {
      get
      {
        return this.IsSetupedCut1 && this.IsSetupedCut2 && this.IsSetupedCut3 && this.IsSetupedCut4 && this.IsSetupedCut5 && this.IsSetupedPickup && this.IsSetupedBG;
      }
    }

    public bool IsUseLastPickupAnim()
    {
      if (this.m_pickup_anim_type_list.Count <= 0)
        return false;
      return this.m_pickup_anim_type_list[this.DropCurrent.Index] == GachaController.PickupAnimType.Pickup || this.m_pickup_anim_type_list[this.DropCurrent.Index] == GachaController.PickupAnimType.PickupUseAnim;
    }

    [DebuggerHidden]
    private IEnumerator LoadAnimationMaterial()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaController.\u003CLoadAnimationMaterial\u003Ec__Iterator4()
      {
        \u0024this = this
      };
    }

    private void SetupCutAnimStone(int excite)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CutAnimStone, (UnityEngine.Object) null))
        return;
      this.m_CutAnimStone.SetActive(true);
      GachaAnimationParts component = this.m_CutAnimStone.GetComponent<GachaAnimationParts>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.Setup(excite);
    }

    private void SetupAnimationParts()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut3, (UnityEngine.Object) null))
      {
        GachaAnimationParts[] componentsInChildren = this.m_Cut3.GetComponentsInChildren<GachaAnimationParts>();
        if (componentsInChildren != null && componentsInChildren.Length > 0)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
            componentsInChildren[index].Setup(componentsInChildren[index].state != GachaAnimationParts.StateType.After ? this.mSecondSeq : GachaResultData.DropMaxExcite);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut4, (UnityEngine.Object) null))
      {
        GachaDropAnimationParts componentInChildren1 = this.m_Cut4.GetComponentInChildren<GachaDropAnimationParts>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
          componentInChildren1.Setup(this.m_DropObjects, GachaResultData.drops);
        GachaAnimationParts componentInChildren2 = this.m_Cut4.GetComponentInChildren<GachaAnimationParts>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
          componentInChildren2.Setup(GachaResultData.DropMaxExcite);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5, (UnityEngine.Object) null))
      {
        GachaAnimationParts[] componentsInChildren = this.m_Cut5.GetComponentsInChildren<GachaAnimationParts>();
        if (componentsInChildren != null && componentsInChildren.Length > 0)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
            componentsInChildren[index].Setup(componentsInChildren[index].state != GachaAnimationParts.StateType.After ? this.DropCurrent.Excites[0] : this.DropCurrent.Excites[1]);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_pu, (UnityEngine.Object) null))
      {
        GachaAnimationParts[] componentsInChildren = this.m_Cut5_pu.GetComponentsInChildren<GachaAnimationParts>();
        if (componentsInChildren != null && componentsInChildren.Length > 0)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
            componentsInChildren[index].Setup(componentsInChildren[index].state != GachaAnimationParts.StateType.After ? this.DropCurrent.Excites[0] : this.DropCurrent.Excites[1]);
        }
      }
      this.RefreshPickupParts();
    }

    private void PlayStartupAnimation()
    {
      this.m_BackGround.SetActive(true);
      this.m_CurrentAnim = this.m_Cut1_Anim;
      this.m_Cut1.SetActive(true);
    }

    private void UpdateAnimationState()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut1_Anim))
      {
        if (this.m_CurrentAnim.isPlaying)
          return;
        this.m_CurrentAnim = this.m_Cut2_Anim;
        this.m_Cut1.SetActive(false);
        this.m_Cut2.SetActive(true);
      }
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut2_Anim))
      {
        if (this.m_CurrentAnim.isPlaying)
          return;
        this.m_CurrentAnim = this.m_Cut3_Anim;
        this.m_Cut2.SetActive(false);
        this.m_Cut3.SetActive(true);
      }
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut3_Anim))
      {
        if (this.m_CurrentAnim.isPlaying)
          return;
        this.m_CurrentAnim = this.m_Cut4_Anim;
        this.m_Cut3.SetActive(false);
        this.m_Cut4.SetActive(true);
      }
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut4_Anim))
      {
        if (this.m_CurrentAnim.isPlaying)
          return;
        this.m_Cut4.SetActive(false);
        this.PlayAnimCut5();
      }
      else
      {
        if ((!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_Anim) || !this.IsUseLastPickupAnim()) && (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_pu_Anim) || !this.IsUseLastPickupAnim()) || this.m_CurrentAnim.isPlaying)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5, (UnityEngine.Object) null))
          this.m_Cut5.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_pu, (UnityEngine.Object) null))
          this.m_Cut5_pu.SetActive(false);
        this.m_Pickup.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CharaCamera, (UnityEngine.Object) null))
          ((Component) this.m_CharaCamera).gameObject.SetActive(true);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackCamera, (UnityEngine.Object) null))
          return;
        ((Component) this.m_BackCamera).gameObject.SetActive(true);
      }
    }

    private void SkipAnimationState()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut1_Anim) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut2_Anim) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut3_Anim))
      {
        this.m_CurrentAnim = this.m_Cut4_Anim;
        this.m_Cut1.SetActive(false);
        this.m_Cut2.SetActive(false);
        this.m_Cut3.SetActive(false);
        this.m_Cut4.SetActive(true);
      }
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut4_Anim))
      {
        this.m_Cut4.SetActive(false);
        this.PlayAnimCut5();
      }
      else
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_Anim) && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_pu_Anim))
          return;
        string str = string.Empty;
        foreach (AnimationState animationState in this.m_CurrentAnim)
        {
          if (this.m_CurrentAnim.IsPlaying(animationState.name))
          {
            str = animationState.name;
            break;
          }
        }
        if (string.IsNullOrEmpty(str))
          return;
        this.m_CurrentAnim[str].normalizedTime = 1f;
      }
    }

    private void SetNextDropAnimation(bool is_pu)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CharaCamera, (UnityEngine.Object) null))
        ((Component) this.m_CharaCamera).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackCamera, (UnityEngine.Object) null))
        ((Component) this.m_BackCamera).gameObject.SetActive(false);
      this.PlayAnimCut5();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround, (UnityEngine.Object) null))
        this.m_BackGround.SetActive(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround_Result, (UnityEngine.Object) null))
        return;
      this.m_BackGround_Result.SetActive(false);
    }

    private bool IsStartupAnimationEnd(bool is_pu)
    {
      return is_pu ? UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) null) && (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_pu_Anim) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_Anim)) && !this.m_CurrentAnim.isPlaying && !GameUtility.IsAnimatorRunning((Component) this.m_Pickup_Animator) : UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CurrentAnim, (UnityEngine.Object) this.m_Cut5_Anim) && !this.m_CurrentAnim.isPlaying;
    }

    private void EndStartupAnimation()
    {
      this.m_CurrentAnim = (Animation) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5, (UnityEngine.Object) null))
        this.m_Cut5.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Cut5_pu, (UnityEngine.Object) null))
        this.m_Cut5_pu.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Pickup, (UnityEngine.Object) null))
        this.m_Pickup.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CharaCamera, (UnityEngine.Object) null))
        ((Component) this.m_CharaCamera).gameObject.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackCamera, (UnityEngine.Object) null))
        ((Component) this.m_BackCamera).gameObject.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround, (UnityEngine.Object) null))
        this.m_BackGround.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround_Result, (UnityEngine.Object) null))
        return;
      this.m_BackGround_Result.SetActive(true);
    }

    private bool IsDropEnd()
    {
      return this.DropCurrent != null && this.DropCurrent.Index >= this.GachaSequence;
    }

    private void PlayAnimCut5()
    {
      GameObject gameObject;
      if (this.m_pickup_anim_type_list[this.DropCurrent.Index] == GachaController.PickupAnimType.PickupUseAnim)
      {
        this.m_CurrentAnim = this.m_Cut5_pu_Anim;
        this.m_Cut5_pu.SetActive(true);
        gameObject = this.m_Cut5_pu;
      }
      else
      {
        this.m_CurrentAnim = this.m_Cut5_Anim;
        this.m_Cut5.SetActive(true);
        gameObject = this.m_Cut5;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        GachaAnimationParts[] componentsInChildren = gameObject.GetComponentsInChildren<GachaAnimationParts>();
        if (componentsInChildren != null && componentsInChildren.Length > 0)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
            componentsInChildren[index].Setup(componentsInChildren[index].state != GachaAnimationParts.StateType.After ? this.DropCurrent.Excites[0] : this.DropCurrent.Excites[1]);
        }
        GachaDropAnimationParts componentInChildren = gameObject.GetComponentInChildren<GachaDropAnimationParts>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PrevDropObject, (UnityEngine.Object) null))
            this.m_PrevDropObject.SetActive(false);
          componentInChildren.Setup(this.m_DropObjects[this.DropCurrent.Index]);
          this.m_PrevDropObject = this.m_DropObjects[this.DropCurrent.Index];
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BackGround, (UnityEngine.Object) null))
      {
        Animator component = this.m_BackGround.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !((Behaviour) component).enabled)
          ((Behaviour) component).enabled = true;
      }
      this.RefreshPickupParts();
    }

    private void RefreshPickupParts()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PickupParts, (UnityEngine.Object) null))
        return;
      GachaDropData currentDropData = this.CurrentDropData;
      if (currentDropData != null && (currentDropData.type == GachaDropData.Type.Unit || currentDropData.type == GachaDropData.Type.Item && currentDropData.unitOrigin != null))
      {
        UnitParam unit = currentDropData.type != GachaDropData.Type.Unit ? currentDropData.unitOrigin : currentDropData.unit;
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_PickupParts.Image, AssetPath.UnitImage(unit, unit.GetJobId(0)));
      }
      else
        this.m_PickupParts.Image.texture = (Texture) null;
    }

    private void ResetUnitVoice()
    {
      if (this.m_UnitVoice == null)
        return;
      this.m_UnitVoice.StopAll();
      this.m_UnitVoice.Cleanup();
      this.m_UnitVoice = (MySound.Voice) null;
    }

    private void SetupUnitVoice()
    {
      this.ResetUnitVoice();
      GachaDropData currentDropData = this.CurrentDropData;
      if (currentDropData == null || currentDropData.type != GachaDropData.Type.Unit || !currentDropData.isNew)
        return;
      if (currentDropData.unit == null)
      {
        DebugUtility.LogError("UnitParamが指定されていません.");
      }
      else
      {
        string charName = AssetPath.UnitVoiceFileName(currentDropData.unit, jobVoice: string.Empty);
        if (string.IsNullOrEmpty(charName))
        {
          DebugUtility.LogError("UnitParamにボイス設定がありません.");
        }
        else
        {
          string sheetName = "VO_" + charName;
          string cueNamePrefix = charName + "_";
          this.m_UnitVoice = new MySound.Voice(sheetName, charName, cueNamePrefix);
        }
      }
    }

    private void PlayUnitVoice()
    {
      if (this.m_UnitVoice == null)
        return;
      this.m_UnitVoice.Play(this.UNIT_GET_VOICE, this.UNIT_GET_VOICE_DELAY, true);
    }

    private class DropInfo
    {
      public int Index { get; private set; }

      public string Name { get; private set; }

      public int Rarity { get; private set; }

      public bool IsShard { get; private set; }

      public bool IsItem { get; private set; }

      public string OName { get; private set; }

      public int Num { get; private set; }

      public int[] Excites { get; private set; }

      public bool IsConceptCard { get; private set; }

      public bool IsCardUnit { get; private set; }

      public int CardUnitRarity { get; private set; }

      public bool IsPickup { get; private set; }

      public static GachaController.DropInfo Create(GachaController self)
      {
        GachaController.DropInfo dropInfo = new GachaController.DropInfo();
        dropInfo.Reflesh(self);
        return dropInfo;
      }

      public void Reflesh(GachaController self, int index = 0)
      {
        if (index >= self.GachaSequence)
          return;
        GachaDropData drop = GachaResultData.drops[index];
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        self.IsLithograph = true;
        this.IsItem = false;
        this.IsShard = false;
        this.IsConceptCard = false;
        this.IsCardUnit = false;
        this.CardUnitRarity = 0;
        this.IsPickup = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ConceptCardNameText, (UnityEngine.Object) null))
          self.ConceptCardNameText.text = string.Empty;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ConceptCardFrame, (UnityEngine.Object) null))
          ((Component) self.ConceptCardFrame).gameObject.SetActive(false);
        self.ResetGetUnitAnim();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mConceptCardRenderer, (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self.mConceptCardRenderer).gameObject);
          self.mConceptCardRenderer = (ConceptCardCompositeRenderer) null;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.MaterialRuneType, (UnityEngine.Object) null))
          ((Component) self.MaterialRuneType).gameObject.SetActive(false);
        this.IsPickup = drop.isPickup;
        if (drop.type == GachaDropData.Type.Unit)
        {
          this.Name = drop.unit.name;
          this.Rarity = drop.Rare;
          this.OName = string.Empty;
          instance1.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(drop.unit, drop.unit.GetJobId(0)));
          instance1.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(drop.unit, drop.unit.GetJobId(0)));
          instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.UnitIconSmall(drop.unit, drop.unit.GetJobId(0)));
          self.RefreshGachaImageSize(((Graphic) self.DropMaterialImage).rectTransform, drop.type);
          self.RefreshGachaImageSize(((Graphic) self.DropMaterialBlurImage).rectTransform, drop.type);
          this.Excites = drop.excites;
        }
        else if (drop.type == GachaDropData.Type.Item)
        {
          this.Name = drop.item.name;
          this.Rarity = drop.Rare;
          this.OName = string.Empty;
          this.Num = drop.num;
          this.Excites = drop.excites;
          self.MaterialCount.text = this.Num.ToString();
          string str = this.Name + "x" + this.Num.ToString();
          self.MaterialName.text = str;
          self.MaterialComment.text = !string.IsNullOrEmpty(drop.item.Expr) ? drop.item.Expr : drop.item.Flavor;
          self.IsLithograph = drop.item.type == EItemType.UnitPiece;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.MaterialRuneType, (UnityEngine.Object) null) && drop.item.type == EItemType.Rune)
          {
            RuneParam runeParamByItemId = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParamByItemId(drop.item.iname);
            if (runeParamByItemId != null)
            {
              ((Component) self.MaterialRuneType).gameObject.SetActive(true);
              self.MaterialRuneType.SetSprite(runeParamByItemId.SetEffTypeIconIndex.ToString());
            }
          }
          if (drop.unitOrigin != null)
          {
            this.IsShard = true;
            this.IsItem = false;
            UnitParam unitParamForPiece = instance1.MasterParam.GetUnitParamForPiece(drop.item.iname);
            this.OName = unitParamForPiece == null ? string.Empty : unitParamForPiece.iname;
            instance1.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(drop.unitOrigin, drop.unitOrigin.GetJobId(0)));
            instance1.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.UnitImage(drop.unitOrigin, drop.unitOrigin.GetJobId(0)));
            instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(drop.item));
            self.RefreshGachaImageSize(((Graphic) self.DropMaterialImage).rectTransform, drop.type);
            self.RefreshGachaImageSize(((Graphic) self.DropMaterialBlurImage).rectTransform, drop.type);
            self.DropMaterialIconFrameImage.sprite = GameSettings.Instance.GetItemFrame(drop.item);
            if (unitParamForPiece == null)
              return;
            GameObject gameObject = ((Component) self.GaugeObject.transform.Find("UnitShard_gauge")).gameObject;
            UnitData unitDataByUnitId = instance1.Player.FindUnitDataByUnitID(unitParamForPiece.iname);
            if (unitDataByUnitId != null)
            {
              int awakeLv = unitDataByUnitId.AwakeLv;
              if (awakeLv >= unitDataByUnitId.GetAwakeLevelCap())
                return;
              gameObject.GetComponent<GachaUnitShard>().Refresh(unitParamForPiece, this.Name, awakeLv, drop.num, index);
            }
            else
              gameObject.GetComponent<GachaUnitShard>().Refresh(unitParamForPiece, this.Name, get_shard: drop.num, current_index: index);
          }
          else
          {
            this.IsShard = false;
            this.IsItem = true;
            self.DropMaterialImage.texture = (Texture) null;
            self.DropMaterialBlurImage.texture = (Texture) null;
            instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ItemIcon(drop.item));
            self.DropMaterialIconFrameImage.sprite = GameSettings.Instance.GetItemFrame(drop.item);
            if (drop.item.type != EItemType.UnitPiece)
              return;
            UnitParam unitParamForPiece = instance1.MasterParam.GetUnitParamForPiece(drop.item.iname);
            this.OName = unitParamForPiece == null ? string.Empty : unitParamForPiece.iname;
            if (string.IsNullOrEmpty(this.OName))
              return;
            GameObject gameObject = ((Component) self.GaugeObject.transform.Find("UnitShard_gauge")).gameObject;
            UnitData unitDataByUnitId = instance1.Player.FindUnitDataByUnitID(unitParamForPiece.iname);
            if (unitDataByUnitId != null)
            {
              int awakeLv = unitDataByUnitId.AwakeLv;
              if (awakeLv >= unitDataByUnitId.GetAwakeLevelCap())
                return;
              gameObject.GetComponent<GachaUnitShard>().Refresh(unitParamForPiece, this.Name, awakeLv, drop.num, index);
            }
            else
              gameObject.GetComponent<GachaUnitShard>().Refresh(unitParamForPiece, this.Name, get_shard: drop.num, current_index: index);
          }
        }
        else if (drop.type == GachaDropData.Type.Artifact)
        {
          GameSettings instance2 = GameSettings.Instance;
          this.Name = drop.artifact.name;
          this.Rarity = drop.Rare;
          this.OName = string.Empty;
          this.Num = drop.num;
          this.Excites = drop.excites;
          self.MaterialCount.text = this.Num.ToString();
          string str = this.Name + "x" + this.Num.ToString();
          self.MaterialName.text = str;
          self.MaterialComment.text = !string.IsNullOrEmpty(drop.artifact.Expr) ? drop.artifact.Expr : drop.artifact.Flavor;
          self.IsLithograph = false;
          this.IsShard = false;
          this.IsItem = true;
          self.DropMaterialImage.texture = (Texture) null;
          self.DropMaterialBlurImage.texture = (Texture) null;
          instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ArtifactIcon(drop.artifact));
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) || this.Rarity >= instance2.ArtifactIcon_Frames.Length)
            return;
          self.DropMaterialIconFrameImage.sprite = instance2.ArtifactIcon_Frames[this.Rarity];
        }
        else
        {
          if (drop.type != GachaDropData.Type.ConceptCard)
            return;
          this.Name = drop.conceptcard.name;
          if (drop.num > 1)
            this.Name = this.Name + "x" + (object) drop.num;
          this.Rarity = drop.Rare;
          this.OName = string.Empty;
          if (drop.conceptcard.IsComposite())
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(self.ConceptCardRendererPrefab);
            self.mConceptCardRenderer = gameObject.GetComponent<ConceptCardCompositeRenderer>();
            self.mConceptCardRenderer.Setup(drop.conceptcard);
            ((Component) self.mConceptCardRenderer).gameObject.SetActive(true);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mConceptCardRenderer, (UnityEngine.Object) null))
            {
              self.DropMaterialImage.texture = (Texture) self.mConceptCardRenderer.RenderTexture;
              self.DropMaterialBlurImage.texture = (Texture) self.mConceptCardRenderer.RenderTexture;
            }
          }
          else
          {
            instance1.ApplyTextureAsync(self.DropMaterialImage, AssetPath.ConceptCard(drop.conceptcard));
            instance1.ApplyTextureAsync(self.DropMaterialBlurImage, AssetPath.ConceptCard(drop.conceptcard));
          }
          instance1.ApplyTextureAsync(self.DropMaterialIconImage, AssetPath.ConceptCardIcon(drop.conceptcard));
          self.RefreshGachaImageSize(((Graphic) self.DropMaterialImage).rectTransform, drop.type);
          self.RefreshGachaImageSize(((Graphic) self.DropMaterialBlurImage).rectTransform, drop.type);
          this.Excites = drop.excites;
          this.IsConceptCard = true;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ConceptCardNameText, (UnityEngine.Object) null))
            self.ConceptCardNameText.text = this.Name;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ConceptCardFrame, (UnityEngine.Object) null))
          {
            ((Component) self.ConceptCardFrame).gameObject.SetActive(true);
            self.ConceptCardFrame.ImageIndex = this.Rarity;
          }
          if (drop.cardunit == null)
            return;
          self.SetGetUnitParameter(drop.cardunit, drop.conceptcard.name);
          this.IsCardUnit = true;
          this.CardUnitRarity = (int) drop.cardunit.rare;
        }
      }

      public void Next(GachaController self)
      {
        this.Reflesh(self, ++this.Index);
        self.SetupUnitVoice();
      }
    }

    private enum GachaFlowType : byte
    {
      Rare,
      Normal,
      Special,
    }

    private class State_InitDropThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openItem = self.OpenItem;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) openItem, (UnityEngine.Object) null))
          return;
        openItem.SetActive(true);
        int num = self.mCurrentPage * self.mViewStoneCount;
        int mViewStoneCount = self.mViewStoneCount;
        string str1 = "item_" + mViewStoneCount.ToString();
        GameObject gameObject = ((Component) openItem.transform.Find(str1)).gameObject;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return;
        for (int _index = 0; _index < mViewStoneCount && GachaResultData.drops.Length > _index + num; ++_index)
        {
          GachaDropData drop = GachaResultData.drops[_index + num];
          GameObject thumbnailObject = self.GetThumbnailObject(_index);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) thumbnailObject, (UnityEngine.Object) null))
          {
            string str2 = "item_" + (_index + 1).ToString();
            Transform transform = gameObject.transform.Find(str2);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            {
              self.SetupThumbnail(thumbnailObject, drop);
              SerializeValueBehaviour component = ((Component) transform).GetComponent<SerializeValueBehaviour>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                component.list.SetField("thumbnail", thumbnailObject);
              GameParameter.UpdateAll(thumbnailObject);
              thumbnailObject.transform.SetParent(transform, false);
            }
          }
        }
        gameObject.SetActive(true);
        self.item_root = gameObject;
        self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
      }
    }

    private class State_OpenDropThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        int num = self.mCurrentPage * self.mViewStoneCount;
        string str = "item_" + (self.thumbnail_count + 1).ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.item_root.transform.Find(str), (UnityEngine.Object) null) && self.thumbnail_count + num < GachaResultData.drops.Length && GachaResultData.drops[self.thumbnail_count + num].type == GachaDropData.Type.Unit)
          self.mState.GotoState<GachaController.State_OpenDropMaterialT>();
        else
          self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
      }
    }

    private class State_WaitThumbnailAnimation : State<GachaController>
    {
      private bool isSetup;
      private Animation anim;

      public override void Begin(GachaController self)
      {
        int num = self.mCurrentPage * self.mViewStoneCount;
        string str = "item_" + (self.thumbnail_count + 1).ToString();
        Transform transform = self.item_root.transform.Find(str);
        GachaDropData.Type _type = self.thumbnail_count + num >= GachaResultData.drops.Length ? GachaDropData.Type.None : GachaResultData.drops[self.thumbnail_count + num].type;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          return;
        GameObject gameObject1 = ((Component) transform).gameObject;
        gameObject1.SetActive(true);
        SerializeValueBehaviour component = gameObject1.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("SerializeValueBehaviourがアタッチされていません");
        }
        else
        {
          GameObject gameObject2 = component.list.GetGameObject("thumbnail");
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("SerializeValueにサムネイルのGameObjectが指定されていません.");
          }
          else
          {
            GameObject thumbnailTypeObject = self.GetThumbnailTypeObject(gameObject2, _type);
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) thumbnailTypeObject, (UnityEngine.Object) null))
            {
              DebugUtility.LogError("対象のサムネイルオブジェクトがありません.");
            }
            else
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) thumbnailTypeObject, (UnityEngine.Object) null))
                this.anim = thumbnailTypeObject.GetComponent<Animation>();
              thumbnailTypeObject.SetActive(true);
              gameObject2.SetActive(true);
              this.isSetup = true;
            }
          }
        }
      }

      public override void Update(GachaController self)
      {
        if (!this.isSetup)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.anim, (UnityEngine.Object) null))
        {
          if (this.anim.isPlaying)
            return;
          self.mState.GotoState<GachaController.State_CheckThumbnail>();
        }
        else
          self.mState.GotoState<GachaController.State_CheckThumbnail>();
      }
    }

    private class State_OpenDropMaterialT : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        openMaterial.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && self.DropIndex < GachaResultData.drops.Length)
          {
            int num = (int) GachaResultData.drops[self.DropIndex].unit.rare + 1;
            bool flag1 = GachaResultData.drops[self.DropIndex].unitOrigin != null;
            bool flag2 = GachaResultData.drops[self.DropIndex].item != null;
            component.SetInteger("rariry", num);
            component.SetBool("shard", flag1);
            component.SetBool("item", flag2);
          }
        }
        self.mState.GotoState<GachaController.State_WaitDropmaterialT>();
      }
    }

    private class State_WaitDropmaterialT : State<GachaController>
    {
      public override void Begin(GachaController self) => self.PlayUnitVoice();

      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_DisableDropMaterialT>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_DisableDropMaterialT : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        for (int index = 0; index < self.ResetMaterials.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ResetMaterials[index], (UnityEngine.Object) null))
            self.ResetMaterials[index].SetActive(false);
        }
        GameObject openMaterial = self.OpenMaterial;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          openMaterial.SetActive(false);
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
            component.SetBool("reset", false);
          }
        }
        self.mState.GotoState<GachaController.State_WaitThumbnailAnimation>();
      }
    }

    private class State_CheckThumbnail : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.OnShardEffect && (self.DropCurrent.IsItem || self.DropCurrent.IsShard))
        {
          GachaDropData currentDropData = self.CurrentDropData;
          if (currentDropData != null && currentDropData.item != null && currentDropData.item.type == EItemType.UnitPiece)
          {
            UnitParam unitParamForPiece = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(currentDropData.item.iname);
            if (unitParamForPiece != null)
            {
              UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(unitParamForPiece.iname);
              if (unitDataByUnitId != null && unitDataByUnitId.GetAwakeLevelCap() > unitDataByUnitId.AwakeLv)
              {
                self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
                return;
              }
            }
          }
        }
        self.DropCurrent.Next(self);
        if (!self.IsDropEnd())
        {
          ++self.thumbnail_count;
          if (self.thumbnail_count < self.mViewStoneCount)
          {
            self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
            return;
          }
          if (self.mFlowType == GachaController.GachaFlowType.Special)
          {
            self.mState.GotoState<GachaController.State_CheckNextDropSet>();
            return;
          }
        }
        self.mState.GotoState<GachaController.State_WaitEndInput>();
      }
    }

    private class State_CheckNextDropSet : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mCurrentPage + 1 < self.mMaxPage)
        {
          ++self.mCurrentPage;
          self.mState.GotoState<GachaController.State_WaitForInput_NextDropSet>();
        }
        else
          self.mState.GotoState<GachaController.State_WaitEndInput>();
      }
    }

    private class State_WaitForInput_NextDropSet : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 101);
      }

      public override void Update(GachaController self)
      {
        if (!self.IsNextDropSet)
          return;
        self.IsNextDropSet = false;
        self.mState.GotoState<GachaController.State_InitNextDropSet>();
      }
    }

    private class State_InitNextDropSet : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 102);
        self.ResetThumbnailList();
        self.thumbnail_count = 0;
        self.mState.GotoState<GachaController.State_InitDropThumbnail>();
      }
    }

    private class State_OpenDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        UnitParam unitParamForPiece = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(GachaResultData.drops[self.thumbnail_count].item.iname);
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(self.DropMaterialImage, AssetPath.UnitImage(unitParamForPiece, unitParamForPiece.GetJobId(0)));
        openMaterial.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            int raremax = (int) unitParamForPiece.raremax;
            bool flag1 = false;
            bool flag2 = false;
            component.SetInteger("rariry", raremax);
            component.SetBool("shard", flag1);
            component.SetBool("item", flag2);
          }
        }
        self.mState.GotoState<GachaController.State_WaitDropmaterialShard>();
      }
    }

    private class State_WaitDropmaterialShard : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_SettingDisableDropMaterialShard>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_SettingDisableDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.SetBool("reset", true);
        }
        for (int index = 0; index < self.ResetMaterials.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ResetMaterials[index], (UnityEngine.Object) null))
            self.ResetMaterials[index].SetActive(false);
        }
        self.mState.GotoState<GachaController.State_DisableDropMaterialShard>();
      }
    }

    private class State_DisableDropMaterialShard : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        GameObject openMaterial = self.OpenMaterial;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          openMaterial.SetActive(false);
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
          }
        }
        openMaterial.SetActive(false);
        self.DropCurrent.Next(self);
        if (!self.IsDropEnd())
        {
          ++self.thumbnail_count;
          if (self.thumbnail_count < self.mViewStoneCount)
          {
            self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
            return;
          }
          if (self.mFlowType == GachaController.GachaFlowType.Special)
          {
            self.mState.GotoState<GachaController.State_CheckNextDropSet>();
            return;
          }
        }
        self.mState.GotoState<GachaController.State_WaitEndInput>();
      }
    }

    private class State_Init : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.isSkipping = false;
        self.mState.GotoState<GachaController.State_WaitStartupAnimation>();
      }

      [DebuggerHidden]
      private IEnumerator MoveNextState()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_Init.\u003CMoveNextState\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    private class State_WaitStartupAnimation : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.StartCoroutine(self.LoadAnimationMaterial());
      }

      public override void Update(GachaController self)
      {
        if (!self.IsSetupedCut)
          return;
        if (self.mClicked)
        {
          self.mClicked = false;
          self.SkipAnimationState();
        }
        else if (self.IsStartupAnimationEnd(self.IsUseLastPickupAnim()))
        {
          self.EndStartupAnimation();
          self.mState.GotoState<GachaController.State_EnableDropMaterial>();
        }
        else
          self.UpdateAnimationState();
      }
    }

    private class State_WaitInputFlick : State<GachaController>
    {
      private bool mSuccessDistX;
      private bool mSuccessDistY;
      private Animator atr;

      public override void Begin(GachaController self)
      {
        this.atr = ((Component) self).gameObject.GetComponent<Animator>();
      }

      public override void Update(GachaController self)
      {
        if (self.mDraged)
        {
          this.atr.SetTrigger("is_flick_action");
          this.atr.ResetTrigger("back_sequence");
          Vector3 vector3_1 = Vector3.op_Subtraction(new Vector3(self.mDragEndX, 0.0f, 0.0f), new Vector3(self.mTouchController.DragStart.x, 0.0f, 0.0f));
          float magnitude1 = ((Vector3) ref vector3_1).magnitude;
          if ((double) Mathf.Sign(self.mDragEndX - self.mTouchController.DragStart.x) < 0.0 && (double) magnitude1 > (double) self.MIN_SWIPE_DIST_X)
            this.mSuccessDistX = true;
          Vector3 vector3_2 = Vector3.op_Subtraction(new Vector3(0.0f, self.mDragEndY, 0.0f), new Vector3(0.0f, self.mTouchController.DragStart.y, 0.0f));
          float magnitude2 = ((Vector3) ref vector3_2).magnitude;
          if ((double) Mathf.Sign(self.mDragEndY - self.mTouchController.DragStart.y) < 0.0 && (double) magnitude2 > (double) self.MIN_SWIPE_DIST_Y)
            this.mSuccessDistY = true;
          self.mDraged = false;
          if (this.mSuccessDistX && this.mSuccessDistY)
          {
            this.mSuccessDistX = false;
            this.mSuccessDistY = false;
            self.mIgnoreDragVelocity = true;
            this.atr.SetTrigger("is_flick_finish");
            this.atr.SetInteger("seqF_root", self.mFirstSeq);
            this.atr.SetInteger("seqS_root", self.mSecondSeq);
            this.atr.SetInteger("seqT_root", self.mThirdSeq);
            this.atr.SetInteger("seqFo_root", self.mFourthSeq);
            self.mState.GotoState<GachaController.State_WaitBeforeSummons>();
            return;
          }
          this.atr.ResetTrigger("is_flick_action");
          this.atr.SetTrigger("back_sequence");
        }
        if (!self.mDraging)
          return;
        this.atr.SetTrigger("is_flick_action");
      }
    }

    private class State_WaitBeforeSummons : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
      }

      public override void Update(GachaController self)
      {
        if (self.mClicked)
        {
          self.mClicked = false;
          Animator component1 = ((Component) self).gameObject.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
            component1.SetBool("is_skip", true);
          if (self.mJingleHandle != null)
          {
            self.mJingleHandle.Stop(0.0f);
            self.mJingleHandle = MonoSingleton<MySound>.Instance.PlayLoop("JIN_0016", "JIN_0016", MySound.EType.JINGLE);
          }
          GachaVoice component2 = ((Component) self).GetComponent<GachaVoice>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            component2.Stop();
          if (self.mFlowType != GachaController.GachaFlowType.Rare)
            self.mState.GotoState<GachaController.State_EnableDropMaterial>();
          else
            self.mState.GotoState<GachaController.State_SettingDropStone>();
        }
        else
        {
          if (!GameUtility.CompareAnimatorStateName(((Component) self).gameObject, "SequenceAnim7_Low") && !GameUtility.CompareAnimatorStateName(((Component) self).gameObject, "SequenceAnim7_Middle") && !GameUtility.CompareAnimatorStateName(((Component) self).gameObject, "SequenceAnim7_High"))
            return;
          self.mState.GotoState<GachaController.State_SettingDropStone>();
        }
      }
    }

    private class State_SettingDropStoneSkip : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mState.GotoState<GachaController.State_EnableDropMaterial>();
      }
    }

    private class State_SettingDropStone : State<GachaController>
    {
      private float mRadius;
      private float mAppear;

      public override void Begin(GachaController self)
      {
        if (self.GachaSequence <= 0)
        {
          DebugUtility.LogError("排出結果が存在しません");
        }
        else
        {
          this.mRadius = self.StoneRadius;
          this.mAppear = self.StoneAppear;
          self.StartCoroutine(this.SetDropStone(self.DropStone, self.mViewStoneCount));
        }
      }

      [DebuggerHidden]
      private IEnumerator SetDropStone(GameObject obj, int count)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_SettingDropStone.\u003CSetDropStone\u003Ec__Iterator0()
        {
          obj = obj,
          count = count,
          \u0024this = this
        };
      }

      private void CreateDropStone(GameObject gobj, Vector3 pos, int num)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gobj, (UnityEngine.Object) null))
          DebugUtility.LogError(string.Empty);
        else if (num < 0)
          DebugUtility.LogError(string.Empty);
        else if (GachaResultData.drops == null || GachaResultData.drops.Length <= num)
        {
          DebugUtility.LogError(string.Empty);
        }
        else
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(gobj);
          gameObject.transform.SetParent(gobj.transform.parent, false);
          gameObject.transform.localPosition = pos;
          this.SetDropStoneTexture(gameObject, GachaResultData.drops[num], this.self.mFlowType != GachaController.GachaFlowType.Normal);
          this.self.mDropStones.Add(gameObject);
        }
      }

      [DebuggerHidden]
      private IEnumerator CreateDropStone(GameObject obj, int count)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_SettingDropStone.\u003CCreateDropStone\u003Ec__Iterator1()
        {
          obj = obj,
          count = count,
          \u0024this = this
        };
      }

      private void SetDropStoneTexture(GameObject obj, GachaDropData drop, bool isCoin = true)
      {
        GameObject gameObject1 = ((Component) obj.transform.Find("stone_3d2")).gameObject;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
          return;
        GameObject gameObject2 = ((Component) gameObject1.transform.Find("stone_root")).gameObject;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null) || drop.type == GachaDropData.Type.None)
          return;
        GameObject gameObject3 = ((Component) gameObject2.transform.Find("stone_base")).gameObject;
        if (!isCoin)
        {
          ((Component) gameObject2.transform.Find("stone_base")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.stoneBaseN;
          ((Component) gameObject2.transform.Find("stone_hand01_move")).gameObject.SetActive(false);
          ((Component) gameObject2.transform.Find("stone_hand02_move")).gameObject.SetActive(false);
          ((Renderer) ((Component) gameObject2.transform.Find("stone_eye01")).gameObject.GetComponent<MeshRenderer>()).enabled = false;
          ((Renderer) ((Component) gameObject2.transform.Find("stone_eye02")).gameObject.GetComponent<MeshRenderer>()).enabled = false;
        }
        else if (drop.type == GachaDropData.Type.ConceptCard)
        {
          int index = drop.excites[0] - 1 < 0 ? drop.excites[0] : drop.excites[0] - 1;
          ((Component) gameObject2.transform.Find("stone_base")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.ConceptCardStoneBases[index];
        }
        else if (drop.type == GachaDropData.Type.Item && drop.unitOrigin == null)
        {
          int index = drop.excites[0] - 1 < 0 ? drop.excites[0] : drop.excites[0] - 1;
          ((Component) gameObject2.transform.Find("stone_base")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.ItemStoneBases[index];
        }
        else if (drop.type == GachaDropData.Type.Artifact)
        {
          int index = drop.excites[0] - 1 < 0 ? drop.excites[0] : drop.excites[0] - 1;
          ((Component) gameObject2.transform.Find("stone_base")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.ArtifactStoneBases[index];
        }
        else
        {
          int index = drop.excites[0] - 1 < 0 ? drop.excites[0] : drop.excites[0] - 1;
          ((Component) gameObject2.transform.Find("stone_base")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.StoneBases[index];
          ((Component) ((Component) gameObject2.transform.Find("stone_hand01_move")).gameObject.transform.Find("stone_hand01")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.StoneHand01s[index];
          ((Component) ((Component) gameObject2.transform.Find("stone_hand02_move")).gameObject.transform.Find("stone_hand02")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.StoneHand02s[index];
          ((Component) gameObject2.transform.Find("stone_eye01")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.StoneEye01s[index];
          ((Component) gameObject2.transform.Find("stone_eye02")).gameObject.GetComponent<Renderer>().material.mainTexture = this.self.StoneEye02s[index];
        }
      }
    }

    private class State_WaitDropStoneS : State<GachaController>
    {
      public override void Begin(GachaController self) => self.mDropStones[0].SetActive(true);

      public override void Update(GachaController self)
      {
        if (!self.IsAssetDownloadDone())
          return;
        self.mState.GotoState<GachaController.State_OpenDropStone>();
      }
    }

    private class State_WaitDropStone : State<GachaController>
    {
      private float mNextStateTime;
      private bool mAllActivated;
      private float mWaitTime = 0.1f;

      public override void Begin(GachaController self)
      {
        self.StartCoroutine(this.SetActiveDropStone());
      }

      [DebuggerHidden]
      private IEnumerator SetActiveDropStone()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaController.State_WaitDropStone.\u003CSetActiveDropStone\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }

      public override void Update(GachaController self)
      {
        if (!this.mAllActivated || !self.IsAssetDownloadDone())
          return;
        if ((double) this.mNextStateTime > 0.0)
          this.mNextStateTime -= Time.deltaTime;
        else
          self.mState.GotoState<GachaController.State_MoveDropStone>();
      }
    }

    private class State_MoveDropStone : State<GachaController>
    {
      private GameObject mStone;
      private float spd = 80f;
      private Vector3 mDestination = Vector3.zero;

      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count <= 0)
        {
          self.mState.GotoState<GachaController.State_WaitEndInput>();
        }
        else
        {
          this.mDestination = new Vector3(0.0f, self.StoneCenterHeight, 0.0f);
          this.mStone = self.mDropStones[0];
          if (!self.isSkipping)
            return;
          this.mStone.transform.localPosition = this.mDestination;
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
      }

      public override void Update(GachaController self)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStone, (UnityEngine.Object) null))
          return;
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          this.mStone.transform.localPosition = this.mDestination;
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
        else if ((double) Vector3.Distance(this.mStone.transform.localPosition, this.mDestination) < 0.10000000149011612)
        {
          self.mState.GotoState<GachaController.State_OpenDropStone>();
        }
        else
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.mDestination, this.mStone.transform.localPosition);
          Vector3 normalized = ((Vector3) ref vector3).normalized;
          Transform transform = this.mStone.transform;
          transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.op_Multiply(normalized, this.spd), Time.deltaTime));
        }
      }
    }

    private class State_OpenDropStone : State<GachaController>
    {
      private GameObject mStone;
      private Animator at;

      public override void Begin(GachaController self)
      {
        this.mStone = ((Component) self.mDropStones[0].transform.Find("stone_3d2")).gameObject;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mStone))
        {
          this.at = this.mStone.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.at, (UnityEngine.Object) null))
          {
            this.at.SetTrigger("trigger_break");
            if (self.mFlowType != GachaController.GachaFlowType.Normal)
            {
              this.at.SetBool("is_coin", true);
              if (self.DropCurrent.Excites[0] != self.DropCurrent.Excites[1])
              {
                this.at.SetInteger("first_break", self.DropCurrent.Excites[1]);
                if (self.DropCurrent.Excites[1] != self.DropCurrent.Excites[2])
                  this.at.SetInteger("second_break", self.DropCurrent.Excites[2]);
                else
                  this.at.SetInteger("second_break", 0);
              }
              else
              {
                if (self.DropCurrent.Excites[0] != self.DropCurrent.Excites[2])
                  this.at.SetInteger("first_break", self.DropCurrent.Excites[2]);
                else
                  this.at.SetInteger("first_break", 0);
                this.at.SetInteger("second_break", 0);
              }
            }
            else
              this.at.SetBool("is_coin", false);
          }
        }
        if (!self.isSkipping)
          return;
        self.isSkipping = false;
      }

      public override void Update(GachaController self)
      {
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          self.mState.GotoState<GachaController.State_DestroyDropStone>();
        }
        else
        {
          if (GameUtility.IsAnimatorRunning(this.mStone))
            return;
          self.mState.GotoState<GachaController.State_DestroyDropStone>();
        }
      }
    }

    private class State_DestroyDropStone : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count <= 0)
        {
          self.mState.GotoState<GachaController.State_CheckDropStone>();
        }
        else
        {
          self.mDropStones[0].SetActive(false);
          self.mDropStones.RemoveAt(0);
          self.mState.GotoState<GachaController.State_EnableDropMaterial>();
        }
      }
    }

    private class State_ActionRevolver : State<GachaController>
    {
      private Vector3 mNewAngle;
      private float mMoveSpeed;
      private float mTheta;

      public override void Begin(GachaController self)
      {
        this.mTheta = 360f / (float) self.GachaSequence;
        Transform parent = self.DropStone.transform.parent;
        this.mNewAngle = new Vector3(parent.localEulerAngles.x, parent.localEulerAngles.y + this.mTheta, parent.localEulerAngles.z);
        this.mMoveSpeed = this.mTheta / self.StoneRotateTime;
      }

      public override void Update(GachaController self)
      {
        Transform parent = self.DropStone.transform.parent;
        if (self.CheckSkip())
        {
          self.isSkipping = true;
          parent.localEulerAngles = this.mNewAngle;
          self.mState.GotoState<GachaController.State_CheckDropStone>();
        }
        else if (1.0 < (double) Mathf.DeltaAngle(parent.localEulerAngles.y, this.mNewAngle.y))
          parent.Rotate(Vector3.op_Multiply(this.mMoveSpeed * Time.deltaTime, Vector3.up));
        else
          self.mState.GotoState<GachaController.State_CheckDropStone>();
      }
    }

    private class State_EnableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mDropMaterial.SetActive(true);
        if (self.mFlowType != GachaController.GachaFlowType.Rare)
        {
          self.mState.GotoState<GachaController.State_InitDropThumbnail>();
        }
        else
        {
          GameObject openMaterial = self.OpenMaterial;
          openMaterial.SetActive(true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
          {
            GameObject gameObject1 = ((Component) openMaterial.transform.Find("lithograph_col")).gameObject;
            GameObject gameObject2 = ((Component) openMaterial.transform.Find("lithograph_eff")).gameObject;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
            {
              Image component1 = gameObject1.GetComponent<Image>();
              Image component2 = gameObject2.GetComponent<Image>();
              component1.sprite = self.LithographBases[self.GetRarityTextureIndex(self.Rarity)];
              ((Behaviour) component1).enabled = self.IsLithograph;
              ((Behaviour) component2).enabled = self.IsLithograph;
            }
            Animator component = openMaterial.GetComponent<Animator>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.SetInteger("rariry", self.Rarity);
              component.SetBool("shard", self.Shard);
              component.SetBool("item", self.Item);
              component.SetBool("ccard", self.IsConceptCard);
              component.SetBool("pickup", self.Pickup);
            }
          }
          if (self.isSkipping)
            self.isSkipping = false;
          self.mState.GotoState<GachaController.State_OpenDropMaterial>();
        }
      }
    }

    private class State_OpenDropMaterial : State<GachaController>
    {
      private GameObject go;
      private string[] ShardAnimList = new string[11]
      {
        "DropMaterial_rare1_Shard",
        "DropMaterial_rare2_Shard",
        "DropMaterial_rare3_Shard",
        "DropMaterial_rare4_Shard",
        "DropMaterial_rare5_Shard",
        "DropMaterial_pickup_rare5_Shard",
        "DropMaterial_item1",
        "DropMaterial_item2",
        "DropMaterial_item3",
        "DropMaterial_item4",
        "DropMaterial_item5"
      };

      public override void Begin(GachaController self)
      {
        if (!self.OnShardEffect || string.IsNullOrEmpty(self.DropCurrent.OName))
        {
          self.mState.GotoState<GachaController.State_WaitDropMaterial>();
        }
        else
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(self.DropCurrent.OName);
          if (unitDataByUnitId != null && unitDataByUnitId.GetAwakeLevelCap() <= unitDataByUnitId.AwakeLv)
            self.mState.GotoState<GachaController.State_WaitDropMaterial>();
          else
            this.go = self.OpenMaterial;
        }
      }

      public override void Update(GachaController self)
      {
        if (!string.IsNullOrEmpty(self.DropCurrent.OName))
        {
          if (GameUtility.IsAnimatorRunning(this.go))
            return;
          foreach (string shardAnim in this.ShardAnimList)
          {
            if (GameUtility.CompareAnimatorStateName(this.go, shardAnim))
            {
              self.mState.GotoState<GachaController.State_WaitGaugeAnimation>();
              break;
            }
          }
        }
        else
          self.mState.GotoState<GachaController.State_WaitDropMaterial>();
      }
    }

    private class State_WaitGaugeAnimation : State<GachaController>
    {
      private GachaUnitShard unitshard;

      public override void Begin(GachaController self)
      {
        this.unitshard = ((Component) self.GaugeObject.transform.Find("UnitShard_gauge")).GetComponent<GachaUnitShard>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitshard, (UnityEngine.Object) null))
          return;
        if (this.unitshard.IsReachingAwakelv())
        {
          this.MoveNextPhase();
        }
        else
        {
          this.unitshard.Reset();
          self.GaugeObject.SetActive(true);
          this.unitshard.Restart();
        }
      }

      private void MoveNextPhase()
      {
        this.self.GaugeObject.SetActive(false);
        if (this.self.mFlowType != GachaController.GachaFlowType.Rare)
        {
          if (this.unitshard.IsReachingUnlockUnit())
          {
            this.self.mState.GotoState<GachaController.State_OpenDropMaterialShard>();
          }
          else
          {
            this.self.DropCurrent.Next(this.self);
            if (!this.self.IsDropEnd())
            {
              ++this.self.thumbnail_count;
              if (this.self.thumbnail_count < this.self.mViewStoneCount)
              {
                this.self.mState.GotoState<GachaController.State_OpenDropThumbnail>();
                return;
              }
              if (this.self.mFlowType == GachaController.GachaFlowType.Special)
              {
                this.self.mState.GotoState<GachaController.State_CheckNextDropSet>();
                return;
              }
            }
            this.self.mState.GotoState<GachaController.State_WaitEndInput>();
          }
        }
        else
          this.self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
      }

      public override void Update(GachaController self)
      {
        if (this.unitshard.IsReachingAwakelv())
        {
          if (!self.mClicked)
            return;
          self.mClicked = false;
          this.MoveNextPhase();
        }
        else
        {
          if (self.mClicked)
          {
            self.mClicked = false;
            this.unitshard.OnClicked();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitshard, (UnityEngine.Object) null))
          {
            if (this.unitshard.IsRunningAnimator)
              return;
            this.MoveNextPhase();
          }
          else
          {
            this.unitshard = ((Component) self.GaugeObject.transform.Find("UnitShard_gauge")).GetComponent<GachaUnitShard>();
            if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitshard, (UnityEngine.Object) null))
              return;
            this.unitshard.Reset();
            this.unitshard.Restart();
          }
        }
      }
    }

    private class State_WaitDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mClicked = false;
        self.PlayUnitVoice();
      }

      public override void Update(GachaController self)
      {
        if (self.mClicked && !GameUtility.IsAnimatorRunning(self.OpenMaterial))
        {
          self.mClicked = false;
          self.mState.GotoState<GachaController.State_CheckCardUnit>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_CheckCardUnit : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.DropCurrent.IsConceptCard && self.DropCurrent.IsCardUnit)
          self.mState.GotoState<GachaController.State_CloseAnimDropMaterial>();
        else
          self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
      }
    }

    private class State_CloseAnimDropMaterial : State<GachaController>
    {
      private Animator m_OpenMaterial;

      public override void Begin(GachaController self)
      {
        Animator component = self.OpenMaterial.GetComponent<Animator>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.SetBool("cardunit", true);
        component.SetInteger("rariry", 0);
        this.m_OpenMaterial = component;
      }

      public override void Update(GachaController self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_OpenMaterial, (UnityEngine.Object) null) || GameUtility.IsAnimatorRunning((Component) this.m_OpenMaterial))
          return;
        self.SetupGetCardUnitAnim();
        self.mState.GotoState<GachaController.State_WaitCardAnim>();
      }
    }

    private class State_WaitCardAnim : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (self.FinishedCardUnitAnimation && self.mClicked)
        {
          self.mClicked = false;
          self.FinishedCardUnitAnimation = false;
          self.mState.GotoState<GachaController.State_ResetCardAnim>();
        }
        else
          self.mClicked = false;
      }
    }

    private class State_ResetCardAnim : State<GachaController>
    {
      public override void Begin(GachaController self) => self.ResetGetCardUnitAnim();

      public override void Update(GachaController self)
      {
        if (!self.NextGachaPhase)
          return;
        self.NextGachaPhase = false;
        self.mState.GotoState<GachaController.State_SettingDisableDropMaterial>();
      }
    }

    private class State_SettingDisableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.IsDropEnd())
        {
          self.mState.GotoState<GachaController.State_EndSetting>();
        }
        else
        {
          self.mDropMaterial.SetActive(false);
          GameObject openMaterial = self.OpenMaterial;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
          {
            Animator component = openMaterial.GetComponent<Animator>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetBool("reset", true);
          }
          for (int index = 0; index < self.ResetMaterials.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.ResetMaterials[index], (UnityEngine.Object) null))
              self.ResetMaterials[index].SetActive(false);
          }
          self.mState.GotoState<GachaController.State_DisableDropMaterial>();
        }
      }
    }

    private class State_DisableDropMaterial : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.mDropMaterial.SetActive(false);
        GameObject openMaterial = self.OpenMaterial;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) openMaterial, (UnityEngine.Object) null))
        {
          Animator component = openMaterial.GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.SetInteger("rariry", 0);
            component.SetBool("shard", false);
            component.SetBool("item", false);
            component.SetBool("reset", false);
            component.SetBool("ccard", false);
          }
        }
        self.DropCurrent.Next(self);
        if (self.IsDropEnd())
          self.mState.GotoState<GachaController.State_EndSetting>();
        else
          self.mState.GotoState<GachaController.State_NextDropAnimation>();
      }
    }

    private class State_NextDropAnimation : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        self.SetNextDropAnimation(self.DropCurrent.IsPickup);
      }

      public override void Update(GachaController self)
      {
        if (self.mClicked)
        {
          self.mClicked = false;
          self.EndStartupAnimation();
          self.mState.GotoState<GachaController.State_EnableDropMaterial>();
        }
        else if (self.IsStartupAnimationEnd(self.IsUseLastPickupAnim()))
        {
          self.EndStartupAnimation();
          self.mState.GotoState<GachaController.State_EnableDropMaterial>();
        }
        else
          self.UpdateAnimationState();
      }
    }

    private class State_CheckDropStone : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        if (self.mDropStones.Count > 0)
          self.mState.GotoState<GachaController.State_MoveDropStone>();
        else
          self.mState.GotoState<GachaController.State_EndSetting>();
      }
    }

    private class State_WaitEndInput : State<GachaController>
    {
      public override void Update(GachaController self)
      {
        if (!self.mClicked)
          return;
        self.mClicked = false;
        self.mState.GotoState<GachaController.State_EndSetting>();
      }
    }

    private class State_EndSetting : State<GachaController>
    {
      public override void Begin(GachaController self)
      {
        FlowNode_Variable.Set("GACHA_TYPE", (string) null);
        FlowNode_Variable.Set("SEQ_FIRST", (string) null);
        FlowNode_Variable.Set("SEQ_SECOND", (string) null);
        FlowNode_Variable.Set("SEQ_THIRD", (string) null);
        FlowNode_Variable.Set("GACHA_INPUT", (string) null);
        FlowNode_Variable.Set("GACHA_ANIMATION_END", (string) null);
        FlowNode_Variable.Set("GACHA_CIRCLE_SET", (string) null);
        if (self.mJingleHandle != null)
          self.mJingleHandle.Stop(1f);
        self.AllAnimSkip = false;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) self, "GACHA_ANIM_FINISH");
      }
    }

    private enum PickupAnimType : byte
    {
      None,
      Pickup,
      PickupUseAnim,
    }
  }
}

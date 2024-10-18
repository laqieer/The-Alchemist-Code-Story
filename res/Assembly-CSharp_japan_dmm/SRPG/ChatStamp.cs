// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStamp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "SetStampId", FlowNode.PinTypes.Output, 0)]
  public class ChatStamp : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Transform StampListRoot;
    [SerializeField]
    private Button NextPage;
    [SerializeField]
    private Button PrevPage;
    private GameObject[] mStampObjects;
    private ChatStampParam[] mStampParams;
    public static readonly string CHAT_STAMP_MASTER_PATH = "Data/Stamp";
    public static readonly string CHAT_STAMP_IMAGE_PATH = "Stamps/StampTable";
    public static readonly int STAMP_VIEW_MAX = 6;
    private int MaxPage;
    private int mCurrentPageIndex;
    private bool IsRefresh;
    private bool IsSending;
    private int mPrevSelectId = -1;
    private int mPrevSelectIndex = -1;
    private SpriteSheet mStampImages;
    private bool IsImageLoaded;
    private bool mImageLoaded;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.NextPage, (Object) null))
      {
        ((Selectable) this.NextPage).interactable = false;
        // ISSUE: method pointer
        ((UnityEvent) this.NextPage.onClick).AddListener(new UnityAction((object) this, __methodptr(OnNext)));
      }
      if (!Object.op_Inequality((Object) this.PrevPage, (Object) null))
        return;
      ((Selectable) this.PrevPage).interactable = false;
      // ISSUE: method pointer
      ((UnityEvent) this.PrevPage.onClick).AddListener(new UnityAction((object) this, __methodptr(OnPrev)));
    }

    private void OnNext()
    {
      this.mCurrentPageIndex = Mathf.Min(this.mCurrentPageIndex + 1, this.MaxPage);
      this.mPrevSelectId = -1;
      this.mPrevSelectIndex = -1;
      this.RefreshPager();
      this.IsRefresh = false;
    }

    private void OnPrev()
    {
      this.mCurrentPageIndex = Mathf.Max(this.mCurrentPageIndex - 1, 0);
      this.mPrevSelectId = -1;
      this.mPrevSelectIndex = -1;
      this.RefreshPager();
      this.IsRefresh = false;
    }

    private bool SetupChatStampMaster()
    {
      string src = AssetManager.LoadTextData(ChatStamp.CHAT_STAMP_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_ChatStampParam[] jsonArray = JSONParser.parseJSONArray<JSON_ChatStampParam>(src);
        this.mStampParams = jsonArray != null ? new ChatStampParam[jsonArray.Length] : throw new InvalidJSONException();
        for (int index = 0; index < jsonArray.Length; ++index)
        {
          ChatStampParam chatStampParam = new ChatStampParam();
          if (chatStampParam.Deserialize(jsonArray[index]))
            this.mStampParams[index] = chatStampParam;
        }
        this.MaxPage = jsonArray.Length % ChatStamp.STAMP_VIEW_MAX <= 0 ? jsonArray.Length / ChatStamp.STAMP_VIEW_MAX : jsonArray.Length / ChatStamp.STAMP_VIEW_MAX + 1;
      }
      catch
      {
        return false;
      }
      return true;
    }

    private void Start()
    {
      this.SetupChatStampMaster();
      if (this.mStampParams != null)
      {
        this.MaxPage = this.mStampParams.Length / ChatStamp.STAMP_VIEW_MAX;
        this.MaxPage = this.mStampParams.Length % ChatStamp.STAMP_VIEW_MAX <= 0 ? this.MaxPage : this.MaxPage + 1;
      }
      this.mCurrentPageIndex = 0;
      this.RefreshPager();
      if (!Object.op_Equality((Object) this.mStampImages, (Object) null) || this.mImageLoaded)
        return;
      this.StartCoroutine(this.LoadStampImages());
    }

    private void OnEnable() => this.IsRefresh = false;

    private void OnDisable()
    {
      if (this.mStampObjects != null && this.mStampObjects.Length > 0)
      {
        for (int index = 0; index < this.mStampObjects.Length; ++index)
        {
          this.mStampObjects[index].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
          this.mStampObjects[index].SetActive(false);
        }
      }
      this.mPrevSelectId = -1;
      this.mPrevSelectIndex = -1;
    }

    private void RefreshPager()
    {
      if (this.MaxPage == 0 && this.mCurrentPageIndex == 0)
        return;
      if (Object.op_Inequality((Object) this.NextPage, (Object) null))
        ((Selectable) this.NextPage).interactable = this.MaxPage > this.mCurrentPageIndex + 1;
      if (!Object.op_Inequality((Object) this.PrevPage, (Object) null))
        return;
      ((Selectable) this.PrevPage).interactable = 0 <= this.mCurrentPageIndex - 1;
    }

    private void Update()
    {
      if (this.IsRefresh || !this.IsImageLoaded)
        return;
      this.IsRefresh = true;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.mStampParams == null || this.mStampParams.Length <= 0)
        return;
      if ((this.mStampObjects == null || this.mStampObjects.Length <= 0) && Object.op_Inequality((Object) this.StampListRoot, (Object) null) && this.StampListRoot.childCount > 0)
      {
        this.mStampObjects = new GameObject[this.StampListRoot.childCount];
        for (int index = 0; index < this.StampListRoot.childCount; ++index)
        {
          Transform child = this.StampListRoot.GetChild(index);
          if (Object.op_Inequality((Object) child, (Object) null))
          {
            this.mStampObjects[index] = ((Component) child).gameObject;
            ((Component) child).gameObject.SetActive(false);
          }
        }
      }
      if (this.mStampObjects == null || this.mStampObjects.Length <= 0)
        return;
      for (int index = 0; index < this.mStampObjects.Length; ++index)
      {
        this.mStampObjects[index].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        this.mStampObjects[index].SetActive(false);
      }
      int num = ChatStamp.STAMP_VIEW_MAX * this.mCurrentPageIndex;
      for (int index = 0; index < this.mStampObjects.Length && num + index < this.mStampParams.Length; ++index)
      {
        ChatStampParam mStampParam = this.mStampParams[num + index];
        if (mStampParam != null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          ChatStamp.\u003CRefresh\u003Ec__AnonStorey1 refreshCAnonStorey1 = new ChatStamp.\u003CRefresh\u003Ec__AnonStorey1();
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey1.\u0024this = this;
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey1.index = index;
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey1.id = mStampParam.id;
          GameObject mStampObject = this.mStampObjects[index];
          Sprite sprite = this.mStampImages.GetSprite(mStampParam.img_id);
          Image component1 = mStampObject.GetComponent<Image>();
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.sprite = !Object.op_Inequality((Object) sprite, (Object) null) ? (Sprite) null : sprite;
          Button component2 = this.mStampObjects[index].GetComponent<Button>();
          if (Object.op_Inequality((Object) component2, (Object) null))
          {
            ((UnityEventBase) component2.onClick).RemoveAllListeners();
            // ISSUE: method pointer
            ((UnityEvent) component2.onClick).AddListener(new UnityAction((object) refreshCAnonStorey1, __methodptr(\u003C\u003Em__0)));
          }
          mStampObject.SetActive(true);
        }
      }
    }

    private void OnTapStamp(int id, int index)
    {
      if (id == this.mPrevSelectId)
      {
        this.mPrevSelectId = -1;
        this.mPrevSelectIndex = -1;
        FlowNode_Variable.Set("SELECT_STAMP_ID", id.ToString());
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
      }
      else
      {
        if (this.mPrevSelectId != -1 && this.mPrevSelectIndex != -1)
        {
          Transform transform = this.mStampObjects[this.mPrevSelectIndex].transform;
          if (Object.op_Inequality((Object) transform, (Object) null))
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        this.mPrevSelectId = id;
        this.mPrevSelectIndex = index;
        Transform transform1 = this.mStampObjects[index].transform;
        if (!Object.op_Inequality((Object) transform1, (Object) null))
          return;
        transform1.localScale = new Vector3(1f, 1f, 1f);
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadStampImages()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatStamp.\u003CLoadStampImages\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}

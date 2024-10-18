// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStamp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "SetStampId", FlowNode.PinTypes.Output, 0)]
  public class ChatStamp : MonoBehaviour, IFlowInterface
  {
    public static readonly string CHAT_STAMP_MASTER_PATH = "Data/Stamp";
    public static readonly string CHAT_STAMP_IMAGE_PATH = "Stamps/StampTable";
    public static readonly int STAMP_VIEW_MAX = 6;
    private int mPrevSelectId = -1;
    private int mPrevSelectIndex = -1;
    [SerializeField]
    private Transform StampListRoot;
    [SerializeField]
    private Button NextPage;
    [SerializeField]
    private Button PrevPage;
    private GameObject[] mStampObjects;
    private ChatStampParam[] mStampParams;
    private int MaxPage;
    private int mCurrentPageIndex;
    private bool IsRefresh;
    private bool IsSending;
    private SpriteSheet mStampImages;
    private bool IsImageLoaded;
    private bool mImageLoaded;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.NextPage != (UnityEngine.Object) null)
      {
        this.NextPage.interactable = false;
        this.NextPage.onClick.AddListener(new UnityAction(this.OnNext));
      }
      if (!((UnityEngine.Object) this.PrevPage != (UnityEngine.Object) null))
        return;
      this.PrevPage.interactable = false;
      this.PrevPage.onClick.AddListener(new UnityAction(this.OnPrev));
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
        if (jsonArray == null)
          throw new InvalidJSONException();
        this.mStampParams = new ChatStampParam[jsonArray.Length];
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
      if (!((UnityEngine.Object) this.mStampImages == (UnityEngine.Object) null) || this.mImageLoaded)
        return;
      this.StartCoroutine(this.LoadStampImages());
    }

    private void OnEnable()
    {
      this.IsRefresh = false;
    }

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
      if ((UnityEngine.Object) this.NextPage != (UnityEngine.Object) null)
        this.NextPage.interactable = this.MaxPage > this.mCurrentPageIndex + 1;
      if (!((UnityEngine.Object) this.PrevPage != (UnityEngine.Object) null))
        return;
      this.PrevPage.interactable = 0 <= this.mCurrentPageIndex - 1;
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
      if ((this.mStampObjects == null || this.mStampObjects.Length <= 0) && ((UnityEngine.Object) this.StampListRoot != (UnityEngine.Object) null && this.StampListRoot.childCount > 0))
      {
        this.mStampObjects = new GameObject[this.StampListRoot.childCount];
        for (int index = 0; index < this.StampListRoot.childCount; ++index)
        {
          Transform child = this.StampListRoot.GetChild(index);
          if ((UnityEngine.Object) child != (UnityEngine.Object) null)
          {
            this.mStampObjects[index] = child.gameObject;
            child.gameObject.SetActive(false);
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
      for (int index1 = 0; index1 < this.mStampObjects.Length && num + index1 < this.mStampParams.Length; ++index1)
      {
        ChatStampParam mStampParam = this.mStampParams[num + index1];
        if (mStampParam != null)
        {
          int index = index1;
          int id = mStampParam.id;
          GameObject mStampObject = this.mStampObjects[index1];
          Sprite sprite = this.mStampImages.GetSprite(mStampParam.img_id);
          Image component1 = mStampObject.GetComponent<Image>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            component1.sprite = !((UnityEngine.Object) sprite != (UnityEngine.Object) null) ? (Sprite) null : sprite;
          Button component2 = this.mStampObjects[index1].GetComponent<Button>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            component2.onClick.RemoveAllListeners();
            component2.onClick.AddListener((UnityAction) (() => this.OnTapStamp(id, index)));
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
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        this.mPrevSelectId = id;
        this.mPrevSelectIndex = index;
        Transform transform1 = this.mStampObjects[index].transform;
        if (!((UnityEngine.Object) transform1 != (UnityEngine.Object) null))
          return;
        transform1.localScale = new Vector3(1f, 1f, 1f);
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadStampImages()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatStamp.\u003CLoadStampImages\u003Ec__IteratorF2() { \u003C\u003Ef__this = this };
    }
  }
}

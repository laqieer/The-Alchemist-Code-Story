// Decompiled with JetBrains decompiler
// Type: SRPG.AwardListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AwardListItem : MonoBehaviour
  {
    public static readonly string EXTRA_GRADE_IMAGEPATH = "AwardImage/ExtraAwards";
    public static readonly int MAX_GRADE = 6;
    [SerializeField]
    private Image BackGround;
    [SerializeField]
    private Text AwardName;
    [SerializeField]
    private GameObject Cursor;
    [SerializeField]
    private GameObject Badge;
    private string mAwardIname;
    private bool mIsSelected;
    private bool mHasItem;
    private bool mIsRefresh;
    private bool mRemove;
    private GameManager gm;

    private void Awake()
    {
      if ((UnityEngine.Object) this.BackGround != (UnityEngine.Object) null)
        this.BackGround.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AwardName != (UnityEngine.Object) null)
      {
        this.AwardName.text = LocalizedText.Get("sys.TEXT_NO_AWARD");
        this.AwardName.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.Cursor != (UnityEngine.Object) null)
        this.Cursor.SetActive(false);
      if (!((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Badge.SetActive(false);
    }

    private void Update()
    {
      if (!this.mIsRefresh)
        return;
      this.mIsRefresh = false;
      this.Refresh();
    }

    public void SetUp(string iname, bool hasItem = false, bool selected = false, bool remove = false)
    {
      if (string.IsNullOrEmpty(iname) && !remove)
      {
        DebugUtility.LogError("iname is null!");
      }
      else
      {
        this.mAwardIname = iname;
        this.mHasItem = hasItem;
        this.mIsSelected = selected;
        this.mRemove = remove;
        this.mIsRefresh = true;
      }
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.gm == (UnityEngine.Object) null)
        this.gm = MonoSingleton<GameManager>.Instance;
      AwardParam awardParam;
      if (!this.mRemove)
      {
        awardParam = this.gm.MasterParam.GetAwardParam(this.mAwardIname);
        if (awardParam == null)
          return;
      }
      else
        awardParam = this.CreateRemoveAwardData();
      if ((UnityEngine.Object) this.BackGround != (UnityEngine.Object) null)
      {
        ImageArray component = this.BackGround.GetComponent<ImageArray>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          if (this.mHasItem)
          {
            if (AwardListItem.MAX_GRADE <= awardParam.grade)
            {
              component.ImageIndex = 0;
              if (!string.IsNullOrEmpty(awardParam.bg))
                this.SetExtraAwardImage(awardParam.bg);
              awardParam.name = string.Empty;
            }
            else
              component.ImageIndex = awardParam.grade + 1;
          }
          else
            component.ImageIndex = !this.mRemove ? 1 : 0;
        }
      }
      if ((UnityEngine.Object) this.AwardName != (UnityEngine.Object) null)
      {
        if (awardParam.grade == -1)
        {
          this.AwardName.text = awardParam.name;
          this.AwardName.gameObject.SetActive(true);
        }
        else
        {
          this.AwardName.text = !this.mHasItem ? LocalizedText.Get("sys.TEXT_NO_AWARD") : awardParam.name;
          this.AwardName.gameObject.SetActive(this.mHasItem);
        }
      }
      if (this.mRemove || !((UnityEngine.Object) this.Cursor != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Badge != (UnityEngine.Object) null))
        return;
      this.Cursor.SetActive(this.mIsSelected);
      this.Badge.SetActive(this.mIsSelected);
    }

    private AwardParam CreateRemoveAwardData()
    {
      return new AwardParam() { grade = -1, iname = string.Empty, name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD") };
    }

    private bool SetExtraAwardImage(string bg)
    {
      if (string.IsNullOrEmpty(bg))
        return false;
      ImageArray component = this.BackGround.GetComponent<ImageArray>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null)
        component.sprite = spriteSheet.GetSprite(bg);
      return true;
    }
  }
}

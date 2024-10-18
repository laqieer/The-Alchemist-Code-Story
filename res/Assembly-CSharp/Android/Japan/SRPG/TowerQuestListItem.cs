// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerQuestListItem : MonoBehaviour
  {
    private Color UnknownColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    private TowerQuestListItem.Type now_type = TowerQuestListItem.Type.Unknown;
    [SerializeField]
    private GameObject mBody;
    [SerializeField]
    private GameObject mCleared;
    [SerializeField]
    private GameObject mLocked;
    [SerializeField]
    private Graphic mGraphicRoot;
    [SerializeField]
    private ImageArray[] mBanner;
    [SerializeField]
    private GameObject mCursor;
    [SerializeField]
    private Text mText;
    [SerializeField]
    private Text m_FloorText;
    public CanvasRenderer Source;
    private const string FLOOR_NO_PREFIX = "floorNo_";
    private RectTransform mBodyTransform;

    public RectTransform rectTransform { get; private set; }

    public ImageArray[] Banner
    {
      get
      {
        return this.mBanner;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.mBody != (UnityEngine.Object) null)
        this.mBodyTransform = this.mBody.GetComponent<RectTransform>();
      this.rectTransform = this.GetComponent<RectTransform>();
    }

    public void OnFocus(bool value)
    {
      if (!((UnityEngine.Object) this.mBodyTransform != (UnityEngine.Object) null))
        return;
      if (value)
        this.mBodyTransform.localScale = new Vector3(1f, 1f, 1f);
      else
        this.mBodyTransform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }

    private void SetVisible(TowerQuestListItem.Type type)
    {
      this.now_type = type;
      GameUtility.SetGameObjectActive(this.mCleared, false);
      GameUtility.SetGameObjectActive(this.mLocked, false);
      GameUtility.SetGameObjectActive(this.mCursor, false);
      GameUtility.SetGameObjectActive((Component) this.m_FloorText, false);
      switch (type)
      {
        case TowerQuestListItem.Type.Locked:
          this.Source.SetColor(Color.gray);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          GameUtility.SetGameObjectActive(this.mLocked, true);
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          break;
        case TowerQuestListItem.Type.Cleared:
          this.Source.SetColor(Color.white);
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive(this.mCleared, true);
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          break;
        case TowerQuestListItem.Type.Current:
          this.Source.SetColor(Color.white);
          this.mBanner[0].ImageIndex = 0;
          this.mBanner[1].ImageIndex = 0;
          GameUtility.SetGameObjectActive((Component) this.m_FloorText, true);
          GameUtility.SetGameObjectActive(this.mCursor, true);
          break;
        case TowerQuestListItem.Type.Unknown:
          this.Source.SetColor(this.UnknownColor);
          this.mBanner[0].ImageIndex = 1;
          this.mBanner[1].ImageIndex = 1;
          break;
      }
    }

    public void SetNowImage()
    {
      this.SetVisible(this.now_type);
    }

    public void UpdateParam(TowerFloorParam param, int floorNo)
    {
      if (param == null)
      {
        this.SetVisible(TowerQuestListItem.Type.Unknown);
      }
      else
      {
        QuestParam questParam = param.Clone((QuestParam) null, true);
        bool flag = questParam.IsQuestCondition();
        if (flag && questParam.state != QuestStates.Cleared)
          this.SetVisible(TowerQuestListItem.Type.Current);
        else if (questParam.state == QuestStates.Cleared)
          this.SetVisible(TowerQuestListItem.Type.Cleared);
        else if (!flag)
          this.SetVisible(TowerQuestListItem.Type.Locked);
        if (param != null && (UnityEngine.Object) this.mText != (UnityEngine.Object) null)
          this.mText.text = param.title + " " + param.name;
        if (!((UnityEngine.Object) this.m_FloorText != (UnityEngine.Object) null))
          return;
        this.m_FloorText.text = param.GetFloorNo().ToString() + "!";
      }
    }

    private enum Type
    {
      Locked,
      Cleared,
      Current,
      Unknown,
      TypeEnd,
    }
  }
}

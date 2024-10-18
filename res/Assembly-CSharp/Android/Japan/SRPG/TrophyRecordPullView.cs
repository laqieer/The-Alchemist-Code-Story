// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyRecordPullView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TrophyRecordPullView : SRPG_ListBase
  {
    private float FRAME_MERGIN = 25f;
    [SerializeField]
    private int CREATE_CHILD_COUNT = 20;
    private TrophyRecordPullView.eState state = TrophyRecordPullView.eState.CLOSED;
    [SerializeField]
    private float CLOSE_SECOND = 0.15f;
    private float DEFAULT_OPEN_SPEED_AREA = 500f;
    private float OPEN_SPEED = 2000f;
    private float HI_OPEN_SPEED = 20000f;
    private float HI_CLOSE_SPEED = 9000f;
    [SerializeField]
    private GameObject[] original_objects;
    [SerializeField]
    private GameObject badge;
    [SerializeField]
    private VerticalLayoutGroup vertical_layout_group;
    [SerializeField]
    private Transform contents_parent;
    [SerializeField]
    private LayoutElement root_layout_element;
    [SerializeField]
    private RectTransform view_port_rect;
    [SerializeField]
    private RectTransform grid_rect;
    [SerializeField]
    private RectTransform contents_transform;
    [SerializeField]
    private RectTransform button_open_rect;
    [SerializeField]
    private RectTransform button_close_rect;
    [SerializeField]
    private BitmapText comp_trophy_count_text;
    [SerializeField]
    private BitmapText total_trophy_count_text;
    private TrophyCategoryData category_data;
    private TrophyList trophy_list;
    private int comp_trophy_count;
    private int index;
    private float item_distance;
    private float view_mergin;
    private float start_button_open_size;
    private Vector2 start_pos;
    private Vector2 target_pos;
    private float default_min_height;
    private float target_view_port_size;
    private float anim_speed;
    private float move_value;

    private TrophyRecordPullView.eState State
    {
      get
      {
        return this.state;
      }
    }

    public int HashCode
    {
      get
      {
        return this.category_data.Param.hash_code;
      }
    }

    public bool IsStateOpen
    {
      get
      {
        if (this.state != TrophyRecordPullView.eState.OPEN)
          return this.state == TrophyRecordPullView.eState.OPENED;
        return true;
      }
    }

    public bool IsStateOpened
    {
      get
      {
        return this.state == TrophyRecordPullView.eState.OPENED;
      }
    }

    public bool IsStateClose
    {
      get
      {
        if (this.state != TrophyRecordPullView.eState.CLOSE && this.state != TrophyRecordPullView.eState.CLOSED)
          return this.state == TrophyRecordPullView.eState.CLOSE_IMMEDIATE;
        return true;
      }
    }

    public bool IsStateClosed
    {
      get
      {
        return this.state == TrophyRecordPullView.eState.CLOSED;
      }
    }

    public float RootLayoutElementMinHeightDef
    {
      get
      {
        return this.root_layout_element.minHeight - this.default_min_height;
      }
    }

    public int Index
    {
      get
      {
        return this.index;
      }
    }

    public float ItemDistance
    {
      get
      {
        return this.item_distance;
      }
    }

    public float TargetViewPortSize
    {
      get
      {
        return this.target_view_port_size;
      }
    }

    public float VerticalLayoutSpacing
    {
      get
      {
        return this.vertical_layout_group.spacing;
      }
    }

    protected override RectTransform GetRectTransform()
    {
      return this.trophy_list.transform as RectTransform;
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.badge != (UnityEngine.Object) null))
        return;
      this.button_open_rect.gameObject.SetActive(false);
      this.button_close_rect.gameObject.SetActive(true);
      this.badge.SetActive(false);
    }

    private void Update()
    {
      switch (this.state)
      {
        case TrophyRecordPullView.eState.OPEN:
          this.UpdateOpen();
          break;
        case TrophyRecordPullView.eState.CLOSE:
          this.UpdateClose();
          break;
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.UpdateCloseImmediate();
          break;
      }
    }

    public void Init(string _title_str)
    {
      this.gameObject.SetActive(true);
      Text componentInChildren = this.GetComponentInChildren<Text>();
      if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        componentInChildren.text = _title_str;
      if (this.original_objects.Length > 0)
      {
        for (int index = 0; index < this.original_objects.Length; ++index)
          this.original_objects[index].SetActive(false);
      }
      if (!((UnityEngine.Object) this.root_layout_element != (UnityEngine.Object) null))
        return;
      this.default_min_height = this.root_layout_element.minHeight;
    }

    public void Setup(int _index, TrophyList _trophy_list)
    {
      this.index = _index;
      this.item_distance = Mathf.Abs(this.contents_transform.anchoredPosition.y + this.grid_rect.anchoredPosition.y);
      this.trophy_list = _trophy_list;
      this.view_mergin = this.item_distance * (float) (this.index + 1);
    }

    public void SetCategoryData(TrophyCategoryData _category_data)
    {
      this.category_data = _category_data;
    }

    public void Refresh(RectTransform _scroll_trans_rect)
    {
      if (this.State != TrophyRecordPullView.eState.OPENED || !this.gameObject.activeInHierarchy || (UnityEngine.Object) _scroll_trans_rect == (UnityEngine.Object) null)
        return;
      this.ClearItems();
      this.Setup(this.index, this.trophy_list);
      this.RefreshDisplayParam();
      this.CreateContents();
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, this.target_view_port_size);
      this.root_layout_element.minHeight = this.default_min_height + this.target_view_port_size;
      this.button_open_rect.sizeDelta = new Vector2(0.0f, this.view_port_rect.sizeDelta.y - this.contents_transform.anchoredPosition.y);
      this.target_pos = new Vector2(_scroll_trans_rect.anchoredPosition.x, (float) this.index * this.item_distance);
      _scroll_trans_rect.anchoredPosition = new Vector2(0.0f, this.target_pos.y);
    }

    public void RefreshDisplayParam()
    {
      this.comp_trophy_count = 0;
      for (int index = 0; index < this.category_data.Trophies.Count; ++index)
      {
        if (this.category_data.Trophies[index].IsCompleted)
          ++this.comp_trophy_count;
      }
      if ((UnityEngine.Object) this.comp_trophy_count_text != (UnityEngine.Object) null && (UnityEngine.Object) this.total_trophy_count_text != (UnityEngine.Object) null)
      {
        int num1 = Mathf.Min(this.CREATE_CHILD_COUNT, this.comp_trophy_count);
        int num2 = Mathf.Min(this.CREATE_CHILD_COUNT, this.category_data.Trophies.Count);
        this.comp_trophy_count_text.text = num1.ToString();
        this.total_trophy_count_text.text = num2.ToString();
      }
      if (!((UnityEngine.Object) this.badge != (UnityEngine.Object) null))
        return;
      this.badge.SetActive(this.comp_trophy_count > 0);
    }

    public void StartOpen()
    {
      this.move_value = 0.0f;
      this.anim_speed = this.OPEN_SPEED;
      this.button_open_rect.gameObject.SetActive(true);
      this.button_close_rect.gameObject.SetActive(false);
      this.start_button_open_size = this.button_open_rect.sizeDelta.y;
      this.ChangeState(TrophyRecordPullView.eState.OPEN);
    }

    private void EndOpen()
    {
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, this.target_view_port_size);
      this.root_layout_element.minHeight = this.default_min_height + this.target_view_port_size;
      this.button_open_rect.sizeDelta = new Vector2(0.0f, this.view_port_rect.sizeDelta.y - this.contents_transform.anchoredPosition.y);
    }

    private void UpdateOpen()
    {
      float num = this.anim_speed * Time.deltaTime;
      this.move_value += num;
      if ((double) this.move_value >= (double) this.DEFAULT_OPEN_SPEED_AREA)
        this.anim_speed = this.HI_OPEN_SPEED;
      float y = Mathf.Min(this.view_port_rect.sizeDelta.y + num, this.target_view_port_size);
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, y);
      this.root_layout_element.minHeight = Mathf.Min(this.root_layout_element.minHeight + num, this.default_min_height + this.target_view_port_size);
      this.button_open_rect.sizeDelta = new Vector2(0.0f, Mathf.Max(this.view_port_rect.sizeDelta.y - this.contents_transform.anchoredPosition.y, this.start_button_open_size));
      if ((double) y < (double) this.target_view_port_size)
        return;
      this.ChangeState(TrophyRecordPullView.eState.OPENED);
    }

    public void StartClose()
    {
      if ((UnityEngine.Object) this.contents_parent == (UnityEngine.Object) null)
        return;
      this.anim_speed = Mathf.Min(this.target_view_port_size / this.CLOSE_SECOND, this.HI_CLOSE_SPEED);
      this.target_view_port_size = 0.0f;
      this.ChangeState(TrophyRecordPullView.eState.CLOSE);
    }

    private void EndClose()
    {
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, this.target_view_port_size);
      this.root_layout_element.minHeight = this.default_min_height + this.target_view_port_size;
      this.button_open_rect.sizeDelta = new Vector2(0.0f, this.start_button_open_size);
    }

    private void UpdateClose()
    {
      float num = this.anim_speed * Time.deltaTime;
      float y = Mathf.Max(this.view_port_rect.sizeDelta.y - num, this.target_view_port_size);
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, y);
      this.root_layout_element.minHeight = Mathf.Max(this.root_layout_element.minHeight - num, this.default_min_height + this.target_view_port_size);
      this.button_open_rect.sizeDelta = new Vector2(0.0f, Mathf.Max(this.view_port_rect.sizeDelta.y - this.contents_transform.anchoredPosition.y, this.start_button_open_size));
      if ((double) y > (double) this.target_view_port_size)
        return;
      this.ChangeState(TrophyRecordPullView.eState.CLOSED);
    }

    private void StartClosed()
    {
      this.ClearItems();
      this.button_open_rect.gameObject.SetActive(false);
      this.button_close_rect.gameObject.SetActive(true);
    }

    protected void StartCloseImmediate()
    {
      if ((UnityEngine.Object) this.contents_parent == (UnityEngine.Object) null)
        return;
      this.target_view_port_size = 0.0f;
    }

    protected void EndCloseImmediate()
    {
      this.view_port_rect.sizeDelta = new Vector2(this.view_port_rect.sizeDelta.x, this.target_view_port_size);
      this.root_layout_element.minHeight = this.default_min_height + this.target_view_port_size;
      this.button_open_rect.sizeDelta = new Vector2(0.0f, this.start_button_open_size);
    }

    protected void UpdateCloseImmediate()
    {
      this.ChangeState(TrophyRecordPullView.eState.CLOSED);
    }

    public void CreateContents()
    {
      if (this.original_objects.Length <= 0 || (UnityEngine.Object) this.contents_parent == (UnityEngine.Object) null)
        return;
      float num = 0.0f;
      List<GameObject> instances = this.CreateInstances();
      if (instances == null)
        return;
      for (int index = 0; index < instances.Count; ++index)
      {
        instances[index].transform.SetParent(this.contents_parent, false);
        RectTransform component = instances[index].GetComponent<RectTransform>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          num += component.rect.height * this.grid_rect.localScale.y;
      }
      if ((UnityEngine.Object) this.vertical_layout_group != (UnityEngine.Object) null)
        num += this.vertical_layout_group.spacing * (float) (instances.Count - 1);
      this.target_view_port_size = num - this.grid_rect.transform.localPosition.y;
      this.target_view_port_size += this.FRAME_MERGIN;
    }

    private List<GameObject> CreateInstances()
    {
      if (this.category_data == null || this.category_data.Trophies.Count <= 0)
        return (List<GameObject>) null;
      List<GameObject> gameObjectList = new List<GameObject>();
      int createChildCount = this.CREATE_CHILD_COUNT;
      for (int index = 0; index < this.category_data.Trophies.Count && createChildCount != 0; ++index)
      {
        ListItemEvents listItemEvents = this.trophy_list.MakeTrophyPlate(this.category_data.Trophies[index], this.category_data.Trophies[index].IsCompleted);
        if ((UnityEngine.Object) listItemEvents != (UnityEngine.Object) null)
        {
          --createChildCount;
          this.AddItem(listItemEvents);
          gameObjectList.Add(listItemEvents.gameObject);
          listItemEvents.DisplayRectMergin = new Vector2(0.0f, this.view_mergin);
          listItemEvents.ParentScale = (Vector2) this.grid_rect.localScale;
        }
      }
      return gameObjectList;
    }

    public void ChangeState(TrophyRecordPullView.eState _new_state)
    {
      if (this.state == _new_state)
        return;
      switch (this.state)
      {
        case TrophyRecordPullView.eState.OPEN:
          this.EndOpen();
          break;
        case TrophyRecordPullView.eState.CLOSE:
          this.EndClose();
          break;
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.EndCloseImmediate();
          break;
      }
      this.state = _new_state;
      switch (this.state)
      {
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.StartCloseImmediate();
          break;
        case TrophyRecordPullView.eState.CLOSED:
          this.StartClosed();
          break;
      }
    }

    public void OnClickEvent()
    {
      this.trophy_list.SetClickTarget(this);
    }

    public enum eState
    {
      OPEN,
      OPENED,
      CLOSE,
      CLOSE_IMMEDIATE,
      CLOSED,
    }
  }
}

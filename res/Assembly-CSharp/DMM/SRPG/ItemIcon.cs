// Decompiled with JetBrains decompiler
// Type: SRPG.ItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ItemIcon : BaseIcon
  {
    protected const string TooltipPath = "UI/ItemTooltip";
    protected const string ICON_NAME_UNKNOWN = "IT_UNKNOWN";
    [Space(10f)]
    public GameParameter.ItemInstanceTypes InstanceType;
    public int InstanceIndex;
    [Space(10f)]
    public RawImage Icon;
    public Image Frame;
    public Text Num;
    public Slider NumSlider;
    public bool Tooltip;
    public bool TapTooltip;
    public Text HaveNum;
    public bool IsSecret;
    protected ItemParam mSecretItemParam;
    [Description("個数表記GameObjectへの参照")]
    public GameObject SecretAmount;
    [Description("装備可能なユニットが存在する場合に表示状態を変更するバッジへの参照")]
    public Image SecretBadge;
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始までの時間")]
    public float SecretWaitSec = 1f;
    [Description("「？」アイコン⇒正規アイコン変更アニメトリガー名")]
    public string SecretAnimName = string.Empty;
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始後、アイコンを差し替えるまでの時間")]
    public float SecretAnimWaitSec = 0.2f;
    [Description("「？」期限付きアイテムを示すアイコンを表示するか")]
    [SerializeField]
    private bool IsDispLimitedIcon;
    protected SRPG.Tooltip mTooltip;
    protected bool mReqExchgSecretIcon;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam;
        this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out int _);
        return itemParam != null;
      }
    }

    protected override void ShowTooltip(Vector2 screen) => this.CreateTooltip(screen, false);

    protected override void ShowTooltipByTap(Vector2 _screen) => this.CreateTooltip(_screen, true);

    private void CreateTooltip(Vector2 screen, bool _is_tap)
    {
      if (Object.op_Inequality((Object) this.mTooltip, (Object) null) && Object.op_Inequality((Object) ((Component) this.mTooltip).gameObject, (Object) null))
        return;
      RectTransform transform = ((Component) this).transform as RectTransform;
      SRPG.Tooltip tooltip1 = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
      if (Object.op_Equality((Object) tooltip1, (Object) null))
        return;
      SRPG.Tooltip tooltip2 = Object.Instantiate<SRPG.Tooltip>(tooltip1);
      if (_is_tap)
      {
        tooltip2.CloseOnPress = true;
        tooltip2.DestroyDelay = 0.0f;
      }
      this.mTooltip = tooltip2;
      LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip2.Body);
      Rect rect1 = tooltip2.Body.rect;
      float width = ((Rect) ref rect1).width;
      Vector2 vector2_1 = screen;
      float num1 = screen.x - width / 2f;
      if ((double) num1 < 0.0)
        vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.right, -num1));
      RectTransform component = ((Component) ((Component) ((Component) this).transform).GetComponentInParent<Canvas>().rootCanvas).GetComponent<RectTransform>();
      LayoutRebuilder.ForceRebuildLayoutImmediate(component);
      Rect rect2 = component.rect;
      float num2 = ((Rect) ref rect2).width - (screen.x + width / 2f);
      if ((double) num2 < 0.0)
        vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.left, -num2));
      Vector2 vector2_2 = vector2_1;
      Vector2 up = Vector2.up;
      Rect rect3 = transform.rect;
      double height = (double) ((Rect) ref rect3).height;
      Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(up, (float) height), 0.5f);
      SRPG.Tooltip.TooltipPosition = Vector2.op_Addition(vector2_2, vector2_3);
      ItemParam itemParam;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out int _);
      DataSource.Bind<ItemParam>(((Component) tooltip2).gameObject, itemParam);
      RuneData dataOfClass = DataSource.FindDataOfClass<RuneData>(((Component) this).gameObject, (RuneData) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<RuneData>(((Component) tooltip2).gameObject, dataOfClass);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
      if (!this.Tooltip && this.TapTooltip)
      {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
          return;
        RectTransform transform = (RectTransform) ((Component) this).transform;
        Vector2 _screen = Vector2.op_Implicit(((Transform) transform).TransformPoint(Vector2.op_Implicit(Vector2.zero)));
        CanvasScaler componentInParent = ((Component) transform).GetComponentInParent<CanvasScaler>();
        if (Object.op_Inequality((Object) componentInParent, (Object) null))
        {
          Vector3 localScale = ((Component) componentInParent).transform.localScale;
          _screen.x /= localScale.x;
          _screen.y /= localScale.y;
        }
        this.ShowTooltipByTap(_screen);
      }
      else
        base.OnPointerDown(eventData);
    }

    public override void UpdateValue()
    {
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).gameObject, out itemParam, out itemNum);
      if (itemParam == null)
        return;
      this.mSecretItemParam = itemParam;
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
      {
        if (this.IsSecret)
        {
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon("IT_UNKNOWN"));
          if (Object.op_Implicit((Object) this.SecretAmount))
            this.SecretAmount.SetActive(false);
          if (Object.op_Implicit((Object) this.SecretBadge))
            ((Behaviour) this.SecretBadge).enabled = false;
          if (this.IsDispLimitedIcon)
            ((Component) this.Icon).RequireComponent<ItemLimitedIconAttach>().Hide();
        }
        else
        {
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
          if (this.IsDispLimitedIcon)
            ((Component) this.Icon).RequireComponent<ItemLimitedIconAttach>().Refresh(itemParam);
        }
      }
      if (Object.op_Inequality((Object) this.Frame, (Object) null))
      {
        if (this.IsSecret)
        {
          if (GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0)
            this.Frame.sprite = GameSettings.Instance.ItemIcons.NormalFrames[0];
        }
        else
          this.Frame.sprite = GameSettings.Instance.GetItemFrame(itemParam);
      }
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.text = itemNum.ToString();
      if (Object.op_Inequality((Object) this.NumSlider, (Object) null))
        this.NumSlider.value = (float) itemNum / (float) itemParam.cap;
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int num = -1;
      if (itemParam.iname == "$COIN")
      {
        num = MonoSingleton<GameManager>.Instance.Player.Coin;
      }
      else
      {
        ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
        if (itemDataByItemParam != null)
          num = itemDataByItemParam.Num;
      }
      if (num >= 0)
        this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", (object) num);
      else
        this.HaveNum.text = string.Empty;
    }

    public virtual void ExchgSecretIcon()
    {
      if (!this.IsSecret || this.mReqExchgSecretIcon || this.mSecretItemParam == null)
        return;
      this.mReqExchgSecretIcon = true;
      this.StartCoroutine(this.exchgSecretIcon());
    }

    [DebuggerHidden]
    protected virtual IEnumerator exchgSecretIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ItemIcon.\u003CexchgSecretIcon\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    protected void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mTooltip, (Object) null) || !Object.op_Inequality((Object) ((Component) this.mTooltip).gameObject, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mTooltip).gameObject);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactRarityCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "分解を選択", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "売却を選択", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "キャンセルを選択", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "ウィンドウを閉じる", FlowNode.PinTypes.Output, 100)]
  public class ArtifactRarityCheck : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_DECOMPOSE = 0;
    private const int INPUT_SELL = 1;
    private const int INPUT_CANCEL = 2;
    private const int OUTPUT_CLOSE_ACTION = 100;
    [SerializeField]
    private GameObject mArtifactTemplate;
    [SerializeField]
    private RectTransform mArtifactObjectParent;
    [SerializeField]
    private LText mLText;
    [SerializeField]
    private GameObject mButtonDecompose;
    [SerializeField]
    private GameObject mButtonSell;
    private ArtifactRarityCheck.Type mType;
    public ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent OnDecideEvent;
    public ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent OnCancelEvent;
    private GameObject mArgGo;
    private List<ArtifactData> mArtifactDataList;
    private int mRarity;

    private void Start()
    {
      if ((UnityEngine.Object) this.mArgGo == (UnityEngine.Object) null || this.mArtifactDataList == null || (this.mType == ArtifactRarityCheck.Type.NONE || (UnityEngine.Object) this.mLText == (UnityEngine.Object) null))
        return;
      if ((UnityEngine.Object) this.mButtonDecompose != (UnityEngine.Object) null && this.mType == ArtifactRarityCheck.Type.DECOMPOSE)
        this.mButtonSell.SetActive(false);
      else if ((UnityEngine.Object) this.mButtonSell != (UnityEngine.Object) null && this.mType == ArtifactRarityCheck.Type.SELL)
        this.mButtonDecompose.SetActive(false);
      this.mLText.text = string.Format(LocalizedText.Get(this.mLText.text), (object) this.mRarity.ToString());
      this.mArtifactTemplate.SetActive(false);
      foreach (ArtifactData mArtifactData in this.mArtifactDataList)
      {
        if ((int) mArtifactData.Rarity + 1 >= this.mRarity)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mArtifactTemplate);
          gameObject.transform.SetParent((Transform) this.mArtifactObjectParent, false);
          DataSource.Bind<ArtifactData>(gameObject, mArtifactData, false);
          gameObject.SetActive(true);
        }
      }
    }

    public void Setup(ArtifactRarityCheck.Type type, GameObject arg_go, ArtifactData[] artifact_data, int rarity)
    {
      this.mType = type;
      this.mArgGo = arg_go;
      this.mArtifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) artifact_data);
      this.mRarity = rarity;
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent == null)
        return;
      this.OnDecideEvent(this.mArgGo);
    }

    private void OnCancel()
    {
      if (this.OnCancelEvent == null)
        return;
      this.OnCancelEvent(this.mArgGo);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.OnDecide();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 1:
          this.OnDecide();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          this.OnCancel();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
      }
    }

    public enum Type
    {
      NONE,
      SELL,
      DECOMPOSE,
    }

    public delegate void OnArtifactRarityCheckDecideEvent(GameObject go);

    public delegate void OnArtifactRarityCheckCancelEvent(GameObject go);
  }
}

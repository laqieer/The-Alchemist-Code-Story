// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditBirthday
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnEndEditBirthday", 58751)]
[FlowNode.Pin(0, "Valid", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(1, "Invalid", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnEndEditBirthday : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField TargetYear;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField TargetMonth;
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField TargetDay;
  [FlowNode.DropTarget(typeof (InputField), true)]
  public Button ok;

  private void Start()
  {
    if ((UnityEngine.Object) this.TargetYear != (UnityEngine.Object) null)
      this.TargetYear.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEditYear(this.TargetYear)));
    if ((UnityEngine.Object) this.TargetMonth != (UnityEngine.Object) null)
      this.TargetMonth.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEditMonth(this.TargetMonth)));
    if ((UnityEngine.Object) this.TargetDay != (UnityEngine.Object) null)
      this.TargetDay.onEndEdit.AddListener((UnityAction<string>) (_param1 => this.OnEndEditDay(this.TargetDay)));
    this.enabled = true;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if ((UnityEngine.Object) this.TargetYear != (UnityEngine.Object) null && this.TargetYear.onEndEdit != null)
      this.TargetYear.onEndEdit.RemoveAllListeners();
    if ((UnityEngine.Object) this.TargetMonth != (UnityEngine.Object) null && this.TargetMonth.onEndEdit != null)
      this.TargetMonth.onEndEdit.RemoveAllListeners();
    if (!((UnityEngine.Object) this.TargetDay != (UnityEngine.Object) null) || this.TargetDay.onEndEdit == null)
      return;
    this.TargetDay.onEndEdit.RemoveAllListeners();
  }

  private void OnEndEditYear(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    DebugUtility.Log("OnEndEditYear:" + field.text);
    this.OutputResult();
  }

  private void OnEndEditMonth(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    DebugUtility.Log("OnEndEditMonth:" + field.text);
    this.OutputResult();
  }

  private void OnEndEditDay(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.text.Length <= 0)
      return;
    DebugUtility.Log("OnEndEditDay:" + field.text);
    this.OutputResult();
  }

  public override void OnActivate(int pinID)
  {
  }

  private void OutputResult()
  {
    int result1 = 0;
    int result2 = 0;
    int result3 = 0;
    DateTime now = DateTime.Now;
    if ((UnityEngine.Object) this.TargetYear == (UnityEngine.Object) null || string.IsNullOrEmpty(this.TargetYear.text) || (!int.TryParse(this.TargetYear.text, out result1) || now.Year < result1) || result1 < 1900)
      this.ActivateOutputLinks(1);
    else if ((UnityEngine.Object) this.TargetMonth == (UnityEngine.Object) null || string.IsNullOrEmpty(this.TargetMonth.text) || (!int.TryParse(this.TargetMonth.text, out result2) || result2 < 1) || (12 < result2 || now.Year == result1 && now.Month < result2))
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      int num;
      try
      {
        num = DateTime.DaysInMonth(result1, result2);
      }
      catch
      {
        this.ActivateOutputLinks(1);
        return;
      }
      if ((UnityEngine.Object) this.TargetDay == (UnityEngine.Object) null || string.IsNullOrEmpty(this.TargetDay.text) || (!int.TryParse(this.TargetDay.text, out result3) || result3 < 1) || num < result3 || now.Year == result1 && now.Month == result2 && now.Day < result3)
      {
        this.ActivateOutputLinks(1);
      }
      else
      {
        GlobalVars.EditedYear = result1;
        GlobalVars.EditedMonth = result2;
        GlobalVars.EditedDay = result3;
        this.ActivateOutputLinks(0);
      }
    }
  }
}

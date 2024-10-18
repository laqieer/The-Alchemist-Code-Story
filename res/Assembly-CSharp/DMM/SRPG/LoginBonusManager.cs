// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "LoginBonusをCloseした", FlowNode.PinTypes.Output, 100)]
  public class LoginBonusManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OT_WINDOW_DESTROY = 100;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string LoginBonusMonthPrefab = string.Empty;
    [SerializeField]
    [StringIsResourcePath(typeof (GameObject))]
    private string TotalBonusPrefab = string.Empty;
    [SerializeField]
    private Transform PrefabRoot;

    public string TableID { get; set; }

    public bool IsLoginCall { get; set; }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      GlobalVars.MonthlyLoginBonus_SelectTableIname = string.Empty;
      GlobalVars.MonthlyLoginBonus_SelectRecoverDay = -1;
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.PrefabRoot, (Object) null))
        DebugUtility.LogError("PrefabRootが指定されていません.");
      else if (string.IsNullOrEmpty(this.LoginBonusMonthPrefab))
        DebugUtility.LogError("LoginBonusMonthPrefabが指定されていません.");
      else
        this.StartCoroutine(this.LoadPrefab());
    }

    [DebuggerHidden]
    private IEnumerator LoadPrefab()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusManager.\u003CLoadPrefab\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void OnDestroyLoginBonusWindow(GameObject obj)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}

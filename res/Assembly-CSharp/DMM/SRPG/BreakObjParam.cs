// Decompiled with JetBrains decompiler
// Type: SRPG.BreakObjParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class BreakObjParam
  {
    private string mIname;
    private string mName;
    private string mExpr;
    private string mUnitId;
    private eMapBreakClashType mClashType;
    private eMapBreakAIType mAiType;
    private eMapBreakSideType mSideType;
    private eMapBreakRayType mRayType;
    private bool mIsUI;
    private int[] mRestHps;
    private int mAliveClock;
    private EUnitDirection mAppearDir;

    public string Iname => this.mIname;

    public string Name => this.mName;

    public string Expr => this.mExpr;

    public string UnitId => this.mUnitId;

    public eMapBreakClashType ClashType => this.mClashType;

    public eMapBreakAIType AiType => this.mAiType;

    public eMapBreakSideType SideType => this.mSideType;

    public eMapBreakRayType RayType => this.mRayType;

    public bool IsUI => this.mIsUI;

    public int[] RestHps => this.mRestHps;

    public int AliveClock => this.mAliveClock;

    public EUnitDirection AppearDir => this.mAppearDir;

    public void Deserialize(JSON_BreakObjParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mUnitId = json.unit_id;
      this.mClashType = (eMapBreakClashType) json.clash_type;
      this.mAiType = (eMapBreakAIType) json.ai_type;
      this.mSideType = (eMapBreakSideType) json.side_type;
      this.mRayType = (eMapBreakRayType) json.ray_type;
      this.mIsUI = json.is_ui != 0;
      this.mRestHps = (int[]) null;
      if (!string.IsNullOrEmpty(json.rest_hps))
      {
        string[] strArray = json.rest_hps.Split(',');
        if (strArray != null && strArray.Length != 0)
        {
          this.mRestHps = new int[strArray.Length];
          for (int index = 0; index < strArray.Length; ++index)
          {
            int result = 0;
            int.TryParse(strArray[index], out result);
            this.mRestHps[index] = result;
          }
        }
      }
      this.mAliveClock = json.clock;
      this.mAppearDir = (EUnitDirection) json.appear_dir;
    }
  }
}

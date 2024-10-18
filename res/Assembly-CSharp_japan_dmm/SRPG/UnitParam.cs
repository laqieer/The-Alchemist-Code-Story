// Decompiled with JetBrains decompiler
// Type: SRPG.UnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class UnitParam
  {
    private static string[] UnitHourVoices = new string[24]
    {
      "chara_0007",
      "chara_0007",
      "chara_0007",
      "chara_0007",
      "chara_0002",
      "chara_0002",
      "chara_0002",
      "chara_0002",
      "chara_0003",
      "chara_0003",
      "chara_0003",
      "chara_0003",
      "chara_0004",
      "chara_0004",
      "chara_0004",
      "chara_0004",
      "chara_0005",
      "chara_0005",
      "chara_0005",
      "chara_0005",
      "chara_0006",
      "chara_0006",
      "chara_0006",
      "chara_0006"
    };
    public static int MASTER_QUEST_RARITY = 5;
    public static int MASTER_QUEST_LV = 1;
    public static string AI_TYPE_DEFAULT = "AI_ENEMY";
    public FlagManager flag;
    public string iname;
    public string name;
    public string model;
    public OString birth = (OString) (string) null;
    public int birthID;
    public OString grow = (OString) (string) null;
    public string[] jobsets;
    public string[] tags;
    public string piece;
    public string subPiece;
    public string ability;
    public string ma_quest;
    public ESex sex;
    public byte rare;
    public byte raremax;
    public EUnitType type;
    public EElement element;
    public byte sw = 1;
    public byte sh = 1;
    public byte sd = 1;
    public string dbuki;
    public byte search;
    public short height;
    public short weight;
    public DateTime available_at = DateTime.MinValue;
    public string image;
    public string voice;
    public UnitParam.Status ini_status = new UnitParam.Status();
    public UnitParam.Status max_status = new UnitParam.Status();
    public string[] leader_skills = new string[RarityParam.MAX_RARITY];
    public string[] recipes = new string[RarityParam.MAX_RARITY];
    public UnitParam.NoJobStatus no_job_status;
    public string[] skins;
    private JobSetParam[] mJobSetCache;
    public string unlock_time;
    public string unit_piece_shop_group;
    private List<JobSetParam> mClassChangeJobSetCache;
    private static EElement[] WeakElements = new EElement[7]
    {
      EElement.None,
      EElement.Water,
      EElement.Thunder,
      EElement.Fire,
      EElement.Wind,
      EElement.Dark,
      EElement.Shine
    };
    private static EElement[] ResistElements = new EElement[7]
    {
      EElement.None,
      EElement.Wind,
      EElement.Fire,
      EElement.Thunder,
      EElement.Water,
      EElement.None,
      EElement.None
    };

    public bool IsHero() => this.flag.Is(0);

    public bool IsSummon() => this.flag.Is(1);

    public bool IsThrow() => this.flag.Is(2);

    public bool IsKnockBack() => this.flag.Is(3);

    public bool IsChanging() => this.flag.Is(4);

    public bool IsInspiration() => this.flag.Is(5);

    public bool IsNoRecommended() => this.flag.Is(6);

    public bool IsStopped() => this.type == EUnitType.Gem || this.type == EUnitType.Treasure;

    public bool IsFlyingPass => this.flag.Is(7);

    public void CacheReferences(MasterParam master)
    {
      if (this.jobsets == null || this.mJobSetCache != null)
        return;
      this.mJobSetCache = new JobSetParam[this.jobsets.Length];
      for (int index = 0; index < this.jobsets.Length; ++index)
        this.mJobSetCache[index] = master.GetJobSetParam(this.jobsets[index]);
    }

    public JobSetParam GetJobSetFast(int index)
    {
      return this.mJobSetCache != null && 0 <= index && index < this.mJobSetCache.Length ? this.mJobSetCache[index] : (JobSetParam) null;
    }

    public string SexPrefix => this.sex.ToPrefix();

    public bool Deserialize(JSON_UnitParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.model = json.mdl;
      this.grow = (OString) json.grow;
      this.piece = json.piece;
      this.subPiece = json.sub_piece;
      this.birth = (OString) json.birth;
      this.birthID = json.birth_id;
      this.ability = json.ability;
      this.ma_quest = json.ma_quest;
      this.sex = (ESex) json.sex;
      this.rare = (byte) json.rare;
      this.raremax = (byte) json.raremax;
      this.type = (EUnitType) json.type;
      this.element = (EElement) json.elem;
      this.flag.Set(0, json.hero != 0);
      this.sw = (byte) json.sw;
      this.sh = (byte) json.sh;
      this.sd = (byte) json.sd;
      this.dbuki = json.dbuki;
      this.search = (byte) json.search;
      this.flag.Set(1, json.notsmn == 0);
      if (!string.IsNullOrEmpty(json.available_at))
      {
        try
        {
          this.available_at = DateTime.Parse(json.available_at);
        }
        catch
        {
          this.available_at = DateTime.MaxValue;
        }
      }
      this.height = (short) json.height;
      this.weight = (short) json.weight;
      this.jobsets = (string[]) null;
      this.mJobSetCache = (JobSetParam[]) null;
      this.tags = (string[]) null;
      if (json.skins != null && json.skins.Length >= 1)
      {
        this.skins = new string[json.skins.Length];
        for (int index = 0; index < json.skins.Length; ++index)
          this.skins[index] = json.skins[index];
      }
      if (UnitParam.NoJobStatus.IsExistParam(json))
      {
        this.no_job_status = new UnitParam.NoJobStatus();
        this.no_job_status.SetParam(json);
      }
      if (this.type == EUnitType.EventUnit)
        return true;
      if (json.jobsets != null)
      {
        this.jobsets = new string[json.jobsets.Length];
        for (int index = 0; index < this.jobsets.Length; ++index)
          this.jobsets[index] = json.jobsets[index];
      }
      if (json.tag != null)
        this.tags = json.tag.Split(',');
      if (this.ini_status == null)
        this.ini_status = new UnitParam.Status();
      this.ini_status.SetParamIni(json);
      this.ini_status.SetEnchentParamIni(json);
      if (this.max_status == null)
        this.max_status = new UnitParam.Status();
      this.max_status.SetParamMax(json);
      this.max_status.SetEnchentParamMax(json);
      this.leader_skills[0] = json.ls1;
      this.leader_skills[1] = json.ls2;
      this.leader_skills[2] = json.ls3;
      this.leader_skills[3] = json.ls4;
      this.leader_skills[4] = json.ls5;
      this.leader_skills[5] = json.ls6;
      this.recipes[0] = json.recipe1;
      this.recipes[1] = json.recipe2;
      this.recipes[2] = json.recipe3;
      this.recipes[3] = json.recipe4;
      this.recipes[4] = json.recipe5;
      this.recipes[5] = json.recipe6;
      this.image = json.img;
      this.voice = json.vce;
      this.flag.Set(2, json.no_trw == 0);
      this.flag.Set(3, json.no_kb == 0);
      this.flag.Set(4, json.no_chg == 0);
      this.flag.Set(5, json.no_insp == 0);
      this.flag.Set(6, json.no_recommended != 0);
      this.unlock_time = json.unlck_t;
      this.unit_piece_shop_group = json.unit_piece_shop_group;
      this.flag.Set(7, json.no_pass == 0);
      return true;
    }

    public RecipeParam GetRarityUpRecipe(int rarity)
    {
      if (this.recipes == null)
        return (RecipeParam) null;
      if (rarity < 0 || rarity >= RarityParam.MAX_RARITY || rarity >= (int) this.raremax)
        return (RecipeParam) null;
      string recipe = this.recipes[rarity];
      return string.IsNullOrEmpty(recipe) ? (RecipeParam) null : MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(recipe);
    }

    public static int GetLeaderSkillLevel(int rarity, int awakeLv) => 1;

    public static void CalcUnitLevelStatus(UnitParam unit, int unitLv, ref BaseStatus status)
    {
      GrowParam growParam = MonoSingleton<GameManager>.GetInstanceDirect().GetGrowParam((string) unit.grow);
      if (growParam != null && growParam.curve != null)
      {
        growParam.CalcLevelCurveStatus(unitLv, ref status, unit.ini_status, unit.max_status);
      }
      else
      {
        unit.ini_status.param.CopyTo(status.param);
        if (unit.ini_status.enchant_resist != null)
          unit.ini_status.enchant_resist.CopyTo(status.enchant_resist);
      }
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      status.enchant_assist.esa_fire = (short) (int) fixParam.EsaAssist;
      status.enchant_assist.esa_water = (short) (int) fixParam.EsaAssist;
      status.enchant_assist.esa_wind = (short) (int) fixParam.EsaAssist;
      status.enchant_assist.esa_thunder = (short) (int) fixParam.EsaAssist;
      status.enchant_assist.esa_shine = (short) (int) fixParam.EsaAssist;
      status.enchant_assist.esa_dark = (short) (int) fixParam.EsaAssist;
      status.enchant_resist.esa_fire = (short) (int) fixParam.EsaResist;
      status.enchant_resist.esa_water = (short) (int) fixParam.EsaResist;
      status.enchant_resist.esa_wind = (short) (int) fixParam.EsaResist;
      status.enchant_resist.esa_thunder = (short) (int) fixParam.EsaResist;
      status.enchant_resist.esa_shine = (short) (int) fixParam.EsaResist;
      status.enchant_resist.esa_dark = (short) (int) fixParam.EsaResist;
      status.param.rec = (OShort) fixParam.IniValRec;
    }

    public static void CalcUnitElementStatus(EElement element, ref BaseStatus status)
    {
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      int elementResistUpRate = (int) fixParam.ElementResistUpRate;
      int elementResistDownRate = (int) fixParam.ElementResistDownRate;
      EElement weakElement = UnitParam.GetWeakElement(element);
      EElement resistElement = UnitParam.GetResistElement(element);
      status.element_resist[weakElement] += (short) elementResistDownRate;
      status.element_resist[resistElement] += (short) elementResistUpRate;
    }

    public static EElement GetWeakElement(EElement type) => UnitParam.WeakElements[(int) type];

    public static EElement GetResistElement(EElement type) => UnitParam.ResistElements[(int) type];

    public static string GetBirthplaceName(int birth_id)
    {
      return birth_id == 0 ? string.Empty : LocalizedText.Get(string.Format("sys.BIRTH_PLACE_{0}", (object) birth_id));
    }

    public int GetUnlockNeedPieces()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam((int) this.rare);
      return rarityParam != null ? (int) rarityParam.UnitUnlockPieceNum : 0;
    }

    public bool CheckEnableUnlock()
    {
      if (string.IsNullOrEmpty(this.piece) || !this.IsSummon() || MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this) != null)
        return false;
      int unlockNeedPieces = this.GetUnlockNeedPieces();
      return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.piece) >= unlockNeedPieces && this.CheckAvailable(TimeManager.ServerTime) && new UnitListWindow.Data(this).unlockable;
    }

    public bool CheckAvailable(DateTime now) => !(now < this.available_at);

    public string GetJobVoice(string jobName) => this.GetJobOptions(jobName, false);

    public string GetJobImage(string jobName) => this.GetJobOptions(jobName, true);

    private string GetJobOptions(string jobName, bool isImage) => string.Empty;

    public string GetJobId(int jobIndex)
    {
      JobSetParam jobSetFast = this.GetJobSetFast(jobIndex);
      return jobSetFast != null ? jobSetFast.job : string.Empty;
    }

    public bool IsNormalSize => this.sw == (byte) 1 && this.sh == (byte) 1;

    public void SetClassChangeJobsetCache(List<JobSetParam> cache)
    {
      this.mClassChangeJobSetCache = cache;
    }

    public bool HasJobParam(string jobParamIname)
    {
      for (int index = 0; index < this.jobsets.Length; ++index)
      {
        JobSetParam jobSetFast = this.GetJobSetFast(index);
        if (jobSetFast != null && jobSetFast.job == jobParamIname)
          return true;
      }
      return this.mClassChangeJobSetCache != null && this.mClassChangeJobSetCache.Find((Predicate<JobSetParam>) (js => js.job == jobParamIname)) != null;
    }

    public bool HasArtifactByTag(string artifactTagName)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index = 0; index < this.jobsets.Length; ++index)
      {
        JobSetParam jobSetFast = this.GetJobSetFast(index);
        if (jobSetFast != null)
        {
          JobParam jobParam = masterParam.GetJobParam(jobSetFast.job);
          if (jobParam != null && jobParam.DefaultArtifact != null && jobParam.DefaultArtifact.tag == artifactTagName)
            return true;
        }
      }
      if (this.mClassChangeJobSetCache != null)
      {
        for (int index = 0; index < this.mClassChangeJobSetCache.Count; ++index)
        {
          JobSetParam jobSetParam = this.mClassChangeJobSetCache[index];
          if (jobSetParam != null)
          {
            JobParam jobParam = masterParam.GetJobParam(jobSetParam.job);
            if (jobParam != null && jobParam.DefaultArtifact != null && jobParam.DefaultArtifact.tag == artifactTagName)
              return true;
          }
        }
      }
      return false;
    }

    public static string GetCurrentHourVoice()
    {
      if (UnitParam.UnitHourVoices == null || UnitParam.UnitHourVoices.Length < 0)
      {
        DebugUtility.LogError("時報ボイスのcueIDの設定がありません.");
        return string.Empty;
      }
      DateTime now = DateTime.Now;
      if (now.Hour < UnitParam.UnitHourVoices.Length)
        return UnitParam.UnitHourVoices[now.Hour];
      DebugUtility.LogError("時報ボイス[" + now.Hour.ToString() + "]が設定されていません.");
      return string.Empty;
    }

    private enum FLAG_TYPE
    {
      HERO,
      SUMMON,
      THROW,
      KNOCK_BACK,
      CHANGING,
      INSP,
      NORECOMMENDED,
      FLYING_PASS,
    }

    [MessagePackObject(true)]
    public class Status
    {
      public StatusParam param = new StatusParam();
      public EnchantParam enchant_resist;

      public void SetParamIni(JSON_UnitParam json)
      {
        this.param.hp = (OInt) json.hp;
        this.param.mp = (OShort) json.mp;
        this.param.atk = (OShort) json.atk;
        this.param.def = (OShort) json.def;
        this.param.mag = (OShort) json.mag;
        this.param.mnd = (OShort) json.mnd;
        this.param.dex = (OShort) json.dex;
        this.param.spd = (OShort) json.spd;
        this.param.cri = (OShort) json.cri;
        this.param.luk = (OShort) json.luk;
      }

      public void SetParamMax(JSON_UnitParam json)
      {
        this.param.hp = (OInt) json.mhp;
        this.param.mp = (OShort) json.mmp;
        this.param.atk = (OShort) json.matk;
        this.param.def = (OShort) json.mdef;
        this.param.mag = (OShort) json.mmag;
        this.param.mnd = (OShort) json.mmnd;
        this.param.dex = (OShort) json.mdex;
        this.param.spd = (OShort) json.mspd;
        this.param.cri = (OShort) json.mcri;
        this.param.luk = (OShort) json.mluk;
      }

      public void SetEnchentParamIni(JSON_UnitParam json)
      {
        if (!this.IsExistEnchentParamIni(json))
          return;
        this.enchant_resist = new EnchantParam();
        this.enchant_resist.poison = (short) json.rpo;
        this.enchant_resist.paralyse = (short) json.rpa;
        this.enchant_resist.stun = (short) json.rst;
        this.enchant_resist.sleep = (short) json.rsl;
        this.enchant_resist.charm = (short) json.rch;
        this.enchant_resist.stone = (short) json.rsn;
        this.enchant_resist.blind = (short) json.rbl;
        this.enchant_resist.notskl = (short) json.rns;
        this.enchant_resist.notmov = (short) json.rnm;
        this.enchant_resist.notatk = (short) json.rna;
        this.enchant_resist.zombie = (short) json.rzo;
        this.enchant_resist.death = (short) json.rde;
        this.enchant_resist.knockback = (short) json.rkn;
        this.enchant_resist.resist_debuff = (short) json.rdf;
        this.enchant_resist.berserk = (short) json.rbe;
        this.enchant_resist.stop = (short) json.rcs;
        this.enchant_resist.fast = (short) json.rcu;
        this.enchant_resist.slow = (short) json.rcd;
        this.enchant_resist.donsoku = (short) json.rdo;
        this.enchant_resist.rage = (short) json.rra;
        this.enchant_resist.single_attack = (short) json.rsa;
        this.enchant_resist.area_attack = (short) json.raa;
        this.enchant_resist.dec_ct = (short) json.rdc;
        this.enchant_resist.inc_ct = (short) json.ric;
        this.enchant_resist.side_attack = (short) json.ras;
        this.enchant_resist.back_attack = (short) json.rab;
        this.enchant_resist.obst_reaction = (short) json.ror;
        this.enchant_resist.forced_targeting = (short) json.rft;
      }

      private bool IsExistEnchentParamIni(JSON_UnitParam json)
      {
        return json.rpo != 0 || json.rpa != 0 || json.rst != 0 || json.rsl != 0 || json.rch != 0 || json.rsn != 0 || json.rbl != 0 || json.rns != 0 || json.rnm != 0 || json.rna != 0 || json.rzo != 0 || json.rde != 0 || json.rkn != 0 || json.rdf != 0 || json.rbe != 0 || json.rcs != 0 || json.rcu != 0 || json.rcd != 0 || json.rdo != 0 || json.rra != 0 || json.rsa != 0 || json.raa != 0 || json.rdc != 0 || json.ric != 0 || json.ras != 0 || json.rab != 0 || json.ror != 0 || json.rft != 0;
      }

      public void SetEnchentParamMax(JSON_UnitParam json)
      {
        if (!this.IsExistEnchentParamMax(json))
          return;
        this.enchant_resist = new EnchantParam();
        this.enchant_resist.poison = (short) json.mrpo;
        this.enchant_resist.paralyse = (short) json.mrpa;
        this.enchant_resist.stun = (short) json.mrst;
        this.enchant_resist.sleep = (short) json.mrsl;
        this.enchant_resist.charm = (short) json.mrch;
        this.enchant_resist.stone = (short) json.mrsn;
        this.enchant_resist.blind = (short) json.mrbl;
        this.enchant_resist.notskl = (short) json.mrns;
        this.enchant_resist.notmov = (short) json.mrnm;
        this.enchant_resist.notatk = (short) json.mrna;
        this.enchant_resist.zombie = (short) json.mrzo;
        this.enchant_resist.death = (short) json.mrde;
        this.enchant_resist.knockback = (short) json.mrkn;
        this.enchant_resist.resist_debuff = (short) json.mrdf;
        this.enchant_resist.berserk = (short) json.mrbe;
        this.enchant_resist.stop = (short) json.mrcs;
        this.enchant_resist.fast = (short) json.mrcu;
        this.enchant_resist.slow = (short) json.mrcd;
        this.enchant_resist.donsoku = (short) json.mrdo;
        this.enchant_resist.rage = (short) json.mrra;
        this.enchant_resist.single_attack = (short) json.mrsa;
        this.enchant_resist.area_attack = (short) json.mraa;
        this.enchant_resist.dec_ct = (short) json.mrdc;
        this.enchant_resist.inc_ct = (short) json.mric;
        this.enchant_resist.side_attack = (short) json.mras;
        this.enchant_resist.back_attack = (short) json.mrab;
        this.enchant_resist.obst_reaction = (short) json.mror;
        this.enchant_resist.forced_targeting = (short) json.mrft;
      }

      private bool IsExistEnchentParamMax(JSON_UnitParam json)
      {
        return json.mrpo != 0 || json.mrpa != 0 || json.mrst != 0 || json.mrsl != 0 || json.mrch != 0 || json.mrsn != 0 || json.mrbl != 0 || json.mrns != 0 || json.mrnm != 0 || json.mrna != 0 || json.mrzo != 0 || json.mrde != 0 || json.mrkn != 0 || json.mrdf != 0 || json.mrbe != 0 || json.mrcs != 0 || json.mrcu != 0 || json.mrcd != 0 || json.mrdo != 0 || json.mrra != 0 || json.mrsa != 0 || json.mraa != 0 || json.mrdc != 0 || json.mric != 0 || json.mras != 0 || json.mrab != 0 || json.mror != 0 || json.mrft != 0;
      }

      public BaseStatus CreateBaseStatus()
      {
        BaseStatus baseStatus = new BaseStatus();
        this.param.CopyTo(baseStatus.param);
        if (this.enchant_resist != null)
          this.enchant_resist.CopyTo(baseStatus.enchant_resist);
        return baseStatus;
      }
    }

    [MessagePackObject(true)]
    public class NoJobStatus
    {
      public string default_skill = string.Empty;
      public JobTypes jobtype;
      public RoleTypes role;
      public byte mov;
      public byte jmp;
      public int inimp;

      public void SetParam(JSON_UnitParam json)
      {
        this.default_skill = json.dskl;
        this.jobtype = (JobTypes) json.jt;
        this.role = (RoleTypes) json.role;
        this.mov = (byte) json.mov;
        this.jmp = (byte) json.jmp;
        this.inimp = json.inimp;
      }

      public static bool IsExistParam(JSON_UnitParam json)
      {
        return json.dskl != null || json.jt != 0 || json.role != 0 || json.mov != 0 || json.jmp != 0 || json.inimp != 0;
      }
    }
  }
}

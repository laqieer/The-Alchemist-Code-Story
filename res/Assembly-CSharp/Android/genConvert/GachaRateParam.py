def GachaRateParam(json):
    this={}#GachaRateParamjson)
    #if(json==null)
        #DebugUtility.LogError("***FlowNode_ReqGachaRate:Deserialize'sjsonisnull")
        #returnfalse
    if 'rate' in json:
        this['m_rate'] = json['rate']
    #if(json.itype=="item")
        #this.m_type=GachaDropData.Type.Item
        #ItemParamitemParam=MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(json.iname)
        #if(itemParam==null)
            #DebugUtility.LogError("GachaInfoRate=>iname:"+json.iname+"はItemParamに存在しません.")
            #returnfalse
        #this.m_name=itemParam.name
        #this.m_rarity=itemParam.rare
        if 'num' in json:
            this['m_num'] = json['num']
        #this.m_hash=itemParam.iname.GetHashCode()
    #elseif(json.itype=="unit")
        #this.m_type=GachaDropData.Type.Unit
        #UnitParamunitParam=MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.iname)
        #if(unitParam==null)
            #DebugUtility.LogError("GachaInfoRate=>iname:"+json.iname+"はUnitParamに存在しません.")
            #returnfalse
        #this.m_name=unitParam.name
        #this.m_rarity=(int)unitParam.rare
        #this.m_elem=unitParam.element
    #elseif(json.itype=="artifact")
        #this.m_type=GachaDropData.Type.Artifact
        #ArtifactParamartifactParam=MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname)
        #if(artifactParam==null)
            #DebugUtility.LogError("GachaInfoRate=>iname:"+json.iname+"はArtifactParamに存在しません.")
            #returnfalse
        #this.m_name=artifactParam.name
        if 'rare' in json:
            this['m_rarity'] = json['rare']==-1?artifactParam['rare']ini:json['rare']
    #elseif(json.itype=="concept_card")
        #this.m_type=GachaDropData.Type.ConceptCard
        #ConceptCardParamconceptCardParam=MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname)
        #if(conceptCardParam==null)
            #DebugUtility.LogError("GachaInfoRate=>iname:"+json.iname+"はArtifactParamに存在しません.")
            #returnfalse
        #this.m_name=conceptCardParam.name
        #this.m_rarity=conceptCardParam.rare
    #returntrue
return this

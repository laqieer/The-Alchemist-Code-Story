def DynamicTransformUnitParam(json):
    this={}#DynamicTransformUnitParamjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'tr_unit_id' in json:
        this['mTrUnitId'] = json['tr_unit_id']
    if 'turn' in json:
        this['mTurn'] = json['turn']
    if 'upper_to_abid' in json:
        this['mUpperToAbId'] = json['upper_to_abid']
    if 'lower_to_abid' in json:
        this['mLowerToAbId'] = json['lower_to_abid']
    if 'react_to_abid' in json:
        this['mReactToAbId'] = json['react_to_abid']
    if 'ct_eff' in json:
        this['mCancelEffect'] = json['ct_eff']
    if 'ct_dis_ms' in json:
        this['mCancelDisMs'] = json['ct_dis_ms']
    if 'ct_app_ms' in json:
        this['mCancelAppMs'] = json['ct_app_ms']
    #this.mFlags=(DynamicTransformUnitParam.Flags)0
    #if(json.is_no_wa!=0)
    #this.mFlags|=DynamicTransformUnitParam.Flags.IsNoWeaponAbility
    #if(json.is_no_va!=0)
    #this.mFlags|=DynamicTransformUnitParam.Flags.IsNoVisionAbility
    #if(json.is_no_item!=0)
    #this.mFlags|=DynamicTransformUnitParam.Flags.IsNoItems
    #if(json.is_tr_hpf!=0)
    #this.mFlags|=DynamicTransformUnitParam.Flags.IsTransHpFull
    #if(json.is_cc_hpf==0)
    #return
    #this.mFlags|=DynamicTransformUnitParam.Flags.IsCancelHpFull
return this

def ItemParam(json):
    this={}#ItemParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'type' in json:
        this['type'] = ENUM['EItemType'][json['type']]
    if 'tabtype' in json:
        this['tabtype'] = ENUM['EItemTabType'][json['tabtype']]
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'cap' in json:
        this['cap'] = json['cap']
    if 'invcap' in json:
        this['invcap'] = json['invcap']
    if 'eqlv' in json:
        this['equipLv'] = Math.Max
    if 'coin' in json:
        this['coin'] = json['coin']
    if 'tc' in json:
        this['tour_coin'] = json['tc']
    if 'ac' in json:
        this['arena_coin'] = json['ac']
    if 'mc' in json:
        this['multi_coin'] = json['mc']
    if 'pp' in json:
        this['piece_point'] = json['pp']
    if 'buy' in json:
        this['buy'] = json['buy']
    if 'sell' in json:
        this['sell'] = json['sell']
    if 'encost' in json:
        this['enhace_cost'] = json['encost']
    if 'enpt' in json:
        this['enhace_point'] = json['enpt']
    if 'facilitypt' in json:
        this['facility_point'] = json['facilitypt']
    if 'val' in json:
        this['value'] = json['val']
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'skill' in json:
        this['skill'] = json['skill']
    if 'recipe' in json:
        this['recipe'] = json['recipe']
    #this.quests=(string)null
    if 'is_valuables' in json:
        this['is_valuables'] = json['is_valuables']>0
    if 'cmn_type' in json:
        this['cmn_type'] = json['cmn_type']
    #if(json.quests!=null)
        #for(intindex=0index<json.quests.Length++index)
        if 'quests' in json:
            this['quests'] = json['quests']
    #returntrue
return this

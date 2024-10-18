def GuildEmblemParam(json):
    this={}#GuildEmblemParamjson)
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    if 'cnds_type' in json:
        this['mConditionsType'] = json['cnds_type']
    if 'cnds_val' in json:
        this['mConditionsValue'] = json['cnds_val']
    if 'image' in json:
        this['mImage'] = json['image']
    #this.mStartAt=DateTime.MinValue
    #if(!string.IsNullOrEmpty(json.start_at))
    #DateTime.TryParse(json.start_at,outthis.mStartAt)
    #this.mEndAt=DateTime.MinValue
    #if(string.IsNullOrEmpty(json.end_at))
    #return
    #DateTime.TryParse(json.end_at,outthis.mEndAt)
return this

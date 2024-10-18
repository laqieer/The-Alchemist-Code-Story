def ArchiveParam(json):
    this={}#ArchiveParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'area_iname' in json:
        this['area_iname'] = json['area_iname']
    if 'area_iname_multi' in json:
        this['area_iname_multi'] = json['area_iname_multi']
    if 'type' in json:
        this['type'] = ENUM['ArchiveTypes'][json['type']]
    #this.begin_at=DateTime.MinValue
    #if(!string.IsNullOrEmpty(json.begin_at))
    #DateTime.TryParse(json.begin_at,outthis.begin_at)
    #this.end_at=DateTime.MaxValue
    #if(!string.IsNullOrEmpty(json.end_at))
    #DateTime.TryParse(json.end_at,outthis.end_at)
    #this.keys=newList<KeyItem>()
    #if(!string.IsNullOrEmpty(json.keyitem1)&&json.keynum1>0)
    #this.keys.Add(newKeyItem()
        #iname=json.keyitem1,
        #num=json.keynum1
        #})
        if 'keytime' in json:
            this['keytime'] = json['keytime']
        if 'unit1' in json:
            this['unit1'] = json['unit1']
        if 'unit2' in json:
            this['unit2'] = json['unit2']
        #if(json.items==null)
        #return
        if 'items' in json:
            this['items'] = newArchiveItemsParam[json['items'].Length]
        #intindex=0
        #foreach(JSON_ArchiveItemsParamarchiveItemsParaminjson.items)
            #this.items=newArchiveItemsParam()
            #this.items.type=(ArchiveItemTypes)archiveItemsParam.type
            #this.items[index++].id=archiveItemsParam.id
    #
    #publicboolIsAvailable()
        #DateTimeserverTime=TimeManager.ServerTime
        #if(this.begin_at<=serverTime)
        #returnserverTime<this.end_at
        #returnfalse
return this

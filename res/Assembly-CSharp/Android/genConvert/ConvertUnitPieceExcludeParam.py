def ConvertUnitPieceExcludeParam(json):
    this={}#ConvertUnitPieceExcludeParamjson)
    #if(json==null)
    #return
    if 'id' in json:
        this['id'] = json['id']
    if 'unit_piece_iname' in json:
        this['unit_piece_iname'] = json['unit_piece_iname']
return this

def ViewGuild(json):
    this={}#ViewGuildjson)
    if 'id' in json:
        this['id'] = json['id']
    if 'name' in json:
        this['name'] = json['name']
    if 'award_id' in json:
        this['award_id'] = json['award_id']
    if 'level' in json:
        this['level'] = json['level']
    if 'count' in json:
        this['count'] = json['count']
    if 'max_count' in json:
        this['max_count'] = json['max_count']
return this

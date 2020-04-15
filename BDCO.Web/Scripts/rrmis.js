
var gs = [
        { "text": "Sponsored Child", "value": 1 },
        { "text": "Severe Acute Malnutrition", "value": 2 },
        { "text": "Moderate Acute Malnutrition", "value": 3 },
        { "text": "Severe Under Weight", "value": 4 },
        { "text": "Modarate Under Weight", "value": 5 },
        { "text": "Severe Stunted", "value": 6 },
        { "text": "Modarate Stunted ", "value": 7 },
        { "text": "Reffered For Malnutrition", "value": 8 },
        { "text": "Drop Out", "value": 9 },
        { "text": "Children Returned To Health", "value": 10 },
        { "text": "New Child Last Month", "value": 11 },
        { "text": "Children Need Attention", "value": 12 }
];
var as = [{ text: "1", value: "1" }, { text: "2", value: "2" }, { text: "3", value: "3" }, { text: "4", value: "4" }];
//var lt = [{ text: "City Corporation", value: "1" }, { text: "Municipality", value: "2" }, { text: "Upazila", value: "3" }];
var lt = [{ text: "City Corporation", textBangla: "সিটি কর্পোরেশন", value: "3" }, { text: "Municipality", textBangla: "পৌরসভা", value: "2" }, { text: "Upazila", textBangla: "উপজেলা", value: "1" }];

var benTypeArr = [    
    { "text": "Child 6-23 Month", "value": "1" },
    { "text": "Child 24-59 Month", "value": "2" },
    { "text": "Pregnant Woman", "value": "3" },
    { "text": "Lactating Woman", "value": "4" }
    //{ "text": "Under 2", "value": "10" },
    
];
var benStatusArr = [
    { "text": "BSFP", "value": "1" },
    { "text": "TSFP", "value": "2" }
];
var inCareArr = [
    { "text": "BSFP", "value": "1" },
    { "text": "TSFP", "value": "2" },
    { "text": "Not in care (Refer to OTP)", "value": "3" }
];

var consistencyArr = [
                   //{ "text": "-----SELECT-----", "value": "" },
                   { "text": "Regular", "value": "Regular" },
                   { "text": "Irregular", "value": "Irregular" },
                   { "text": "Absent", "value": "Absent" }
];
var practiceArr = [
                   //{ "text": "-----SELECT-----", "value": "" },
                   { "text": "Practicing Message", "value": 1 },
                   { "text": "Not Practicing Messages", "value": 2 },
                   { "text": "Practicing Content", "value": 3 },
                    { "text": "Not Practicing Content", "value": 4 }
];
var practiceRegularityArr = [
                   //{ "text": "-----SELECT-----", "value": "" },
                   { "text": "Practice Regularly", "value": 1 },
                   { "text": "Practice Irregularly", "value": 2 },
                    { "text": "Didn't Practice", "value": 3 }
];

function setSJMISFilters(filter, extra,extraAction) {

    filter.addDate(['FromDate', 'ToDate'], 'fc-y'); filter.addEmpty();
    filter.addText(['BenId'], 'fc-g');
    //ben id + name
    if (extra != null) filter.addDropDownJson(extra, 'fc-g');
    if (extraAction != null && extraAction != 'undefined') filter.addDropDownAction(extraAction, 'fc-g'); filter.addEmpty();
    filter.addDropDownAction(['Users'], 'fc-g'); filter.addEmpty();
    //block//

    filter.addDropDownAction(['Block'], 'fc-g');
    filter.addDropDownAction(['DistrictCode'], 'fc-p');
    //filter.addDropDownJson([['LocationType', lt]], 'fc-p');
    filter.addDropDownCascade('UpazilaCode', 'DistrictCode', '/Filter/GetUpazilaCode', [['DistrictCode', 'DistrictCode']], 'fc-p');
    filter.addDropDownCascade('UnionCode', 'UpazilaCode', '/Filter/GetUnionCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode']], 'fc-p');
    filter.addDropDownCascade('VillageCode', 'UnionCode', '/Filter/GetVillageCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode'], ['UnionCode', 'UnionCode']], 'fc-p');
}


function setFromToDateFilters(filter) {
    filter.addDate(['FromDate', 'ToDate'], 'fc-y'); filter.addEmpty();    
}

function setGeoFilters(filter) {
    filter.addDropDownAction(['DistrictCode'], 'fc-p');
    //filter.addDropDownJson([['LocationType', lt]], 'fc-p');
    filter.addDropDownCascade('UpazilaCode', 'DistrictCode', '/Filter/GetUpazilaCode', [['DistrictCode', 'DistrictCode']],  'fc-p');
    filter.addDropDownCascade('UnionCode', 'UpazilaCode', '/Filter/GetUnionCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode']], 'fc-p');
    filter.addDropDownCascade('VillageCode', 'UnionCode', '/Filter/GetVillageCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode'], ['UnionCode', 'UnionCode']], 'fc-p');
}
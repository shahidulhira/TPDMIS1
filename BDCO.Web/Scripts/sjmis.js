

var mcCap = {
    "MCheckListBE1": "অভিভাবক সচেতনতামূলক", "MCheckListBE2": "পড়া-গণিত অধিবেশন", "MCheckListBE3": "কার্যকর উঠান বৈঠক", "MCheckListBE4": "বিদ্যালয় পরিদর্শন",
    "MCheckListECCD1": "অভিভাবক সভা পর্যবেক্ষণ", "MCheckListECCD2": "অভিভাবকের বাড়ি পর্যবেক্ষণ", "MCheckListECCD3": "প্রাক-প্রাথমিক কার্যক্রম পর্যবেক্ষণ", "MCheckListECCD4": "প্রাক-সাক্ষরতা ও গণিত প্যারেন্টিং",
    "MCheckListSHN1": "নিরাপদ পানি পান ও বর্জ্য ব্যবস্থাপনা", "MCheckListSHN2": "টয়লেট ও হাত ধোয়া",
    "MCheckListMNCHN1": "বাড়ি পরিদর্শনের কাউন্সেলিং", "MCheckListMNCHN1": "বাড়ি পরিদর্শনে কাউন্সেলিং ফলোআপ",
    "MCheckListAD": "স্বাস্থ্য বিষয়ক সেশন", "MCheckListAD2": "বাড়ি ভিত্তিক কাউনন্সেলিং",
    "MCheckListCBHE": "কমিউনিটি ভিত্তিক স্বাস্থ্য শিক্ষা", "MCheckListCP1": "ঝুঁকিপূর্ণ শিশু সনাক্তকরণ", "MCheckListCP2": "শিশু সুরক্ষা অধিবেশন", "MCheckListCP3": "কমিউনিটি কাউন্সেলিং সেশন",
    "MCheckListRC1": "আরসি পর্যবেক্ষণ"
};

var mc = [
       { text: "মৌলিক শিক্ষা কর্মসূচি-অভিভাবক সচেতনতামূলক হোম ভিজিট চেকলিস্ট", value: "MCheckListBE1" },
       { text: "পড়া-গণিত অধিবেশন পর্যবেক্ষণ চেকলিস্ট", value: "MCheckListBE2" },
    { text: "মৌলিক শিক্ষা কর্মসূচি - কার্যকর উঠান বৈঠক এর চেকলিস্ট", value: "MCheckListBE3" },
    { text: "বিদ্যালয় পরিদর্শন চেকলিস্ট", value: "MCheckListBE4" },
       { text: "প্রারম্ভিক উদ্দীপনা (০-৩) বিষয়ক অভিভাবক সভা পর্যবেক্ষণ চেকলিষ্ট", value: "MCheckListECCD1" },
       { text: "প্রারম্ভিক উদ্দীপনা (০-৩) বিষয়ক অভিভাবকের বাড়ি পর্যবেক্ষণ চেকলিষ্ট", value: "MCheckListECCD2" },
       { text: "প্রাক-প্রাথমিক (৩-৪) কার্যক্রম পর্যবেক্ষণ চেকলিস্ট", value: "MCheckListECCD3" },
       { text: "প্রাক-সাক্ষরতা ও গণিত প্যারেন্টিং চেকলিস্ট", value: "MCheckListECCD4" },
       { text: "কমিউনিটি ভিত্তিক নিরাপদ পানি পান ও বর্জ্য ব্যবস্থাপনা কার্যক্রম পর্যবেক্ষণ চেকলিস্ট", value: "MCheckListSHN1" },
       { text: "কমিউনিটি ভিত্তিক টয়লেট ও হাত ধোয়া কার্যক্রম পর্যবেক্ষণ চেকলিস্ট", value: "MCheckListSHN2" },
       { text: "র্গভবতী মা এর  (স্বামী,শ্বশুর–শ্বাশুড়সিহ ) বাড়ি পরিদর্শনের কাউন্সেলিং পর্যবেক্ষণ চেকলিষ্ট", value: "MCheckListMNCHN1" },
       { text: "গর্ভবতী মায়েদের বাড়ি পরিদর্শনে কাউন্সেলিং ফলোআপ এর পর্যবেক্ষণ চেকলিষ্ট", value: "MCheckListMNCHN2" },
       { text: "জীবন দক্ষতা এবং যৌন ও প্রজনন স্বাস্থ্য সেশন-এর চেকলিস্ট", value: "MCheckListAD" },
       { text: "বাড়ি ভিত্তিক কিশোর- কিশোরী ও অবিভাবকদের কাউনন্সেলিং পর্যবেক্ষণ সেশন-এর চেকলিস্ট", value: "MCheckListAD2" },
       { text: "কমিউনিটি - ভিত্তিক স্বাস্থ্য শিক্ষা সেশন-এর চেকলিস্ট", value: "MCheckListCBHE" },
       { text: "শিশু সুরক্ষা কর্মসূচি - ঝুঁকিপূর্ণ শিশু সনাক্তকরণ চেকলিস্ট", value: "MCheckListCP1" },
       { text: "শিশু সুরক্ষা কর্মসূচি - শিশু সুরক্ষা অধিবেশন ভিজিট চেকলিস্ট", value: "MCheckListCP2" },
       { text: "কমিউনিটি পর্যায়ে শিশু সুরক্ষা বিষয়ক কাউন্সেলিং সেশন-এর পর্যবেক্ষণ চেকলিস্ট", value: "MCheckListCP3" },
       { text: "রিসোর্সসেন্টার পর্যবেক্ষণ-এর চেকলিস্ট", value: "MCheckListRC1" }
];

var monthArr = [
       { text: "January", value: "1" },
       { text: "February", value: "2" },
       { text: "March ", value: "3" },
       { text: "April", value: "4" },
       { text: "May", value: "5" },
       { text: "June", value: "6" },
       { text: "July", value: "7" },
       { text: "August", value: "8" },
       { text: "September", value: "9" },
       { text: "October", value: "10" },
       { text: "November", value: "11" },
       { text: "December", value: "12" }];

var yearArr = [{ text: "2018", value: "2018" },{ text: "2019", value: "2019" }, { text: "2020", value: "2020" }, { text: "2021 ", value: "2021" }, { text: "2022", value: "2022" }, { text: "2023", value: "2023" }, { text: "2024", value: "2024" }, { text: "2025", value: "2025" }];

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
var VerificationStatusArr = [
    { text: "Verified", value: "1" },
    { text: "Rejected", value: "2" },
    { text: "Not Verified", value: "0" }

];
function setSJMISFilters(filter, extra,extraAction) {

    filter.addDate(['FromDate', 'ToDate'], 'fc-y'); filter.addEmpty();
    filter.addText(['BenId'], 'fc-g');
    if (extra != null) filter.addDropDownJson(extra, 'fc-g');
    if (extraAction != null && extraAction != 'undefined') filter.addDropDownAction(extraAction, 'fc-g'); filter.addEmpty();
    filter.addDropDownAction(['Users', 'RcNSchoolCode'], 'fc-g'); filter.addEmpty();


    filter.addDropDownAction(['DistrictCode'], 'fc-p');
    filter.addDropDownJson([['LocationType', lt]], 'fc-p');
    filter.addDropDownCascade('UpazilaCode', 'LocationType', '/Filter/GetUpazilaCode', [['DistrictCode', 'DistrictCode'], ['value', 'LocationType']], 'UpazilaName', 'fc-p');
    filter.addDropDownCascade('UnionCode', 'Upazila', '/Filter/GetUnionCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode']], 'UnionName', 'fc-p');
    filter.addDropDownCascade('VillageCode', 'Union', '/Filter/GetVillageCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode'], ['UnionCode', 'UnionCode']], 'VillageName', 'fc-p');
}


function setSJMISFromToDateFilters(filter) {
    filter.addDate(['FromDate', 'ToDate'], 'fc-y'); filter.addEmpty();    
}


function setSJMISGeoFilters(filter) {
    filter.addDropDownAction(['DistrictCode'], 'fc-p');
    filter.addDropDownJson([['LocationType', lt]], 'fc-p');
    filter.addDropDownCascade('UpazilaCode', 'LocationType', '/Filter/GetUpazilaCode', [['DistrictCode', 'DistrictCode'], ['value', 'LocationType']], 'UpazilaName', 'fc-p');
    filter.addDropDownCascade('UnionCode', 'Upazila', '/Filter/GetUnionCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode']], 'UnionName', 'fc-p');
    filter.addDropDownCascade('VillageCode', 'Union', '/Filter/GetVillageCode', [['DistrictCode', 'DistrictCode'], ['UpazilaCode', 'UpazilaCode'], ['UnionCode', 'UnionCode']], 'VillageName', 'fc-p');
}
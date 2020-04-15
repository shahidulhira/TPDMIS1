
var GraphManager = {
    loadChartInfo: function (ctrlName, chartType, title, data, lstCategory) {

        $("#" + ctrlName).kendoChart({
            theme: "flat",
            title: {
                text: title
            },
            legend: {
                position: "top"
            },
            seriesDefaults: {
                type: chartType
            },
            series: data,
            valueAxis: {
                labels: {
                    format: "{0}%"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: lstCategory,
                line: {
                    visible: false
                },
                labels: {
                    padding: { top: 135 }
                }
            },
            tooltip: {
                visible: true,
                format: "{0}%",
                template: "#= series.name #: #= value #"
            }
        });
    },
    createChartServiceTrend: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1ArrHandWash = [];
        var series2ArrDrinkWater = [];
        var series3ArrToilet = [];
        var series4ArrWasteManagement = [];
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                if (odata[i].MonthName != 'Grand Total') {
                    legendArr.push(odata[i].MonthName);

                    series1ArrHandWash.push(odata[i].ObservedHandWash);
                    series2ArrDrinkWater.push(odata[i].ObservedDrink);
                    series3ArrToilet.push(odata[i].ObservedToilet);
                    series4ArrWasteManagement.push(odata[i].ObservedWastBin);


                }
            }
        }

        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "line"
            },
            series:
                [{
                    //type: "scatter",
                    name: "Hand Wash",
                    data: series1ArrHandWash,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                }, {
                    name: "Safe Drinking Water",
                    data: series2ArrDrinkWater,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                }, {
                    name: "Toilet Condition",
                    data: series3ArrToilet,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                },
                {
                    name: "Waste Management",
                    data: series4ArrWasteManagement,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });

    },


    createChartDeworming: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrTotalChild = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.DewormingObserved);
            seriesArrTotalChild.push(odata.TotalChild);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Children Dewormed",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                }, {
                    name: "Total Children",
                    data: seriesArrTotalChild,
                    labels: {
                        visible: true,
                        template: "#if (value > 0) {# #: value # #}#",
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });
    }

    ,
    createChartCBHE: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrAttained = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.SessionObserved);
            seriesArrAttained.push(odata.SessionAttained);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Total Member",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "Present",
                    data: seriesArrAttained,
                    labels: {
                        visible: true
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });
    }
    ,
    createChartHandWash: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrAttained = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.HandWashObserved);
            seriesArrAttained.push(odata.HandWashAttained);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Ovserved",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "Attained",
                    data: seriesArrAttained,
                    labels: {
                        visible: true
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });

    }
    ,
    createChartDrinkingWater: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrAttained = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.DrinkingWaterObserved);
            seriesArrAttained.push(odata.DrinkingBoiledWater);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Ovserved",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "Drink Boiled Water",
                    data: seriesArrAttained,
                    labels: {
                        visible: true
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });

    }
    ,
    createChartToilet: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrAttained = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.ToiletObserved);
            seriesArrAttained.push(odata.Toilet4CriteriaMeet);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Ovserved",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "MET 4 CRITERIA",
                    data: seriesArrAttained,
                    labels: {
                        visible: true
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });

    }
    ,
    createChartWasteManagement: function (CtrlName, title, odata) {

        var legendArr = [];
        var seriesArrOvserved = [];
        var seriesArrAttained = [];
        if (odata != null) {
            seriesArrOvserved.push(odata.WasteMangementObserved);
            seriesArrAttained.push(odata.WasteMangement3CriteriaMeet);
        }
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: title,
                font: "bold 14px  Tw Cen MT Condensed"
            },
            legend: {
                position: "bottom"
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "bar"
            },
            seriesColors: ['#F4B183', '#A5A5A5', '#a1de54', '#ffb650', '#22c37a'],
            series:
                [{
                    //type: "scatter",
                    name: "Ovserved",
                    data: seriesArrOvserved,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "MET 3 CRITERIA",
                    data: seriesArrAttained,
                    labels: {
                        visible: true
                    }
                }
                ]
            ,
            valueAxis: {
                labels: {
                    format: "{0}"
                },
                line: {
                    visible: false
                },
                axisCrossingValue: 0
            },
            categoryAxis: {
                categories: legendArr,
                line: {
                    visible: false
                },
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "8px Arial,Helvetica,sans-serif",
                    color: "black"
                },
                majorGridLines: {
                    visible: false
                }
            }
            ,
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });

    }
}


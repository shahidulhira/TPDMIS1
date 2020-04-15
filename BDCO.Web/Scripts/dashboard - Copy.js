var ChartManager = {
    createAreaChartSamMamGam: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        var series3Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.SAM.length; i++) {
                legendArr.push(odata.SAM[i].LegendText);
                series1Arr.push(odata.SAM[i].Total);
                series2Arr.push(odata.MAM[i].Total);
                series3Arr.push(odata.GAM[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    area: {
                        line: {
                            style: "smooth"
                        }
                    }
                },
                series:
                    [{
                        //type: "scatter",
                        name: "SAM",
                        data: series1Arr,
                        //color: "#f73636",
                        color: "url(#svg-gradient-sam)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },

                        line: {
                            color: "#f73636",
                            width: 1,
                            style: "smooth"
                        },
                    },
                    {
                        //type: "scatter",
                        name: "MAM",
                        data: series2Arr,
                        //color: "purple",
                        color: "url(#svg-gradient-mam)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth",
                            color: "purple",
                            width: 1
                        }

                    }, {
                        //type: "scatter",
                        name: "GAM",
                        data: series3Arr,
                        //color: "#ec4141",
                        color: "url(#svg-gradient-gam)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth",
                            color: "#ec4141",
                            width: 1
                        }
                    }

                    ],

                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },

                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    background: "red",
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }
    ,
    createAreaChartAdmissionBSFPTSFP: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.BSFP.length; i++) {
                legendArr.push(odata.BSFP[i].LegendText);
                series1Arr.push(odata.BSFP[i].Total);
                series2Arr.push(odata.TSFP[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        name: "BSFP",
                        data: series1Arr,
                        color: "#a0d396",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "TSFP",
                        data: series2Arr,
                        color: "#f2d028",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }

                    ],
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeintensity: 1,
                        opacityfrom: 0.7,
                        opacityto: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }
    ,
    createAreaChartAdmissionU5PLW: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.BSFP.length; i++) {
                legendArr.push(odata.BSFP[i].LegendText);
                series1Arr.push(odata.U5[i].Total);
                series2Arr.push(odata.PLW[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        name: "U5",
                        data: series1Arr,
                        color: "#a0d396",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "PLW",
                        data: series2Arr,
                        color: "#f2d028",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }

                    ],
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeintensity: 1,
                        opacityfrom: 0.7,
                        opacityto: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }
    ,
    createAreaChartAdmissionU5PLW_BSFP_TSFP: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.U5.length; i++) {
                legendArr.push(odata.U5[i].LegendText);
                series1Arr.push(odata.U5[i].Total);
                series2Arr.push(odata.PLW[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        name: "U5",
                        data: series1Arr,
                        //color: "#a0d396",
                        color: "url(#svg-gradient-u5)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "PLW",
                        data: series2Arr,
                        //color: "#f2d028",
                        color: "url(#svg-gradient-plw)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }

                    ],
                //fill: {
                //    type: 'gradient',
                //    gradient: {
                //        shadeintensity: 1,
                //        opacityfrom: 0.7,
                //        opacityto: 0.9,
                //        stops: [0, 90, 100]
                //    }
                //},
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    background: "red",
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }

    ,
    createAreaChartAdmissionU5PLW_TSFP: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.BSFP.length; i++) {
                legendArr.push(odata.BSFP[i].LegendText);
                series1Arr.push(odata.U5[i].Total);
                series2Arr.push(odata.PLW[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        name: "U5",
                        data: series1Arr,
                        color: "#a0d396",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "PLW",
                        data: series2Arr,
                        color: "#f2d028",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }

                    ],
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeintensity: 1,
                        opacityfrom: 0.7,
                        opacityto: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }
     ,
    createAreaChartAdmissionTSFP_MUAC_ZSCORE: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.TSFP_U5_MUAC.length; i++) {
                legendArr.push(odata.TSFP_U5_MUAC[i].LegendText);
                series1Arr.push(odata.TSFP_U5_MUAC[i].Total);
                series2Arr.push(odata.TSFP_U5_ZSCORE[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    area: {
                        line: {
                            style: "smooth"
                        }
                    }
                },
                series:
                    [{
                        //type: "scatter",
                        name: "MUAC",
                        data: series1Arr,
                        //color: "#a0d396",
                        color: "url(#svg-gradient-u5)",

                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth",
                            color: "#a0d396",
                            width: 1
                        }
                    },
                    {
                        //type: "scatter",
                        name: "ZSCORE",
                        data: series2Arr,
                        //color: "#f2d028",
                        color: "url(#svg-gradient-plw)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth",
                            color: "#f2d028",
                            width: 1
                        }
                    }

                    ],
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeintensity: 1,
                        opacityfrom: 0.7,
                        opacityto: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    background: "red",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }
     ,
    createAreaChartFoodDistribution: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.FoodDistribution.length; i++) {
                legendArr.push(odata.FoodDistribution[i].LegendText);
                series1Arr.push(odata.FoodDistribution[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        //name: trendType,
                        data: series1Arr,
                        //color: "#a0d396",
                        color: "url(#svg-gradient)",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }],
                //fill: {
                //    type: 'gradient',
                //    gradient: {
                //        shadeintensity: 1,
                //        opacityfrom: 0.7,
                //        opacityto: 0.9,
                //        stops: [0, 90, 100]
                //    }
                //},
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 90,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    background: "red",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }

     ,
    createAreaChartTSFPPerformanceTrend: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        var series3Arr = [];
        var series4Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.Recovery.length; i++) {
                legendArr.push(odata.Recovery[i].LegendText);
                series1Arr.push(odata.Recovery[i].Total);
                series2Arr.push(odata.Defaulter[i].Total);
                series3Arr.push(odata.Death[i].Total);
                series4Arr.push(odata.NonResponder[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                //theme: 'flat',
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
                    type: "area",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        name: "Recovery",
                        data: series1Arr,
                        color: "#008000",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "Defaulter",
                        data: series2Arr,
                        color: "#f2d028",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "Death",
                        data: series3Arr,
                        color: "red",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    },
                    {
                        //type: "scatter",
                        name: "NonResponder",
                        data: series4Arr,
                        color: "#D08502",
                        labels: {
                            visible: false,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }

                    ],
                fill: {
                    type: 'gradient',
                    gradient: {
                        shadeintensity: 1,
                        opacityfrom: 0.7,
                        opacityto: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value # %"
                }
            });
        }
    }

    ,
    createPieChartTSFPPerformanceTotal: function (CtrlName, title, odata) {
        var legendArr = [];
        var series1Arr = [];
        var series2Arr = [];
        if (odata != null) {
            for (var i = 0; i < odata.PieChartData.length; i++) {
                legendArr.push(odata.PieChartData[i].LegendText);
                series1Arr.push(odata.PieChartData[i].Total);
            }
            $("#" + CtrlName).kendoChart({
                theme: 'flat',
                title: {
                    text: title,
                    font: "bold 14px  Tw Cen MT Condensed"
                },
                legend: {
                    position: "bottom",
                    visible: true
                },
                chartArea: {
                    background: ""
                },
                seriesDefaults: {
                    type: "pie",
                    style: "smooth"
                },
                series:
                    [{
                        //type: "scatter",
                        // name: "U5",
                        data: series1Arr,
                        //color: "#a0d396",
                        labels: {
                            visible: true,
                            template: "#if (value > 0) {# #: value # #}#",
                        },
                        line: {
                            style: "smooth"
                        }
                    }
                    ],

                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',
                        visible: true
                    },
                    line: {
                        visible: false
                    },
                    majorGridLines: {
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
                        skip: 0,
                        rotation: 0,
                        font: "8px Arial,Helvetica,sans-serif",
                        color: "black",
                        visible: true
                    },
                    majorGridLines: {
                        visible: false
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    //template: "" + labelName + ": #= value #"
                    template: " #= value #"
                }
            });
        }
    }

    ,
    createPieChartGMPStatus: function (CtrlName, title, odata) {

        if (odata != null) {


            $("#" + CtrlName).kendoChart({
                theme: 'flat',

                legend: {
                    visible: true
                },
                chartArea: {
                    background: ""
                },
                seriesDefaults: {
                    labels: {
                        visible: true,
                        background: "transparent",
                        template: "#= category #: \n #= value#"
                    }
                },
                series: [{
                    type: "pie",
                    startAngle: 150,
                    data: odata
                }],
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= category #: \n #= value#"
                }
            });
        }
    }
}

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
                    format: "{0}"
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
                    padding: { top: 0 }
                }
            },
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name #: #= value #"
            }
        });
    },
    createChartACRCWise: function (CtrlName, CtrlName2, odata) {
        var legendArr = [];
        var series1ArrSG = [];
        var series2ArrNG = [];
        var series3ArrSN = [];
        var series4ArrWR = [];
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                legendArr.push(odata[i].RC);
                series1ArrSG.push(odata[i].SG);
                series2ArrNG.push(odata[i].NG);
                series3ArrSN.push(odata[i].SN);
                series4ArrWR.push(odata[i].WR ? odata[i].WR : 0);
            }
            $("#" + CtrlName).kendoChart({
                theme: 'flat',
                title: {
                    text: "RESOURCE CENTER WISE SCHOOL GOING AND OUT OF SCHOOL CHILDREN",
                    font: "13px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "column"
                },
                series:
                    [{                       
                        name: "ServicePoint Going",
                        data: series1ArrSG,
                        labels: {
                            visible: true
                        }
                    }, {
                        name: "Out Of ServicePoint",
                        data: series2ArrNG,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name #: #= value #"
                }
            });
            $("#" + CtrlName2).kendoChart({
                theme: 'flat',
                title: {
                    text: "RESOURCE CENTER WISE WORKING AND SPECIAL NEED CHILDREN",
                    font: "13px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "column"
                },
                series:
                    [ {
                        name: "Special Need",
                        data: series3ArrSN,
                        labels: {
                            visible: true
                        }
                    },
                    {
                        name: "Working Children",
                        data: series4ArrWR,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
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
    },

    createChartBRRCWise: function (CtrlName1, CtrlName2, odata) {
        var legendArr = [];
        //var series1ArrTotal = [];
        var series2ArrBRN = [];
        var series3ArrBRA = [];
        var series4ArrBRNPer = [];
        var series5ArrBRAPer = [];       
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                legendArr.push(odata[i].RC);
                //series1ArrTotal.push(odata[i].Total);
                series2ArrBRN.push(odata[i].BRN);
                series3ArrBRA.push(odata[i].BRA);
                series4ArrBRNPer.push(odata[i].BRNPer);
                series5ArrBRAPer.push(odata[i].BRAPer);
            }
            $("#" + CtrlName1).kendoChart({
                theme: 'flat',
                title: {
                    text: "RESOURCE CENTER WISE BIRTH REGISTRATION INFORMATION",
                    font: "13px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "column"
                },
                series:
                    [ {
                        name: "Not Available",
                        data: series2ArrBRN,
                        labels: {
                            visible: true
                        }
                    }, {
                        name: "Available",
                        data: series3ArrBRA,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name #: #= value #"
                }
            });

            $("#" + CtrlName2).kendoChart({
                theme: 'flat',
                title: {
                    text: "RESOURCE CENTER WISE BIRTH REGISTRATION AVAILABILITY(%)",
                    font: "13px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "column"
                },
                series:
                    [
                    {
                        name: "Not Available",
                        data: series4ArrBRNPer,
                        labels: {
                            format: "{0}%",
                            visible: true
                        }
                        ,
                        tooltip: {
                            visible: true,
                            format: "{0}",
                            template: '#=kendo.format("Not Available {0}%", value)#'
                        }
                    },
                     {
                         name: "Available",
                         data: series5ArrBRAPer,
                         labels: {
                             format: "{0}%",
                             visible: true
                         }
                         ,
                         tooltip: {
                             visible: true,
                             format: "{0}",
                             template: '#=kendo.format("Available {0}%", value)#'
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
                //,
                //tooltip: {
                //    visible: true,
                //    format: "{0}",
                //    template: '#=kendo.format("{0:0}%", value)#'                   
                //}
            });

        }
    },
    createChartAgeSegRCWise: function (CtrlName, TitleName, odata) {
            var legendArr = [];
            var series1ArrC0_1 = [];
            var series2ArrC0_2 = [];
            if (odata != null) {
                for (var i = 0; i < odata.length; i++) {
                    legendArr.push(odata[i].RC);
                    series1ArrC0_1.push(odata[i].C0_1);
                    series2ArrC0_2.push(odata[i].C1_2);
                }
                $("#" + CtrlName).kendoChart({
                    //chartArea: {
                    //    width: 500,
                    //    height: 500
                    //},
                    theme: 'flat',
                    //theme: 'blueOpal',
                    title: {
                        text: TitleName,
                        font: "13px roboto"
                    },
                    legend: {
                        position: "bottom"
                    },
                    seriesDefaults: {
                        type: "column",
                        startAngle: 90
                    },
                    series:
                        [{
                            name: "বয়স: ০-১ বছর ",
                            data: series1ArrC0_1,
                            labels: {
                                visible: true
                            }
                        }, {
                            name: "বয়স: ১-২ বছর ",
                            data: series2ArrC0_2,
                            labels: {
                                visible: true
                            }
                        },
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
                            font: "12px Arial,Helvetica,sans-serif",
                            color: "black"
                        }
                    }
                    ,
                    tooltip: {
                        visible: true,
                        format: "{0}",
                        template: "#= series.name #: #= value # জন"
                    }
                });
            }
    },
    createChartBTRCWise: function (CtrlName, TitleName, odata) {
        
    var legendArr = [];
    var series1ArrChildren = [];
    var series2ArrAdult = [];
    if (odata != null) {
        for (var i = 0; i < odata.length; i++) {
            legendArr.push(odata[i].RC);
            series1ArrChildren.push(odata[i].Children);
            series2ArrAdult.push(odata[i].Adult);
        }
        $("#" + CtrlName).kendoChart({
             
            theme: 'flat',
            
            title: {
                text: TitleName,
                font: "13px roboto"
            },
            legend: {
                position: "bottom"
            },
            seriesDefaults: {
                type: "column",
            },
            series:
                [{
                    name: "Children",
                    data: series1ArrChildren,
                    labels: {
                        visible: true
                    }
                }, {
                    name: "Adult",
                    data: series2ArrAdult,
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
                    font: "12px Arial,Helvetica,sans-serif",
                    color: "black"
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
},
    createChartHHRCWise: function (CtrlName, TitleName, odata) {
        
        var legendArr = [];
        var series1ArrNoOfHH = [];
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                legendArr.push(odata[i].RC);
                series1ArrNoOfHH.push(odata[i].NoOfHH);
               
            }
            $("#" + CtrlName).kendoChart({
                theme: 'flat',
                title: {
                    text: TitleName,
                    font: "13px roboto"                    
                },
                legend: {
                    position: "bottom",
                    labels: { visible: true, font: "10px Arial,Helvetica,sans-serif", }

                },
                dataSource: {
                    data: odata
                },
                seriesDefaults: {
                    type: "column",
                    startAngle: 150
                },
                series: [{
                    //type: "donut",
                    field: "NoOfHH",
                    categoryField: "RC",
                    //explodeField: "explode"
                    labels: { visible: true, template: '#=kendo.format("{0:0}",value)#', position: 'outsideEnd', font: "12px roboto,sans-serif", },
                }],
                categoryAxis: {
                    labels: {
                        template: '#=kendo.format("{0:0}", value)#',
                        rotation: 0,
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                },
                labels: {
                    visible: true,
                    background: "transparent",
                    position: "insideEnd",
                    template: "#= category #: \n #= value#%"
                  
                },
                seriesColors: ["#ff7663", "#34BAF6", "#B4E575", "#006634"],

                
                tooltip: {
                    visible: true,
                    template: "${ category } - ${ value }"
                }
            });
            
        }
    },
   
    createChartSlumRCWise: function (CtrlName, TitleName, Chartype, odata) {
        $("#" + CtrlName).kendoChart({
            theme: 'flat',
            title: {
                text: TitleName,
                font: "14px roboto"
            },
            legend: {
                position: "bottom",
                labels: { visible: true, font: "12px Arial,Helvetica,sans-serif", }
            },
            dataSource: {
                data: odata
            },
            series: [{
                type: "column",
                field: "NoOfSlum",
                categoryField: "RC",
                //explodeField: "explode"
                labels: {
                            visible: true
                        }
            }],
            categoryAxis: {
                labels: {
                    template: '#=kendo.format("{0:0}", value)#',
                    rotation: 0,
                    font: "12px Arial,Helvetica,sans-serif",
                    color: "black"
                }
            },
            //seriesColors: ["#fad84a", "#b4e575", "#fad84a", "#4caf50", "#ffc572"],
            tooltip: {
                visible: true,
                template: "${ category } - ${ value }"
            }
        });
            
    },
  
   
    createChartReachICDPRCWise: function (CtrlName, CtrlName2, charttype, odata) {
        
        var legendArr = [];
        var series1ArrTotal = [];
        var series2ArrReceived = [];
        var series3ArrNotReceived = [];
        var series4ArrRPer = [];
        var series5ArrNPer = [];
        
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                legendArr.push(odata[i].RC);
                series1ArrTotal.push(odata[i].Total);
                series2ArrReceived.push(odata[i].Received);
                series3ArrNotReceived.push(odata[i].NotReceived);
                series4ArrRPer.push(odata[i].RPer);
                series5ArrNPer.push(odata[i].NPer);
            }
            
            $("#" + CtrlName).kendoChart({
                theme: 'flat',
                title: {
                    text: "RC WISE ICDP SERVICE RECEIVED FROM MSS",
                    font: "14px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: charttype
                },
                series:
                    [ {
                        name: "Received",
                        data: series2ArrReceived,
                        labels: {
                            visible: true
                        }
                    }, {
                        name: "Not Received",
                        data: series3ArrNotReceived,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name #: #= value #"
                }
            });
            $("#" + CtrlName2).kendoChart({
                theme: 'flat',
                title: {
                    text: "RC WISE ICDP SERVICE RECEIVED FROM MSS(%)",
                    font: "14px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: charttype
                },
                series:
                    [ {
                        name: "Received",
                        data: series4ArrRPer,
                        labels: {
                            format: "{0}%",
                            visible: true
                        }
                    }, {
                        name: "Not Received",
                        data: series5ArrNPer,
                        labels: {
                            format: "{0}%",
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
               ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name #: #= value # %"
                }
            });
        }

    },
    createChartReachOtherRCWise: function (CtrlName, CtrlName2, charttype, odata) {
        //debugger
        var legendArr = [];
        var series1ArrTotal = [];
        var series2ArrReceived = [];
        var series3ArrNotReceived = [];
        var series4ArrRPer = [];
        var series5ArrNPer = [];
        //debugger;
        if (odata != null) {
            for (var i = 0; i < odata.length; i++) {
                legendArr.push(odata[i].RC);
                series1ArrTotal.push(odata[i].Total);
                series2ArrReceived.push(odata[i].Received);
                series3ArrNotReceived.push(odata[i].NotReceived);
                series4ArrRPer.push(odata[i].RPer);
                series5ArrNPer.push(odata[i].NPer);
            }
            debugger
            $("#" + CtrlName).kendoChart({
                theme: 'flat',
                title: {
                    text: "RC WISE ICDP SERVICE RECEIVED FROM OTHER",
                    font: "14px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: charttype
                },
                series:
                    [ {
                        name: "Received",
                        data: series2ArrReceived,
                        labels: {
                            visible: true
                        }
                    }, {
                        name: "Not Received",
                        data: series3ArrNotReceived,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
                    }
                }
                ,
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name #: #= value #"
                }
            });

            $("#" + CtrlName2).kendoChart({
                theme: 'flat',
                title: {
                    text: "RC WISE ICDP SERVICE RECEIVED FROM OTHER (%)",
                    font: "14px roboto"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: charttype
                },
                series:
                    [ {
                        name: "Received",
                        data: series4ArrRPer,
                        labels: {
                            visible: true
                        }
                    }, {
                        name: "Not Received",
                        data: series5ArrNPer,
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
                        font: "12px Arial,Helvetica,sans-serif",
                        color: "black"
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

    },
}



var GraphManager = {

    createChartGMPTrend: function (CtrlName, title, odata, labelName) {

        var legendArr = [];
        var series1Arr = [];

        if (odata != null) {
            for (var i = 0; i < odata.TrenChartData.length; i++) {
                if (odata.TrenChartData[i].MonthName != 'Grand Total') {
                    legendArr.push(odata.TrenChartData[i].MonthName);
                    if (labelName == "REACH")
                        series1Arr.push(odata.TrenChartData[i].TotalReach);
                    if (labelName == "RECOVERED")
                        series1Arr.push(odata.TrenChartData[i].TotalRecovered);
                    if (labelName == "CRITICAL")
                        series1Arr.push(odata.TrenChartData[i].TotalAtRisk);

                    // series1Arr.push(Math.floor(Math.random() * 10) + 1);                   
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
                        //name: labelName,
                        data: series1Arr,
                        labels: {
                            visible: true,
                            template: "#if (value > 0) {# #: value # #}#",
                        }
                    }
                    ]
                ,
                valueAxis: {
                    labels: {
                        format: "{0}",
                        skip: 0,
                        template: '#=kendo.format("{0:0}", value)#',

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
                        skip: 0,
                        rotation: 45,
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
                    template: "" + labelName + ": #= value #"
                }
            });
        }
    },


}


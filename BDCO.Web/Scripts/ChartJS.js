

var ChartManager = {
    createChartWithoutBackground: function (ctrlName, chartType, title, data, lstCategory, labelRotation, fontStyle, labelPadding, seriesColor, legendPosition, titlePosition) {
        var LabelRotation = 0;
        if (labelRotation != 'undefined')
            LabelRotation = labelRotation;
        var FontStyle = '"12px Arial,Helvetica,sans-serif"';
        if (fontStyle != 'undefined')
            FontStyle = fontStyle;
        var LabelPaddingTop = 10;
        if (labelPadding != 'undefined')
            LabelPaddingTop = labelPadding;
        var SeriesColor = ['#17c3b1', '#ff7563', '#a1de54', '#ffb650', '#22c37a'];
        if (seriesColor != 'undefined')
            SeriesColor = seriesColor;
        var LegendPosition = "Top";
        if (legendPosition != 'undefined')
            LegendPosition = legendPosition;
        var TitlePosition = "Center";
        if (titlePosition != 'undefined')
            TitlePosition = titlePosition;
        
        var chart = $("#" + ctrlName).kendoChart({
            theme: "flat",
            title: {
                text: title,
                align: TitlePosition
            },
            legend: {
                position: LegendPosition
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: chartType,
                style: "smooth"
            },
            series: data,
            seriesColors: SeriesColor,
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
                majorGridLines: {
                    visible: false
                },
                labels: {
                    rotation: LabelRotation,
                    padding: { top: LabelPaddingTop },
                    font: FontStyle
                }
            },
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= series.name # #= value #"
            }
        });


    }
}
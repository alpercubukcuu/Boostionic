$(function() {
    "use strict";

    $('.sparkbar').sparkline('html', { type: 'bar' });

    // Total Revenue
    $(document).ready(function() {
        var options = {
            chart: {
                height: 300,
                type: 'line',
                toolbar: {
                    show: false,
                },
            },
            colors: ['#59c4bc', '#637aae'],
            series: [{
                name: 'Fees',
                type: 'column',
                data: [440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160]
            }, {
                name: 'Donation',
                type: 'line',
                data: [23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16]
            }],
            stroke: {
                width: [0, 4]
            },        
            
            //labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dev'],
            labels: ['01 Aug 2020', '02 Aug 2020', '03 Aug 2020', '04 Aug 2020', '05 Aug 2020', '06 Aug 2020', '07 Aug 2020', '08 Aug 2020', '09 Aug 2020', '10 Aug 2020', '11 Aug 2020', '12 Aug 2020'],
            xaxis: {
                type: 'datetime'
            },
            yaxis: [{
                title: {
                    text: 'Fees',
                },

            }, {
                opposite: true,
                title: {
                    text: 'Donation'
                }
            }]
        }
        var chart = new ApexCharts(
            document.querySelector("#apex-chart-line-column"),
            options
        );

        chart.render();
    });


    // Gender-Ratio
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Gender-Ratio', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 63],
                    ['data2', 37]
                ],
                type: 'donut', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"]
                },
                names: {
                    // name of each serie
                    'data1': 'Boys',
                    'data2': 'Girls'
                }
            },
            axis: {
            },
            legend: {
                show: true, //hide legend
            },
            padding: {
                bottom: 0,
                top: 0
            },
        });
    });

    // world map
    var mapData = {
        "US": 298,			
        "AU": 760,
        "CA": 870,
        "IN": 2000000,
        "GB": 120,
    };
    if( $('#world-map-markers2').length > 0 ){
        $('#world-map-markers2').vectorMap(
        {
            map: 'world_mill_en',
            backgroundColor: 'transparent',
            borderColor: '#fff',
            borderOpacity: 0.25,
            borderWidth: 0,
            color: '#e6e6e6',
            regionStyle : {
                initial : {
                fill : '#ececec'
                }
            },

            markerStyle: {
                initial: {
                            r: 5,
                            'fill': '#fff',
                            'fill-opacity':1,
                            'stroke': '#000',
                            'stroke-width' : 1,
                            'stroke-opacity': 0.4
                        },
                },
        
            markers: [
                { latLng: [37.09,-95.71], name: 'America' },                
                { latLng: [-25.27, 133.77], name: 'Australia' },
                { latLng: [56.13,-106.34], name: 'Canada' },
                { latLng: [20.59,78.96], name: 'India' },
                { latLng: [55.37,-3.43], name: 'United Kingdom' },
            ],

            series: {
                regions: [{
                    values: {
                        "US": '#339af6',						
                        "AU": '#02b5b2',
                        "IN": '#f1a627',
                        "GB": '#445771',
                        "CA": '#68bb35',
                    },
                    attribute: 'fill'
                }]
            },
            hoverOpacity: null,
            normalizeFunction: 'linear',
            zoomOnScroll: false,
            scaleColors: ['#000000', '#000000'],
            selectedColor: '#000000',
            selectedRegions: [],
            enableZoom: false,
            hoverColor: '#fff',
        });
    }
});

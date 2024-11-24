$(function () {
    "use strict";

    // Household Income/Value
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Household-Income', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 234, 192, 172, 201, 144],
                    ['data2', 99, 129, 188, 117, 121]
                ],
                type: 'bar', // default type of chart
                groups: [
                    [ 'data1', 'data2']
                ],
                colors: {
                    'data1': Iconic.colors["theme-dark2"],
                    'data2': Iconic.colors["theme-dark3"]
                },
                names: {
                    // name of each serie
                    'data1': 'Income',
                    'data2': 'Value'
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['London', 'Tokyo', 'Paris', 'Sydney', 'Berlin']
                },
            },
            bar: {
                width: 16
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

    //Conversion by Age
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Conversion-by-Age', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 23, 33, 8, 17, 41, 37],
                    ['data2', 17, 17, 15, 17, 29, 42]
                ],
                type: 'bar', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-dark2"],
                    'data2': Iconic.colors["theme-dark3"]
                },
                names: {
                    // name of each serie
                    'data1': '10-30 age',
                    'data2': '30-60 age'
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Americas', 'Asia', 'Africa', 'Europe', 'Australia', 'Oceania']
                },
                rotated: true,
            },
            bar: {
                width: 10
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

    //Which channels are driving engagement?
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#driving-engagement', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 23, 33, 8, 17, 41, 37],
                    ['data2', 17, 17, 15, 17, 29, 42]
                ],
                type: 'bar', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-dark2"],
                    'data2': Iconic.colors["theme-dark3"]
                },
                names: {
                    // name of each serie
                    'data1': 'Sessions',
                    'data2': 'Pageviews'
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Americas', 'Asia', 'Africa', 'Europe', 'Australia', 'Oceania']
                },
                rotated: true,
            },
            bar: {
                width: 10
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

    if( $('#Top-Country').length > 0 ){

        $('#Top-Country').vectorMap(
        {
            map: 'world_mill_en',
            backgroundColor: 'transparent',
            borderColor: '#fff',
            borderOpacity: 0.25,
            borderWidth: 0,
            color: '#e6e6e6',
            regionStyle : {
                initial : {
                fill : '#cccccc'
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
        
            markers : [{
                latLng : [21.00, 78.00],
                name : 'INDIA : 350'
            
            },
                {
                latLng : [-33.00, 151.00],
                name : 'Australia : 250'
                
            },
                {
                latLng : [36.77, -119.41],
                name : 'USA : 250'
                
            },
                {
                latLng : [55.37, -3.41],
                name : 'UK   : 250'
                
            },
                {
                latLng : [25.20, 55.27],
                name : 'UAE : 250'
            
            }],

            series: {
                regions: [{
                    values: {
                        "US": '#2CA8FF',
                        "SA": '#49c5b6',
                        "AU": '#18ce0f',
                        "IN": '#f96332',
                        "GB": '#FFB236',
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

    $('.knob').knob({
        draw: function() {
            this.i.css({
                'margin-right': '-' + ((this.w * 3 / 4 + 2) >> 0) + 'px',
                'margin-left': 'auto'
            });
        }
    });
   
});
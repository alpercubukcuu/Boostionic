$(function() {
    "use strict";

    // Annual Report
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Annual-Report', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 11, 8, 15, 18, 19, 17, 7, 7, 5, 7, 17, 7],
                    ['data2', 7, 7, 5, 7, 9, 12, 8, 15, 18, 19, 7, 9]
                ],
                type: 'bar', // default type of chart
                groups: [
                    [ 'data1', 'data2']
                ],
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"]
                },
                names: {
                    // name of each serie
                    'data1': 'Data1',
                    'data2': 'Date2'
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec']
                },
            },
            bar: {
                width: 16
            },
            legend: {
                show: false, //hide legend
            },
            padding: {
                left: -5,
                bottom: -10,
                top: 0
            },
        });
    });

    // Sales by Category
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#chart-donut', // id of chart wrapper
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
                    'data1': 'Online',
                    'data2': 'Offline'
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

    // vector map
	var mapData = {
        "US": 298,			
        "AU": 760,
        "CA": 870,
        "IN": 2000000,
        "GB": 120,
    };
	if( $('#world-map-markers').length > 0 ){
		$('#world-map-markers').vectorMap(
		{
			map: 'world_mill_en',
			backgroundColor: 'transparent',
			borderColor: '#fff',
			borderOpacity: 0.25,
			borderWidth: 0,
            color: '#e6e6e6',
            
			regionStyle : {
				initial : {
				    fill : '#dfdfdf'
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
                { latLng: [55.37,-3.43], name: 'United Kingdom' },
                { latLng: [56.13,-106.34], name: 'Canada' },
            ],

			series: {
				regions: [{
					values: {
						"US": '#59c4bc',						
						"AU": '#7954ad',
                        "GB": '#2faaa1',
                        "CA": '#637aae',
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

    $('.sparkbar').sparkline('html', { type: 'bar' });

});

$(function() {
    "use strict";

    // progress bars
    $('.progress .progress-bar').progressbar({
        display_text: 'none'
    });

    $('.knob').knob({
        'draw' : function () { 
            $(this.i).val(this.cv + '%')
        }
    });

    // Report by Sector
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Report-by-Sector', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 63],
                    ['data2', 44],
                    ['data3', 12],
                    ['data4', 14]
                ],
                type: 'pie', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"],
                    'data3': Iconic.colors["theme-cyan3"],
                    'data4': Iconic.colors["theme-cyan4"]
                },
                names: {
                    // name of each serie
                    'data1': 'Realestate',
                    'data2': 'Education',
                    'data3': 'Healthcare',
                    'data4': 'Finance'
                }
            },
            axis: {
            },
            legend: {
                show: false, //hide legend
            },
            padding: {
                bottom: 0,
                top: 0
            },
        });
    });

    // workload by team
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Workload-by-team', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 23, 33, 8, 17, 41, 37],
                    ['data2', 17, 17, 15, 17, 29, 42]
                ],
                type: 'bar', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"]
                },
                names: {
                    // name of each serie
                    'data1': 'Week',
                    'data2': 'Month'
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Design', 'Deve', 'Marketing', 'Sales', 'QA', 'Finance']
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
});
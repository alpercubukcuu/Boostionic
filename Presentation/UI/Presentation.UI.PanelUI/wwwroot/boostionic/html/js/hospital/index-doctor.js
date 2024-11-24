$(function() {
    "use strict";

    $('.sparkline-pie').sparkline('html', {
        type: 'pie',
        offset: 90,
        width: '160px',
        height: '160px',
        sliceColors: ['#02b5b2', '#445771', '#ffcd55']
    })

    // top doctor widget chart
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#TW-Permanent-Doctors', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 11, 8, 15, 18, 19, 17, 7, 9, 12, 8, 15, 22],
                    ['data2', 7, 7, 5, 7, 9, 12, 8, 15, 18, 19, 11, 15],
                    ['data3', 12, 8, 15, 18, 19, 11, 15, 7, 7, 5, 7, 9],
                    ['data4', 7, 7, 5, 19, 11, 15, 7, 9, 12, 8, 15, 18],
                ],
                type: 'bar', // default type of chart
                groups: [
                    [ 'data1', 'data2', 'data3', 'data4']
                ],
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"],
                    'data3': Iconic.colors["theme-cyan3"],
                    'data4': Iconic.colors["theme-cyan4"],
                },
                names: {
                    // name of each serie
                    'data1': 'Neurosurgeon',
                    'data2': 'Dentist',
                    'data3': 'Audiology',
                    'data4': 'Gynecologist',
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
            },
            bar: {
                width: 8
            },
            legend: {
                show: false, //hide legend
            },
            padding: {
                bottom: -30,
                left: -2,
                top: 0
            },
        });
    });
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#TW-Contract-Base', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 11, 8, 15, 18, 19, 17, 7, 9, 12, 8, 15, 22],
                    ['data2', 7, 7, 5, 7, 9, 12, 8, 15, 18, 19, 11, 15],
                    ['data3', 12, 8, 15, 18, 19, 11, 15, 7, 7, 5, 7, 9],
                    ['data4', 7, 7, 5, 19, 11, 15, 7, 9, 12, 8, 15, 18],
                ],
                type: 'bar', // default type of chart
                groups: [
                    [ 'data1', 'data2', 'data3', 'data4']
                ],
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"],
                    'data3': Iconic.colors["theme-cyan3"],
                    'data4': Iconic.colors["theme-cyan4"],
                },
                names: {
                    // name of each serie
                    'data1': 'Neurosurgeon',
                    'data2': 'Dentist',
                    'data3': 'Audiology',
                    'data4': 'Gynecologist',
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
            },
            bar: {
                width: 8
            },
            legend: {
                show: false, //hide legend
            },
            padding: {
                bottom: -30,
                left: -2,
                top: 0
            },
        });
    });
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#TW-Total-Nurse', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 11, 8, 15, 18, 19, 17, 7, 9, 12, 8, 15, 22],
                    ['data2', 7, 7, 5, 7, 9, 12, 8, 15, 18, 19, 11, 15],
                    ['data3', 12, 8, 15, 18, 19, 11, 15, 7, 7, 5, 7, 9],
                    ['data4', 7, 7, 5, 19, 11, 15, 7, 9, 12, 8, 15, 18],
                ],
                type: 'bar', // default type of chart
                groups: [
                    [ 'data1', 'data2', 'data3', 'data4']
                ],
                colors: {
                    'data1': Iconic.colors["theme-cyan1"],
                    'data2': Iconic.colors["theme-cyan2"],
                    'data3': Iconic.colors["theme-cyan3"],
                    'data4': Iconic.colors["theme-cyan4"],
                },
                names: {
                    // name of each serie
                    'data1': 'Neurosurgeon',
                    'data2': 'Dentist',
                    'data3': 'Audiology',
                    'data4': 'Gynecologist',
                }
            },
            axis: {
                x: {
                    type: 'category',
                    // name of each category
                    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                },
            },
            bar: {
                width: 8
            },
            legend: {
                show: false, //hide legend
            },
            padding: {
                bottom: -30,
                left: -2,
                top: 0
            },
        });
    });

    // Use by Gander
    $(document).ready(function(){
        var chart = c3.generate({
            bindto: '#Use-by-gander', // id of chart wrapper
            data: {
                columns: [
                    // each columns data
                    ['data1', 73],
                    ['data2', 27],
                ],
                type: 'donut', // default type of chart
                colors: {
                    'data1': Iconic.colors["theme-purple1"],
                    'data2': Iconic.colors["theme-purple2"],
                },
                names: {
                    // name of each serie
                    'data1': 'Male',
                    'data2': 'Female',
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
});
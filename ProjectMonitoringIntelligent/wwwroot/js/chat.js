"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    var config = {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: [],
            datasets: [{
                label: 'Température',
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: []
            }]
        },

        // Configuration options go here
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Monitoring Intelligent'
            },
            tooltips: {
                mode: 'index',
                intersect: false,
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Temps'
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Value'
                    },
                    ticks: {
                        suggestedMin: 26.5,
                        suggestedMax: 29.5
                    }
                }]
            }
        }
}

var ctx = document.getElementById('myChart').getContext('2d');
    window.myLine = new Chart(ctx, config);

connection.start().then(function () {
    connection.invoke("SendData").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveData", function (data) {
    config.data.labels.push("");
    config.data.datasets[0].data.push(data);
    window.myLine.update();
});


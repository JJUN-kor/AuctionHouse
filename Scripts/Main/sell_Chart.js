


window.onload = function () {


    $('#search_btn').click(function () {
        //alert($("#insert_name").val());

        $.ajax({
            type: 'POST',
            url: "/Home/Get_Item_Name",
            data: { "item_name": $('#insert_name').val() },

            success: function (data) {
                alert('아이템 검색 성공');
                console.log(data);


                new Chart(document.getElementById("myChart2"), {
                    type: 'line',
                    data: {
                        labels: ['test', 'test', 'test', 'test', 'test'],
                        datasets: [{
                            label: data.Item_name,
                            data: [
                                102,
                                85,
                                114,
                                150,
                                130
                            ],
                            borderColor: "rgba(255, 201, 14, 1)",
                            backgroundColor: "rgba(255, 201, 14, 0.5)",
                            fill: false,
                            lineTension: 0
                        }]
                    },
                    options: {
                        responsive: true,
                        title: {
                            display: true,
                            text: ''
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
                                    labelString: 'date'
                                }
                            }],
                            yAxes: [{
                                display: true,
                                ticks: {
                                    suggestedMin: 0,
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: 'price'
                                }
                            }]
                        }
                    }
                });



            },

            error: function (_status, err) {
                alert('아이템 이름 로딩 오류');
                console.log(err);

            }


        });
    });



    setTimeout(function () {
        $.ajax({

            type: 'POST',
            url: '/Home/Get_Top_Search_Item',
            dataType: 'JSON',

            success: function (data) {
                console.log(data);
                //data[0].Item_name, data[1].Item_name, data[2].Item_name, data[3].Item_name, data[4].Item_name 
                //data[0].Search_count, data[1].Search_count, data[2].Search_count, data[3].Search_count, data[4].Search_count

                var ctx = document.getElementById('myChart');
                var myChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: [data[0].Item_name, data[1].Item_name, data[2].Item_name, data[3].Item_name, data[4].Item_name],
                        datasets: [{
                            label: '# of Votes',
                            data: [data[0].Search_count, data[1].Search_count, data[2].Search_count, data[3].Search_count, data[4].Search_count],
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(153, 102, 255, 0.2)'
                            ],
                            borderColor: [
                                'rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true
                                }
                            }]
                        }
                    }
                });



                new Chart(document.getElementById("myChart2"), {
                    type: 'line',
                    data: {
                        labels: ['test', 'test', 'test', 'test', 'test'],
                        datasets: [{
                            label: data[0].Item_name,
                            data: [
                                200,
                                201,
                                195,
                                190,
                                240
                            ],
                            borderColor: "rgba(255, 201, 14, 1)",
                            backgroundColor: "rgba(255, 201, 14, 0.5)",
                            fill: false,
                            lineTension: 0
                        }]
                    },
                    options: {
                        responsive: true,
                        title: {
                            display: true,
                            text: ''
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
                                    labelString: '날짜'
                                }
                            }],
                            yAxes: [{
                                display: true,
                                ticks: {
                                    suggestedMin: 0,
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: '가격'
                                }
                            }]
                        }
                    }
                });




            },

            error: function (error) {
                console.log(error);
            }



        })
    },1500);




}




    /*
    var ctx = document.getElementById('myChart2');
    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            datasets: [{
                label: '# of Votes',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
    */
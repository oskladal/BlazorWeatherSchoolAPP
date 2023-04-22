// Data retrieved https://en.wikipedia.org/wiki/List_of_cities_by_average_temperature

function RenderCharts(data, graf_id) {

    Highcharts.stockChart(graf_id, JSON.parse(data));
}

function RenderChartsNew(data, graf_id) {

    Highcharts.chart(graf_id, JSON.parse(data));

    
}

function DeleteGraf(id) {
    document.getElementById(id).innerHTML = "";
}
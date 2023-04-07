// Data retrieved https://en.wikipedia.org/wiki/List_of_cities_by_average_temperature

function RenderCharts(data) {

    //Highcharts.chart('graf1', JSON.parse(data));

    Highcharts.stockChart('graf1', JSON.parse(data));
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;


using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using DBConnect.Models;
using Highsoft.Web.Mvc.Stocks.Rendering;

namespace BlazorWeatherSchoolAPP.Source
{
    public class GraphCharts
    {
        public static string SecondGraph(Quantities quantities, Quantities quantitiesawg) {

            double pm1 = quantities.Quantities326.pm_1;
            DateTime firstime = quantities.SensorFirstTime;
            double pm1_min = quantitiesawg.Quantities326.pm_1_min;
            double pm1_max = quantitiesawg.Quantities326.pm_1_max;
            double pm1_avg = quantitiesawg.Quantities326.pm_1;
            DateTime pm1_min_ts = quantitiesawg.Quantities326.pm1_ts_min;
            DateTime pm1_max_ts = quantitiesawg.Quantities326.pm1_ts_max;
            double pm2_5 = quantities.Quantities326.pm_2p5;
            double pm_2p5_min = quantitiesawg.Quantities326.pm_2p5_min;
            double pm_2p5_max = quantitiesawg.Quantities326.pm_2p5_max;
            double pm2p5_avg = quantitiesawg.Quantities326.pm_2p5;
            DateTime pm_2p5_min_ts = quantitiesawg.Quantities326.pm2p5_ts_min;
            DateTime pm_2p5_max_ts = quantitiesawg.Quantities326.pm2p5_ts_max;
            double pm10 = quantities.Quantities326.pm_10;
            double pm_10_min = quantitiesawg.Quantities326.pm_10_min;
            double pm_10_max = quantitiesawg.Quantities326.pm_10_max;
            double pm10_avg = quantitiesawg.Quantities326.pm_10;
            DateTime pm_10_min_ts = quantitiesawg.Quantities326.pm10_ts_min;
            DateTime pm_10_max_ts = quantitiesawg.Quantities326.pm10_ts_max;
            double pm2_5_24 = quantities.Quantities326.pm_2p5_24_hour;
            double pm10_24 = quantities.Quantities326.pm_10_24_hour;

          
            var chartOptions = new Highcharts
            {
                Title = new Title
                {
                    Text = "Vnitřní prachové částice"
                },
                Subtitle = new Subtitle
                {
                    Text = "AirLink VŠCHT, měřeno:"
                },
                XAxis = new List<XAxis>
{
                new XAxis
                {
                    Type = "category",
                 
                }
            },
                YAxis = new List<YAxis>
{
                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "μg/m&sup3"
                    }
                }
            },
                Legend = new Legend
                {
                    Enabled = false
                },
                Tooltip = new Tooltip
                {
                    HeaderFormat = "<span style='font-size:10px'>{point.key}</span><table style='font-size:12px'>",
                    PointFormat = "<tr><td style='color:blue;padding:0'>{series.name}: </td><td style='padding:0'><b>{point.y:.1f}&nbsp;μg/m&sup3</b></td></tr>",
                    FooterFormat = "</table>",
                    Shared = true,
                    UseHTML = true
                },

                PlotOptions = new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.1,
                        BorderWidth = 0,
                        GroupPadding = 0, // nastavení menší mezery mezi sloupci,
       
                    }
                },

                Series = new List<Series>
{
                new ColumnSeries
                {
                    Name = "PM1",
                    ColorByPoint = true,
            
                    Data = new List<ColumnSeriesData>
    {
                        new ColumnSeriesData { Name = "Aktuální", Y = pm1 },
                        new ColumnSeriesData {Name = "Max", Y = pm1_max},
                        new ColumnSeriesData {Name = "Min", Y=pm1_min},
                        new ColumnSeriesData {Name = "Průměr", Y=pm1_avg},

                    }
                },
                new ColumnSeries
                {
                    Name = "PM2.5",
                    ColorByPoint = true,
                    Data = new List<ColumnSeriesData>
    {
                        new ColumnSeriesData { Name = "Aktuální", Y = pm2_5 },
                        new ColumnSeriesData {Name = "Max", Y = pm_2p5_max},
                        new ColumnSeriesData {Name = "Min", Y= pm_2p5_min},
                        new ColumnSeriesData {Name = "Průměr", Y= pm2p5_avg},
                        new ColumnSeriesData { Name = "Za 24 hod", Y = pm2_5_24 },

                    }
                },
                new ColumnSeries
                {
                    Name = "PM10",
                    ColorByPoint = true,
                    Data = new List<ColumnSeriesData>
    {
                        new ColumnSeriesData { Name = "Aktuální", Y = pm2_5 },
                        new ColumnSeriesData {Name = "Max", Y = pm_10_max},
                        new ColumnSeriesData {Name = "Min", Y= pm_10_min},
                        new ColumnSeriesData {Name = "Průměr", Y= pm10_avg},
                        new ColumnSeriesData { Name = "Za 24 hod", Y = pm10_24 },

                    }
                },
                
            },   
            };

            chartOptions.ID = "graf1";
            var renderer = new HighchartsRenderer(chartOptions);

            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;

        }
    }
}


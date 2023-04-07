using System;
using System.Net.NetworkInformation;


using DBConnect.Models;
using System.Linq;
using System.Collections;
using Highsoft.Web.Mvc.Stocks;
using Highsoft.Web.Mvc.Stocks.Rendering;


//using Highsoft.Web.Mvc.Charts;
//using Highsoft.Web.Mvc.Charts.Rendering;

namespace BlazorWeatherSchoolAPP.Source
{
	public class GraphsGenerator
	{
		public GraphsGenerator()
		{ 
		}

		public static string BasicGraph(List<Quantities> quantities) {

            var teploty = new List<SplineSeriesData>();
            var teplotavenkovni = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teploty.Add(new SplineSeriesData() {
                    Y = i.Quantities243.temp_in,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000 
                });

                teplotavenkovni.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.temp,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
            }

            var chartOptions = new Highstock
            {
                Chart = new Chart()
                {
                    Type = ChartType.Line
                },
                
                Title = new Title
                {
                    Text = "Area chart with negative values"
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Nuclear weapon states"
                    },

                    DateTimeLabelFormats = new Hashtable
    {
                        { "day", "%d.%m.%Y" },
                        { "month", "%m.%Y" },
                        { "year", "%Y" }
                    },
                    
                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "Nuclear weapon states"
                    }
                }

            },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " °" + "C",
                },

                PlotOptions = new PlotOptions()
                {

                    Line = new PlotOptionsLine()
                    {
                        DataLabels = new PlotOptionsLineDataLabels()
                        {
                            Enabled = true
                        }
                    },

                    Series = new PlotOptionsSeries
                    {
                        TurboThreshold = Int32.MaxValue
                        
                        
                    }



                },

                Time = new Time {
                    TimezoneOffset = -(double)TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalMinutes
        },

                Credits = new Credits
                {
                    Enabled = false
                },
                Series = new List<Series>
                {
                    new SplineSeries
                    {
                        Name = "Vnitřní teplota",
                        Data = teploty,
                        ShowInNavigator = true 
                    },

                    new SplineSeries
                    {
                        Name = "Venkovní teplota",
                        Data = teplotavenkovni,
                        ShowInNavigator = true
                    }


                }
            };
            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;


        }
	}
}


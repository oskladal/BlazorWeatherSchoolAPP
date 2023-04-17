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
		
		public static string BasicGraphVnitrniTeplotaWLL(List<Quantities> quantities) {

            var teploty = new List<SplineSeriesData>();
            var rosny_bod = new List<SplineSeriesData>();
            var heat_index = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teploty.Add(new SplineSeriesData() {
                    Y = i.Quantities243.temp_in,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000 
                });

                rosny_bod.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.dew_point_in,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                heat_index.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.heat_index_in,
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
                    Text = "Teplota vzduchu"
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
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
                        Text = "Teplota (°C)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hours", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
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
                        Name = "Teplota",
                        Data = teploty,
                        ShowInNavigator = true 
                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod",
                        Data = rosny_bod,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "Heat index",
                        Data = heat_index,
                        ShowInNavigator = true
                    }



                }
            };
            chartOptions.ID = "chart1";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniTeplotaAirLink(List<Quantities> quantities)
        {

            var teploty = new List<SplineSeriesData>();
            var rosny_bod = new List<SplineSeriesData>();
            var heat_index = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teploty.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.temp,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rosny_bod.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.dew_point,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                heat_index.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.heat_index,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Teplota vzduchu"
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
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
                        Text = "Teplota (°C)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hours", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
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

                Time = new Time
                {
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
                        Name = "Teplota",
                        Data = teploty,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod",
                        Data = rosny_bod,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "Heat index",
                        Data = heat_index,
                        ShowInNavigator = true
                    }



                }
            };
            chartOptions.ID = "chart2";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniTeplotaWLLAwg(List<Quantities> quantities)
        {

            var teplota_awg = new List<SplineSeriesData>();
            var teplota_max = new List<SplineSeriesData>();
            var teplota_min = new List<SplineSeriesData>();
            //var rosny_bod = new List<SplineSeriesData>();
            //var rosny_max = new List<SplineSeriesData>();
            //var rosny_min = new List<SplineSeriesData>();
            //var heat_index = new List<SplineSeriesData>();
            //var heat_index_max = new List<SplineSeriesData>();
            //var heat_index_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teplota_awg.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.temp_in,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.temp_in_max,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.temp_in_min,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
                //rosny_bod.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.dew_point_in,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});

                //rosny_max.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.dew_point_in_max,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});

                //rosny_min.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.dew_point_in_min,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});
                //heat_index.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.heat_index_in,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});

                //heat_index_max.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.heat_index_in_max,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});

                //heat_index_min.Add(new SplineSeriesData()
                //{
                //    Y = i.Quantities243.heat_index_in_min,
                //    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                //});
            }

            var chartOptions = new Highstock
            {
                Chart = new Chart()
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Teplota prům. max. min."
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum"
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
                        Text = "Teplota (°C)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Enabled = false
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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

                Time = new Time
                {
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
                        Name = "Teplota prům.",
                        Data = teplota_awg,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "Teplota max.",
                        Data = teplota_max,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "Teplota min.",
                        Data = teplota_min,
                        ShowInNavigator = true
                    },

                    //new SplineSeries
                    //{
                    //    Name = "Rosný bod prům.",
                    //    Data = rosny_bod,
                    //    ShowInNavigator = true
                    //},
                    //new SplineSeries
                    //{
                    //    Name = "Rosný bod max.",
                    //    Data = rosny_max,
                    //    ShowInNavigator = true
                    //},

                    //new SplineSeries
                    //{
                    //    Name = "Rosný bod min.",
                    //    Data = rosny_min,
                    //    ShowInNavigator = true
                    //},
                    //new SplineSeries
                    //{
                    //    Name = "Heat index prům.",
                    //    Data = heat_index,
                    //    ShowInNavigator = true
                    //},
                    //new SplineSeries
                    //{
                    //    Name = "Heat index max.",
                    //    Data = heat_index_max,
                    //    ShowInNavigator = true
                    //},

                    //new SplineSeries
                    //{
                    //    Name = "Heat index min.",
                    //    Data = heat_index_min,
                    //    ShowInNavigator = true
                    //}



                }
            };
            chartOptions.ID = "chart3";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniTeplotaAirLinkAwg(List<Quantities> quantities)
        {

            var teplota_awg = new List<SplineSeriesData>();
            var teplota_max = new List<SplineSeriesData>();
            var teplota_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teplota_awg.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.temp,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.temp_max,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.temp_min,
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
                    Text = "Teplota prům. max. min."
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum"
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
                        Text = "Teplota (°C)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Enabled = false
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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

                Time = new Time
                {
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
                        Name = "Teplota prům.",
                        Data = teplota_awg,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "Teplota max.",
                        Data = teplota_max,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "Teplota min.",
                        Data = teplota_min,
                        ShowInNavigator = true
                    },

                }
            };
            chartOptions.ID = "chart4";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }
    }
}


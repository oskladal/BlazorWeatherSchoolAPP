using System;
using System.Net.NetworkInformation;


using DBConnect.Models;
using System.Linq;
using System.Collections;
using Highsoft.Web.Mvc.Stocks;
using Highsoft.Web.Mvc.Stocks.Rendering;
using System.Reflection.Metadata.Ecma335;



//using Highsoft.Web.Mvc.Charts;
//using Highsoft.Web.Mvc.Charts.Rendering;

namespace BlazorWeatherSchoolAPP.Source
{
	public class GraphGenerator
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
                    Text = "Teplota vzduchu",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },
                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
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
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " °" + "C",
                    ValueDecimals = 1,
                    

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

        public static string BasicGraphVnitrniOvzdusiAirLink(List<Quantities> quantities)
        {

            var pm1 = new List<SplineSeriesData>();
            var pm2 = new List<SplineSeriesData>();
            var pm10 = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                pm1.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_1,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm2.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_2p5,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm10.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_10,
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
                    Text = "Množství částic",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },

                },
                Legend = new Legend
                {
                    Enabled = true,
                },

                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
                    },



                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "Množství částic (μg/m&sup3)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " μg/m&sup3",
                    ValueDecimals = 1,
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
                        Name = "PM1",
                        Data = pm1,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "PM2.5",
                        Data = pm2,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "PM10",
                        Data = pm10,
                        ShowInNavigator = true
                    }



                }
            };
            chartOptions.ID = "chart11";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniAQIAirLink(List<Quantities> quantities)
        {

            var aqi = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                aqi.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.aqi_val,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000,

                }) ;

            }

            var chartOptions = new Highstock
            {
                Chart = new Chart()
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "AQI",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },
                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {   
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
                    },


                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "AQI"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = "",
                    ValueDecimals = 1,
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
                        Name = "AQI",
                        Data = aqi,
                        ShowInNavigator = true
                    },

                }
            };
            chartOptions.ID = "chart12";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniVlhkostWLL(List<Quantities> quantities)
        {

            var vlhkost = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.hum_in,
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
                    Text = "Vlhkost vzduchu",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },
                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
                    },



                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "Vlhkost (%)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " %",
                    ValueDecimals = 1,
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
                        Name = "Vlhkost",
                        Data = vlhkost,
                        ShowInNavigator = true
                    },
                }
            };
            chartOptions.ID = "chart7";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniVlhkostAirLink(List<Quantities> quantities)
        {

            var vlhkost = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.hum,
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
                    Text = "Vlhkost vzduchu",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },
                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
                    },



                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "Vlhkost (%)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " %",
                    ValueDecimals = 1,
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
                        Name = "Vlhkost",
                        Data = vlhkost,
                        ShowInNavigator = true
                    },
                }
            };
            chartOptions.ID = "chart9";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }
        public static string BasicGraphVnitrniTlakWLL(List<Quantities> quantities)
        {

            var tlak = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                tlak.Add(new SplineSeriesData()
                {
                    Y = i.Quantities242.bar_sea_level,
                    X = ((DateTimeOffset)i.Quantities242.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Tlak vzduchu přepočtený na hladinu moře",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },
                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
                    },


                }

            },
                YAxis = new List<YAxis>
            {

                new YAxis
                {
                    Title = new YAxisTitle
                    {
                        Text = "Tlak (hPa)"
                    }
                }

            },
                RangeSelector = new RangeSelector()
                {
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " hPa",
                    ValueDecimals = 1,
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
                        Name = "Tlak",
                        Data = tlak,
                        ShowInNavigator = true
                    },

                }
            };
            chartOptions.ID = "chart5";

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
                    Text = "Teplota vzduchu",
                    Style = new Hashtable { { "fontSize", "20px" }, { "fontWeight", "bold" }, { "color", "#3f3f3f" }, { "font-family", "Arial" }, },

                },
                Legend = new Legend
                {
                    Enabled = true,
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {   
                    Title = new XAxisTitle
                    {
                        Text = "Datum a čas"
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
                    Buttons = new List<RangeSelectorButton>() { new RangeSelectorButton { Type = "hour", Count = 12, Text = "12h" }, new RangeSelectorButton { Type = "day", Count = 1, Text = "1d" }, new RangeSelectorButton { Type = "day", Count = 2, Text = "2d" }, new RangeSelectorButton { Type = "all", Count = 1, Text = "vše" } },
                    InputEnabled = false,
                    Selected = 1
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y %H:%M",
                    ValueSuffix = " °" + "C",
                    ValueDecimals = 1,
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
            chartOptions.ID = "chart3";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }
    }
}


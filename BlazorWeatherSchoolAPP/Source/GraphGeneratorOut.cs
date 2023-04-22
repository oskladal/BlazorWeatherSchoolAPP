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
    public class GraphsGeneratorOut
    {
        public static string BasicGraphVenkovniTeplota(List<Quantities> quantities)
        {

            var teploty = new List<SplineSeriesData>();
            var rosny_bod = new List<SplineSeriesData>();
            var heat_index = new List<SplineSeriesData>();
            var chlad_vetru = new List<SplineSeriesData>();
            var teplota_pudy = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                teploty.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.temp,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rosny_bod.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.dew_point,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                heat_index.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.heat_index,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                chlad_vetru.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_chill,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_pudy.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.temp_1,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Teplota"
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
                        Name = "Teplota vzduchu",
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
                    },
                     new SplineSeries
                    {
                        Name = "Chlad větru",
                        Data = chlad_vetru,
                        ShowInNavigator = true
                    },
                    new SplineSeries
                    {
                        Name = "Teplota půdy",
                        Data = teplota_pudy,
                        ShowInNavigator = true
                    }
                }
            };
            chartOptions.ID = "chart1";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniVitr(List<Quantities> quantities)
        {

            var rychlost = new List<SplineSeriesData>();
            var rychlost10 = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                rychlost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_speed_last,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rychlost10.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_speed_avg_last_10_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Rychlost větru"
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
                        Text = "Rychlost (km/h)"
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
                    ValueSuffix = " km/h",
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
                        Name = "Rychlost větru",
                        Data = rychlost,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "Rychlost větru prům. 10 min.",
                        Data = rychlost10,
                        ShowInNavigator = true
                    },

                }
            };
            chartOptions.ID = "chart3";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniVlhkost(List<Quantities> quantities)
        {

            var vlhkost = new List<SplineSeriesData>();
            var vlhkost_puda = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.hum,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_puda.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.moist_soil_1,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Vlhkost vzduchu a půdy"
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
                        Text = "Vlhkost (%)"
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
                        Name = "Vlhkost vzduchu",
                        Data = vlhkost,
                        ShowInNavigator = true
                    },

                    new SplineSeries
                    {
                        Name = "Vlhkost půdy",
                        Data = vlhkost_puda,
                        ShowInNavigator = true
                    },

                }
            };
            chartOptions.ID = "chart5";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniSrazky(List<Quantities> quantities)
        {

            var srazky_last_15 = new List<SplineSeriesData>();
            var intenzita = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                srazky_last_15.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rainfall_last_15_min_mm,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000,

                });

                intenzita.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rain_rate_last_mm,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Dešťové srážky"
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
                                Text = "mm"
                            }
                        },
                    new YAxis
                        {
                            Opposite = true,
                            Title = new YAxisTitle
                            {
                                Text = "mm/h"
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
                        Name = "Úhrn srážek za 15 min.",
                        Data = srazky_last_15,
                        ShowInNavigator = true,
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "Intenzita srážek",
                        Data = intenzita,
                        ShowInNavigator = true,
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm/h",
                        }
                    },

                }
            };
            chartOptions.ID = "chart7";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniSlunce(List<Quantities> quantities)
        {

            var intenzita = new List<SplineSeriesData>();
            var uv = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                intenzita.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.solar_rad,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000,

                });

                uv.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.uv_index,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Intenzita slunečního záření a UV index"
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
                                Text = "W/m&sup2"
                            }
                        },
                    
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
                        Name = "Intenzita slunečního záření",
                        Data = intenzita,
                        ShowInNavigator = true,
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " W/m&sup2",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "UV index",
                        Data = uv,
                        ShowInNavigator = true,
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = "",
                        }
                    },

                }
            };
            chartOptions.ID = "chart9";

            var renderer = new HighstockRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }


    }
}


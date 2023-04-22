using System;
using System.Net.NetworkInformation;


using DBConnect.Models;
using System.Linq;
using System.Collections;
using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;




//using Highsoft.Web.Mvc.Charts;
//using Highsoft.Web.Mvc.Charts.Rendering;

namespace BlazorWeatherSchoolAPP.Source
{
    public class GraphsGeneratorOutAwg
    {
        public static string BasicGraphVenkovniTeplotaAwg(List<Quantities> quantities)
        {

            var teploty = new List<SplineSeriesData>();
            var teploty_max = new List<SplineSeriesData>();
            var teploty_min = new List<SplineSeriesData>();
            var rosny_bod = new List<SplineSeriesData>();
            var rosny_bod_max = new List<SplineSeriesData>();
            var rosny_bod_min = new List<SplineSeriesData>();
            var heat_index = new List<SplineSeriesData>();
            var heat_index_max = new List<SplineSeriesData>();
            var heat_index_min = new List<SplineSeriesData>();
            var chlad_vetru = new List<SplineSeriesData>();
            var chlad_vetru_max = new List<SplineSeriesData>();
            var chlad_vetru_min = new List<SplineSeriesData>();
            var teplota_pudy = new List<SplineSeriesData>();
            var teplota_pudy_max = new List<SplineSeriesData>();
            var teplota_pudy_min = new List<SplineSeriesData>();
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

                teploty_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.temp_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rosny_bod_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.dew_point_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                heat_index_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.heat_index_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                chlad_vetru_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_chill_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_pudy_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.temp_1_max,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teploty_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.temp_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rosny_bod_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.dew_point_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                heat_index_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.heat_index_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                chlad_vetru_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_chill_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                teplota_pudy_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.temp_1_min,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

            }

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Teplota"
                },
                XAxis = new List<XAxis>
                {
                    new XAxis
                    {
                        Type = "datetime",
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

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Teplota vzduchu prům.",
                        Data = teploty,
                        ShowInLegend = false,
                        LinkedTo = "Teploty",
                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod prům.",
                        Data = rosny_bod,
                        ShowInLegend = false,
                        LinkedTo = "Rosny",
                    },
                    new SplineSeries
                    {
                        Name = "Heat index prům.",
                        Data = heat_index,                      
                        ShowInLegend = false,
                        LinkedTo = "Heat",
                    },
                    new SplineSeries
                    {
                        Name = "Chlad větru prům.",
                        Data = chlad_vetru,                    
                        ShowInLegend = false,
                        LinkedTo = "Chlad",
                    },
                    new SplineSeries
                    {
                        Name = "Teplota půdy prům.",
                        Data = teplota_pudy,                       
                        ShowInLegend = false,
                        LinkedTo = "Puda",
                    },

                    new SplineSeries
                    {
                        Name = "Teplota vzduchu max.",
                        Data = teploty_max,                       
                        ShowInLegend = false,
                        LinkedTo = "Teploty",
                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod max.",
                        Data = rosny_bod_max,                      
                        ShowInLegend = false,
                        LinkedTo = "Rosny",
                    },
                    new SplineSeries
                    {
                        Name = "Heat index max.",
                        Data = heat_index_max,                       
                        ShowInLegend = false,
                        LinkedTo = "Heat",
                    },
                    new SplineSeries
                    {
                        Name = "Chlad větru max.",
                        Data = chlad_vetru_max,                       
                        ShowInLegend = false,
                        LinkedTo = "Chlad",
                    },
                    new SplineSeries
                    {
                        Name = "Teplota půdy max.",
                        Data = teplota_pudy_max,                      
                        ShowInLegend = false,
                        LinkedTo = "Puda",
                    },

                    new SplineSeries
                    {
                        Name = "Teplota vzduchu min.",
                        Data = teploty_min,                       
                        ShowInLegend = false,
                        LinkedTo = "Teploty",

                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod min.",
                        Data = rosny_bod_min,                       
                        ShowInLegend = false,
                        LinkedTo = "Rosny",

                    },
                    new SplineSeries
                    {
                        Name = "Heat index min.",
                        Data = heat_index_min,                      
                        ShowInLegend = false,
                        LinkedTo = "Heat",
                    },
                    new SplineSeries
                    {
                        Name = "Chlad větru min.",
                        Data = chlad_vetru_min,                   
                        ShowInLegend = false,
                        LinkedTo = "Chlad",

                    },
                    new SplineSeries
                    {
                        Name = "Teplota půdy min.",
                        Data = teplota_pudy_min,
                        ShowInLegend = false,
                        LinkedTo = "Puda",

                    },

                    new SplineSeries {
                        Name = "Teplota vzduchu",
                        Id = "Teploty",
                        Visible = true,
                        ShowInLegend = true
                     },
                    new SplineSeries {
                        Name = "Rosný bod",
                        Id = "Rosny",
                        Visible = false,
                        ShowInLegend = true
                     },

                    new SplineSeries {
                        Name = "Heat index",
                        Id = "Heat",
                        Visible = false,
                        ShowInLegend = true
                     },

                    new SplineSeries {
                        Name = "Chlad větru",
                        Id = "Chlad",
                        Visible = false,
                        ShowInLegend = true
                     },

                    new SplineSeries {
                        Name = "Teplota půdy",
                        Id = "Puda",
                        Visible = false,
                        ShowInLegend = true
                     },


                }
            };


           

            chartOptions.ID = "chart2";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniVitrAwg(List<Quantities> quantities)
        {

            var rychlost = new List<SplineSeriesData>();
            var rychlost_max = new List<SplineSeriesData>();
            var rychlost_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                rychlost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_speed_avg_last_10_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rychlost_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_speed_last_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                rychlost_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.wind_speed_last_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                

            }

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Rychlost větru"
                },
                XAxis = new List<XAxis>
                {
                    new XAxis
                    {
                        Type = "datetime",
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
                            Text = "Rychlost (km/h)"
                        }
                    }

                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Rychlost prům.",
                        Data = rychlost,
                        ShowInLegend = false,
                        LinkedTo = "rychlost",
                    },

                    new SplineSeries
                    {
                        Name = "Rychlost max.",
                        Data = rychlost_max,
                        ShowInLegend = false,
                        LinkedTo = "rychlost",
                    },
                    new SplineSeries
                    {
                        Name = "Rychlost min.",
                        Data = rychlost_min,
                        ShowInLegend = false,
                        LinkedTo = "rychlost",
                    },
                  
                    new SplineSeries {
                        Name = "Rychlost větru",
                        Id = "rychlost",
                        Visible = true,
                        ShowInLegend = true
                     },
                    


                }
            };
            chartOptions.ID = "chart4";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniVlhkostAwg(List<Quantities> quantities)
        {

            var vlhkost = new List<SplineSeriesData>();
            var vlhkost_max = new List<SplineSeriesData>();
            var vlhkost_min = new List<SplineSeriesData>();
            var vlhkost_pudy = new List<SplineSeriesData>();
            var vlhkost_pudy_max = new List<SplineSeriesData>();
            var vlhkost_pudy_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.hum,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.hum_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.hum_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_pudy.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.moist_soil_1,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_pudy_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.moist_soil_1,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_pudy_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities56.moist_soil_1_min,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });



            }

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Vlhkost vzduchu a půdy prům. max. min."
                },
                XAxis = new List<XAxis>
                {
                    new XAxis
                    {
                        Type = "datetime",
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
                            Text = "Vlhkost (%)"
                        }
                    }

                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Vlhkost prům.",
                        Data = vlhkost,
                        ShowInLegend = false,
                        LinkedTo = "vlhkost",
                    },

                    new SplineSeries
                    {
                        Name = "Vlhkost max.",
                        Data = vlhkost_max,
                        ShowInLegend = false,
                        LinkedTo = "vlhkost",
                    },
                    new SplineSeries
                    {
                        Name = "Vlhkost min.",
                        Data = vlhkost_min,
                        ShowInLegend = false,
                        LinkedTo = "vlhkost",
                    },

                    new SplineSeries
                    {
                        Name = "Vlhkost půdy prům.",
                        Data = vlhkost_pudy,
                        ShowInLegend = false,
                        LinkedTo = "vlhkostpud",
                    },

                    new SplineSeries
                    {
                        Name = "Vlhkost půdy max.",
                        Data = vlhkost_pudy_max,
                        ShowInLegend = false,
                        LinkedTo = "vlhkostpud",
                    },
                    new SplineSeries
                    {
                        Name = "Vlhkost půdy min.",
                        Data = vlhkost_pudy_min,
                        ShowInLegend = false,
                        LinkedTo = "vlhkostpud",
                    },


                    new SplineSeries {
                        Name = "Vlhkost vzduchu",
                        Id = "vlhkost",
                        Visible = true,
                        ShowInLegend = true,
                     },

                    new SplineSeries {
                        Name = "Vlhkost půdy",
                        Id = "vlhkostpud",
                        Visible = false,
                        ShowInLegend = true,
                     },



                }
            };
            chartOptions.ID = "chart6";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniSrazkyAwg(List<Quantities> quantities)
        {

            var srazky = new List<SplineSeriesData>();
            var intenzita = new List<SplineSeriesData>();
            var intenzita_max = new List<SplineSeriesData>();
            var intenzita_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                srazky.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rainfall_daily_mm,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                intenzita.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rain_rate_last_mm,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                intenzita_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rain_rate_last_mm_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                intenzita_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.rain_rate_last_mm_min,
                    X = ((DateTimeOffset)i.Quantities56.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
            }

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Srážky prům. max. min."
                },
                XAxis = new List<XAxis>
                {
                    new XAxis
                    {
                        Type = "datetime",
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

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Denní úhrn srážek",
                        Data = srazky,
                        ShowInLegend = true,
                        Visible = true,
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "Intenzita srážek prům.",
                        Data = intenzita,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm/h",
                        }
                    },
                    new SplineSeries
                    {
                        Name = "Intenzita srážek min.",
                        Data = intenzita_min,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm/h",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "Intenzita srážek max.",
                        Data = intenzita_max,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " mm/h",
                        }
                    },

                    new SplineSeries {
                        Name = "Intenzita srážek",
                        Id = "int",
                        Visible = true,
                        ShowInLegend = true,
                     },

                }
            };
            chartOptions.ID = "chart8";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVenkovniSlunceAwg(List<Quantities> quantities)
        {

            var intenzita = new List<SplineSeriesData>();
            var intenzita_max = new List<SplineSeriesData>();
            var intenzita_min = new List<SplineSeriesData>();
            var uv = new List<SplineSeriesData>();
            var uv_max = new List<SplineSeriesData>();
            var uv_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                intenzita.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.solar_rad,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                intenzita_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.solar_rad_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                intenzita_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.solar_rad_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                uv.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.uv_index,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
                uv_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.uv_index_max,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
                uv_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities46.uv_index_min,
                    X = ((DateTimeOffset)i.Quantities46.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

            }

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
                {
                    Type = ChartType.Line
                },

                Title = new Title
                {
                    Text = "Intenzita slunečního záření a UV index prům. max. min."
                },
                XAxis = new List<XAxis>
                {
                    new XAxis
                    {
                        Type = "datetime",
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
                                Text = "W/m&sup2"
                            }
                        },
                },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Intenzita slunečního záření prům.",
                        Data = intenzita,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " W/m&sup2",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "Intenzita slunečního záření max.",
                        Data = intenzita_max,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " W/m&sup2",
                        }
                    },
                    new SplineSeries
                    {
                        Name = "Intenzita slunečního záření min.",
                        Data = intenzita_min,
                        ShowInLegend = false,
                        LinkedTo = "int",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = " W/m&sup2",
                        }
                    },

                    new SplineSeries
                    {
                        Name = "UV index prům.",
                        Data = uv,
                        ShowInLegend = false,
                        LinkedTo = "uv",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = "",
                        }
                    },
                    new SplineSeries
                    {
                        Name = "UV index max.",
                        Data = uv_max,
                        ShowInLegend = false,
                        LinkedTo = "uv",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = "",
                        }
                    },
                    new SplineSeries
                    {
                        Name = "UV index min.",
                        Data = uv_min,
                        ShowInLegend = false,
                        LinkedTo = "uv",
                        Tooltip = new SplineSeriesTooltip
                        {
                            ValueSuffix = "",
                        }
                    },

                    new SplineSeries {
                        Name = "Intenzita slunečního záření",
                        Id = "int",
                        Visible = true,
                        ShowInLegend = true,
                     },

                    new SplineSeries {
                        Name = "UV index",
                        Id = "int",
                        Visible = false,
                        ShowInLegend = true,
                     },

                }
            };
            chartOptions.ID = "chart8";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

    }
}


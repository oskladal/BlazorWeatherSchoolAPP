using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect.Models;
using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using Microsoft.AspNetCore.Builder;

namespace BlazorWeatherSchoolAPP.Source
{
    public class GraphGeneratorAwg
    {
        
       
            public static string BasicGraphVnitrniTeplotaWLLAwg(List<Quantities> quantities)
            {

                var teplota_awg = new List<SplineSeriesData>();
                var teplota_max = new List<SplineSeriesData>();
                var teplota_min = new List<SplineSeriesData>();
                var dewpoint = new List<SplineSeriesData>();
                var dewpoint_max = new List<SplineSeriesData>();
                var dewpoint_min = new List<SplineSeriesData>();
                var heatindex = new List<SplineSeriesData>();
                var heatindex_max = new List<SplineSeriesData>();
                var heatindex_min = new List<SplineSeriesData>();
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

                    dewpoint.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.dew_point_in,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                    });

                    dewpoint_max.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.dew_point_in_max,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                    });

                    dewpoint_min.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.dew_point_in_min,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                    });
                    heatindex.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.heat_index_in,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                    });

                    heatindex_max.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.heat_index_in_max,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                    });

                    heatindex_min.Add(new SplineSeriesData()
                    {
                        Y = i.Quantities243.heat_index_in_min,
                        X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                        Text = "Teplota prům. max. min."
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
                        Name = "Teplota prům.",
                        Data = teplota_awg,
                        ShowInLegend = false,
                        LinkedTo = "temp",

                    },

                    new SplineSeries
                    {
                        Name = "Teplota max.",
                        Data = teplota_max,
                        ShowInLegend = false,
                        LinkedTo = "temp",

                    },
                    new SplineSeries
                    {
                        Name = "Teplota min.",
                        Data = teplota_min,
                        ShowInLegend = false,
                        LinkedTo = "temp",

                    },
                    new SplineSeries
                    {
                        Name = "Rosný bod prům.",
                        Data = dewpoint,
                        ShowInLegend = false,
                        LinkedTo = "ros",

                    },

                    new SplineSeries
                    {
                        Name = "Rosný bod max.",
                        Data = dewpoint_max,
                        ShowInLegend = false,
                        LinkedTo = "ros",

                    },
                    new SplineSeries
                    {
                        Name = "Rosný bod min.",
                        Data = dewpoint_min,
                        ShowInLegend = false,
                        LinkedTo = "ros",

                    },
                    new SplineSeries
                    {
                        Name = "Heat index prům.",
                        Data = heatindex,
                        ShowInLegend = false,
                        LinkedTo = "heat",

                    },

                    new SplineSeries
                    {
                        Name = "Heat index max.",
                        Data = heatindex_max,
                        ShowInLegend = false,
                        LinkedTo = "heat",

                    },
                    new SplineSeries
                    {
                        Name = "Heat index min.",
                        Data = heatindex_min,
                        ShowInLegend = false,
                        LinkedTo = "heat",

                    },

                    new SplineSeries {
                        Name = "Teplota vzduchu",
                        Id = "temp",
                        Visible = true,
                        ShowInLegend = true
                     },
                    new SplineSeries {
                        Name = "Rosný bod",
                        Id = "ros",
                        Visible = false,
                        ShowInLegend = true
                     },
                    new SplineSeries {
                        Name = "Heat index",
                        Id = "heat",
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

            var chartOptions = new Highcharts
            {
                Chart = new Highsoft.Web.Mvc.Charts.Chart
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
                        Name = "Teplota prům.",
                        Data = teplota_awg,
                        ShowInLegend = false,
                        LinkedTo = "Teploty",
                    },

                    new SplineSeries
                    {
                        Name = "Teplota max.",
                        Data = teplota_max,
                        ShowInLegend = false,
                        LinkedTo = "Teploty",
                    },
                    new SplineSeries
                    {
                        Name = "Teplota min.",
                        Data = teplota_min,
                        ShowInLegend = false,
                        LinkedTo = "Teploty",
                    },

                    new SplineSeries {
                        Name = "Teplota vzduchu",
                        Id = "Teploty",
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

        public static string BasicGraphVnitrniTlakWLLAwg(List<Quantities> quantities)
        {

            var tlak_awg = new List<SplineSeriesData>();
            var tlak_max = new List<SplineSeriesData>();
            var tlak_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                tlak_awg.Add(new SplineSeriesData()
                {
                    Y = i.Quantities242.bar_sea_level,
                    X = ((DateTimeOffset)i.Quantities242.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                tlak_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities242.bar_sea_level_max,
                    X = ((DateTimeOffset)i.Quantities242.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                tlak_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities242.bar_sea_level_min,
                    X = ((DateTimeOffset)i.Quantities242.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Tlak vzduchu přepočtený na hladinu moře prům. max. min."
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
                        Text = "Tlak (hPa)"
                    }
                }

            },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "Tlak prům.",
                        Data = tlak_awg,
                        ShowInLegend = false,
                        LinkedTo = "tlak",
                    },

                    new SplineSeries
                    {
                        Name = "Tlak max.",
                        Data = tlak_max,
                        ShowInLegend = false,
                        LinkedTo = "tlak",
                    },
                    new SplineSeries
                    {
                        Name = "Tlak min.",
                        Data = tlak_min,
                        ShowInLegend = false,
                        LinkedTo = "tlak",
                    },

                     new SplineSeries {
                        Name = "Tlak vzduchu",
                        Id = "tlak",
                        Visible = true,
                        ShowInLegend = true
                     },

                }
            };
            chartOptions.ID = "chart6";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniVlhkostWLLAwg(List<Quantities> quantities)
        {

            var vlhkost_awg = new List<SplineSeriesData>();
            var vlhkost_max = new List<SplineSeriesData>();
            var vlhkost_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost_awg.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.hum_in,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.hum_in_max,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities243.hum_in_min,
                    X = ((DateTimeOffset)i.Quantities243.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Vlhkost prům. max. min."
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
                        Data = vlhkost_awg,
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

                    new SplineSeries {
                        Name = "Vlhkost vzduchu",
                        Id = "vlhkost",
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

        public static string BasicGraphVnitrniVlhkostAirLinkAwg(List<Quantities> quantities)
        {

            var vlhkost_awg = new List<SplineSeriesData>();
            var vlhkost_max = new List<SplineSeriesData>();
            var vlhkost_min = new List<SplineSeriesData>();
            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                vlhkost_awg.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.hum,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.hum_max,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                vlhkost_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.hum_min,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Vlhkost prům. max. min."
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {   Type = "datetime",
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
                        Data = vlhkost_awg,
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

                    new SplineSeries {
                        Name = "Vlhkost vzduchu",
                        Id = "vlhkost",
                        Visible = true,
                        ShowInLegend = true,
                     },

                }
            };
            chartOptions.ID = "chart10";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniOvzdusiAirLinkAwg(List<Quantities> quantities)
        {

            var pm1 = new List<SplineSeriesData>();
            var pm2 = new List<SplineSeriesData>();
            var pm10 = new List<SplineSeriesData>();
            var pm1_max = new List<SplineSeriesData>();
            var pm2_max = new List<SplineSeriesData>();
            var pm10_max = new List<SplineSeriesData>();
            var pm1_min = new List<SplineSeriesData>();
            var pm2_min = new List<SplineSeriesData>();
            var pm10_min = new List<SplineSeriesData>();
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
                pm1_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_1_max,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm2_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_2p5_max,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm10_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_10_max,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });
                pm1_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_1_min,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm2_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_2p5_min,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                pm10_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.pm_10_min,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "Množství části prům. max. min."
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {   Type = "datetime",
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
                        Text = "μg/m&sup3"
                    }
                }

            },


                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "PM1 prům.",
                        Data = pm1,
                        ShowInLegend = false,
                        LinkedTo = "pm1",
                    },

                    new SplineSeries
                    {
                        Name = "PM1 max.",
                        Data = pm1_max,
                        ShowInLegend = false,
                        LinkedTo = "pm1",
                    },
                    new SplineSeries
                    {
                        Name = "PM1 min.",
                        Data = pm1_min,
                        ShowInLegend = false,
                        LinkedTo = "pm1",
                    },
                    new SplineSeries
                    {
                        Name = "PM2.5 prům.",
                        Data = pm2,
                        ShowInLegend = false,
                        LinkedTo = "pm2",
                    },

                    new SplineSeries
                    {
                        Name = "PM2.5 max.",
                        Data = pm2_max,
                        ShowInLegend = false,
                        LinkedTo = "pm2",
                    },
                    new SplineSeries
                    {
                        Name = "PM2.5 min.",
                        Data = pm2_min,
                        ShowInLegend = false,
                        LinkedTo = "pm2",
                    },
                    new SplineSeries
                    {
                        Name = "PM10 prům.",
                        Data = pm10,
                        ShowInLegend = false,
                        LinkedTo = "pm10",
                    },

                    new SplineSeries
                    {
                        Name = "PM10 max.",
                        Data = pm10_max,
                        ShowInLegend = false,
                        LinkedTo = "pm10",
                    },
                    new SplineSeries
                    {
                        Name = "PM10 min.",
                        Data = pm10_min,
                        ShowInLegend = false,
                        LinkedTo = "pm10",
                    },

                    new SplineSeries {
                        Name = "PM1",
                        Id = "pm1",
                        Visible = true,
                        ShowInLegend = true,
                     },

                    new SplineSeries {
                        Name = "PM2.5",
                        Id = "pm2",
                        Visible = false,
                        ShowInLegend = true,
                     },

                    new SplineSeries {
                        Name = "PM10",
                        Id = "pm10",
                        Visible = false,
                        ShowInLegend = true,
                     },

                }
            };
            chartOptions.ID = "chart13";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }

        public static string BasicGraphVnitrniAQIAirLinkAwg(List<Quantities> quantities)
        {

            var aqi = new List<SplineSeriesData>();
            var aqi_max = new List<SplineSeriesData>();
            var aqi_min = new List<SplineSeriesData>();

            List<long> datumy = new List<long>();

            foreach (var i in quantities)
            {
                aqi.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.aqi_val,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                aqi_max.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.aqi_val_max,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
                });

                aqi_min.Add(new SplineSeriesData()
                {
                    Y = i.Quantities326.aqi_val_min,
                    X = ((DateTimeOffset)i.Quantities326.ts.ToLocalTime()).ToUnixTimeSeconds() * 1000
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
                    Text = "AQI prům. max. min."
                },
                XAxis = new List<XAxis>
            {
                new XAxis
                {   Type = "datetime",
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
                        Text = "AQI"
                    }
                }

            },

                Tooltip = new Tooltip
                {
                    Shared = true,
                    XDateFormat = "%d.%m.%Y",
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
                        Name = "AQI prům.",
                        Data = aqi,
                        ShowInLegend = false,
                        LinkedTo = "int",
                    },

                    new SplineSeries
                    {
                        Name = "AQI max.",
                        Data = aqi_max,
                        ShowInLegend = false,
                        LinkedTo = "int",
                    },
                    new SplineSeries
                    {
                        Name = "AQI min.",
                        Data = aqi_min,
                        ShowInLegend = false,
                        LinkedTo = "int",
                    },

                    new SplineSeries {
                        Name = "AQI",
                        Id = "int",
                        Visible = true,
                        ShowInLegend = true,
                     },
                }
            };
            chartOptions.ID = "chart14";

            var renderer = new HighchartsRenderer(chartOptions);
            string chart = renderer.GetJsonOptionsForBlazor();
            return chart;
        }
    }
}


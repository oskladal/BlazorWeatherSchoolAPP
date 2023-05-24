var lang_cs = {
	months: [
		"Leden",
		"Únor",
		"Březen",
		"Duben",
		"Květen",
		"Červen",
		"Červenec",
		"Srpen",
		"Září",
		"Říjen",
		"Listopad",
		"Prosinec"
	],
	weekdays: [
		"Neděle",
		"Pondělí",
		"Úterý",
		"Středa",
		"Čtvrtek",
		"Pátek",
		"Sobota",

	],
	downloadJPEG: "Stáhnout obrázek JPEG",
	downloadPDF: "Stáhnout dokument PDF",
	downloadPNG: "Stáhnout obrázek PNG",
	downloadSVG: "Stáhnout obrázek SVG",
	printChart: "Tisknout graf",
	resetZoom: "Resetovat zoom",
	resetZoomTitle: "Resetovat zoom",
	viewInFullScreen: "Zobrazit na celou obrazovku",
	downloadCSV: "Stáhnout dokument CSV",
	downloadXLS: "Stáhnout dokument XLS",
	viewDataTabel: "Zobrazit tabulku hodnot",
	hideDataTabel: "Skrýt tabulku hodnot",
	getWeekDays: function () {
		return this.weekdays;
	},
	getMonths: function () {
		return this.months;
	},
	getShortWeekDays: function () {
		return this.weekdays.map(function (day) {
			return day.substring(0, 3);
		});
	},
	getShortMonths: function () {
		return this.months.map(function (month) {
			return month.substring(0, 3);
		});
	}
}

Highcharts.setOptions({
	lang: {
		months: lang_cs.getMonths(),
		weekdays: lang_cs.getWeekDays(),
		shortWeekdays: lang_cs.getShortWeekDays(),
		shortMonths: lang_cs.getShortMonths(),
		downloadJPEG: lang_cs.downloadJPEG,
		downloadPDF: lang_cs.downloadPDF,
		downloadPNG: lang_cs.downloadPNG,
		downloadSVG: lang_cs.downloadSVG,
		printChart: lang_cs.printChart,
		resetZoom: lang_cs.resetZoom,
		resetZoomTitle: lang_cs.resetZoomTitle,
		viewFullscreen: lang_cs.viewInFullScreen,
		downloadCSV: lang_cs.downloadCSV,
		downloadXLS: lang_cs.downloadXLS,
		viewData: lang_cs.viewDataTabel,
		hideData: lang_cs.hideDataTabel,

	}
});


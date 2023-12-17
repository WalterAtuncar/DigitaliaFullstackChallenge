import { Component, OnInit } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexStroke,
  ApexMarkers,
  ApexYAxis,
  ApexGrid,
  ApexTooltip,
  ApexLegend,
} from 'ng-apexcharts';
export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  colors: string[];
  yaxis: ApexYAxis;
  grid: ApexGrid;
  legend: ApexLegend;
  tooltip: ApexTooltip;
};
@Component({
  selector: 'app-dashboard2',
  templateUrl: './dashboard2.component.html',
  styleUrls: ['./dashboard2.component.scss'],
})
export class Dashboard2Component implements OnInit {
  public lineChartOptions: Partial<ChartOptions>;
  public areaUsageChartOptions: Partial<ChartOptions>;
  breadscrums = [
    {
      title: 'Dashboad',
      items: [],
      active: 'Resultados',
    },
  ];
  public surveys: any[] = [];
  constructor() {}

  ngOnInit() {
    this.areaUsageChart();
    this.surveys = this.getSurveys();
  }
  getSurveys(): any[] {
    const mockSurveys = [
      {
        title: 'Encuesta 1',
        question: 'Pregunta de la Encuesta 1',
        categories: ['Ene', 'Feb', 'Mar'],
        series1Data: [10, 20, 15],
        series2Data: [15, 5, 35],
        series3Data: [20, 30, 10]
      },
      {
        title: 'Encuesta 2',
        question: 'Pregunta de la Encuesta 2',
        categories: ['Abr', 'May', 'Jun'],
        series1Data: [20, 30, 40],
        series2Data: [35, 15, 45],
        series3Data: [30, 40, 10]
      },
      // Añadir más encuestas según sea necesario
    ];

    return mockSurveys.map(survey => ({
      title: survey.title,
      question: survey.question,
      chartOptions: this.createChartOptions(survey.categories, survey.series1Data, survey.series2Data, survey.series3Data)
    }));
  }
  private createChartOptions(categories: string[], series1Data: number[], series2Data: number[], series3Data: number[]): Partial<ChartOptions> {
    return {
      series: [
        {
          name: 'Aprueba',
          data: series1Data
        },
        {
          name: 'Desaprueba',
          data: series2Data
        },
        {
          name: 'No precisa',
          data: series3Data
        }
      ],
      chart: {
        height: 350,
        type: 'line'
      },
      xaxis: {
        categories: categories
      }
    };
  }
  
  private areaUsageChart() {
    this.areaUsageChartOptions = {
        series: [
            {
                name: 'Aprueba',
                data: [20, 25, 15, 30, 28, 22, 18],
            },
            {
                name: 'Desaprueba',
                data: [10, 8, 12, 15, 9, 11, 13],
            },
            {
                name: 'No precisa',
                data: [15, 18, 14, 20, 19, 15, 17],
            }
        ],
        chart: {
            height: 270,
            type: 'line',
            foreColor: '#9aa0ac',
            dropShadow: {
                enabled: true,
                color: '#000',
                top: 18,
                left: 7,
                blur: 10,
                opacity: 0.2,
            },
            toolbar: {
                show: false,
            },
        },
        colors: ['#9F78FF', '#858585', '#FF5733', '#33FF57'],
        stroke: {
            curve: 'smooth',
        },
        grid: {
            row: {
                colors: ['transparent', 'transparent'],
                opacity: 0.5,
            },
        },
        markers: {
            size: 3,
        },
        xaxis: {
            categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul'],
            title: {
                text: 'Mes',
            },
        },
        yaxis: {
            min: 0,
            max: 40,
        },
        legend: {
            position: 'top',
            horizontalAlign: 'right',
            floating: true,
            offsetY: -25,
            offsetX: -5,
        },
        tooltip: {
            theme: 'dark',
            marker: {
                show: true,
            },
            x: {
                show: true,
            },
        },
    };
}

}

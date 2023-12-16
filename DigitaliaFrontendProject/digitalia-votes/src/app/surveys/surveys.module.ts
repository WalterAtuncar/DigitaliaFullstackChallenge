import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SurveysRoutingModule } from './surveys-routing.module';
import { SurveyListComponent } from './survey-list/survey-list.component';
import { SurveyVoteComponent } from './survey-vote/survey-vote.component';
import { SurveyResultsComponent } from './survey-results/survey-results.component';


@NgModule({
  declarations: [
    SurveyListComponent,
    SurveyVoteComponent,
    SurveyResultsComponent
  ],
  imports: [
    CommonModule,
    SurveysRoutingModule
  ]
})
export class SurveysModule { }

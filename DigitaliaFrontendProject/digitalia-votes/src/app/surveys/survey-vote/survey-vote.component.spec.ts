import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyVoteComponent } from './survey-vote.component';

describe('SurveyVoteComponent', () => {
  let component: SurveyVoteComponent;
  let fixture: ComponentFixture<SurveyVoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyVoteComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SurveyVoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

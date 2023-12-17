import { Component, OnInit } from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { validateVote, Vote } from '../all-surveys/surveys';
import { TeachersService } from '../all-surveys/teachers.service';

@Component({
  selector: 'app-edit-teacher',
  templateUrl: './edit-teacher.component.html',
  styleUrls: ['./edit-teacher.component.sass'],
})
export class EditTeacherComponent implements OnInit{
  rowData: any;
  
  breadscrums = [
    {
      title: 'Votación de Encuesta',
      items: ['Voto'],
      active: 'Encuesta',
    },
  ];
  voteReq : Vote={
    voteID: 0,
    surveyID: 0,
    optionID: 0,
    userID: 0,
    voteDate: null
  }
  selectedOption: number = 0;
  voteOptions = ['Aprueba', 'Desaprueba', 'No precisa'];
  userID: number;
  validateVote: validateVote ={
    userID: 0,
    surveyID: 0
  };
  block: boolean = false;
  txtLabel: string ='';
  constructor(private fb: UntypedFormBuilder,public teachersService: TeachersService, 
    private snackBar: MatSnackBar,private router: Router) {
    this.rowData = this.teachersService.getData();
    this.userID = JSON.parse(localStorage.getItem('currentUser')).userID;
    this.validateVote.surveyID = this.rowData.surveyID;
    this.validateVote.userID = this.userID;
  }
  ngOnInit(): void {
    
    this.teachersService.validateVote(this.validateVote).subscribe(res =>{
      if(res > 0){
        this.selectedOption = res;
        this.block = true;
        this.txtLabel ='Ya se realizó esta encuesta. ';
      }
    })
    if(this.rowData.isActive == false){
      this.block = true;
      this.txtLabel = this.txtLabel + 'La encuesta esta inactiva'
    }
  }
    
  submitVote() {
    if(this.selectedOption == 0){
      this.showNotification(
        'snackbar-danger',
        'Debe elegir una opción...!!!',
        'bottom',
        'center'
      );
      return;
    }
    this.voteReq.surveyID = this.rowData.surveyID;
    this.voteReq.optionID = this.selectedOption;
    this.voteReq.userID = this.userID;
    this.teachersService.insertVotes(this.voteReq).subscribe(res => {
      if(res >0){
        this.showNotification(
          'snackbar-succsess',
          'Voto exitosamente...!!!',
          'bottom',
          'center'
        );
        this.router.navigate(['/admin/dashboard/dashboard2']);
      }
    })
  }
  showNotification(colorName, text, placementFrom, placementAlign) {
    this.snackBar.open(text, '', {
      duration: 2000,
      verticalPosition: placementFrom,
      horizontalPosition: placementAlign,
      panelClass: colorName,
    });
  }
}

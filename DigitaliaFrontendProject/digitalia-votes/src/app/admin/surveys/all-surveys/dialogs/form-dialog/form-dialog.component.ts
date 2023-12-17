import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { TeachersService } from '../../teachers.service';
import {
  UntypedFormControl,
  Validators,
  UntypedFormGroup,
  UntypedFormBuilder,
} from '@angular/forms';
import { Teachers } from '../../teachers.model';
import { Survey } from '../../surveys';
@Component({
  selector: 'app-form-dialog',
  templateUrl: './form-dialog.component.html',
  styleUrls: ['./form-dialog.component.scss'],
})
export class FormDialogComponent {
  action: string;
  dialogTitle: string;
  proForm: UntypedFormGroup;
  teachers: Teachers;
  user: Survey ={
    surveyID: 0,
    userID: 0,
    title: '',
    description: '',
    question: ''
  };

  imageError: string;
  isImageSaved: boolean;
  cardImageBase64: string;
  imagenBase64: string;
  userID: number;

  constructor(
    public dialogRef: MatDialogRef<FormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public teachersService: TeachersService,
    private fb: UntypedFormBuilder
  ) {
    this.userID = JSON.parse(localStorage.getItem('currentUser')).userID;
    console.log("userID", this.userID)
    this.action = data.action;
    if (this.action === 'edit') {
      this.dialogTitle = 'Editar Encuesta ' + this.data.user.surveyID;
      this.user = data.user;
      if(data.user.profilePicture!=undefined){
        this.cardImageBase64 = data.user.profilePicture;
        this.isImageSaved = true;
      }else{
        this.isImageSaved = false;
      }
      
    } else {
      this.dialogTitle = 'Nueva Encuesta';      
    }
    this.proForm = this.createContactForm();
  }
  formControl = new UntypedFormControl('', [
    Validators.required,
  ]);
  getErrorMessage() {
    return this.formControl.hasError('required')
      ? 'Required field'
      : this.formControl.hasError('email')
      ? 'Not a valid email'
      : '';
  }
  createContactForm(): UntypedFormGroup {
    return this.fb.group({
      title: [this.user.title],
      description: [this.user.description],
      question: [this.user.question]
    });
  }
  submit() {
    console.log(this.proForm.value);
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  public confirmAdd(): void {
    let data =this.proForm.getRawValue();
    this.user.title = data.title;
    this.user.question = data.question;
    this.user.description = data.description;
    this.user.userID = this.userID;

    if(this.user.surveyID == 0){
      this.user.creationDate = null;
      this.user.isActive = true;
      this.teachersService.insert(this.user).subscribe(res =>{
        if(res > 0){
          this.dialogRef.close(1);
        }else{
          this.dialogRef.close(0);
        }
      })

    }else{
      this.teachersService.update(this.user).subscribe(res =>{
        if(res){
          console
          this.dialogRef.close(1);
        }else{
          this.dialogRef.close(0);
        }
      })
    }
  }
}

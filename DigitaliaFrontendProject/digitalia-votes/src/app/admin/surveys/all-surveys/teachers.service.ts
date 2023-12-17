import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Teachers } from './teachers.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { User } from '../all-surveys/user.model';
import { Survey, Vote } from './surveys';
import { environment } from 'src/environments/environment';
@Injectable()
export class TeachersService extends UnsubscribeOnDestroyAdapter {
 
  
  
  private dataVote: any;
  isTblLoading = true;
  dataChange: BehaviorSubject<User[]> = new BehaviorSubject<User[]>([]);
  // Temporarily stores data from dialogs
  dialogData: any;
  userList: User[];
  constructor(private httpClient: HttpClient) {
    super();
  }
  get data(): User[] {
    return this.dataChange.value;
  }
  getDialogData() {
    return this.dialogData;
  }
  setdataVote(dataVote: any) {
    this.dataVote = dataVote;
  }

  getData() {
    return this.dataVote;
  }

  // Llámalo para borrar los datos cuando ya no se necesiten
  clearData() {
    this.dataVote = undefined;
  }
  getAllUsers() : Observable<Survey[]>{
    return this.httpClient.get<any>(`${environment.api}/surveys`)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  getResults(surveyID: number) {
    return this.httpClient.get<any>(`${environment.api}/votes/getResults/`+surveyID)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  insert(obj: Survey):  Observable<any> {
    return this.httpClient.post<any>(`${environment.api}/surveys`, obj)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  insertVotes(voteReq: Vote):  Observable<any> {
    return this.httpClient.post<any>(`${environment.api}/votes`, voteReq)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  validateVote(validateVote: any):  Observable<any> {
    return this.httpClient.post<any>(`${environment.api}/votes/validateVote`, validateVote)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  update(obj: Survey):  Observable<any> {
    return this.httpClient.put<any>(`${environment.api}/surveys`, obj)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
  deleteTeachers(obj: Survey): Observable<any> {
    obj.isActive = false;
    return this.httpClient.put<any>(`${environment.api}/surveys`, obj)
    .pipe(
      map((res) =>{
        return res.objModel
      })
    );
  }
}

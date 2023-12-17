import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { User, UserLogin, UserRegister } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(user : UserLogin) {
    return this.http
      .post<any>(`${environment.api}/authentication/login`, user)
      .pipe(
        map((user) => {
          user.objModel.role ='Admin';
          user.objModel.token = user.token;
          localStorage.setItem('currentUser', JSON.stringify(user.objModel));
          this.currentUserSubject.next(user.objModel);
          return user;
        })
      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    return of({ success: false });
  }

  registerUser(userRq: UserRegister) {
    return this.http
      .post<any>(`${environment.api}/users`, userRq)
      .pipe(
        map((res) => {         
          return res;
        })
      );
  }
}

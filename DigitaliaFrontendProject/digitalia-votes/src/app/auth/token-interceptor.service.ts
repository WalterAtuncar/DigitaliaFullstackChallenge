import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService  implements HttpInterceptor {

  constructor() { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const tokenizedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.getToken()}`
      }
    });
    return next.handle(tokenizedReq);
  }

  private getToken(): string {
    const token = localStorage.getItem('token');
    return token ? token : '';
  }
}

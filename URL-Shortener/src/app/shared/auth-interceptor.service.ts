import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { ActivatedRoute, Router } from '@angular/router';
  import { throwError } from 'rxjs';
  import { catchError, map } from 'rxjs/operators';
  
  @Injectable()
  export class AuthInterceptorService implements HttpInterceptor {
    constructor(private router: Router, private route: ActivatedRoute) {}
  
    intercept(req: HttpRequest<any>, next: HttpHandler) {
      const authCode = JSON.parse(localStorage.getItem('user') ?? '')?.accessToken;
      const authReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + authCode),
      });
  
      return next.handle(authReq).pipe(
        map((event: HttpEvent<any>) => {
          return event;
        }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            localStorage.clear();
            this.router.navigate(['auth'], {
              relativeTo: this.route,
            });
          }
          return throwError(error);
        })
      );
    }
  }
  
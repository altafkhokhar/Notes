import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

import { UserService } from '../Service/userservice';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private userService: UserService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.userService.isLogged()) {
            
            var token = JSON.parse(localStorage.getItem(this.userService.userItem)).token;
            console.log(token);
           
            request = request.clone({
                setHeaders: {
                    Authorization: 'Bearer '+ token
                }
            });
        }

        return next.handle(request).pipe(catchError(err => {
            console.log(err);
            if (err.status == "400") {
                alert('UserName or email already exist!');
                return;
            }
            //if (err.status === 401) {
            //    // auto logout if 401 response returned from api
            //    this.userService.logout();
            //    location.reload(true);
            //    this.router.navigateByUrl('/sign-up'); 
            //}            

            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}

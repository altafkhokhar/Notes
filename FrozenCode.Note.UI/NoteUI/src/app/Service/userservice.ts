import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRouteSnapshot, Router } from '@angular/router';

import { iApiResult } from "../shared/iApiResult";
import {  User, IUser, ILoginUser } from '../shared/note-detail.model';


@Injectable({
  providedIn: 'root'
})

export class UserService {
   
    readonly userRootURL = 'http://localhost:50210/api/Users/';
    result: iApiResult<User>;
    userItem: string = "currentUser";

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    
    constructor(private httpClient: HttpClient, private router: Router) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    signUp(user: IUser) { 
        console.log(user);
        this.httpClient.post(this.userRootURL + 'RegisterUser', user).toPromise()
            .then(res => {
                if (res === true) {
                    console.log(res);
                    alert('User registered succesfully!');
                    this.router.navigateByUrl('/sign-in'); 
                }
               
            }).catch(rejected => this.handleError(rejected));
    }

    handleError(response) {
        console.log(response);
        if (response.status == "400") {
            alert('UserName or email already exist!')
        }
        else
            alert('Error occured while processing!');

    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem(this.userItem);
        this.currentUserSubject.next(null);
        this.router.navigateByUrl('/sign-in'); 
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
     

        var data = { "UserName": username, "Password": password };

        this.httpClient.post<any>(this.userRootURL + 'Authenticate', data).toPromise()
            .then(res => {
                // store user details and basic auth credentials in local storage to keep user logged in between page refreshes
                //res.authdata = window.btoa(username + ':' + password);
                localStorage.setItem(this.userItem, JSON.stringify(res));
                this.currentUserSubject.next(res);

                this.router.navigateByUrl('/list-notes'); 
            });
    }

    isLogged() {
       
        return localStorage.getItem(this.userItem) === null ? false : true;

    }
}

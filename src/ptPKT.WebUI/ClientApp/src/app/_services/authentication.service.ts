import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { UserInfo } from '../_models/userinfo.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<UserInfo>;
    public currentUser: Observable<UserInfo>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<UserInfo>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    login(email: string, password: string) {
        return this.http.post<UserInfo>(`/api/Auth/SignIn`, { email: email, password: password })
            .pipe(
                map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}

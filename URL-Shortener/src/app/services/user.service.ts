import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthUser } from '../models/auth-user.model';
import { AuthenticateUserService } from '../shared/authenticate-user.service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(
        private http: HttpClient,
        protected authenticateUserService: AuthenticateUserService,
        private router: Router
    ) { }

    login(user: {}) {
        this.http
            .post<AuthUser>('https://localhost:7058/Authenticate', user)
            .subscribe({
                next: (user: AuthUser) => {
                    localStorage.setItem('user', JSON.stringify(user));
                    this.authenticateUserService.user.next(user);
                    this.router.navigate(['']);
                }
            });
    }

    signUp(user: {}): string {
        this.http
            .post('https://localhost:7058/', user)
            .subscribe({
                next: () => 'Registration Successful!',
                error: () => 'Registration Failed! Please try again.'
            });
        return '';
    }
}
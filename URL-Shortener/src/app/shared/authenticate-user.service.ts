import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AuthUser } from '../models/auth-user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticateUserService {
  user = new BehaviorSubject<AuthUser>({
    accessToken: '',
    email: '',
    firstName: '',
    phoneNumber: '',
    lastName: '',
    refreshToken: '',
    refreshTokenExpiryTime: '',
    userName: ''
  });
}

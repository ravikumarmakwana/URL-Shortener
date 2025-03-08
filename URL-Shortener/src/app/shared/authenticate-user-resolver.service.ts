import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthUser } from '../models/auth-user.model';
import { AuthenticateUserService } from './authenticate-user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticateUserResolverService implements Resolve<AuthUser> {
  constructor(private authenticateUserService: AuthenticateUserService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.authenticateUserService.user.next(
      JSON.parse(localStorage.getItem('user') ?? '')
    );
    return JSON.parse(localStorage.getItem('user') ?? '');
  }
}

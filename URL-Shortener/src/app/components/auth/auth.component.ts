import { Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-auth',
  imports: [FormsModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  @ViewChild('signUp') signUpForm!: NgForm;
  @ViewChild('login') loginForm!: NgForm;
  signInMessage!: string;

  constructor(
    private userService: UserService
  ) { }

  onLogin() {
    this.userService.login(this.loginForm.form.value);
  }

  onSignUp() {
    this.signInMessage = this.userService.signUp(this.signUpForm.form.value);
  }
}

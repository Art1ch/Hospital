import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { LoginRequestModel } from '../../models/login-request-model';
import { AuthService } from '../../services/auth-service';
import { RegisterRequestModel } from '../../models/register-request-model';

@Component({
  selector: 'app-auth-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './auth-form.html',
  styleUrl: './auth-form.scss',
})
export class AuthForm {
  authForm: FormGroup;
  isLoginMode = true;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
  ) 
  {
    this.authForm = this.fb.group({
      email: [''],
      password: [''],
      phone: [''],
    });
  }

  onSubmit(): void {
    const formData = this.authForm.value;
    
    if (this.isLoginMode) {
      const request: LoginRequestModel = {
        email: formData.email,
        password: formData.password,
      } 
      this.authService.login(request).subscribe({
        complete: () => { this.authService },
        error: (error) => { console.error(error) },
      });
    } 
    else {
      const request: RegisterRequestModel = {
        email: formData.email,
        password: formData.password,
        phoneNumber: formData.phone ? formData.phoneNumber : ''
      }
      this.authService.register(request).subscribe({
        complete: () => { this.authService },
        error: (error) => { console.error(error) }
      });
    }
  }

  switchMode(): void {
    this.isLoginMode = !this.isLoginMode;
    this.authForm.reset();
  }
}

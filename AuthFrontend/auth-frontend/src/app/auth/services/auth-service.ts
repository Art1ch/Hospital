import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequestModel } from '../models/login-request-model';
import { RegisterRequestModel } from '../models/register-request-model';
import { AuthResponseModel } from '../models/auth-response-model';
import { environments } from '../../../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = environments.authApiUrl;

  constructor(private httpClient: HttpClient)
  {}
  
  register(request: RegisterRequestModel) : Observable<AuthResponseModel> {
    const requestBody = {
      email: request.email,
      password: request.password,
      phoneNumber: request.phoneNumber
    }
    const observer = this.httpClient.post<AuthResponseModel>(this.apiUrl, {body: requestBody});
    
    return observer;
  }

  login(request: LoginRequestModel) : Observable<AuthResponseModel> {
    const requestBody = {
      email: request.email,
      password: request.password,
    }
    const observer = this.httpClient.post<AuthResponseModel>(this.apiUrl, {body: requestBody});
    
    return observer;
  }
}

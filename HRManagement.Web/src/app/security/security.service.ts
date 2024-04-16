import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { LoginModel } from './login/login-model';
import { RegisterModel } from './register/register-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthResponseModel } from './auth-response-model';
import { UserModel } from './user-model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService extends CrudService<LoginModel | RegisterModel> {

  user$: BehaviorSubject<UserModel | undefined> = new BehaviorSubject<UserModel | undefined>(undefined);

  constructor(injector: Injector,
    private _cookieService: CookieService
  ) {
    super(injector);
  }

  login(model: LoginModel): Observable<AuthResponseModel> {
    return this.httpClient.post<AuthResponseModel>(`${this.APIUrl}/Login`, model)
  }

  register(model: RegisterModel): Observable<AuthResponseModel> {
    return this.httpClient.post<AuthResponseModel>(`${this.APIUrl}/Register`, model)
  }

  logout(): void {
    this.user$.next(undefined);
    localStorage.clear();
    this._cookieService.delete('Authorization', '/');
  }

  getUser(): UserModel | undefined {
    const userName = localStorage.getItem('userName');

    if (userName) {
      const user: UserModel = {
        userName: userName
      };

      return user;
    }

    return undefined;
  
  }

  setUser(user: UserModel): void {
    this.user$.next(user);

    localStorage.setItem('userName', user.userName);
  }

  user(): Observable<UserModel | undefined> {
    return this.user$.asObservable();
  }

  override getResourceUrl(): string {
      return 'Common/Security';
  }
}

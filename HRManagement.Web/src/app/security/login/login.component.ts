import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { SecurityService } from '../security.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { AuthResponseModel } from '../auth-response-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  hide: boolean = true;
  loginForm: FormGroup = new FormGroup({});

  constructor(
    private _fb: FormBuilder,
    private _securityService: SecurityService,
    private _snackbar: MatSnackBar,
    private _cookieService: CookieService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loginForm = this._fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      this._securityService.login(this.loginForm.value).subscribe({
        next: (response: AuthResponseModel) => {
          this._snackbar.open('Login successful', 'Okay');

          const expirationDate = new Date(response.expires);

          this._cookieService.set('Authorization', `Bearer ${response.token}`, expirationDate, '/', undefined, true, 'Strict');

          this._securityService.setUser({ userName: this.loginForm.value.userName });

          this._router.navigate(['/']);
        },
        error: (e) => {
          this._snackbar.open("Unexpected error ocurred", "Okay");
        }
      });
    } else {
      this._snackbar.open('The login form is invalid. Please try again.');
    }
  }
}

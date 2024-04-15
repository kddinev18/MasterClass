import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Router } from '@angular/router';
import { SecurityService } from '../security.service';
import { CookieService } from 'ngx-cookie-service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});

  constructor(
    private _fb: FormBuilder,
    private _securityService: SecurityService,
    private _router: Router,
    private _snackbar: MatSnackBar,
    private _cookieService: CookieService
  ) { }

  ngOnInit(): void {
    this.registerForm = this._fb.group({
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onRegister(): void {
    if (this.registerForm.valid) {
      this._securityService.register(this.registerForm.value).subscribe({
        next: (authResponse) => {
          const expirationDate = new Date(authResponse.expires);

          this._cookieService.set('Authorization', `Bearer ${authResponse.token}`, expirationDate, '/', undefined, true, 'Strict');

          this._securityService.setUser({ userName: this.registerForm.value.userName });

          this._router.navigate(['/']);
        },
        error: (e) => {
          this._snackbar.open("There was an error with your request", "Okay");
        }
      });
    }
  }
}

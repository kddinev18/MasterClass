import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CookieService } from 'ngx-cookie-service';
import { SecurityService } from '../security/security.service';
import { Router } from '@angular/router';

export const requestInterceptor: HttpInterceptorFn = (req, next) => {
  debugger
  const cookieService = inject(CookieService);
  const snackbar = inject(MatSnackBar);
  const securityService = inject(SecurityService);
  const router = inject(Router);

  const token = cookieService.get('Authorization');

  if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `${token}`
        }
      });
    } else {
      snackbar.open('Your session has expired. Please log in again.');
      securityService.logout();
      router.navigate(['/login']);
  }

  return next(req);
};

import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const requestInterceptor: HttpInterceptorFn = (req, next) => {
  const cookieService = inject(CookieService);

  const token = cookieService.get('Authorization');

  if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `${token}`
        }
      });
    } 
  // else {
  //     snackbar.open('Your session has expired. Please log in again.');
  //     securityService.logout();
  //     router.navigate(['/login']);
  // }

  return next(req);
};

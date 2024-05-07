import { inject } from '@angular/core';
import { CanActivateFn, Router, UrlSegment } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SecurityService } from '../../security/security.service';
import { UserModel } from '../../security/user-model';

export const authGuard: CanActivateFn = (route, state) => {

  const cookieService: CookieService = inject(CookieService);
  const snackbar: MatSnackBar = inject(MatSnackBar);
  const securityService: SecurityService = inject(SecurityService);
  const router: Router = inject(Router);

  const user: UserModel | undefined = securityService.getUser();

  if (cookieService.get('Authorization')) {
    let token = cookieService.get('Authorization');
    token = token.replace('Bearer ', '');
    const decodedToken = jwtDecode(token);

    if (decodedToken.exp && decodedToken.exp < Date.now() / 1000) {
      snackbar.open('Your session has expired. Please log in again.', undefined, { duration: 3000 });
      securityService.logout();
      router.navigate(['/login']);
      return false;
    }

    if ((route.url[0].path.includes('login') || route.url[0].path.includes('register')) && user) {
      router.navigate(['/']);
      return false;
    }

    return true;
  }

  securityService.logout();
  router.navigate(['/login']);
  return false;
};

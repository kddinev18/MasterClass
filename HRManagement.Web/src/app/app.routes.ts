import { Routes } from '@angular/router';
import { EmployeesComponent } from './employees/employees.component';

export const routes: Routes = [
    { path: '', redirectTo: 'employees', pathMatch: 'full' },
    { path: 'employees', component: EmployeesComponent },
    // { path: 'departments',  },
    // { path: 'positions',  },
    // { path: 'login',  },
    // { path: 'register',  },
    // { path: 'not-found',  },
    { path: '**', redirectTo: '/not-found' }
];

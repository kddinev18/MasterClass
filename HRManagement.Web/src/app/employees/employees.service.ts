import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { EmployeeModel } from './models/employee-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService extends CrudService<EmployeeModel> {

  constructor(injector: Injector) {
    super(injector)
  }

  getAllEmployees(): Observable<EmployeeModel[]> {
    return this.httpClient.get<EmployeeModel[]>(`${this.APIUrl}/GetAll`);
  }

  override getResourceUrl(): string {
      return 'HrManagement/Employee';
  }
}

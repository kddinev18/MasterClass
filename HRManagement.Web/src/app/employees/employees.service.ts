import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { EmployeeModel } from './models/employee-model';
import { Observable } from 'rxjs';
import { BaseFilterModel } from '../shared/models/base-filter-model';
import { EmployeeFilterModel } from '../shared/models/employee-filter-model';
import { EmployeeFormModel } from './models/employee-form-model';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService extends CrudService<EmployeeModel> {

  constructor(injector: Injector) {
    super(injector)
  }

  getAllEmployees(filters: BaseFilterModel<EmployeeFilterModel>): Observable<EmployeeModel[]> {
    return this.httpClient.post<EmployeeModel[]>(`${this.APIUrl}/GetAll`, filters);
  }

  addOrUpdate(employee: EmployeeFormModel): Observable<number> {
    return this.httpClient.post<number>(`${this.APIUrl}/AddOrUpdate`, employee);
  }

  override getResourceUrl(): string {
      return 'HrManagement/Employee';
  }
}

import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../../core/services/crud.service';
import { NomenclatureModel } from '../models/nomenclature-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NomenclatureService extends CrudService<NomenclatureModel> {

  constructor(injector: Injector) {
    super(injector);
  }

  getJobs(): Observable<NomenclatureModel[]> {
    return this.httpClient.get<NomenclatureModel[]>(`${this.APIUrl}/GetJobs`);
  }

  getDepartments(): Observable<NomenclatureModel[]> {
    return this.httpClient.get<NomenclatureModel[]>(`${this.APIUrl}/GetDepartments`);
  }

  getManagers(): Observable<NomenclatureModel[]> {
    return this.httpClient.get<NomenclatureModel[]>(`${this.APIUrl}/GetManagers`);
  }

  override getResourceUrl(): string {
      return 'HrManagement/Nomenclatures';
  }
}

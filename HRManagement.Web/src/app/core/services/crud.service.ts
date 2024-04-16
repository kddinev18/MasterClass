import { HttpErrorResponse } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { BaseRequestService } from './base-request.service';

@Injectable({
  providedIn: 'root'
})
export class CrudService<T> extends BaseRequestService<T> {

  constructor(injector: Injector) {
    super(injector);
  }

  public get(id: number | string): Observable<T> {
    return this.httpClient.get<T>(`${this.APIUrl}/${id}`)
          .pipe(
            catchError(e => this.handleError(e))
          );
  }

  public create(model: T): Observable<T> {
    return this.httpClient.post<T>(`${this.APIUrl}`, model)
        .pipe(
            catchError(e => {
                return this.handleError(e);
            })
        );
  }

  public update(id: number | string, model: T): Observable<T> {
    return this.httpClient.put<T>(`${this.APIUrl}/${id}`, model)
        .pipe(
            catchError(e => {
                return this.handleError(e);
            })
        );
  }

  public delete(id: number | string): Observable<T> {
    return this.httpClient.delete<T>(`${this.APIUrl}/${id}`)
            .pipe(
                catchError(e => {
                    return this.handleError(e);
                })
            );
  }

  override getResourceUrl(): string {
    throw new Error('Method not implemented.');
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => error);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseRequestService<T> {

  protected httpClient: HttpClient;

  constructor(protected injector: Injector) {
    this.httpClient = injector.get<HttpClient>(HttpClient);
  }

  protected get baseUrl(): string { return 'https://localhost:7184/' }

  protected get APIPrefix(): string { return 'api/' };

  protected get APIUrl(): string { return this.baseUrl + this.APIPrefix + this.getResourceUrl() };

  abstract getResourceUrl(): string;
}

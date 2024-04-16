export class BaseFilterModel<T> {
    page: number;
    pageSize: number;
    sortBy?: string;
    sortDirection?: string;
    filters?: T;
  
    constructor(page: number, pageSize: number, sortBy: string, sortDirection: string, filters: T) {
        this.page = page;
        this.pageSize = pageSize;
        this.sortBy = sortBy;
        this.sortDirection = sortDirection;
        this.filters = filters;
    }
}
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { EmployeeModel } from './models/employee-model';
import { EmployeesService } from './employees.service';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { BaseFilterModel } from '../shared/models/base-filter-model';
import { EmployeeFilterModel } from '../shared/models/employee-filter-model';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeDialogComponent } from './employee-dialog/employee-dialog.component';
import { Actions } from '../shared/enums/actions';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmployeePromoteDialogComponent } from './employee-promote-dialog/employee-promote-dialog.component';
import { EmployeePromoteModel } from './models/employee-promote-model';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FiltersComponent } from '../shared/filters/filters.component';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NomenclatureModel } from '../shared/models/nomenclature-model';
import { NomenclatureService } from '../shared/services/nomenclature.service';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    MatButtonModule,
    FiltersComponent,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatIconModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    DatePipe
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit, AfterViewInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  initialFilter: BaseFilterModel<EmployeeFilterModel> = {
    page: 1,
    pageSize: 5
  };

  filters: BaseFilterModel<EmployeeFilterModel>[] = [];
  jobs: NomenclatureModel[] = [];
  managers: NomenclatureModel[] = [];
  departments: NomenclatureModel[] = [];
  count: number = 0;
  filterValue: EmployeeFilterModel | undefined = undefined;

  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'email',
    'phoneNumber',
    'hireDate',
    'job',
    'manager',
    'department',
    'actions'
  ];
  dataSource!: MatTableDataSource<EmployeeModel>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _nomenclatureService: NomenclatureService,
    private _employeeService: EmployeesService
  ) {
    this.dataSource = new MatTableDataSource<EmployeeModel>();
  }

  ngOnInit(): void {
    forkJoin([
      this._nomenclatureService.getJobs(),
      this._nomenclatureService.getManagers(),
      this._nomenclatureService.getDepartments()
    ])
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(([jobs, managers, departments]) => {
      this.jobs = jobs;
      this.managers = managers;
      this.departments = departments;
    });

    this.fetchData();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  add(): void {
    const dialog = this._dialog.open(EmployeeDialogComponent, {
      width: '600px',
      data: {
        title: 'Add Employee',
        action: Actions.CREATE
      }
    });

    dialog.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (result) => {
        debugger
        if (result?.reload) {
          this.fetchData();

          if (result.managerId) {
            this._nomenclatureService.getManagers().pipe(takeUntil(this._unsubscribeAll)).subscribe(managers => {
              this.managers = managers;
            });
          }
        }
      }
    });
  }

  promote(id: number) {
    const dialog = this._dialog.open(EmployeePromoteDialogComponent, {
      width: '600px',
      data: {
        id: id
      }
    });

    dialog.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (promoteRes) => {
        const promoteModel = {
          employeeId: promoteRes.employeeId,
          newJobId: promoteRes.newJobId,
          newDepartmentId: promoteRes.newDepartmentId
        } as EmployeePromoteModel;

        this._employeeService.promote(promoteModel).pipe(takeUntil(this._unsubscribeAll)).subscribe({
          next: () => {
            this._snackbar.open('Employee has been promoted', undefined, {
              duration: 4000
            });

            this.fetchData();
          }
        });
      }
    });
  }

  show(id: number) {
    const dialog = this._dialog.open(EmployeeDialogComponent, {
      width: '600px',
      data: {
        title: 'View Employee Details',
        id: id,
        action: Actions.SHOW
      }
    });
  }

  edit(id: number) {
    const dialog = this._dialog.open(EmployeeDialogComponent, {
      width: '600px',
      data: {
        title: 'Edit Employee',
        id: id,
        action: Actions.EDIT
      }
    });

    dialog.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (result) => {
        if (result) {
          this.fetchData();
        }
      }
    });
  }

  delete(id: number) {
    const dialog = this._dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Delete Employee',
        message: 'Are you sure you want to delete this employee?'
      }
    });

    dialog.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (result) => {
        if (result) {
          this._employeeService.deleteById(id).pipe(takeUntil(this._unsubscribeAll)).subscribe({
            next: () => {
              this._snackbar.open('Employee has been deleted', undefined, {
                duration: 4000
              });

              this.fetchData();
            }
          });
        }
      }
    });
  }

  applyFilter(value: any) {
    if (value.hireDate) {
      const localDate = new Date(value.hireDate.getFullYear(), value.hireDate.getMonth(), value.hireDate.getDate(), value.hireDate.getHours() + 3);
      value.hireDate = localDate.toISOString();
    }

    this.filterValue = value;
    const filter: BaseFilterModel<EmployeeFilterModel> = {
      ...this.initialFilter,
      filters: this.filterValue
    };

    this.fetchData(filter);
  }

  resetFilter(event: any) {
    this.filterValue = undefined;
    
    if (this.paginator) {
      this.paginator.pageIndex = 0;
    }

    this.fetchData();
  }

  changePage(event: PageEvent) {
    const filter: BaseFilterModel<EmployeeFilterModel> = {
      page: event.pageIndex + 1,
      pageSize: event.pageSize,
      filters: this.filterValue
    };

    this.fetchData(filter);
  }
  
  private fetchData(filter = this.initialFilter): void {
    this._employeeService.getAllEmployees(filter).pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      this.dataSource = new MatTableDataSource(data.items);
      this.count = data.count;
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}

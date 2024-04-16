import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { EmployeeModel } from './models/employee-model';
import { EmployeesService } from './employees.service';
import { Subject, takeUntil } from 'rxjs';
import { BaseFilterModel } from '../shared/models/base-filter-model';
import { EmployeeFilterModel } from '../shared/models/employee-filter-model';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeDialogComponent } from './employee-dialog/employee-dialog.component';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    DatePipe
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phoneNumber', 'hireDate', 'job', 'manager', 'department'];
  dataSource!: MatTableDataSource<EmployeeModel>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _dialog: MatDialog,
    private _employeeService: EmployeesService
  ) { }

  ngOnInit(): void {
    const initialFilter = {
      page: 1,
      pageSize: 10
    } as BaseFilterModel<EmployeeFilterModel>;

    this._employeeService.getAllEmployees(initialFilter).pipe(takeUntil(this._unsubscribeAll)).subscribe(employees => {
      this.dataSource = new MatTableDataSource(employees);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  add(): void {
    const dialog = this._dialog.open(EmployeeDialogComponent, {
      width: '600px',
      data: {
        title: 'Add Employee'
      }
    });

    dialog.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (result) => {
        if (result) {
          this._employeeService.getAllEmployees({ page: 1, pageSize: 10 }).pipe(takeUntil(this._unsubscribeAll)).subscribe(employees => {
            this.dataSource = new MatTableDataSource(employees);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
          });
        }
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}

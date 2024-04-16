import { CommonModule } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { NomenclatureService } from '../../shared/services/nomenclature.service';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { NomenclatureModel } from '../../shared/models/nomenclature-model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmployeesService } from '../employees.service';
import { Actions } from '../../shared/enums/actions';

@Component({
  selector: 'app-employee-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDatepickerModule,
    RouterModule,
    MatFormFieldModule,
    MatSelectModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './employee-dialog.component.html',
  styleUrl: './employee-dialog.component.css'
})
export class EmployeeDialogComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  id: number | null = null;
  action: string | null = null;
  employeeForm: FormGroup = new FormGroup({});
  jobs: NomenclatureModel[] = [];
  departments: NomenclatureModel[] = [];
  managers: NomenclatureModel[] = [];

  constructor(
    private _nomenclatureService: NomenclatureService,
    private _snackbar: MatSnackBar,
    private _employeesService: EmployeesService,
    private _fb: FormBuilder,
    public dialogRef: MatDialogRef<EmployeeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {
    this.id = this.data?.id;
    this.action = this.data?.action;

    forkJoin([
      this._nomenclatureService.getJobs(),
      this._nomenclatureService.getDepartments(),
      this._nomenclatureService.getManagers()
    ]).pipe(takeUntil(this._unsubscribeAll)).subscribe(([jobs, departments, managers]) => {
      this.jobs = jobs;
      this.departments = departments;
      this.managers = managers;
    });

    this.employeeForm = this._fb.group({
      id: [null],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      hireDate: ['', [Validators.required]],
      jobId: ['', [Validators.required]],
      managerId: [null],
      departmentId: ['', [Validators.required]]
    });

    if (this.action === Actions.SHOW) {
      this.employeeForm.disable();
    }

    debugger
    if (this.id) {
      this._employeesService.getById(this.id).pipe(takeUntil(this._unsubscribeAll)).subscribe(employee => {
        debugger
        this.employeeForm.patchValue({
          id: employee.id,
          firstName: employee.firstName,
          lastName: employee.lastName,
          email: employee.email,
          phoneNumber: employee.phoneNumber,
          hireDate: employee.hireDate,
          jobId: employee.job.id,
          managerId: employee.manager?.id,
          departmentId: employee.department.id
        });
      }); 
    }
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      this._employeesService.addOrUpdate(this.employeeForm.value).pipe(takeUntil(this._unsubscribeAll)).subscribe({
        next: () => {

          if (this.action === Actions.CREATE) {
            this._snackbar.open('New employee has been saved', undefined, {
              duration: 4000
            });
          } else {
            this._snackbar.open('Employee has been updated', undefined, {
              duration: 4000
            });
          }

          this.dialogRef.close(true);
        }
      });
    }
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}

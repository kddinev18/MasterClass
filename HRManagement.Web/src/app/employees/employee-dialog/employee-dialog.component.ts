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

  employeeForm: FormGroup = new FormGroup({});
  jobs: NomenclatureModel[] = [];
  departments: NomenclatureModel[] = [];
  managers: NomenclatureModel[] = [];

  constructor(
    private _nomenclatureService: NomenclatureService,
    private _snackbar: MatSnackBar,
    private _employeesService: EmployeesService,
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<EmployeeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {
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
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      this._employeesService.addOrUpdate(this.employeeForm.value).pipe(takeUntil(this._unsubscribeAll)).subscribe({
        next: () => {
          this._snackbar.open('New employee has been saved', undefined, {
            duration: 2000
          });

          this._dialogRef.close(true);
        }
      });
    }
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}

import { CommonModule } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { RouterModule } from '@angular/router';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NomenclatureModel } from '../../shared/models/nomenclature-model';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { NomenclatureService } from '../../shared/services/nomenclature.service';

@Component({
  selector: 'app-employee-promote-dialog',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    MatSelectModule,
    MatIconModule,
    FormsModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './employee-promote-dialog.component.html',
  styleUrl: './employee-promote-dialog.component.css'
})
export class EmployeePromoteDialogComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  employeePromoteForm: FormGroup = new FormGroup({});
  
  id: number | null = null;
  jobs: NomenclatureModel[] = [];
  departments: NomenclatureModel[] = [];

  constructor(
    private _fb: FormBuilder,
    private _nomenclatureService: NomenclatureService,
    public dialogRef: MatDialogRef<EmployeePromoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {

    this.employeePromoteForm = this._fb.group({
      newJobId: [null, Validators.required],
      newDepartmentId: [null]
    });

    this.id = this.data.id;

    forkJoin([
      this._nomenclatureService.getJobs(),
      this._nomenclatureService.getDepartments()
    ])
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(([jobs, departments]) => {
      this.jobs = jobs;
      this.departments = departments;
    });
  }

  onSubmit(): void {
    if (this.employeePromoteForm.valid) {
      this.dialogRef.close({
        ...this.employeePromoteForm.value,
        employeeId: this.id
      });
    }
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}

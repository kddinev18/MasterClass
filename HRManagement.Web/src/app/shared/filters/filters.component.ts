import { CommonModule } from '@angular/common';
import { Component, ContentChild, EventEmitter, OnInit, Output, TemplateRef } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [
    CommonModule,
    MatExpansionModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css'
})
export class FiltersComponent implements OnInit {

  filtersFormGroup!: FormGroup;

  @Output() formSubmit: EventEmitter<any> = new EventEmitter<any>();

  @ContentChild('filterTemplate')
  filterTemplate!: TemplateRef<any>;

  constructor() { }

  ngOnInit(): void { 
    this.filtersFormGroup = new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      email: new FormControl(null),
      phoneNumber: new FormControl(null),
      hireDate: new FormControl(null),
      jobId: new FormControl(null),
      managerId: new FormControl(null),
      departmentId: new FormControl(null)
    });
  }

  reset(): void {
    this.filtersFormGroup.reset();
    this.formSubmit.emit(null);
  }

  submit(): void {
    this.formSubmit.emit(this.filtersFormGroup.value);
  }
}

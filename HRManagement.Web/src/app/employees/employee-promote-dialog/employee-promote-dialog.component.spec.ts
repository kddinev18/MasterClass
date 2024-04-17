import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePromoteDialogComponent } from './employee-promote-dialog.component';

describe('EmployeePromoteDialogComponent', () => {
  let component: EmployeePromoteDialogComponent;
  let fixture: ComponentFixture<EmployeePromoteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeePromoteDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployeePromoteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

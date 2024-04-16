export class EmployeeFormModel {
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    managerId: number;
    jobId: number;
    departmentId: number;
  
    constructor(id: number, firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, managerId: number, jobId: number, departmentId: number) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.managerId = managerId;
        this.jobId = jobId;
        this.departmentId = departmentId;
    }
}
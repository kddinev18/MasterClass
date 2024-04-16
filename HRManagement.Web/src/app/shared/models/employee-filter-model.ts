export class EmployeeFilterModel {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    jobId: number;
    managerId: number;
    departmentId: number;
  
    constructor(firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, jobId: number, managerId: number, departmentId: number) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.jobId = jobId;
        this.managerId = managerId;
        this.departmentId = departmentId;
    }
}
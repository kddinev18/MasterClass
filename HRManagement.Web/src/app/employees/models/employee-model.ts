export class EmployeeModel {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    jobTitle: string;
    manager: string;
    department: string;

    constructor(firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, jobTitle: string, manager: string, department: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.jobTitle = jobTitle;
        this.manager = manager;
        this.department = department;
    }
}
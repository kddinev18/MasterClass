export class EmployeeModel {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    managerName: string;
    jobTitle: string;
    departmentName: string;
    previousJobs: string[];

    constructor(id: number, firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, jobTitle: string, managerName: string, departmentName: string, previousJobs: string[]) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.jobTitle = jobTitle;
        this.managerName = managerName;
        this.departmentName = departmentName;
        this.previousJobs = previousJobs;
    }
}
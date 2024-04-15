import { IEmployee } from "../interfaces/iemployee";

export class EmployeeModel implements IEmployee {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    jobPosition: string;
    manager: string;
    department: string;

    constructor(firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, jobPosition: string, manager: string, department: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.jobPosition = jobPosition;
        this.manager = manager;
        this.department = department;
    }
}
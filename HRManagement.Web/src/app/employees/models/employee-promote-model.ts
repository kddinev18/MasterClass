export class EmployeePromoteModel {
    employeeId: number;
    newJobId: number;
    newDepartmentId: number;

    constructor(employeeId: number, newJobId: number, newDepartmentId: number) {
        this.employeeId = employeeId;
        this.newJobId = newJobId;
        this.newDepartmentId = newDepartmentId;
    }
}
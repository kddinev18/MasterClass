import { NomenclatureModel } from "../../shared/models/nomenclature-model";

export class EmployeeResponseModel {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    hireDate: Date;
    job: NomenclatureModel;
    manager: NomenclatureModel;
    department: NomenclatureModel;
    previousJobs: NomenclatureModel[];

    constructor(id: number, firstName: string, lastName: string, email: string, phoneNumber: string, hireDate: Date, job: NomenclatureModel, manager: NomenclatureModel, department: NomenclatureModel, previousJobs: NomenclatureModel[]) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.hireDate = hireDate;
        this.job = job;
        this.manager = manager;
        this.department = department;
        this.previousJobs = previousJobs;
    }
}
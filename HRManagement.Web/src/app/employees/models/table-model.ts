import { EmployeeModel } from "./employee-model";

export class TableModel {
    items: EmployeeModel[];
    count: number;

    constructor(items: EmployeeModel[], count: number) {
        this.items = items;
        this.count = count;
    }
}
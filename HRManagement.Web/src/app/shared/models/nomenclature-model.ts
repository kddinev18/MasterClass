export class NomenclatureModel {
    id: number;
    code: string;
    value: string;

    constructor(id: number, code: string, value: string) {
        this.id = id;
        this.code = code;
        this.value = value;
    }
}
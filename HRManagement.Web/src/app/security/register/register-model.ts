export class RegisterModel {
    userName: string;
    password: string;
    email: string;

    constructor(userName: string, password: string, email: string) {
        this.userName = userName;
        this.password = password;
        this.email = email;
    }
}
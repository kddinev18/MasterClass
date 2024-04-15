export class AuthResponseModel {
    token: string;
    expires: string;

    constructor(token: string, expires: string) {
        this.token = token;
        this.expires = expires;
    }
}
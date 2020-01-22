import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { UserInfo } from "../_models/userinfo.model";
import { Observable } from "rxjs";

@Injectable()
class UserService extends ApiService {
    relativeUrl: string = "/Users/";
    
    public getAllUsers(): Observable<UserInfo[]> {
        return this.get(this.relativeUrl);
    }

    public getUserById(id: number): Observable<UserInfo> {
        return this.get(`${this.relativeUrl}${id}`);
    }
}
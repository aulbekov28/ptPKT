import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { Todo } from "../_models/todo.model";

@Injectable()
export class TodoService extends ApiService {
    relativeUrl: string = "/Todo/";
    
    public getAll(): Observable<Todo[]> {
        return this.get(this.relativeUrl);
    }

    public getById(id: number): Observable<Todo> {
        return this.get(`${this.relativeUrl}${id}`);
    }

    public createTodo(todo: Todo): Observable<Todo> {
        return this.post(`${this.relativeUrl}`,todo);
    }

    public updateTodo(todo: Todo) {
        return this.put(this.relativeUrl,todo)
    }
}
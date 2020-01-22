import {  Base } from './base.model';

export class Todo extends Base {
    title: string;    
    deadline: Date;
    items: TodoItem[];

    constructor(values: Object = {}) {
        super();
        Object.assign(this, values);
    }
}    

export class TodoItem extends Base {
    description: string;
    completed: boolean;
    todoID: number;

    constructor(values: Object = {}) {
        super();
        Object.assign(this, values);
    }
}
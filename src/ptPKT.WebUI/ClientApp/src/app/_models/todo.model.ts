import { Base } from './base.model';

export class Todo extends Base {
    Title: string;    
    Description: string;
    IsDone: Boolean;

    constructor(values: Object = {}) {
        super();
        Object.assign(this, values);
    }
}    
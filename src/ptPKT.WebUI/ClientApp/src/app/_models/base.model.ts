export interface IBase {
    id?: number;
    isDeleted: boolean;
    createDate: Date;
    modifyDate: Date;
}

export class Base implements IBase {
    Id: number;    
    isDeleted: boolean;
    createDate: Date;
    modifyDate: Date;
}
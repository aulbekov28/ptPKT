export interface IBase {
    id?: number;
    isDeleted: boolean;
    createDate: Date;
    modifyDate: Date;
}

export class Base implements IBase {
    id: number;    
    isDeleted: boolean;
    createDate: Date;
    modifyDate: Date;
}
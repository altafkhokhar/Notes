
export class NoteDetail implements INoteDetail {
    Id: string;
    Title: string;
    Description: string;
    Content: string;
   
}

export interface INoteDetail {

    Id: string;
    Title: string;
    Description: string;
    Content: string;
    
}


export class User implements IUser {
    Id: string;
    UserName: string;
    Email: string;

}

export interface IUser {

    Id: string;
    UserName: string;
    Email: string;
   

}

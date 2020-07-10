
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



export class LoginUser implements ILoginUser {
   
    UserName: string;
    Password: string;

}

export interface ILoginUser {
  
    UserName: string;
    Password: string;
}


export class GridNoteDetail implements NoteDetail, INoteDetail {
    Id: string;
    Title: string;
    Description: string;
    Content: string;

    CanEdit: boolean;
    CanShare: boolean;
    CanRead: boolean;
    CanDelete: boolean;
}

export interface IGridNoteDetail {

    Id: string;
    Title: string;
    Description: string;
    Content: string;

    CanEdit: boolean;
    CanShare: boolean;
    CanRead: boolean;
    CanDelete: boolean;
}

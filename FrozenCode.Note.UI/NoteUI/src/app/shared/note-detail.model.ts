
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

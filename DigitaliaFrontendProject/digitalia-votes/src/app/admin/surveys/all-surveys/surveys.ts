export interface Survey {
    surveyID: number;
    userID: number;
    title: string;
    description: string;
    question: string;
    creationDate?: string | null; 
    isActive?: boolean | null;
  }
  export interface Vote {
    voteID: number;
    surveyID: number;
    optionID: number;
    userID: number;
    voteDate?: Date; 
  }
  
  export interface validateVote{
    userID:number;
    surveyID:number;
  }
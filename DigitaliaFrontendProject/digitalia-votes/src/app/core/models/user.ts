import { Role } from './role';

export class User {
  id: number;
  img: string;
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  role: Role;
  token?: string;
}

export interface UserLogin {
  userName: string;
  password: string;
  providerID: string;
}

export interface UserRegister {
  userID: number;
  userName: string;
  email: string;
  passwordHash: string;
  authProvider?: string | null; 
  providerID?: string | null;
  profilePictureUrl?: string | null;
  creationDate?: Date | null; 
  lastAccess?: string | null;
}

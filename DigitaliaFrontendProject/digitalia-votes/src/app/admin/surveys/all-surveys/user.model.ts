import { formatDate } from '@angular/common';
export class User{
    //bd access
    accessId: number;
    userId: number;
    username: string;
    password: string;
    //bd systemsusers
    condoId: number;
    condoName: string;//
    profileTypeId: number;//1admin, 2propietario, 3trabajador
    profileType: string;//
    firstName: string;
    lastName: string;
    secondLastName: string;
    email: string;
    contactNumber: string;
    address: string;
    dateOfBirth: string;
    genderId: number;
    gender: string;//
    profilePicture: string
    documentTypeId: number;
    documentNumber: string;
    createdBy: string;
    createdAt: string;
    updatedBy: string;
    updatedAt: string;
    isDeleted: boolean;

    constructor(user) {
        this.accessId = user.accessId || 0;
        this.userId = user.userId || 0;
        this.username = user.username || '';
        this.password = user.password || '';
        this.condoId = user.condoId || 0;
        this.condoName = user.condoName || '';
        this.profileTypeId = user.profileTypeId || 0;
        this.profileType = user.profileType || '';
        this.firstName = user.firstName || '';
        this.lastName = user.lastName || '';
        this.secondLastName = user.secondLastName || '';
        this.email = user.email || '';
        this.contactNumber = user.contactNumber || '';
        this.address = user.address || '';
        this.dateOfBirth = formatDate(new Date(), 'yyyy-MMM-dd', 'en') || '';
        this.genderId = user.genderId || 0;
        this.gender = user.gender || '';
        this.profilePicture = user.profilePicture || '';
        this.documentTypeId = user.documentTypeId || 0;
        this.documentNumber= user.documentNumber || '';
        this.createdBy = user.createdBy || '';
        this.createdAt = formatDate(new Date(), 'yyyy-MMM-dd', 'en') || '';
        this.updatedBy = user.updatedBy || '';
        this.updatedAt = formatDate(new Date(), 'yyyy-MMM-dd', 'en') || '';
        this.isDeleted = user.isDeleted || false;
    }

}

/*
En el front tienes que ser un objeto para los dos yo te 
recomendaría que agarres el objeto de de system user y 
le agregues usuario- y contraseña- nada más del otro user 
name y password entonces vas a mandar el ID del condominio- 
que va a ser un número vas a mandar el ID del profile- o sea
 el ID del rol van a ver tres tipos de roles el admin el 
 propietario y el trabajador de la empresa después ahí 
 tienes primer nombre segundo nombre después tienes el 
 email número de contacto el género la fecha de nacimiento
  la dirección el tipo de documento el número de documento 
  a esos datos le agregas el usuario y la contraseña y ese 
  objeto lo empieza a base de datos pero todo eso dibujado 
   en el formulario


*/
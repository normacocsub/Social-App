export class Usuario {
    correo: string;
    password: string;
    nombres: string;
    apellidos: string;
    sexo: string;
    imagePerfil: string;
    token: string;

    constructor(){
        this.correo = '';
        this.password = '';
        this.nombres = '';
        this.apellidos = '';
        this.sexo = '';
        this.imagePerfil = '';
        this.token = '';
    }
}

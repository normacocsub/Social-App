import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-registro-usuario',
  templateUrl: './registro-usuario.page.html',
  styleUrls: ['./registro-usuario.page.scss'],
})
export class RegistroUsuarioPage implements OnInit {
  usuario: Usuario;
  constructor(private usuarioService: LoginService) { }

  ngOnInit() {
    this.usuario = new Usuario();
  }

  registrar()
  {
    this.usuarioService.post(this.usuario).subscribe(result => {
      if(result != null){
        console.log(result);
      }
    })
  }

}

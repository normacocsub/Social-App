import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/services/login.service';

import { ModalController } from '@ionic/angular';
import { ModalRegistroInfoPage } from '../modal-registro-info/modal-registro-info.page';
import { Router } from '@angular/router';

import { SpinnerDialog } from '@ionic-native/spinner-dialog/ngx';

@Component({
  selector: 'app-registro-usuario',
  templateUrl: './registro-usuario.page.html',
  styleUrls: ['./registro-usuario.page.scss'],
})
export class RegistroUsuarioPage implements OnInit {
  usuario: Usuario;
  constructor(
    private usuarioService: LoginService,
    private modalController: ModalController,
    private router: Router,
    private spinner: SpinnerDialog
  ) {}

  ngOnInit() {
    this.usuario = new Usuario();
  }

  registrar() {
    this.spinner.show(null, "Cargando", true);
    this.usuarioService.post(this.usuario).subscribe(async (result) => {
      if (result != null) {
        this.spinner.hide();
        const modal = await this.modalController.create({
          component: ModalRegistroInfoPage,
          
        });
        await modal.present();
    
        await modal.onDidDismiss();

        this.router.navigate(['/']);

      }
    });
  }
}

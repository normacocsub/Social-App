import { Component } from '@angular/core';
import { ConfigurationsService } from './services/configurations.service';
import { FingerprintAIO } from '@ionic-native/fingerprint-aio/ngx';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  huellaStatus: boolean = false;
  huellaVerificar: any = false;
  configTheme: boolean = false;
  constructor(
    private configuration: ConfigurationsService,
    private faio: FingerprintAIO
  ) {

    this.configuration.verificarTema().subscribe((result => {
        this.onToggleColorTheme(result);
    }));

    this.configuration.verificarHuella().subscribe((result) => {
      if (result) {
        this.huellaVerifi();
      }
      this.huellaStatus = result;
    });
  }

  huellaVerifi() {
    this.faio
      .registerBiometricSecret({
        description: 'Some biometric description', // optional | Default: null
        secret: 'my-super-secret', // mandatory
        invalidateOnEnrollment: true, // optional | Default: false
        disableBackup: true, // (Android Only) | optional | always `true` on Android
      })
      .then((result: any) => {
        if (result == 'biometric_success') {
          this.huellaVerificar = true;
        }
      })
      .catch((error: any) => console.log(error));
  }

  onToggleColorTheme(option: boolean) {
    if (option) {
      document.body.setAttribute('color-theme', 'dark');
    } else {
      document.body.setAttribute('color-theme', 'ligth');
    }
  }

  toggle = document.querySelector('#themeToggle');
}

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalPrivacidadPage } from './modal-privacidad.page';

const routes: Routes = [
  {
    path: '',
    component: ModalPrivacidadPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalPrivacidadPageRoutingModule {}

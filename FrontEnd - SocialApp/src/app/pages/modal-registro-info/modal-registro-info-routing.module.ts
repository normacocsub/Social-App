import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalRegistroInfoPage } from './modal-registro-info.page';

const routes: Routes = [
  {
    path: '',
    component: ModalRegistroInfoPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalRegistroInfoPageRoutingModule {}

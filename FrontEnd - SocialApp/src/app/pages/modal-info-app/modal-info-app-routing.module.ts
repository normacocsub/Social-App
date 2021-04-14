import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalInfoAppPage } from './modal-info-app.page';

const routes: Routes = [
  {
    path: '',
    component: ModalInfoAppPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalInfoAppPageRoutingModule {}

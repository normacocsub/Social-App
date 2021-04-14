import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalHelpPage } from './modal-help.page';

const routes: Routes = [
  {
    path: '',
    component: ModalHelpPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalHelpPageRoutingModule {}

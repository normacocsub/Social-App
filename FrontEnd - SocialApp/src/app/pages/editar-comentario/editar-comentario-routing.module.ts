import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditarComentarioPage } from './editar-comentario.page';

const routes: Routes = [
  {
    path: '',
    component: EditarComentarioPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EditarComentarioPageRoutingModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PipeReaccionesPipe } from './pipe-reacciones.pipe';



@NgModule({
  declarations: [PipeReaccionesPipe],
  exports: [
    PipeReaccionesPipe,
  ],
  imports: [
    CommonModule,
  ]
})
export class PipeModuleModule { }

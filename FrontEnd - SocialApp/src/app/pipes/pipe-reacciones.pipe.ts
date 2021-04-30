import { Pipe, PipeTransform } from '@angular/core';
import { Reaccion } from '../models/reaccion';

@Pipe({
  name: 'pipeReacciones'
})
export class PipeReaccionesPipe implements PipeTransform {

  transform(reacciones: Reaccion[], categoriaSearch: string): any {
    if(categoriaSearch == "Todos") return reacciones;
    if(categoriaSearch == "Like") return reacciones.filter(r => r.like == true);
    if(categoriaSearch == "Me Encanta")return reacciones.filter(r => r.love == true);
  }

}

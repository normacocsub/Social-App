import { Component, Input, OnInit } from '@angular/core';
import { Publicacion } from 'src/app/interfaces/interfaces';

@Component({
  selector: 'app-ver-publicacion',
  templateUrl: './ver-publicacion.page.html',
  styleUrls: ['./ver-publicacion.page.scss'],
})
export class VerPublicacionPage implements OnInit {

  @Input() publicacion: Publicacion;
  constructor() { }

  ngOnInit() {
  }

}

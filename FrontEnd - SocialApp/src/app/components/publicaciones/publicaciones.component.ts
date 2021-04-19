import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-publicaciones',
  templateUrl: './publicaciones.component.html',
  styleUrls: ['./publicaciones.component.scss'],
})
export class PublicacionesComponent implements OnInit {
  @Input() publicaciones;
  constructor() { }

  ngOnInit() {
    
  }

}

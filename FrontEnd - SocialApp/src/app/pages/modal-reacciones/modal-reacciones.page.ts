import { PublicacionesService } from 'src/app/services/publicaciones.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { IonInput, IonSegment, ModalController } from '@ionic/angular';
import { Publicacion } from 'src/app/models/publicacion';
@Component({
  selector: 'app-modal-reacciones',
  templateUrl: './modal-reacciones.page.html',
  styleUrls: ['./modal-reacciones.page.scss'],
})
export class ModalReaccionesPage implements OnInit {

  @ViewChild(IonInput) input: IonInput;
  @Input() publicacion: Publicacion = new Publicacion();
  usuarioString: string = '';
  publicacionReaccion: any = [];
  personasReaccion: any = [];
  i: number;
  j: number;
  categoriaText = "Todos";
  categorias = ["Todos","Like","Me Encanta"];
  @ViewChild(IonSegment) segment: IonSegment;
  constructor(private publicacionService: PublicacionesService, public modalController: ModalController) { }

  async ngOnInit() {
    //this.consultarReaccion();
  }

  ngAfterViewInit(){
    this.segment.value = this.categorias[0]; 
  }


  closeModal(){
    this.modalController.dismiss();
  }

  cambioCategoria(event){
    this.categoriaText = event.detail.value;
    //this.cargarNoticias(event.detail.value);
  }

  /* consultarReaccion(){
    this.publicacionService.ConsultaPublicaciones().subscribe(result => {
      this.publicacionReaccion = result;
     
    });

    for(this.i; this.i<this.publicacionReaccion.length;this.i++){
        if(this.publicacionReaccion.reacciones[1] == false || this.publicacionReaccion.reacciones[2] == false){
          this.personasReaccion = this.publicacionReaccion;
        }
    }

    if(this.publicacionReaccion){

    } 
  }
  */
  
}

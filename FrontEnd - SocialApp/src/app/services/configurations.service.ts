import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationsService {
  authSubject  =   new  BehaviorSubject(false);
  themeSubject = new BehaviorSubject(false);
  constructor(private storage: Storage) { 
    this.storage.get('huella').then((val:boolean) => {
      if(val != null){
        this.authSubject.next(val);
      }
      else{
        this.authSubject.next(false);
      }
    });

    this.storage.get('theme').then((val:boolean) => {
      if(val != null){
        this.themeSubject.next(val);
      }
      else{
        this.themeSubject.next(false);
      }
    });
  }

  ActualizarHuella(estado: boolean){
    this.storage.set('huella', estado);
    this.authSubject.next(estado);
  }

  verificarHuella() {
    return this.authSubject.asObservable();
  }

  ActualizarTema(estado: boolean){
    this.storage.set('theme', estado);
    this.themeSubject.next(estado);
  }

  verificarTema(){
    return this.themeSubject.asObservable();
  }

}

import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  
  {
    path: 'home',
    loadChildren: () => import('./pages/home/home.module').then( m => m.HomePageModule)
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'modal-publicacion',
    loadChildren: () => import('./pages/modal-publicacion/modal-publicacion.module').then( m => m.ModalPublicacionPageModule)
  },
  {
    path: 'modal-editar-publicacion',
    loadChildren: () => import('./pages/modal-editar-publicacion/modal-editar-publicacion.module').then( m => m.ModalEditarPublicacionPageModule)
  },
  {
    path: 'ver-publicacion',
    loadChildren: () => import('./pages/ver-publicacion/ver-publicacion.module').then( m => m.VerPublicacionPageModule)
  },
  {
    path: 'consultar-publicacion',
    loadChildren: () => import('./pages/consultar-publicacion/consultar-publicacion.module').then( m => m.ConsultarPublicacionPageModule)
  },
  {
    path: 'perfil',
    loadChildren: () => import('./pages/perfil/perfil.module').then( m => m.PerfilPageModule)
  },
  {
    path: 'tabs',
    loadChildren: () => import('./tabs/tabs.module').then( m => m.TabsPageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then( m => m.LoginPageModule)
  },  {
    path: 'registro-usuario',
    loadChildren: () => import('./pages/registro-usuario/registro-usuario.module').then( m => m.RegistroUsuarioPageModule)
  },



];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

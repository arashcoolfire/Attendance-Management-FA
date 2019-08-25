import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import {AuthGuard} from './auth/auth.guard';

const routes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () => import('./userArea/tabs/tabs.module')
        .then(m => m.TabsPageModule),
    canLoad: [AuthGuard]
  },
  {
    path: '',
    loadChildren: () => import('./auth/auth.module')
        .then(m => m.AuthPageModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'adminDash',
    loadChildren: () => import('./adminArea/tabs/tabs.module')
        .then(m => m.TabsPageModule),
    canLoad: [AuthGuard]
  }
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}

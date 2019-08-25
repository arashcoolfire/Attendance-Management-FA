import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'users',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('../users/users.module').then(m => m.UsersPageModule)
          }
        ]
      },
      {
        path: 'attendances',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('../attendances/attendances.module').then(m => m.AttendancesPageModule)
          }
        ]
      },
      {
        path: 'reqHist',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('../reqHist/reqHist.module').then(m => m.ReqHistPageModule)
          }
        ]
      },
      {
        path: '',
        redirectTo: 'attendances',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/tabs/tab1',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TabsPageRoutingModule {}

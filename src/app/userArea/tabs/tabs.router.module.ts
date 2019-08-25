import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
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
        path: 'request',
        children: [
          {
            path: '',
            loadChildren: () =>
              import('../request/request.module').then(m => m.RequestPageModule)
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

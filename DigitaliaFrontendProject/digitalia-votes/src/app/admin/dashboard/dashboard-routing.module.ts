import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Dashboard2Component } from './dashboard2/dashboard2.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard2',
    pathMatch: 'full',
  },  
  {
    path: 'dashboard2',
    component: Dashboard2Component,
  }  
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}

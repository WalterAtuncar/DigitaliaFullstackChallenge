import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllTeachersComponent } from './all-surveys/all-teachers.component';
import { EditTeacherComponent } from './vote-surveys/edit-teacher.component';

const routes: Routes = [
  {
    path: 'all-users',
    component: AllTeachersComponent,
  },

  {
    path: 'edit-user',
    component: EditTeacherComponent,
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeachersRoutingModule {}

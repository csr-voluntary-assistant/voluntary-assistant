import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminNgosListComponent } from './admin-ngos-list/admin-ngos-list.component';
import { AdminNgosVerifyComponent } from './admin-ngos-verify/admin-ngos-verify.component';
import { RouterModule } from '@angular/router';
import { AngularMaterialModule } from '../../shared/angular-material.module';

@NgModule({
  declarations: [AdminNgosListComponent, AdminNgosVerifyComponent],
  imports: [
    CommonModule,
    AngularMaterialModule,
    RouterModule.forRoot([
      { path: 'admin-ngos-list', component: AdminNgosListComponent },
      { path: 'admin-ngos-verify/:id', component: AdminNgosVerifyComponent },
    ]),
  ]
})

export class AdminNgosModule{ }

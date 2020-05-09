import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminNgosListComponent } from './admin-ngos-list/admin-ngos-list.component';
import { AdminNgosVerifyComponent } from './admin-ngos-verify/admin-ngos-verify.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [AdminNgosListComponent, AdminNgosVerifyComponent],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'admin-ngos-list', component: AdminNgosListComponent },
      { path: 'admin-ngos-verify/:id', component: AdminNgosVerifyComponent },
    ]),
  ]
})

export class AdminNgosModule{ }

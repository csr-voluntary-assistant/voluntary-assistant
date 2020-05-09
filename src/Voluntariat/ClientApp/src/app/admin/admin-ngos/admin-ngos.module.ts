import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminNgosListComponent } from './admin-ngos-list/admin-ngos-list.component';
import { AdminNgosVerifyComponent } from './admin-ngos-verify/admin-ngos-verify.component';

@NgModule({
  declarations: [AdminNgosListComponent, AdminNgosVerifyComponent],
  imports: [
    CommonModule
  ]
})

export class AdminNgosModule{ }

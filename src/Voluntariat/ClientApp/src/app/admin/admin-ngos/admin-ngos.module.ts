import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../../shared/angular-material.module';

import { AdminNgosRoutingModule } from './admin-ngos-routing.module';
import { AdminNgosService } from './admin-ngos.service';

@NgModule({
    declarations: [AdminNgosRoutingModule.routedComponents],
    imports: [
        CommonModule,
        AngularMaterialModule,
        AdminNgosRoutingModule
    ],
    providers: [AdminNgosService]
})

export class AdminNgosModule { }

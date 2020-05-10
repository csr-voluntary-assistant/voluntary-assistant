import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../shared/angular-material.module';

import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
    declarations: [AdminRoutingModule.routedComponents],
    imports: [
        CommonModule,
        AngularMaterialModule,
        AdminRoutingModule
    ]
})

export class AdminModule { }

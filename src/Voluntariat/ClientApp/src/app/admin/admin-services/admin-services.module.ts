import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AngularMaterialModule } from '../../shared/angular-material.module';
import { AdminServicesRoutingModule } from './admin-services-routing.module';
import { AdminServicesService } from './admin-services.service';


@NgModule({
    declarations: [AdminServicesRoutingModule.routedComponents],
    imports: [
        CommonModule,
        FormsModule,
        AngularMaterialModule,
        AdminServicesRoutingModule,
    ],
    providers: [AdminServicesService]
})

export class AdminServicesModule { }

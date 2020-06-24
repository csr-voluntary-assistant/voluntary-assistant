import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../../shared/angular-material.module';
import { NGOTabRoutingModule } from './ngo-tab-routing.module';
import { NGOTabService } from './ngo-tab.service';

@NgModule({
    declarations: [NGOTabRoutingModule.routedComponents],
    imports: [
        CommonModule,
        AngularMaterialModule,
        NGOTabRoutingModule
    ],
    providers: [NGOTabService]
})

export class NGOTabModule { }

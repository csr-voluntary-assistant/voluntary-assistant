import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AngularMaterialModule } from '../../shared/angular-material.module';
import { AdminCategoriesRoutingModule } from './admin-categories-routing.module';
import { AdminCategoriesService } from './admin-categories.service';


@NgModule({
    declarations: [AdminCategoriesRoutingModule.routedComponents],
    imports: [
        CommonModule,
        FormsModule,
        AngularMaterialModule,
        AdminCategoriesRoutingModule,
    ],
    providers: [AdminCategoriesService]
})

export class AdminCategoriesModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AngularMaterialModule } from '../shared/angular-material.module';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminNavMenuComponent } from './admin-nav-menu/admin-nav-menu.component';
import { NavigationMenuModule } from '../shared/navigation-menu/navigation-menu.module';

@NgModule({
    declarations: [AdminRoutingModule.routedComponents, AdminNavMenuComponent],
    imports: [
        CommonModule,
        AngularMaterialModule,
        AdminRoutingModule,
        NavigationMenuModule
    ]
})

export class AdminModule { }

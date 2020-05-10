import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { NavigationMenuModule } from '../shared/navigation-menu/navigation-menu.module';
import { PublicNavMenuComponent } from './public-nav-menu/public-nav-menu.component';
import { MatTableModule } from '@angular/material/table';

@NgModule({
    declarations: [
        PublicRoutingModule.routedComponents,
        PublicNavMenuComponent
    ],
    imports: [
        CommonModule,
        PublicRoutingModule,
        NavigationMenuModule,
        MatTableModule
    ]
})
export class PublicModule { }

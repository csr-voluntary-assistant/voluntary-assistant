import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminServicesListComponent } from './admin-services-list/admin-services-list.component';
import { AdminServicesAddComponent } from './admin-services-add/admin-services-add.component';
import { AdminServicesEditComponent } from './admin-services-edit/admin-services-edit.component';
import { AdminServicesDeleteComponent } from './admin-services-delete/admin-services-delete.component';
import { AdminServicesComponent } from './admin-services.component';

const routes: Routes = [
    {
        path: '', component: AdminServicesComponent,
        children: [
            { path: '', redirectTo: 'list', pathMatch: 'full' },

            { path: 'list', component: AdminServicesListComponent },
            { path: 'add', component: AdminServicesAddComponent },
            { path: 'edit/:id', component: AdminServicesEditComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminServicesRoutingModule {
    static routedComponents = [AdminServicesComponent, AdminServicesListComponent, AdminServicesAddComponent, AdminServicesEditComponent, AdminServicesDeleteComponent];
}
